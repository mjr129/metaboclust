using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data.General;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;
using System.Collections;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.DataLoader;
using MGui.Datatypes;
using MGui.Helpers;
using MGui.Controls;
using MGui;
using MetaboliteLevels.Forms.Wizards;

namespace MetaboliteLevels.Forms.Startup
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
        private string CondInfoFileName { get { return _chkCondInfo.Checked ? _txtCondInfo.Text : null; } }

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
            InitializeComponent();
            UiControls.SetIcon( this );

            _btnNewSession.BackColor = _btnNewSession.FlatAppearance.BorderColor
                = _btnReturnToSession.BackColor = _btnReturnToSession.FlatAppearance.BorderColor
                = _btnMostRecent.BackColor = _btnMostRecent.FlatAppearance.BorderColor
                = UiControls.TitleBackColour;

            _btnNewSession.FlatAppearance.MouseOverBackColor
                = _btnReturnToSession.FlatAppearance.MouseOverBackColor
                = _btnMostRecent.FlatAppearance.MouseOverBackColor
                = ColourHelper.Blend( UiControls.TitleBackColour, Color.Black, 0.1 );

            _fileLoadInfo = XmlSettings.LoadAndResave<FileLoadInfo>( FileId.FileLoadInfo, ProgressReporter.GetEmpty(), null );

            // Match program description to title bar
            _lblProgramDescription.BackColor = UiControls.TitleBackColour;
            _lblProgramDescription.ForeColor = UiControls.TitleForeColour;
            _lblOrder.BackColor = UiControls.TitleBackColour;

            // Set texts
            Text = UiControls.Title + " - Load data";
            _tabWelcome.Text = UiControls.Title;

            // Show a warning in 32-bit mode
            _lbl32BitWarning.Visible = IntPtr.Size != 8;

            // Setup the wizard
            _wizard = CtlWizard.BindNew( tabControl1.Parent, tabControl1, CtlWizardOptions.ShowCancel | CtlWizardOptions.HandleBasicChanges );
            _wizard.Pager.PageTitles[0] = UiControls.Title;
            _wizard.HelpClicked += _wizard_HelpClicked;
            _wizard.CancelClicked += _wizard_CancelClicked;
            _wizard.OkClicked += _wizard_OkClicked;
            _wizard.PermitAdvance += _wizard_PermitAdvance;
            _wizard.Pager.PageChanged += _wizard_PageChanged;
            _wizard.TitleHelpText = Manual.DataLoadQueryHelp;

            // Setup the experimental group boxes
            _cbControl = CreateExpConditionBox( _txtControls, _btnBrowseContCond );
            _cbExp = CreateExpConditionBox( _txtExps, _btnBrowseExpCond );

            // Setup annotations
            _cbAutomaticFlag = DataSet.ForDiscreteEnum<EAnnotation>( "Annotation", (EAnnotation) (- 1) ).CreateComboBox(_automaticFlag, null, ENullItemName.NoNullItem);
            _cbManualFlag = DataSet.ForDiscreteEnum<EAnnotation>( "Annotation", (EAnnotation) (- 1) ).CreateComboBox( _manualFlag, null, ENullItemName.NoNullItem );

            // Setup help
            splitContainer1.Panel2Collapsed = true;

            // Setup captions
            linkLabel1.Text = UiControls.Title + " " + UiControls.VersionString.ToString();
            _lblProgramDescription.Text = UiControls.Description;

            // Populate enum boxes
            EnumComboBox.Populate< ELcmsMode>( _lstLcmsMode );
            EnumComboBox.Populate< ETolerance>( _lstTolerance );

            // Populate CompoundLibrary's
            FrmActDataLoad.GetCompoundLibraries( out _compoundLibraries, out _adductLibraries );

            if (_compoundLibraries.Count == 0)
            {
                ReplaceWithMessage( _lstAvailCompounds, _btnAddCompound );
            }

            if (_adductLibraries.Count == 0)
            {
                ReplaceWithMessage( _lstAvailableAdducts, _btnAddAdduct );
            }

            UpdateAvailableCompoundsList();
            UpdateAvailableAdductsList();

            // Add recent entries menu
            var recentWorkspaces = MainSettings.Instance.RecentWorkspaces;
            var lfn = recentWorkspaces.Count != 0 ? recentWorkspaces.Last() : new DataFileNames();
            LoadDataFileNames( lfn );

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

                tsmi.Click += tsmi_Click;

                _cmsRecentWorkspaces.Items.Add( tsmi );
                var x = UiControls.AddMenuCaption( _cmsRecentWorkspaces, "Details..." );
                x.Tag = recentWorkspace;
                x.Click += FrmDataLoadQuery_Click;
            }

            _mnuBrowseWorkspaceSep      = new ToolStripSeparator();
            _cmsRecentWorkspaces.Items.Add( _mnuBrowseWorkspaceSep );
            _mnuBrowseWorkspace         = new ToolStripMenuItem( "Browse..." );
            _mnuBrowseWorkspace.Click   += _mnuBrowseWorkspace_Click;
            _mnuBrowseWorkspace.Font    = new Font( _mnuBrowseWorkspace.Font, FontStyle.Bold );
            _cmsRecentWorkspaces.Items.Add( _mnuBrowseWorkspace );

            _btnDeleteWorkspace.Visible = recentWorkspaces.Count != 0;

            // Sessions
            tsmi = new ToolStripMenuItem
            {
                Text  = "Browse...",
                Font  = FontHelper.BoldFont,
                Tag   = null,
                Image = Resources.MnuOpen
            };

            tsmi.Click += mnuRecentSession_Click;
            _cmsRecentSessions.Items.Add( tsmi );

            _cmsRecentSessions.Items.Add( new ToolStripSeparator() );

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
                tsmi.Click      += mnuRecentSession_Click;

                if (!File.Exists( entry.FileName ))
                {
                    tsmi.ForeColor = Color.FromKnownColor( KnownColor.GrayText );
                }

                _cmsRecentSessions.Items.Add( tsmi );
                UiControls.AddMenuCaptionFilename( _cmsRecentSessions, entry.FileName );
            }

            if (recentSessions.Count == 0)
            {
                _btnReturnToSession.Text += "...";
                _btnMostRecent.Visible    = false;
            }
            else
            {
                var mostRecent = recentSessions[0];

                _btnMostRecent.Text = "    " + mostRecent.Title;
                _tipSideBar.SetToolTip( _btnMostRecent, mostRecent.FileName );
                _btnMostRecent.Tag = mostRecent.FileName;

                if (!File.Exists( mostRecent.FileName ))
                {
                    _btnMostRecent.Visible = false;
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
                if (!string.IsNullOrWhiteSpace( _tipSideBar.GetToolTip( control ) ))
                {
                    if (control is CtlLabel)
                    {
                        control.MouseEnter += Control_MouseEnter;
                        control.MouseLeave += Control_MouseLeave;
                        control.Cursor = Cursors.Help;
                    }

                    control.GotFocus += Control_Click;
                    control.MouseDown += Control_Click;
                }
            }
        }

        private void Control_Click( object sender, EventArgs e )
        {
            if (sender is Label)
            {
                if (splitContainer1.Panel2Collapsed)
                {
                    ToggleHelp();
                }
            }

            ShowControlHelp( (Control)sender );
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

        private void FrmDataLoadQuery_Click( object sender, EventArgs e )
        {
            DataFileNames recentWorkspace = (DataFileNames)((ToolStripLabel)sender).Tag;

            UiControls.ShowSessionInfo( this, recentWorkspace );
        }

        protected override void OnSizeChanged( EventArgs e )
        {
            base.OnSizeChanged( e );

            int x = Math.Max( 0, (ClientSize.Width - 1024) ) / 3;
            int y = Math.Max( 0, (ClientSize.Height - 768) ) / 3;

            splitContainer1.Margin = new Padding( x, y, x, y );
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

                LoadDataFileNames( core.FileNames );
            }
        }

        void _wizard_PageChanged( object sender, EventArgs e )
        {
            if (_wizard.Page != 0)
            {
                _wizard.Options |= (CtlWizardOptions.ShowBack | CtlWizardOptions.ShowNext | CtlWizardOptions.ShowHelp);
            }
            else
            {
                _wizard.Options &= ~(CtlWizardOptions.ShowBack | CtlWizardOptions.ShowNext | CtlWizardOptions.ShowHelp);
            }
        }

        void _wizard_CancelClicked( object sender, CancelEventArgs e )
        {
            if (_wizard.Page == 0)
            {
                _wizard.Page = 0;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        /// Handles wizard - can advance page?
        /// </summary>
        /// <param name="input">Page number</param>
        /// <returns></returns>
        bool _wizard_PermitAdvance( int input )
        {
            switch (input)
            {
                case 0: // Welcome
                    break;

                case 1: // Session name                                           
                    _checker.Check( _txtTitle, _txtTitle.Text.Length != 0, "A session title must be provided." );
                    break;

                case 2: // Select data
                    _checker.Check( _lstLcmsMode, _lstLcmsMode.SelectedIndex != -1, "A LC-MS mode must be provided (use \"other\" for non-LC-MS data)." );
                    _checker.Check( _txtDataSetData, File.Exists( _txtDataSetData.Text ), "Filename not provided or file not found." );
                    _checker.Check( _txtDataSetObs, File.Exists( _txtDataSetObs.Text ), "Filename not provided or file not found." );
                    _checker.Check( _txtDataSetVar, File.Exists( _txtDataSetVar.Text ), "Filename not provided or file not found." );
                    _checker.Check( _txtAltVals, !_chkAltVals.Checked || File.Exists( _txtAltVals.Text ), "Filename not provided or file not found." );
                    _checker.Check( _chkCondInfo, !_chkCondInfo.Checked || File.Exists( _txtCondInfo.Text ), "Filename not provided or file not found." );
                    break;

                case 3: // Statistics
                    _checker.Check( _cbExp.TextBox, !_chkStatT.Checked || _cbExp.SelectedItems != null, "Experimental conditions must be provided to conduct t-tests." );
                    _checker.Check( _cbControl.TextBox, !_chkStatT.Checked || _cbControl.SelectedItems != null, "Control conditions must be provided to conduct t-tests." );
                    break;

                case 4: // Compounds
                    break;

                case 5: // Annotations
                    bool doesntNeedTol = !_chkAutoIdentify.Checked && !_chkPeakPeakMatch.Checked;
                    _checker.Check( _numTolerance, doesntNeedTol || _numTolerance.Value != 0, "A tolerance of zero is probably a mistake." );
                    _checker.Check( _lstTolerance, doesntNeedTol || _lstTolerance.SelectedIndex != -1, "A unit must be specified." );
                    _checker.Check( _txtIdentifications, !_chkIdentifications.Checked || File.Exists( _txtIdentifications.Text ), "Filename not provided or file not found." );
                    break;

                case 6: // Ready
                    break;

                default: // ???
                    return false;
            }

            return _checker.NoErrors;
        }   

        /// <summary>
        /// Loads a session configuration.
        /// </summary>                    
        private void LoadDataFileNames( DataFileNames lfn )
        {
            _txtTitle.Text = lfn.Title;

            _txtDataSetData.Text = lfn.Data;
            _txtDataSetObs.Text  = lfn.ObservationInfo;
            _txtDataSetVar.Text  = lfn.PeakInfo;
            _lstCompounds.Items.Clear();

            if (lfn.CompoundLibraies != null)
            {
                foreach (CompoundLibrary cl in lfn.CompoundLibraies)
                {
                    if (_compoundLibraries.Any( z => z.ContentsMatch( cl ) ))
                    {
                        _lstCompounds.Items.Add( _compoundLibraries.Find( z => z.ContentsMatch( cl ) ) );
                    }
                    else
                    {
                        _lstCompounds.Items.Add( cl );
                    }
                }
            }

            _lstAdducts.Items.Clear();

            if (lfn.AdductLibraries != null)
            {
                foreach (string item in lfn.AdductLibraries)
                {
                    if (_adductLibraries.Any( z => z.Value == item ))
                    {
                        _lstAdducts.Items.Add( _adductLibraries.Find( z => z.Value == item ) );
                    }
                    else
                    {
                        _lstAdducts.Items.Add( new NamedItem<string>( item, item ) );
                    }
                }
            }

            SetText( _txtIdentifications, _chkIdentifications, lfn.Identifications );
            SetText( _txtAltVals, _chkAltVals, lfn.AltData );
            SetText( _txtCondInfo, _chkCondInfo, lfn.ConditionInfo );
            SetCheck( _chkStatT, lfn.StandardStatisticalMethods, EStatisticalMethods.TTest );
            SetCheck( _chkStatP, lfn.StandardStatisticalMethods, EStatisticalMethods.Pearson );

            _chkAutoIdentify.Checked = lfn.AutomaticIdentifications;
            _chkPeakPeakMatch.Checked = lfn.PeakPeakMatching;
            _cbAutomaticFlag.SelectedItem = lfn.AutomaticIdentificationsStatus;
            _cbManualFlag.SelectedItem = lfn.ManualIdentificationsStatus;

            if (lfn.AutomaticIdentifications || lfn.PeakPeakMatching)
            {
                _numTolerance.Value = lfn.AutomaticIdentificationsToleranceValue;
                EnumComboBox.Set( _lstTolerance, lfn.AutomaticIdentificationsToleranceUnit, false );
            }
            else
            {
                _numTolerance.Value = 0.0m;
                EnumComboBox.Set<ETolerance>( _lstTolerance, ETolerance.PartsPerMillion);
            }


            if (!string.IsNullOrWhiteSpace( lfn.Data ))
            {
                EnumComboBox.Set( _lstLcmsMode, lfn.LcmsMode, true );

                _cbExp.SelectedItems = lfn.ConditionsOfInterestString.Where( z => z != null ); // deal with legacy invalid data
                _cbControl.SelectedItems = lfn.ControlConditionsString.Where( z => z != null );
            }
            else
            {
                EnumComboBox.Clear( _lstLcmsMode );
                _txtExps.Text = "";
                _txtControls.Text = "";
            }

            UpdateAvailableCompoundsList();
            UpdateAvailableAdductsList();
        }

        private void SetCheck( CheckBox cb, EStatisticalMethods current, EStatisticalMethods toCheck )
        {
            cb.Checked = current.HasFlag( toCheck );
        }

        void tsmi_Click( object sender, EventArgs e )
        {
            var s = (ToolStripMenuItem)sender;
            var fn = (DataFileNames)s.Tag;

            if (_historyDelete)
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
                LoadDataFileNames( fn );
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

            LoadSession( rs == null ? null : rs.FileName );
        }

        private void SetText( TextBox txt, CheckBox chk, string current )
        {
            _ignoreChanges = true;

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

            _ignoreChanges = false;
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
                fileNames.Title                      = _txtTitle.Text;
                fileNames.LcmsMode                   = EnumComboBox.Get<ELcmsMode>( _lstLcmsMode );
                fileNames.Data                       = _txtDataSetData.Text;
                fileNames.ObservationInfo            = _txtDataSetObs.Text;
                fileNames.PeakInfo                   = _txtDataSetVar.Text;
                fileNames.CompoundLibraies           = _lstCompounds.Items.Cast<CompoundLibrary>().ToList();
                fileNames.Identifications            = _chkIdentifications.Checked ? _txtIdentifications.Text : null;
                fileNames.AdductLibraries            = _lstAdducts.Items.Cast<NamedItem<string>>().Select( z => z.Value ).ToList();
                fileNames.AutomaticIdentifications   = _chkAutoIdentify.Checked;
                fileNames.AutomaticIdentificationsStatus = _cbAutomaticFlag.SelectedItem;
                fileNames.ManualIdentificationsStatus = _cbManualFlag.SelectedItem;
                fileNames.PeakPeakMatching           = _chkPeakPeakMatch.Checked;
                fileNames.Session                    = null;
                fileNames.AltData                    = _chkAltVals.Checked ? _txtAltVals.Text : null;
                fileNames.ConditionInfo              = CondInfoFileName;
                fileNames.ConditionsOfInterestString = new List<string>( _cbExp.GetSelectedItemsOrThrow() );
                fileNames.ControlConditionsString    = new List<string>( _cbControl.GetSelectedItemsOrThrow() );
                fileNames.StandardStatisticalMethods = EStatisticalMethods.None;
                fileNames.StandardStatisticalMethods = GetCheck( _chkStatT, fileNames.StandardStatisticalMethods, EStatisticalMethods.TTest );
                fileNames.StandardStatisticalMethods = GetCheck( _chkStatP, fileNames.StandardStatisticalMethods, EStatisticalMethods.Pearson );
                fileNames.AutomaticIdentificationsToleranceUnit  = EnumComboBox.Get<ETolerance>( _lstTolerance, ETolerance.PartsPerMillion );
                fileNames.AutomaticIdentificationsToleranceValue = _numTolerance.Value;
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
            _result = FrmActDataLoad.Show( this, fileNames, _fileLoadInfo );

            if (_result == null)
            {
                return;
            }

            if (_chkAlarm.Checked)
            {
                FrmActAlarm.Show( this );
            }

            DialogResult = DialogResult.OK;
        }

        private EStatisticalMethods GetCheck( CheckBox cb, EStatisticalMethods current, EStatisticalMethods added )
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
            if (Browse( _txtDataSetData ))
            {
                TryAutoSet( _txtDataSetData.Text, _txtDataSetObs, _fileLoadInfo.AUTOFILE_OBSERVATIONS );
                TryAutoSet( _txtDataSetData.Text, _txtDataSetVar, _fileLoadInfo.AUTOFILE_PEAKS );
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
            Browse( _txtDataSetObs );
        }

        private void _btnDataSetVar_Click( object sender, EventArgs e )
        {
            Browse( _txtDataSetVar );
        }

        private void _btnIdentifications_Click( object sender, EventArgs e )
        {
            _lstCompounds.Items.AddRange( _lstAvailCompounds.SelectedItems.Cast<CompoundLibrary>().ToArray() );
            UpdateAvailableCompoundsList();
            UpdateAutoIdentifyButton();
        }

        private void UpdateAvailableCompoundsList()
        {
            _lstAvailCompounds.Items.Clear();

            foreach (var cl in _compoundLibraries)
            {
                if (!_lstCompounds.Items.Contains( cl ))
                {
                    _lstAvailCompounds.Items.Add( cl );
                }
            }
        }

        private void UpdateAvailableAdductsList()
        {
            _lstAvailableAdducts.Items.Clear();

            foreach (var cl in _adductLibraries)
            {
                if (!_lstAdducts.Items.Contains( cl ))
                {
                    _lstAvailableAdducts.Items.Add( cl );
                }
            }
        }

        private void _btnAltVals_Click( object sender, EventArgs e )
        {
            Browse( _txtAltVals );
        }

        private void _btnCondInfo_Click( object sender, EventArgs e )
        {
            Browse( _txtCondInfo );
        }

        #endregion

        private void CheckTheBox( CheckBox cb, TextBox tb, Button bn )
        {
            tb.Enabled = cb.Checked;
            bn.Enabled = cb.Checked;

            if (cb.Checked && !_ignoreChanges && tb.TextLength == 0)
            {
                bn.PerformClick();
            }
        }

        private void _chkIdentifications_CheckedChanged( object sender, EventArgs e )
        {
            CheckTheBox( _chkIdentifications, _txtIdentifications, _btnIdentifications );
            _cbManualFlag.Enabled = ctlLabel3.Enabled = _chkIdentifications.Checked;
        }

        private void UpdateAutoIdentifyButton()
        {
            var e = EnumComboBox.Get( _lstLcmsMode, ELcmsMode.None );
            _chkAutoIdentify.Enabled = _lstCompounds.Items.Count != 0 && _lstAdducts.Items.Count != 0 && e != ELcmsMode.None;
            _chkAutoIdentify.Checked = _chkAutoIdentify.Enabled;
            _chkPeakPeakMatch.Enabled = e != ELcmsMode.None;
            _chkPeakPeakMatch.Checked = _chkPeakPeakMatch.Checked && _chkPeakPeakMatch.Enabled;
        }

        private void _chkAltVals_CheckedChanged( object sender, EventArgs e )
        {
            CheckTheBox( _chkAltVals, _txtAltVals, _btnAltVals );
        }

        private void _chkCondInfo_CheckedChanged( object sender, EventArgs e )
        {
            CheckTheBox( _chkCondInfo, _txtCondInfo, _btnCondInfo );
        }

        private void _wizard_HelpClicked( object sender, EventArgs e )
        {
            ToggleHelp();
        }

        private void ToggleHelp()
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
            _wizard.HelpText = splitContainer1.Panel2Collapsed ? "Show help" : "Hide help";
        }

        private void _btnDeleteWorkspace_Click( object sender, EventArgs e )
        {
            _historyDelete = true;
            _cmsRecentWorkspaces.Show( _btnDeleteWorkspace, 0, _btnDeleteWorkspace.Height );
        }

        private void _btnRecent_Click( object sender, EventArgs e )
        {
            _historyDelete = false;
            _cmsRecentWorkspaces.Show( _btnRecent, 0, _btnRecent.Height );
        }

        private void toolTip1_Popup( object sender, PopupEventArgs e )
        {
            Control c = e.AssociatedControl;

            ShowControlHelp( c );

            e.Cancel = true;
        }

        private void ShowControlHelp( Control c )
        {                    
            string text = _tipSideBar.GetToolTip( c );

            // We use "*" to signify "nothing" or we don't get the Popup event saying when a control has lost focus.
            if (string.IsNullOrEmpty( text ) || text=="*")
            {          
                text = "Click a control for help";
            }

            if (text.StartsWith("FILEFORMAT"))
            {
                _txtHelp.Text = text.After( "FILEFORMAT\r\n" ).Before( "{" );
                _btnShowFf.Visible = true;
                _btnShowFf.Tag = text;
            }
            else
            {
                _txtHelp.Text = text;
                _btnShowFf.Visible = false;
            }
        }

        private void button2_Click( object sender, EventArgs e )
        {
            if (MainSettings.Instance.RecentSessions.Count == 0)
            {
                BrowseForSession();
            }
            else
            {
                _cmsRecentSessions.Show( _btnReturnToSession, 0, _btnReturnToSession.Height );
            }
        }

        private void BrowseForSession()
        {
            string fileName = this.BrowseForFile( null, UiControls.EFileExtension.Sessions, FileDialogMode.Open, UiControls.EInitialFolder.Sessions );

            if (fileName != null)
            {
                LoadSession( fileName );
            }
        }

        private void LoadSession( string fn )
        {
            if (string.IsNullOrWhiteSpace( fn ))
            {
                BrowseForSession();
                return;
            }

            _result = FrmActDataLoad.Show( this, fn );

            if (_result == null)
            {
                FrmMsgBox.ShowError( this, Manual.UnableToLoadSession );
                return;
            }

            if (_result.FileNames == null)
            {
                _result.FileNames = new DataFileNames();
            }

            // Loaded ok!
            MainSettings.Instance.AddRecentSession( _result );
            MainSettings.Instance.Save();

            if (_result.FileNames.AppVersion == null)
            {
                _result.FileNames.AppVersion = new Version();
            }

            if (_result.FileNames.AppVersion != UiControls.Version)
            {
                if (!FrmActOldData.Show( this, _result.FileNames ))
                {
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void exploreToolStripMenuItem_Click( object sender, EventArgs e )
        {
            UiControls.ExploreTo( this, UiControls.StartupPath );
        }

        private void clearRPathrequiresRestartToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if (FrmMsgBox.ShowYesNo( this, "Restore Settings", "Restore settings to defaults and restart program?", Resources.MsgWarning ))
            {
                UiControls.RestartProgram(this);
            }
        }

        private void contextMenuStrip1_Opening( object sender, CancelEventArgs e )
        {
            _mnuBrowseWorkspace.Visible = !_historyDelete;
            _mnuBrowseWorkspaceSep.Visible = !_historyDelete;
        }

        private void _btnMostRecent_Click( object sender, EventArgs e )
        {
            LoadSession( (string)_btnMostRecent.Tag );
        }

        private void button1_Click( object sender, EventArgs e )
        {
            _wizard.Page += 1;
        }

        private void _lstLcmsMode_SelectedIndexChanged( object sender, EventArgs e )
        {
            UpdateAutoIdentifyButton();
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
            if (!Visible)
            {
                return;
            }

            string conditionFile = _chkCondInfo.Checked ? _txtCondInfo.Text : null;
            string observationFile = _txtDataSetObs.Text;
            bool useObsFile = string.IsNullOrWhiteSpace( conditionFile) || !File.Exists( conditionFile );
            string sourceDescription = useObsFile ? ("O" + observationFile) : ("C" + conditionFile);

            if (_experimentalGroupCacheSource != sourceDescription)
            {
                _experimentalGroupCacheSource = sourceDescription;
                
                _experimentalGroupCache.Clear();

                try
                {
                    FrmWait.Show(this, "Updating experimental groups", null, delegate(ProgressReporter progress)
                    {
                        SpreadsheetReader reader = new SpreadsheetReader()
                        {
                            Progress = progress.SetProgress,
                        };

                        if (useObsFile)
                        {
                            if (File.Exists( observationFile ))
                            {
                                Spreadsheet<string> info = reader.Read<string>( observationFile );
                                int typeCol = info.TryFindColumn( _fileLoadInfo.OBSFILE_GROUP_HEADER );

                                for (int row = 0; row < info.NumRows; row++)
                                {
                                    string id = typeCol == -1 ? "A" : info[row, typeCol];

                                    if (!_experimentalGroupCache.Any( z=> z.Id.ToUpper() == id.ToUpper() ))
                                    {
                                        _experimentalGroupCache.Add( new ExpCond( id, "Type " + id ) );
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var kvp in FrmActDataLoad.LoadConditionInfo( _fileLoadInfo, conditionFile, progress ))
                            {
                                _experimentalGroupCache.Add( new ExpCond( kvp.Key, kvp.Value ) );
                            }
                        }
                    } );
                }
                catch
                {
                   // NA
                }                                                        
            }
        }

        private ConditionBox<string> CreateExpConditionBox( CtlTextBox textBox, Button button )
        {
            return new DataSet<string>()
            {
                Title = "Experimental Conditions",
                Source = new TypeCacheIdsWrapper( this ),        
                ItemNameProvider = ConditionBox_ItemNameProvider,
                ItemDescriptionProvider = ConditionBox_ItemDescriptionProvider,
                DynamicItemRetriever = ConditionBox_DynamicItemRetriever,
            }.CreateConditionBox( textBox, button );
        }

        class ExpCond
        {
            public readonly string Id;
            public readonly string Name;

            public ExpCond( string id, string name )
            {
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return Name;
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
                _owner = owner;
            }

            public IEnumerator<string> GetEnumerator()
            {
                _owner.UpdateCacheOfTypes();
                return _owner._experimentalGroupCache.Select( z => z.Id ).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
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
            LoadDataFileNames( new DataFileNames() );
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
            RemoveSelected( _lstCompounds );
            UpdateAvailableCompoundsList();
            UpdateAutoIdentifyButton();
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
                _lstCompounds.Items.Add( sel );

                UpdateAutoIdentifyButton();
            }
        }

        private void _btnAddAdduct_Click( object sender, EventArgs e )
        {
            _lstAdducts.Items.AddRange( _lstAvailableAdducts.SelectedItems.Cast<NamedItem<string>>().ToArray() );
            UpdateAvailableAdductsList();
            UpdateAutoIdentifyButton();
        }

        private void ctlButton3_Click( object sender, EventArgs e )
        {
            RemoveSelected( _lstAdducts );
            UpdateAvailableAdductsList();
            UpdateAutoIdentifyButton();
        }

        private void _btnBrowseAdducts_Click( object sender, EventArgs e )
        {
            string fn = UiControls.BrowseForFile( this, null, UiControls.EFileExtension.Csv, FileDialogMode.Open, UiControls.EInitialFolder.None );

            if (fn != null)
            {
                _lstAdducts.Items.Add( new NamedItem<string>( fn, fn ) );
            }
        }

        private void _btnReconfigure_Click( object sender, EventArgs e )
        {
            _mnuDebug.Show( _btnReconfigure, new Point( _btnReconfigure.Width, 0 ), ToolStripDropDownDirection.AboveLeft );
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
            _lstCompounds.Items.AddRange( _lstAvailCompounds.Items.Cast<CompoundLibrary>().ToArray() );
            UpdateAvailableCompoundsList();
            UpdateAutoIdentifyButton();
        }

        private void _chkAutoIdentify_CheckedChanged( object sender, EventArgs e )
        {
            _numTolerance.Enabled = _lblTolerance.Enabled = _lstTolerance.Enabled = (_chkAutoIdentify.Checked || _chkPeakPeakMatch.Checked);
            _cbAutomaticFlag.Enabled = ctlLabel1.Enabled = _chkAutoIdentify.Checked;
        }

        private void _txtDataSetData_TextChanged( object sender, EventArgs e )
        {

        }

        private void _lblDataSetData_Click( object sender, EventArgs e )
        {

        }

        private void _btnShowFf_Click( object sender, EventArgs e )
        {
            FrmViewSpreadsheet.Show( this, _btnShowFf.Tag as string, _fileLoadInfo );
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
            Browse( _txtIdentifications );
        }
    }
}
