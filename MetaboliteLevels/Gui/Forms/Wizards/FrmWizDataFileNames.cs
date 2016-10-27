using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetaboliteLevels.Data.Database;
using MetaboliteLevels.Data.Session.Definition;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Main;
using MetaboliteLevels.Gui.Controls;
using MetaboliteLevels.Gui.Datatypes;
using MetaboliteLevels.Gui.Forms.Activities;
using MetaboliteLevels.Gui.Forms.Editing;
using MetaboliteLevels.Gui.Forms.Selection;
using MetaboliteLevels.Gui.Forms.Setup;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Controls;
using MGui.Datatypes;
using MGui.Helpers;

namespace MetaboliteLevels.Gui.Forms.Wizards
{
    /// <summary>
    /// The "please load your data" screen.
    /// </summary>
    public partial class FrmEditDataFileNames : Form
    {
        /// <summary>
        /// The result is stored here
        /// </summary>
        private Core _result;

        /// <summary>
        /// Feedback loop prevention 
        /// </summary>
        private bool _ignoreChanges;

        /// <summary>
        /// Wizard manager
        /// </summary>
        private readonly CtlWizard _wizard;

        /// <summary>
        /// Designates Why the context menu is being shown (load or delete)
        /// </summary>
        private bool _historyDelete;

        /// <summary>
        /// Retrieves the current filename of the experimental groups file, or null if there isn't one
        /// (Used to manage the experimental groups dialogue)
        /// </summary>
        private string CondInfoFileName { get { return this._chkCondInfo.Checked ? this._txtCondInfo.Text : null; } }

        /// <summary>
        /// The file the current list of experimental groups originates from
        /// (The list is kept until this mismatches with <see cref="CondInfoFileName"/>.)
        /// </summary>
        private string _experimentalGroupCacheSource;

        /// <summary>
        /// Experimental conditions editor (for interesting)
        /// </summary>
        private readonly ConditionBox<string> _cbExp;

        /// <summary>
        /// Experimental conditions editor (for control)
        /// </summary>
        private readonly ConditionBox<string> _cbControl;    

        /// <summary>
        /// List of ALL compound libraries
        /// </summary>
        private readonly List<CompoundLibrary> _compoundLibraries;

        /// <summary>
        /// List of ALL adduct libraries
        /// </summary>
        private readonly List<NamedItem<string>> _adductLibraries;                            

        /// <summary>
        /// Cached names of the experimental types (as a hashset)
        /// </summary>
        private readonly List<ExpCond> _experimentalGroupCache = new List<ExpCond>();

        private readonly ToolStripMenuItem _mnuBrowseWorkspace;
        private readonly ToolStripSeparator _mnuBrowseWorkspaceSep;
        private readonly FileLoadInfo _fileLoadInfo;
        private EditableComboBox<EAnnotation> _cbAutomaticFlag;
        private EditableComboBox<EAnnotation> _cbManualFlag;                

        /// <summary>
        /// Constructor.
        /// </summary>
        private FrmEditDataFileNames()
        {
            this.InitializeComponent();
            UiControls.SetIcon( this );

            this._btnNewSession.BackColor = this._btnNewSession.FlatAppearance.BorderColor
                = this._btnReturnToSession.BackColor = this._btnReturnToSession.FlatAppearance.BorderColor
                = this._btnMostRecent.BackColor = this._btnMostRecent.FlatAppearance.BorderColor
                = UiControls.TitleBackColour;

            this._btnNewSession.FlatAppearance.MouseOverBackColor
                = this._btnReturnToSession.FlatAppearance.MouseOverBackColor
                = this._btnMostRecent.FlatAppearance.MouseOverBackColor
                = ColourHelper.Blend( UiControls.TitleBackColour, Color.Black, 0.1 );

            this._fileLoadInfo = XmlSettings.LoadAndResave<FileLoadInfo>( FileId.FileLoadInfo, ProgressReporter.GetEmpty(), null );

            // Match program description to title bar
            this._lblProgramDescription.BackColor = UiControls.TitleBackColour;
            this._lblProgramDescription.ForeColor = UiControls.TitleForeColour;
            this._lblOrder.BackColor = UiControls.TitleBackColour;

            // Set texts
            this.Text = UiControls.Title + " - Load data";
            this._tabWelcome.Text = UiControls.Title;

            // Show a warning in 32-bit mode
            this._lbl32BitWarning.Visible = IntPtr.Size != 8;
                 
            if (Debugger.IsAttached || (System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttribute<DebuggableAttribute>()?.IsJITTrackingEnabled ?? false))
            {
                this._lbl32BitWarning.Text = "A debugger is attached OR this is a debug build.\r\nPerformance will be negatively impacted.";
                this._lbl32BitWarning.Visible = true;
            }

            // Setup the wizard
            this._wizard = CtlWizard.BindNew( this.tabControl1.Parent, this.tabControl1, CtlWizardOptions.ShowCancel | CtlWizardOptions.HandleBasicChanges );
            this._wizard.Pager.PageTitles[0] = UiControls.Title;
            this._wizard.HelpClicked += this._wizard_HelpClicked;
            this._wizard.CancelClicked += this._wizard_CancelClicked;
            this._wizard.OkClicked += this._wizard_OkClicked;
            this._wizard.PermitAdvance += this._wizard_PermitAdvance;
            this._wizard.Pager.PageChanged += this._wizard_PageChanged;
            this.UpdateHelpButton();

            // Setup the experimental group boxes
            this._cbControl = this.CreateExpConditionBox( this._txtControls, this._btnBrowseContCond );
            this._cbExp = this.CreateExpConditionBox( this._txtExps, this._btnBrowseExpCond );

            // Setup annotations
            // -- Passing a null core is okay here provided we don't try to show any columns
            this._cbAutomaticFlag = DataSet.ForDiscreteEnum<EAnnotation>( null, "Annotation", (EAnnotation) (- 1) ).CreateComboBox(this._automaticFlag, null, ENullItemName.NoNullItem);
            this._cbManualFlag = DataSet.ForDiscreteEnum<EAnnotation>( null, "Annotation", (EAnnotation) (- 1) ).CreateComboBox( this._manualFlag, null, ENullItemName.NoNullItem );               

            // Setup help
            this.splitContainer1.Panel2Collapsed = true;

            // Setup captions
            this.linkLabel1.Text = UiControls.Title + " " + UiControls.VersionString.ToString();
            this._lblProgramDescription.Text = UiControls.Description;

            // Populate enum boxes
            EnumComboBox.Populate< ELcmsMode>( this._lstLcmsMode );
            EnumComboBox.Populate< ETolerance>( this._lstTolerance );

            // Populate CompoundLibrary's
            FrmActDataLoad.GetCompoundLibraries( out this._compoundLibraries, out this._adductLibraries );

            if (this._compoundLibraries.Count == 0)
            {
                this.ReplaceWithMessage( this._lstAvailCompounds, this._btnAddCompound );
            }

            if (this._adductLibraries.Count == 0)
            {
                this.ReplaceWithMessage( this._lstAvailableAdducts, this._btnAddAdduct );
            }

            this.UpdateAvailableCompoundsList();
            this.UpdateAvailableAdductsList();

            // Add recent entries menu
            var recentWorkspaces = MainSettings.Instance.RecentWorkspaces;
            this.LoadWorkspace( new DataFileNames() );

            // Workspaces
            ToolStripMenuItem tsmi;
            int index = 0;

            foreach (DataFileNames recentWorkspace in recentWorkspaces.Reverse<DataFileNames>())
            {
                index++;

                tsmi = new ToolStripMenuItem
                {
                    Text = "&" + index + ". " + recentWorkspace.GetDescription(),
                    Tag = recentWorkspace
                };

                tsmi.Click += this._recentWorkspace_Click;

                this._cmsRecentWorkspaces.Items.Add( tsmi );
                var x = UiControls.AddMenuCaption( this._cmsRecentWorkspaces, "Details..." );
                x.Tag = recentWorkspace;
                x.Click += this._recentWorkspaceDetails_Clicked;
            }

            this._mnuBrowseWorkspaceSep      = new ToolStripSeparator();
            this._cmsRecentWorkspaces.Items.Add( this._mnuBrowseWorkspaceSep );
            this._mnuBrowseWorkspace         = new ToolStripMenuItem( "Browse..." );
            this._mnuBrowseWorkspace.Click   += this._mnuBrowseWorkspace_Click;
            this._mnuBrowseWorkspace.Font    = new Font( this._mnuBrowseWorkspace.Font, FontStyle.Bold );
            this._cmsRecentWorkspaces.Items.Add( this._mnuBrowseWorkspace );

            this._btnDeleteWorkspace.Visible = recentWorkspaces.Count != 0;

            // Sessions
            tsmi = new ToolStripMenuItem
            {
                Text  = "Browse...",
                Font  = FontHelper.BoldFont,
                Tag   = null,
                Image = Resources.MnuOpen
            };

            tsmi.Click += this.mnuRecentSession_Click;
            this._cmsRecentSessions.Items.Add( tsmi );

            this._cmsRecentSessions.Items.Add( new ToolStripSeparator() );

            var recentSessions = MainSettings.Instance.RecentSessions;
            HashSet<string> itemsExist = new HashSet<string>();
            index = 0;

            foreach (MainSettings.RecentSession entry in recentSessions)
            {
                if (itemsExist.Contains( entry.FileName ))
                {
                    continue;
                }

                itemsExist.Add( entry.FileName );

                index++;

                tsmi = new ToolStripMenuItem();

                tsmi.Text        = "&" + index + ". " + (string.IsNullOrWhiteSpace( entry.Title ) ? "Untitled" : entry.Title);
                tsmi.ToolTipText = entry.FileName;
                tsmi.Tag         = entry;
                tsmi.Click      += this.mnuRecentSession_Click;

                if (!File.Exists( entry.FileName ))
                {
                    tsmi.ForeColor = Color.FromKnownColor( KnownColor.GrayText );
                }

                this._cmsRecentSessions.Items.Add( tsmi );
                UiControls.AddMenuCaptionFilename( this._cmsRecentSessions, entry.FileName );
            }

            if (recentSessions.Count == 0)
            {
                this._btnReturnToSession.Text += "...";
                this._btnMostRecent.Visible    = false;
            }
            else
            {
                var mostRecent = recentSessions[0];

                this._btnMostRecent.Text = "    " + mostRecent.Title;
                this._tipSideBar.SetToolTip( this._btnMostRecent, mostRecent.FileName );
                this._btnMostRecent.Tag = mostRecent.FileName;

                if (!File.Exists( mostRecent.FileName ))
                {
                    this._btnMostRecent.Visible = false;
                }
            }

            // UiControls.CompensateForVisualStyles( this );

            if (!Application.RenderWithVisualStyles)
            {
                foreach (var l in FormHelper.EnumerateControls<Label>( this ))
                {
                    if (l.ForeColor == Color.CornflowerBlue)
                    {
                        l.BackColor   = Color.FromArgb( 255, 255, 192 );
                        l.ForeColor   = Color.DarkBlue;
                        l.Padding     = new Padding( 8, 8, 8, 8 );
                        l.BorderStyle = BorderStyle.Fixed3D;
                    }
                }
            }

            foreach (Control control in FormHelper.EnumerateControls<Control>( this ))
            {
                if (!string.IsNullOrWhiteSpace( this._tipSideBar.GetToolTip( control ) ))
                {
                    if (control is CtlLabel)
                    {
                        control.MouseEnter += this.Control_MouseEnter;
                        control.MouseLeave += this.Control_MouseLeave;
                        control.Cursor = Cursors.Help;
                    }

                    control.GotFocus += this.Control_Click;
                    control.MouseDown += this.Control_Click;
                }
            }
        }

        private void Control_Click( object sender, EventArgs e )
        {
            if (sender is Label)
            {
                if (this.splitContainer1.Panel2Collapsed)
                {
                    this.ToggleHelp();
                }
            }

            this.ShowControlHelp( (Control)sender );
        }

        private void Control_MouseLeave( object sender, EventArgs e )
        {
            CtlLabel label = (CtlLabel)sender;

            label.LabelStyle = ELabelStyle.Normal;
            label.Font = FontHelper.RegularFont;
        }

        private void Control_MouseEnter( object sender, EventArgs e )
        {
            CtlLabel label = (CtlLabel)sender;

            label.LabelStyle = ELabelStyle.Highlight;
            label.Font = FontHelper.UnderlinedFont;
        }       

        private void _recentWorkspaceDetails_Clicked( object sender, EventArgs e )
        {
            DataFileNames recentWorkspace = (DataFileNames)((ToolStripLabel)sender).Tag;
            UiControls.ShowSessionInfo( this, recentWorkspace );
        }

        protected override void OnSizeChanged( EventArgs e )
        {
            base.OnSizeChanged( e );

            int x = Math.Max( 0, (this.ClientSize.Width - 1024) ) / 3;
            int y = Math.Max( 0, (this.ClientSize.Height - 768) ) / 3;

            this.splitContainer1.Margin = new Padding( x, y, x, y );
        }

        private void ReplaceWithMessage( ListBox list, Button btn )
        {
            var tlp = (TableLayoutPanel)list.Parent;
            var pos = tlp.GetCellPosition( list );

            Label lab     = new Label();
            lab.Text      = "There are no available libraires.\r\nPlease choose the library manually or reconfigure your library path.";
            lab.Dock      = DockStyle.Fill;
            lab.TextAlign = ContentAlignment.MiddleCenter;
            lab.Visible   = true;
            list.Visible  = false;
            btn.Enabled   = false;

            tlp.Controls.Add( lab, pos.Column, pos.Row );
        }

        void _mnuBrowseWorkspace_Click( object sender, EventArgs e )
        {
            string fn = UiControls.BrowseForFile( this, null, UiControls.EFileExtension.Sessions, FileDialogMode.Open, UiControls.EInitialFolder.Sessions );

            if (fn != null)
            {
                Core core = FrmWait.Show<Core>( this, "Retreiving settings", "Retrieving settings from the selected file", z=> XmlSettings.LoadOrDefault<Core>( fn, null, null, z) );

                if (core == null || core.FileNames == null)
                {
                    FrmMsgBox.ShowError( this, "Failed to retrieve the filenames. Try opening the file to check for errors first." );
                    return;
                }

                this.LoadWorkspace( core.FileNames );
            }
        }

        void _wizard_PageChanged( object sender, EventArgs args )
        {
            if (this._wizard.Page != 0)
            {
                this._wizard.Options |= (CtlWizardOptions.ShowBack | CtlWizardOptions.ShowNext);
            }
            else
            {
                this._wizard.Options &= ~(CtlWizardOptions.ShowBack | CtlWizardOptions.ShowNext);
            }

            this.UpdateHelpButton();

            switch (this._wizard.Page)
            {          
                case 4: // Conditions
                    this.UpdateCacheOfTypes();
                    break;

                case 5: // Statistics
                    this._chkAutoTTest.Enabled = this._chkConditions.Checked && this._cbExp.SelectedItems.Any() && this._cbControl.SelectedItems.Any();

                    if (!this._chkAutoTTest.Enabled)
                    {
                        this._lblTTUnavail.Text = "Not available - requires experimental and control conditions to be specified";
                    }
                    else
                    {                                               
                        string controlText = string.Join( ", ", this._cbControl.SelectedItems );
                        this._lblTTUnavail.Text = "Calculates for each peak:\r\n* t-tests against the " + this._cbControl.TextBox.Text + " group for each of: " + this._cbExp.TextBox.Text + "\r\n* Minimum of all p-values";
                    }

                    this._chkAutoPearson.Enabled = this._chkConditions.Checked && this._cbExp.SelectedItems.Any();

                    if (!this._chkAutoPearson.Enabled)
                    {
                        this._lblPearsonUnavail.Text = "Not available - requires experimental conditions to be specified";
                    }
                    else
                    {
                        this._lblPearsonUnavail.Text = "Calculates for each peak:\r\n* Correlations against time for each of: " + this._cbExp.TextBox.Text + "\r\n* Absolute maximum value of all correlations" ;
                    }
                    
                    break;

                case 7: // Annotations
                    ELcmsMode e = EnumComboBox.Get( this._lstLcmsMode, ELcmsMode.None );
                    string msg;
                    string msg2;

                    if (e == ELcmsMode.None)
                    {
                        msg = "Requires an LC-MS mode to be specified";
                        msg2 = msg;
                    }
                    else if (this._lstCompounds.Items.Count == 0)
                    {
                        msg = "Requires at least one compound database to be specified";
                        msg2 = null;
                    }
                    else if (this._lstAdducts.Items.Count == 0)
                    {
                        msg = "Requires at least one adduct database to be specified";
                        msg2 = null;
                    }
                    else
                    {
                        msg = null;
                        msg2 = null;
                    }

                    bool en = msg == null;

                    this._chkAutoIdentify.Enabled = en;
                    this._lblMzMatchUnavail.Visible = !en;
                    this.ctlLabel1.Visible = en;
                    this._cbAutomaticFlag.ComboBox.Visible = en;

                    if (!en)
                    {                                    
                        this._chkAutoIdentify.Checked = false;
                        this._lblMzMatchUnavail.Text = msg;
                    }

                    bool en2 = msg2 == null;
                                                                        
                    this._chkPeakPeakMatch.Enabled = en2;
                    this._lblPeakPeakMatchUnavail.Visible = !en2;


                    if (!en2)
                    {
                        this._chkPeakPeakMatch.Checked = false;
                        this._lblPeakPeakMatchUnavail.Text = msg2;
                    }

                    break;
            }
        }

        private void UpdateHelpButton()
        {              
            this._wizard.TitleHelpIcon = this.splitContainer1.Panel2Collapsed ? CtlTitleBar.EHelpIcon.ShowBar : CtlTitleBar.EHelpIcon.Off;
        }

        void _wizard_CancelClicked( object sender, CancelEventArgs e )
        {
            if (this._wizard.Page == 0)
            {
                this._wizard.Page = 0;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        /// Handles wizard - can advance page?
        /// </summary>
        /// <param name="input">Page number</param>
        /// <returns></returns>
        bool _wizard_PermitAdvance( int input )
        {
            this._checker.Clear();

            switch (input)
            {
                case 0: // Template
                case 1: // Welcome
                    break;

                case 2: // Session name                                           
                    this._checker.Check( this._txtTitle, this._txtTitle.Text.Length != 0, "A session title must be provided." );
                    break;

                case 3: // Select data
                    this._checker.Check( this._lstLcmsMode, this._lstLcmsMode.SelectedIndex != -1, "A LC-MS mode must be provided (use \"other\" for non-LC-MS data)." );
                    this._checker.Check( this._txtDataSetData, File.Exists( this._txtDataSetData.Text ), "Filename not provided or file not found." );
                    this._checker.Check( this._txtDataSetObs, File.Exists( this._txtDataSetObs.Text ), "Filename not provided or file not found." );
                    this._checker.Check( this._txtDataSetVar, File.Exists( this._txtDataSetVar.Text ), "Filename not provided or file not found." );
                    this._checker.Check( this._txtAltVals, !this._chkAltVals.Checked || File.Exists( this._txtAltVals.Text ), "Filename not provided or file not found." );
                    this._checker.Check( this._chkCondInfo, !this._chkCondInfo.Checked || File.Exists( this._txtCondInfo.Text ), "Filename not provided or file not found." );
                    break;

                case 4: // Conditions
                    this._checker.Check( this._chkConditions, !this._chkConditions.Checked || this._cbExp.SelectedItems.Any() || this._cbControl.SelectedItems.Any(), "Checking this box suggests at least one condition will be specified." );
                    break;

                case 5: // Statistics
                    break;

                case 6: // Compounds
                    break;

                case 7: // Annotations
                    bool doesntNeedTol = !this._chkAutoIdentify.Checked && !this._chkPeakPeakMatch.Checked;
                    this._checker.Check( this._numTolerance, doesntNeedTol || this._numTolerance.Value != 0, "A tolerance of zero is probably a mistake." );
                    this._checker.Check( this._lstTolerance, doesntNeedTol || this._lstTolerance.SelectedIndex != -1, "A unit must be specified." );
                    this._checker.Check( this._txtIdentifications, !this._chkIdentifications.Checked || File.Exists( this._txtIdentifications.Text ), "Filename not provided or file not found." );
                    break;

                case 8: // Ready
                    break;

                default: // ???
                    return false;
            }

            return this._checker.NoErrors;
        }   

        /// <summary>
        /// Loads a session configuration.
        /// </summary>                    
        private void LoadWorkspace( DataFileNames lfn )
        {
            this._txtTitle.Text = lfn.Title;

            this._txtDataSetData.Text = lfn.Data;
            this._txtDataSetObs.Text  = lfn.ObservationInfo;
            this._txtDataSetVar.Text  = lfn.PeakInfo;
            this._lstCompounds.Items.Clear();

            if (lfn.CompoundLibraies != null)
            {
                foreach (CompoundLibrary cl in lfn.CompoundLibraies)
                {
                    if (this._compoundLibraries.Any( z => z.ContentsMatch( cl ) ))
                    {
                        this._lstCompounds.Items.Add( this._compoundLibraries.Find( z => z.ContentsMatch( cl ) ) );
                    }
                    else
                    {
                        this._lstCompounds.Items.Add( cl );
                    }
                }
            }

            this._lstAdducts.Items.Clear();

            if (lfn.AdductLibraries != null)
            {
                foreach (string item in lfn.AdductLibraries)
                {
                    if (this._adductLibraries.Any( z => z.Value == item ))
                    {
                        this._lstAdducts.Items.Add( this._adductLibraries.Find( z => z.Value == item ) );
                    }
                    else
                    {
                        this._lstAdducts.Items.Add( new NamedItem<string>( item, item ) );
                    }
                }
            }

            this.SetText( this._txtIdentifications, this._chkIdentifications, lfn.Identifications );
            this.SetText( this._txtAltVals, this._chkAltVals, lfn.AltData );
            this.SetText( this._txtCondInfo, this._chkCondInfo, lfn.ConditionInfo );
            this.SetCheck( this._chkAutoTTest, lfn._standardAutoCreateOptions, EAutoCreateOptions.TTest );
            this.SetCheck( this._chkAutoPearson, lfn._standardAutoCreateOptions, EAutoCreateOptions.Pearson );
            this.SetCheck( this._chkAutoMeanTrend, lfn._standardAutoCreateOptions, EAutoCreateOptions.MeanTrend );
            this.SetCheck( this._chkAutoMedianTrend, lfn._standardAutoCreateOptions, EAutoCreateOptions.MedianTrend );
            this.SetCheck( this._chkAutoUvSC, lfn._standardAutoCreateOptions, EAutoCreateOptions.UvScaleAndCentre );

            this._chkAutoIdentify.Checked = lfn.AutomaticIdentifications;
            this._chkPeakPeakMatch.Checked = lfn.PeakPeakMatching;
            this._cbAutomaticFlag.SelectedItem = lfn.AutomaticIdentificationsStatus;
            this._cbManualFlag.SelectedItem = lfn.ManualIdentificationsStatus;

            if (lfn.AutomaticIdentifications || lfn.PeakPeakMatching)
            {
                this._numTolerance.Value = lfn.AutomaticIdentificationsToleranceValue;
                EnumComboBox.Set( this._lstTolerance, lfn.AutomaticIdentificationsToleranceUnit, false );
            }
            else
            {
                this._numTolerance.Value = 0.0m;
                EnumComboBox.Set<ETolerance>( this._lstTolerance, ETolerance.PartsPerMillion);
            }


            if (!string.IsNullOrWhiteSpace( lfn.Data ))
            {
                EnumComboBox.Set( this._lstLcmsMode, lfn.LcmsMode, true );
                
                this._cbExp.SelectedItems = lfn.ConditionsOfInterestString.Where( z => z != null ); // deal with legacy invalid data
                this._cbControl.SelectedItems = lfn.ControlConditionsString.Where( z => z != null );       
                this._chkConditions.Checked = this._cbExp.SelectedItems.Any() || this._cbControl.SelectedItems.Any();
            }
            else
            {
                EnumComboBox.Clear( this._lstLcmsMode );
                this._txtExps.Text = "";
                this._txtControls.Text = "";
                this._chkConditions.Checked = false;
            }

            this.UpdateAvailableCompoundsList();
            this.UpdateAvailableAdductsList();

            this._radEmptyWorkspace.Checked = lfn.Title == null;
            this._radRecentWorkspace.Checked = lfn.Title != null;
            this._txtPreviousConfig.Text = lfn.Title;
        }

        private void SetCheck( CheckBox cb, EAutoCreateOptions current, EAutoCreateOptions toCheck )
        {
            cb.Checked = current.HasFlag( toCheck );
        }

        void _recentWorkspace_Click( object sender, EventArgs e )
        {
            ToolStripMenuItem s = (ToolStripMenuItem)sender;
            DataFileNames fn = (DataFileNames)s.Tag;

            if (this._historyDelete)
            {
                if (FrmMsgBox.ShowYesNo( this, "Delete from history", "Are you sure you wish to remove the following settings from the history:\r\n\r\n    " + fn.Title, null ))
                {
                    MainSettings.Instance.RecentWorkspaces.Remove( fn );
                    MainSettings.Instance.Save();
                    s.Enabled = false;
                    s.Font = FontHelper.StrikeFont;
                }
            }
            else
            {
                this.LoadWorkspace( fn );
            }
        }

        void mnuRecentSession_Click( object sender, EventArgs e )
        {
            MainSettings.RecentSession rs = ((MainSettings.RecentSession)((ToolStripMenuItem)sender).Tag);

            if (rs != null && !File.Exists( rs.FileName ))
            {
                FrmMsgBox.ShowError( this, $"The session file cannot be found at the specified location: Filename = \"{rs.FileName}\", Title = \"{rs.Title}\", GUID = \"{rs.Guid}\"." );
                return;
            }

            this.LoadSession( rs?.FileName );
        }

        private void SetText( TextBox txt, CheckBox chk, string current )
        {
            this._ignoreChanges = true;

            if (!string.IsNullOrEmpty( current ))
            {
                txt.Text = current;
                chk.Checked = true;
            }
            else
            {
                txt.Text = "";
                chk.Checked = false;
            }

            this._ignoreChanges = false;
        }

        internal static Core Show( Form owner )
        {
            using (FrmEditDataFileNames frm = new FrmEditDataFileNames())
            {
                if (UiControls.ShowWithDim( owner, frm ) == DialogResult.OK)
                {
                    return frm._result;
                }
            }

            return null;
        }

        internal static bool Browse( TextBox textBox, string dFilter =null )
        {
            string filter = dFilter ?? "Data files (*.csv, *.dat, *.jgf)|*.csv;*.dat;*.jgf|Comma separated value (*.csv)|*.csv|JGF files (*.jgf)|*.jgf|Space delimited (*.dat)|*.dat|All files (*.*)|*.*";

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.FileName = textBox.Text;

                if (!string.IsNullOrEmpty( textBox.Text ) && Directory.Exists( Path.GetDirectoryName( textBox.Text ) ))
                {
                    ofd.InitialDirectory = Path.GetDirectoryName( textBox.Text );
                }

                ofd.Filter = filter;

                if (UiControls.ShowWithDim( textBox.FindForm(), ofd ) == DialogResult.OK)
                {
                    if (dFilter == null)
                    {
                        string fileName = ofd.FileName;
                        string ext = Path.GetExtension( fileName ).ToUpper();

                        if (ext != ".CSV")
                        {
                            if (FrmMsgBox.ShowYesNo( textBox.FindForm(), ext, ext + " files are not natively supported. Please use Excel or similar to convert to CSV and make sure your file has both column and row headers (the cell at (0, 0) should be empty). Would you like to open \"" + Path.GetFileName( fileName ) + "\" now?" ))
                            {
                                UiControls.StartProcess( textBox.FindForm(), fileName );
                            }

                            return false;
                        }
                    }

                    textBox.Text = ofd.FileName;

                    return true;
                }

                return false;
            }
        }

        private void _wizard_OkClicked( object sender, EventArgs e )
        {
            var fileNames = new DataFileNames();

            try
            {
                fileNames.Title                      = this._txtTitle.Text;
                fileNames.LcmsMode                   = EnumComboBox.Get<ELcmsMode>( this._lstLcmsMode );
                fileNames.Data                       = this._txtDataSetData.Text;
                fileNames.ObservationInfo            = this._txtDataSetObs.Text;
                fileNames.PeakInfo                   = this._txtDataSetVar.Text;
                fileNames.CompoundLibraies           = this._lstCompounds.Items.Cast<CompoundLibrary>().ToList();
                fileNames.Identifications            = this._chkIdentifications.Checked ? this._txtIdentifications.Text : null;
                fileNames.AdductLibraries            = this._lstAdducts.Items.Cast<NamedItem<string>>().Select( z => z.Value ).ToList();
                fileNames.AutomaticIdentifications   = this._chkAutoIdentify.Checked;
                fileNames.AutomaticIdentificationsStatus = this._cbAutomaticFlag.SelectedItem;
                fileNames.ManualIdentificationsStatus = this._cbManualFlag.SelectedItem;
                fileNames.PeakPeakMatching           = this._chkPeakPeakMatch.Checked;
                fileNames.Session                    = null;
                fileNames.AltData                    = this._chkAltVals.Checked ? this._txtAltVals.Text : null;
                fileNames.ConditionInfo              = this.CondInfoFileName;
                fileNames.ConditionsOfInterestString = this._chkConditions.Checked ? this._cbExp.GetSelectedItemsOrThrow().ToList() : new List<string>();
                fileNames.ControlConditionsString = this._chkConditions.Checked ? this._cbControl.GetSelectedItemsOrThrow().ToList() : new List<string>();
                fileNames._standardAutoCreateOptions = EAutoCreateOptions.None;
                fileNames._standardAutoCreateOptions = this.GetCheck( this._chkAutoTTest, fileNames._standardAutoCreateOptions, EAutoCreateOptions.TTest );
                fileNames._standardAutoCreateOptions = this.GetCheck( this._chkAutoPearson, fileNames._standardAutoCreateOptions, EAutoCreateOptions.Pearson );
                fileNames._standardAutoCreateOptions = this.GetCheck( this._chkAutoMedianTrend, fileNames._standardAutoCreateOptions, EAutoCreateOptions.MedianTrend );
                fileNames._standardAutoCreateOptions = this.GetCheck( this._chkAutoMeanTrend, fileNames._standardAutoCreateOptions, EAutoCreateOptions.MeanTrend );
                fileNames._standardAutoCreateOptions = this.GetCheck( this._chkAutoUvSC, fileNames._standardAutoCreateOptions, EAutoCreateOptions.UvScaleAndCentre );
                fileNames.AutomaticIdentificationsToleranceUnit  = EnumComboBox.Get<ETolerance>( this._lstTolerance, ETolerance.PartsPerMillion );
                fileNames.AutomaticIdentificationsToleranceValue = this._numTolerance.Value;
            }
            catch (Exception ex)
            {
                FrmMsgBox.ShowError( this, "Input error: " + ex.Message );
                return;
            }

            // Save the workspace (even if there is an error)
            MainSettings.Instance.AddRecentWorkspace( fileNames );
            MainSettings.Instance.Save();

            // Load the data
            this._result = FrmActDataLoad.Show( this, fileNames, this._fileLoadInfo );

            if (this._result == null)
            {
                return;
            }

            if (this._chkAlarm.Checked)
            {
                FrmActAlarm.Show( this );
            }

            this.DialogResult = DialogResult.OK;
        }

        private EAutoCreateOptions GetCheck( CheckBox cb, EAutoCreateOptions current, EAutoCreateOptions added )
        {
            if (cb.Checked)
            {
                current |= added;
            }

            return current;
        }

        #region Browse Buttons

        private void _btnDataSet_Click( object sender, EventArgs e )
        {
            if (Browse( this._txtDataSetData ))
            {
                this.TryAutoSet( this._txtDataSetData.Text, this._txtDataSetObs, this._fileLoadInfo.AUTOFILE_OBSERVATIONS );
                this.TryAutoSet( this._txtDataSetData.Text, this._txtDataSetVar, this._fileLoadInfo.AUTOFILE_PEAKS );
            }
        }

        private void TryAutoSet( string firstFileName, TextBox dst, string[] possibleFileNames )
        {                                                                   
            if (dst.TextLength == 0)
            {
                foreach (string s in possibleFileNames)
                {
                    string s2 = s.Replace( "*", Path.GetFileNameWithoutExtension( firstFileName ) );
                    s2 = Path.Combine( Path.GetDirectoryName( firstFileName ), s2 );

                    if (File.Exists( s2 ))
                    {
                        dst.Text = s2;
                        return;
                    }
                }
            }
        }

        private void _btnDataSetObs_Click( object sender, EventArgs e )
        {
            Browse( this._txtDataSetObs );
        }

        private void _btnDataSetVar_Click( object sender, EventArgs e )
        {
            Browse( this._txtDataSetVar );
        }

        private void _btnIdentifications_Click( object sender, EventArgs e )
        {
            this._lstCompounds.Items.AddRange( this._lstAvailCompounds.SelectedItems.Cast<CompoundLibrary>().ToArray() );
            this.UpdateAvailableCompoundsList();     
        }

        private void UpdateAvailableCompoundsList()
        {
            this._lstAvailCompounds.Items.Clear();

            foreach (var cl in this._compoundLibraries)
            {
                if (!this._lstCompounds.Items.Contains( cl ))
                {
                    this._lstAvailCompounds.Items.Add( cl );
                }
            }
        }

        private void UpdateAvailableAdductsList()
        {
            this._lstAvailableAdducts.Items.Clear();

            foreach (var cl in this._adductLibraries)
            {
                if (!this._lstAdducts.Items.Contains( cl ))
                {
                    this._lstAvailableAdducts.Items.Add( cl );
                }
            }
        }

        private void _btnAltVals_Click( object sender, EventArgs e )
        {
            Browse( this._txtAltVals );
        }

        private void _btnCondInfo_Click( object sender, EventArgs e )
        {
            Browse( this._txtCondInfo );
        }

        #endregion

        private void CheckTheBox( CheckBox cb, TextBox tb, Button bn )
        {
            tb.Enabled = cb.Checked;
            bn.Enabled = cb.Checked;

            if (cb.Checked && !this._ignoreChanges && tb.TextLength == 0)
            {
                bn.PerformClick();
            }
        }

        private void _chkIdentifications_CheckedChanged( object sender, EventArgs e )
        {
            this.CheckTheBox( this._chkIdentifications, this._txtIdentifications, this._btnIdentifications );
            this._cbManualFlag.Enabled = this.ctlLabel3.Enabled = this._chkIdentifications.Checked;
        }   

        private void _chkAltVals_CheckedChanged( object sender, EventArgs e )
        {
            this.CheckTheBox( this._chkAltVals, this._txtAltVals, this._btnAltVals );
        }

        private void _chkCondInfo_CheckedChanged( object sender, EventArgs e )
        {
            this.CheckTheBox( this._chkCondInfo, this._txtCondInfo, this._btnCondInfo );
        }

        private void _wizard_HelpClicked( object sender, EventArgs e )
        {
            this.ToggleHelp();
        }

        private void ToggleHelp()
        {
            this.splitContainer1.Panel2Collapsed = !this.splitContainer1.Panel2Collapsed;
            this.UpdateHelpButton();
        }

        private void _btnDeleteWorkspace_Click( object sender, EventArgs e )
        {
           
        }

        private void _btnRecent_Click( object sender, EventArgs e )
        {
            
        }

        private void toolTip1_Popup( object sender, PopupEventArgs e )
        {
            Control c = e.AssociatedControl;

            this.ShowControlHelp( c );

            e.Cancel = true;
        }

        private void ShowControlHelp( Control c )
        {                    
            string text = this._tipSideBar.GetToolTip( c );

            text = Regex.Replace(text, "(?<!\r)\n", "\r\n" );

            // We use "*" to signify "nothing" or we don't get the Popup event saying when a control has lost focus.
            if (string.IsNullOrEmpty( text ) || text=="*")
            {          
                text = "Click a control for help";
            }

            if (text.StartsWith("FILEFORMAT"))
            {
                this._txtHelp.Text = text.After( "FILEFORMAT\r\n" ).Before( "{" ).Replace( "CSVFILE", "The name of a CSV (spreadsheet) file. Click the button below to shown specific details on the data expected." );
                this._btnShowFf.Visible = true;
                this._btnShowFf.Tag = text;
            }
            else
            {
                this._txtHelp.Text = text;
                this._btnShowFf.Visible = false;
            }
        }

        private void button2_Click( object sender, EventArgs e )
        {
            if (MainSettings.Instance.RecentSessions.Count == 0)
            {
                this.BrowseForSession();
            }
            else
            {
                this._cmsRecentSessions.Show( this._btnReturnToSession, 0, this._btnReturnToSession.Height );
            }
        }

        private void BrowseForSession()
        {
            string fileName = this.BrowseForFile( null, UiControls.EFileExtension.Sessions, FileDialogMode.Open, UiControls.EInitialFolder.Sessions );

            if (fileName != null)
            {
                this.LoadSession( fileName );
            }
        }

        private void LoadSession( string fn )
        {
            if (string.IsNullOrWhiteSpace( fn ))
            {
                this.BrowseForSession();
                return;
            }

            this._result = FrmActDataLoad.Show( this, fn );

            if (this._result == null)
            {
                FrmMsgBox.ShowError( this, Resx.Manual.UnableToLoadSession );
                return;
            }

            if (this._result.FileNames == null)
            {
                this._result.FileNames = new DataFileNames();
            }

            // Loaded ok!
            MainSettings.Instance.AddRecentSession( this._result );
            MainSettings.Instance.Save();

            if (this._result.FileNames.AppVersion == null)
            {
                this._result.FileNames.AppVersion = new Version();
            }

            if (this._result.FileNames.AppVersion != UiControls.Version)
            {
                if (!FrmActOldData.Show( this, this._result.FileNames ))
                {
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void exploreToolStripMenuItem_Click( object sender, EventArgs e )
        {
            UiControls.ExploreTo( this, UiControls.StartupPath );
        }

        private void clearRPathrequiresRestartToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if (FrmMsgBox.ShowYesNo( this, "Restore Settings", "Restore settings to defaults and restart program?", Resources.MsgWarning ))
            {
                MainSettings.Instance.Reset();
                MainSettings.Instance.Save();
                UiControls.RestartProgram(this);
            }
        }

        private void contextMenuStrip1_Opening( object sender, CancelEventArgs e )
        {
            this._mnuBrowseWorkspace.Visible = !this._historyDelete;
            this._mnuBrowseWorkspaceSep.Visible = !this._historyDelete;
        }

        private void _btnMostRecent_Click( object sender, EventArgs e )
        {
            this.LoadSession( (string)this._btnMostRecent.Tag );
        }

        private void button1_Click( object sender, EventArgs e )
        {
            this._wizard.Page += 1;
            FrmMsgBox.ShowHint( this, "Select the help button to display the help side-bar describing file-format details, etc.", FrmMsgBox.EDontShowAgainId.HelpSideBar, Resources.MnuHelpBar );
        }

        private void _lstLcmsMode_SelectedIndexChanged( object sender, EventArgs e )
        {                                   
        }

        private void linkLabel1_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            UiControls.ShowAbout( this );
        }

        private void restartToolStripMenuItem_Click( object sender, EventArgs e )
        {
            UiControls.RestartProgram(this);
        }

        public void UpdateCacheOfTypes()
        {     
            string conditionFile = this._chkCondInfo.Checked ? this._txtCondInfo.Text : null;
            string observationFile = this._txtDataSetObs.Text;
            bool useObsFile = string.IsNullOrWhiteSpace( conditionFile) || !File.Exists( conditionFile );
            string sourceDescription = useObsFile ? ("O" + observationFile) : ("C" + conditionFile);

            if (this._experimentalGroupCacheSource != sourceDescription)
            {
                this._experimentalGroupCacheSource = sourceDescription;
                
                this._experimentalGroupCache.Clear();

                try
                {
                    // Don't show the wait form because it takes too little time
                    FrmWait.Show( this, "Updating experimental groups", null,
                        delegate ( ProgressReporter progress )
                        {
                            SpreadsheetReader reader = new SpreadsheetReader()
                            {
                                Progress = progress.SetProgress,
                            }
                            ;

                            if (useObsFile)
                            {
                                if (File.Exists( observationFile ))
                                {
                                    Spreadsheet<string> info = reader.Read<string>( observationFile );
                                    int typeCol = info.TryFindColumn( this._fileLoadInfo.OBSFILE_GROUP_HEADER );

                                    for (int row = 0; row < info.NumRows; row++)
                                    {
                                        string id = typeCol == -1 ? "A" : info[row, typeCol];

                                        if (!this._experimentalGroupCache.Any( z => z.Id.ToUpper() == id.ToUpper() ))
                                        {
                                            this._experimentalGroupCache.Add( new ExpCond( id, "Type " + id ) );
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var kvp in FrmActDataLoad.LoadConditionInfo( this._fileLoadInfo, conditionFile, progress ))
                                {
                                    this._experimentalGroupCache.Add( new ExpCond( kvp.Key, kvp.Value ) );
                                }
                            }
                        } );
                }
                catch
                {
                   // NA
                }                                                        
            }

            this._cbExp.UpdateText();
            this._cbControl.UpdateText();
        }

        private ConditionBox<string> CreateExpConditionBox( CtlTextBox textBox, Button button )
        {
            return new DataSet<string>()
            {
                ListTitle = "Experimental Conditions",
                ListSource = new TypeCacheIdsWrapper( this ),        
                ItemTitle = this.ConditionBox_ItemNameProvider,
                ItemDescription = this.ConditionBox_ItemDescriptionProvider,
                DynamicItemRetriever = this.ConditionBox_DynamicItemRetriever,
            }.CreateConditionBox( textBox, button );
        }

        class ExpCond
        {
            public readonly string Id;
            public readonly string Name;

            public ExpCond( string id, string name )
            {
                this.Id = id;
                this.Name = name;
            }

            public override string ToString()
            {
                return this.Name;
            }
        }

        /// <summary>
        /// Wraps the list of type IDs to make a check to update the list from the
        /// users choices whenever the list is enumerated (e.g. by the options list).
        /// </summary>
        private class TypeCacheIdsWrapper : IEnumerable<string>
        {
            private FrmEditDataFileNames _owner;

            public TypeCacheIdsWrapper( FrmEditDataFileNames owner )
            {
                this._owner = owner;
            }

            public IEnumerator<string> GetEnumerator()
            {                               
                return this._owner._experimentalGroupCache.Select( z => z.Id ).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        private bool ConditionBox_DynamicItemRetriever( string entry, out string item )
        {
            var expCond = this._experimentalGroupCache.FirstOrDefault( z => z.Id == entry.ToUpper() || z.Name.ToUpper() == entry.ToUpper() );

            if (expCond == null)
            {
                item = entry;
                return true;
            }

            item = expCond.Id;
            return true;
        }

        private string ConditionBox_ItemNameProvider( string item )
        {
            var expCond = this._experimentalGroupCache.FirstOrDefault( z => z.Id == item );

            if (expCond == null)
            {
                return item;
            }

            return expCond.Name;
        }

        private string ConditionBox_ItemDescriptionProvider( string item )
        {
            var expCond = this._experimentalGroupCache.FirstOrDefault( z => z.Id == item );

            if (expCond == null)
            {
                return "No details available";
            }

            return expCond.Id + " = \"" + expCond.Name + "\"";
        }    

        private void _chkStatT_CheckedChanged( object sender, EventArgs e )
        {

        }

        private void button1_Click_1( object sender, EventArgs e )
        {
            
        }

        private void resetdoNotShowAgainMessagesToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if (FrmMsgBox.ShowYesNo( this, "Restore Settings", "Clear \"do not show again\" choices and restart program?.", Resources.MsgWarning ))
            {
                MainSettings.Instance.DoNotShowAgain.Clear();
                MainSettings.Instance.Save();
                UiControls.RestartProgram(this);
            }
        }

        private void ctlButton2_Click( object sender, EventArgs e )
        {
            this.RemoveSelected( this._lstCompounds );
            this.UpdateAvailableCompoundsList();    
        }

        private void RemoveSelected( ListBox list )
        {
            int i = list.SelectedIndex;

            if (i != -1)
            {
                list.Items.RemoveAt( i );

                if (i < list.Items.Count)
                {
                    list.SelectedIndex = i;
                }
                else
                {
                    list.SelectedIndex = i - 1;
                }
            }
        }

        private void _btnAddCompoundLibrary_Click( object sender, EventArgs e )
        {
            CompoundLibrary sel = FrmEditCompoundLibrary.Show( this );

            if (sel != null)
            {
                this._lstCompounds.Items.Add( sel );  
            }
        }

        private void _btnAddAdduct_Click( object sender, EventArgs e )
        {
            this._lstAdducts.Items.AddRange( this._lstAvailableAdducts.SelectedItems.Cast<NamedItem<string>>().ToArray() );
            this.UpdateAvailableAdductsList(); 
        }

        private void ctlButton3_Click( object sender, EventArgs e )
        {
            this.RemoveSelected( this._lstAdducts );
            this.UpdateAvailableAdductsList();  
        }

        private void _btnBrowseAdducts_Click( object sender, EventArgs e )
        {
            string fn = UiControls.BrowseForFile( this, null, UiControls.EFileExtension.Csv, FileDialogMode.Open, UiControls.EInitialFolder.None );

            if (fn != null)
            {
                this._lstAdducts.Items.Add( new NamedItem<string>( fn, fn ) );
            }
        }

        private void _btnReconfigure_Click( object sender, EventArgs e )
        {
            this._mnuDebug.Show( this._btnReconfigure, new Point( this._btnReconfigure.Width, 0 ), ToolStripDropDownDirection.AboveLeft );
        }

        private void editPathsAndLibrariesToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if (FrmInitialSetup.Show( this, true ))
            {
                UiControls.RestartProgram(this);
            }
        }

        private void _btnAddAllCompounds_Click( object sender, EventArgs e )
        {
            this._lstCompounds.Items.AddRange( this._lstAvailCompounds.Items.Cast<CompoundLibrary>().ToArray() );
            this.UpdateAvailableCompoundsList();  
        }

        private void _chkAutoIdentify_CheckedChanged( object sender, EventArgs e )
        {
            this._numTolerance.Enabled = this._lblTolerance.Enabled = this._lstTolerance.Enabled = (this._chkAutoIdentify.Checked || this._chkPeakPeakMatch.Checked);
            this._cbAutomaticFlag.Enabled = this.ctlLabel1.Enabled = this._chkAutoIdentify.Checked;
        }

        private void _txtDataSetData_TextChanged( object sender, EventArgs e )
        {

        }

        private void _lblDataSetData_Click( object sender, EventArgs e )
        {

        }

        private void _btnShowFf_Click( object sender, EventArgs e )
        {
            FrmViewSpreadsheet.Show( this, this._btnShowFf.Tag as string, this._fileLoadInfo );
        }

        private void cSVManipulatorToolStripMenuItem_Click( object sender, EventArgs e )
        {
            UiControls.StartProcess( this, Path.Combine( Application.StartupPath, "ConvertGPDataFormat.exe" ) );
        }

        private void _txtDataSetObs_TextChanged( object sender, EventArgs e )
        {

        }

        private void _btnIdentifications_Click_1( object sender, EventArgs e )
        {
            Browse( this._txtIdentifications );
        }    

        private void radioButton2_Click( object sender, EventArgs e )
        {
            Control control = (Control)sender;
            this._historyDelete = false;
            this._cmsRecentWorkspaces.Show( control, 0, control.Height );
        }

        private void radioButton1_CheckedChanged( object sender, EventArgs e )
        {
            this.LoadWorkspace( new DataFileNames() );
        }

        private void ctlButton3_Click_1( object sender, EventArgs e )
        {
            this._historyDelete = true;
            this._cmsRecentWorkspaces.Show( this._btnDeleteWorkspace, 0, this._btnDeleteWorkspace.Height );
        }

        private void _radRecentWorkspace_CheckedChanged( object sender, EventArgs e )
        {

        }

        private void ctlTitleBar1_HelpClicked( object sender, CancelEventArgs e )
        {
            this.ToggleHelp();
        }

        private void tableLayoutPanel5_Paint( object sender, PaintEventArgs e )
        {

        }

        private void _chkConditions_CheckedChanged( object sender, EventArgs e )
        {
            this._lblConditions.Enabled = this.label3.Enabled = this._cbExp.Enabled = this._cbControl.Enabled = this._chkConditions.Checked;
        }
    }
}
