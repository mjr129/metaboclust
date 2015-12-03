﻿using System;
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

namespace MetaboliteLevels.Forms.Startup
{
    /// <summary>
    /// Edits: DataFileNames OR selects the session to load THEN calls FrmDataLoad.
    /// </summary>
    public partial class FrmDataLoadQuery : Form
    {
        private Core _result;
        private bool _ignoreChanges; // feedback loop prevention 
        private readonly CtlWizard _wizard;
        private bool _historyDelete; // Why the context menu is being shown (load | delete)
        private string CondInfoFileName { get { return _chkCondInfo.Checked ? _txtCondInfo.Text : null; } }
        private string _typeCacheFileName;

        private readonly ConditionBox<int> _cbExp;
        private readonly ConditionBox<int> _cbControl;

        private readonly List<CompoundLibrary> _compoundLibraries;
        private readonly List<NamedItem<string>> _adductLibraries;


        private Dictionary<int, string> _typeCacheNames = new Dictionary<int, string>();
        private readonly HashSet<int> _typeCacheIds = new HashSet<int>();
        private readonly ToolStripMenuItem _mnuBrowseWorkspace;
        private readonly ToolStripSeparator _mnuBrowseWorkspaceSep;

        /// <summary>
        /// Constructor.
        /// </summary>
        private FrmDataLoadQuery()
        {
            InitializeComponent();
            Text = UiControls.Title;
            UiControls.SetIcon(this);

            _lbl32Bit.Visible = IntPtr.Size != 8;

            // Setup wizard
            _wizard = CtlWizard.BindNew(tabControl1.Parent, tabControl1, CtlWizardOptions.ShowCancel | CtlWizardOptions.HandleBasicChanges);
            _wizard.Pager.PageTitles[0] = UiControls.Title;
            _wizard.HelpClicked += _btnHelp_LinkClicked;
            _wizard.CancelClicked += _wizard_CancelClicked;
            _wizard.OkClicked += _wizard_OkClicked;
            _wizard.PermitAdvance += _wizard_PermitAdvance;
            _wizard.Pager.PageChanged += Pager_PageChanged;

            // Setup exp. group boxes
            _cbControl = CreateExpConditionBox(_txtControls, _btnBrowseContCond);
            _cbExp = CreateExpConditionBox(_txtExps, _btnBrowseExpCond);

            // Setup help
            splitContainer1.Panel2Collapsed = true;
            SetFocusTooltips(this);

            // Setup captions
            linkLabel1.Text = UiControls.Title + " " + UiControls.VersionString.ToString();
            label13.Text = UiControls.Description;

            // Populate LC-MS modes
            EnumComboBox.Populate(_lstLcmsMode, ELcmsMode.None, true);

            // Populate CompoundLibrary's
            FrmDataLoad.GetCompoundLibraries(out _compoundLibraries, out _adductLibraries);

            if (_compoundLibraries.Count == 0)
            {
                ReplaceWithMessage(_lstAvailCompounds, _btnAddCompound);
            }

            if (_adductLibraries.Count == 0)
            {
                ReplaceWithMessage(_lstAvailableAdducts, _btnAddAdduct);
            }

            UpdateAvailableCompoundsList();
            UpdateAvailableAdductsList();

            // Add recent entries menu
            var recentWorkspaces = MainSettings.Instance.RecentWorkspaces;
            var lfn = recentWorkspaces.Count != 0 ? recentWorkspaces.Last() : new DataFileNames();
            LoadDataFileNames(lfn);

            // Workspaces
            ToolStripMenuItem tsmi;

            foreach (var entry in recentWorkspaces.Reverse<DataFileNames>())
            {
                tsmi = new ToolStripMenuItem
                {
                    Text = (_cmsRecentWorkspaces.Items.Count + 1).ToString() + ". " + entry.GetDescription(),
                    Tag = entry
                };

                tsmi.Click += tsmi_Click;

                _cmsRecentWorkspaces.Items.Add(tsmi);
            }

            _mnuBrowseWorkspaceSep = new ToolStripSeparator();
            _cmsRecentWorkspaces.Items.Add(_mnuBrowseWorkspaceSep);
            _mnuBrowseWorkspace = new ToolStripMenuItem("Browse...");
            _mnuBrowseWorkspace.Click += _mnuBrowseWorkspace_Click;
            _cmsRecentWorkspaces.Items.Add(_mnuBrowseWorkspace);

            _btnDeleteWorkspace.Visible = recentWorkspaces.Count != 0;

            // Sessions
            tsmi = new ToolStripMenuItem
            {
                Text = "Browse...",
                Tag = null,
                Image = Resources.MnuOpen
            };

            tsmi.Click += tsmi2_Click;
            _cmsRecentSessions.Items.Add(tsmi);

            _cmsRecentSessions.Items.Add(new ToolStripSeparator());

            var recentSessions = MainSettings.Instance.RecentSessions;

            foreach (var entry in recentSessions.Reverse<MainSettings.RecentSession>())
            {
                tsmi = new ToolStripMenuItem();

                tsmi.Text = (_cmsRecentSessions.Items.Count - 1).ToString() + ". " + (string.IsNullOrWhiteSpace(entry.Title) ? "Untitled" : entry.Title);
                tsmi.ToolTipText = entry.FileName;
                tsmi.Tag = entry;
                tsmi.Click += tsmi2_Click;

                _cmsRecentSessions.Items.Add(tsmi);
            }

            if (recentSessions.Count == 0)
            {
                _btnReturnToSession.Text += "...";
                _btnMostRecent.Visible = false;
            }
            else
            {
                var mostRecent = recentSessions[recentSessions.Count - 1];
                _btnMostRecent.Text = "    " + mostRecent.Title;
                _tipSideBar.SetToolTip(_btnMostRecent, mostRecent.FileName);
                _btnMostRecent.Tag = mostRecent.FileName;
            }

            UiControls.CompensateForVisualStyles(this);

            if (!Application.RenderWithVisualStyles)
            {
                foreach (var l in UiControls.EnumerateControls<Label>(this))
                {
                    if (l.ForeColor == Color.CornflowerBlue)
                    {
                        l.BackColor = Color.FromArgb(255, 255, 192);
                        l.ForeColor = Color.DarkBlue;
                        l.Padding = new Padding(8, 8, 8, 8);
                        l.BorderStyle = BorderStyle.Fixed3D;
                    }
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            int x = Math.Max(0, (ClientSize.Width - 1024)) / 3;
            int y = Math.Max(0, (ClientSize.Height - 768)) / 3;

            splitContainer1.Margin = new Padding(x, y, x, y);
        }

        private void ReplaceWithMessage(ListBox list, Button btn)
        {
            var tlp = (TableLayoutPanel)list.Parent;
            var pos = tlp.GetCellPosition(list);

            Label lab = new Label();
            lab.Text = "There are no available libraires.\r\nPlease choose the library manually or reconfigure your library path.";
            lab.Dock = DockStyle.Fill;
            lab.TextAlign = ContentAlignment.MiddleCenter;
            lab.Visible = true;
            list.Visible = false;
            btn.Enabled = false;

            tlp.Controls.Add(lab, pos.Column, pos.Row);
        }

        void _mnuBrowseWorkspace_Click(object sender, EventArgs e)
        {
            string fn = UiControls.BrowseForFile(this, null, UiControls.EFileExtension.Sessions, FileDialogMode.Open, UiControls.EInitialFolder.Sessions);

            if (fn != null)
            {
                Core core = FrmWait.Show<Core, string>(this, "Retreiving settings", "Retrieving settings from the selected file", XmlSettings.LoadFromFile<Core>, fn);

                if (core == null || core.FileNames == null)
                {
                    FrmMsgBox.ShowError(this, "Failed to retrieve the filenames. Try opening the file to check for errors first.");
                    return;
                }

                LoadDataFileNames(core.FileNames);
            }
        }

        void Pager_PageChanged(object sender, EventArgs e)
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

        void _wizard_CancelClicked(object sender, CancelEventArgs e)
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

        bool _wizard_PermitAdvance(int input)
        {
            switch (input)
            {
                case 0:
                    return true;

                case 1:
                    return _txtTitle.Text.Length != 0;

                case 2:
                    return _lstLcmsMode.SelectedIndex != -1
                           && File.Exists(_txtDataSetData.Text)
                           && File.Exists(_txtDataSetObs.Text)
                           && File.Exists(_txtDataSetVar.Text)
                           && (!_chkAltVals.Checked || File.Exists(_txtAltVals.Text))
                           && (!_chkCondInfo.Checked || File.Exists(_txtCondInfo.Text));

                case 3:
                    return !_chkStatT.Checked
                           || (_cbExp.SelectedItems != null && _cbControl.SelectedItems != null);

                case 4:
                    return true;

                case 5:
                    return (!_chkIdentifications.Checked || File.Exists(_txtIdentifications.Text));

                case 6:
                    return true;

                default:
                    return false;
            }
        }

        private void SetFocusTooltips(Control t)
        {
            foreach (Control c in t.Controls)
            {
                SetFocusTooltips(c);
            }

            string tt = _tipSideBar.GetToolTip(t);

            if (string.IsNullOrWhiteSpace(tt))
            {
                _tipSideBar.SetToolTip(t, "*");
            }

            t.GotFocus += t_GotFocus;
        }

        void t_GotFocus(object sender, EventArgs e)
        {
            ShowControlHelp((Control)sender);
        }

        private void LoadDataFileNames(DataFileNames lfn)
        {
            _txtTitle.Text = lfn.Title;

            _txtDataSetData.Text = lfn.Data;
            _txtDataSetObs.Text = lfn.ObservationInfo;
            _txtDataSetVar.Text = lfn.PeakInfo;
            _lstCompounds.Items.Clear();

            if (lfn.CompoundLibraies != null)
            {
                foreach (CompoundLibrary cl in lfn.CompoundLibraies)
                {
                    if (_compoundLibraries.Any(z => z.ContentsMatch(cl)))
                    {
                        _lstCompounds.Items.Add(_compoundLibraries.Find(z => z.ContentsMatch(cl)));
                    }
                    else
                    {
                        _lstCompounds.Items.Add(cl);
                    }
                }
            }

            _lstAdducts.Items.Clear();

            if (lfn.AdductLibraries != null)
            {
                foreach (string item in lfn.AdductLibraries)
                {
                    if (_adductLibraries.Any(z => z.Value == item))
                    {
                        _lstAdducts.Items.Add(_adductLibraries.Find(z => z.Value == item));
                    }
                    else
                    {
                        _lstAdducts.Items.Add(new NamedItem<string>(item, item));
                    }
                }
            }

            SetText(_txtIdentifications, _chkIdentifications, lfn.Identifications);
            SetText(_txtAltVals, _chkAltVals, lfn.AltData);
            SetText(_txtCondInfo, _chkCondInfo, lfn.ConditionInfo);
            SetCheck(_chkStatT, lfn.StandardStatisticalMethods, EStatisticalMethods.TTest);
            SetCheck(_chkStatP, lfn.StandardStatisticalMethods, EStatisticalMethods.Pearson);

            if (!string.IsNullOrWhiteSpace(lfn.Data))
            {
                EnumComboBox.Set(_lstLcmsMode, lfn.LcmsMode, true);

                _cbExp.SelectedItems = (lfn.ConditionsOfInterest);
                _cbControl.SelectedItems = (lfn.ControlConditions);
            }
            else
            {
                EnumComboBox.Clear(_lstLcmsMode);
                _txtExps.Text = "";
                _txtControls.Text = "";
            }

            UpdateAvailableCompoundsList();
            UpdateAvailableAdductsList();
        }

        private void SetCheck(CheckBox cb, EStatisticalMethods current, EStatisticalMethods toCheck)
        {
            cb.Checked = current.HasFlag(toCheck);
        }

        void tsmi_Click(object sender, EventArgs e)
        {
            var s = (ToolStripMenuItem)sender;
            var fn = (DataFileNames)s.Tag;

            if (_historyDelete)
            {
                if (FrmMsgBox.ShowYesNo(this, "Delete from history", "Are you sure you wish to remove the following settings from the history:\r\n\r\n    " + fn.Title, null))
                {
                    MainSettings.Instance.RecentWorkspaces.Remove(fn);
                    MainSettings.Instance.Save();
                    s.Enabled = false;
                    s.Font = UiControls.strikeFont;
                }
            }
            else
            {
                LoadDataFileNames(fn);
            }
        }

        void tsmi2_Click(object sender, EventArgs e)
        {
            var rs = ((MainSettings.RecentSession)((ToolStripMenuItem)sender).Tag);

            LoadSession(rs == null ? null : rs.FileName);
        }

        private void SetText(TextBox txt, CheckBox chk, string current)
        {
            _ignoreChanges = true;

            if (!string.IsNullOrEmpty(current))
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

        internal static Core Show(Form owner)
        {
            using (FrmDataLoadQuery frm = new FrmDataLoadQuery())
            {
                if (UiControls.ShowWithDim(owner, frm) == DialogResult.OK)
                {
                    return frm._result;
                }
            }

            return null;
        }

        internal static bool Browse(TextBox textBox, string filter = "Comma separated value (*.csv)|*.csv|All files (*.*)|*.*", bool useDirectory = false)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (useDirectory)
                {
                    ofd.FileName = Path.Combine(textBox.Text, "SELECT DIRECTORY");

                    if (!string.IsNullOrEmpty(textBox.Text) && Directory.Exists(textBox.Text))
                    {
                        ofd.InitialDirectory = Path.GetDirectoryName(textBox.Text);
                    }
                }
                else
                {
                    ofd.FileName = textBox.Text;

                    if (!string.IsNullOrEmpty(textBox.Text) && Directory.Exists(Path.GetDirectoryName(textBox.Text)))
                    {
                        ofd.InitialDirectory = Path.GetDirectoryName(textBox.Text);
                    }
                }

                ofd.Filter = filter;

                if (UiControls.ShowWithDim(textBox.FindForm(), ofd) == DialogResult.OK)
                {
                    if (useDirectory)
                    {
                        textBox.Text = Path.GetDirectoryName(ofd.FileName);
                    }
                    else
                    {
                        textBox.Text = ofd.FileName;
                    }

                    return true;
                }

                return false;
            }
        }

        private void _wizard_OkClicked(object sender, EventArgs e)
        {
            var fileNames = new DataFileNames();

            try
            {
                fileNames.Title = _txtTitle.Text;
                fileNames.LcmsMode = EnumComboBox.Get<ELcmsMode>(_lstLcmsMode);
                fileNames.Data = _txtDataSetData.Text;
                fileNames.ObservationInfo = _txtDataSetObs.Text;
                fileNames.PeakInfo = _txtDataSetVar.Text;
                fileNames.CompoundLibraies = _lstCompounds.Items.Cast<CompoundLibrary>().ToList();
                fileNames.Identifications = _chkIdentifications.Checked ? _txtIdentifications.Text : null;
                fileNames.AdductLibraries = _lstAdducts.Items.Cast<NamedItem<string>>().Select(z => z.Value).ToList();
                fileNames.AutomaticIdentifications = _chkAutoIdentify.Checked;
                fileNames.Session = null;
                fileNames.AltData = _chkAltVals.Checked ? _txtAltVals.Text : null;
                fileNames.ConditionInfo = CondInfoFileName;
                fileNames.ConditionsOfInterest = new List<int>(_cbExp.GetSelectedItemsE());
                fileNames.ControlConditions = new List<int>(_cbControl.GetSelectedItemsE());
                fileNames.StandardStatisticalMethods = EStatisticalMethods.None;
                fileNames.StandardStatisticalMethods = GetCheck(_chkStatT, fileNames.StandardStatisticalMethods, EStatisticalMethods.TTest);
                fileNames.StandardStatisticalMethods = GetCheck(_chkStatP, fileNames.StandardStatisticalMethods, EStatisticalMethods.Pearson);
            }
            catch (Exception ex)
            {
                FrmMsgBox.ShowError(this, "Input error: " + ex.Message);
                return;
            }

            // Save the workspace (even if there is an error)
            MainSettings.Instance.AddRecentWorkspace(fileNames);
            MainSettings.Instance.Save();

            // Load the data
            _result = FrmDataLoad.Show(this, fileNames);

            if (_result == null)
            {
                return;
            }

            if (_chkAlarm.Checked)
            {
                NativeMethods.Beep(3000, 200);
                Thread.Sleep(50);
                NativeMethods.Beep(3000, 200);
                Thread.Sleep(500);
            }

            DialogResult = DialogResult.OK;
        }

        private EStatisticalMethods GetCheck(CheckBox cb, EStatisticalMethods current, EStatisticalMethods added)
        {
            if (cb.Checked)
            {
                current |= added;
            }

            return current;
        }

        #region Browse Buttons

        private void _btnDataSet_Click(object sender, EventArgs e)
        {
            if (Browse(_txtDataSetData))
            {
                TryAutoSet(_txtDataSetData.Text, _txtDataSetObs, "Info.csv", "ObsInfo.csv", "Observations.csv", "*.jgf");
                TryAutoSet(_txtDataSetData.Text, _txtDataSetVar, "VarInfo.csv", "PeakInfo.csv", "Peaks.csv", "Variables.csv");
            }
        }

        private void TryAutoSet(string firstFileName, TextBox dst, params string[] possibleFileNames)
        {
            if (dst.TextLength == 0)
            {
                foreach (string s in possibleFileNames)
                {
                    string s2 = s.Replace("*", Path.GetFileNameWithoutExtension(firstFileName));
                    s2 = Path.Combine(Path.GetDirectoryName(firstFileName), s2);

                    if (File.Exists(s2))
                    {
                        dst.Text = s2;
                        return;
                    }
                }
            }
        }

        private void _btnDataSetObs_Click(object sender, EventArgs e)
        {
            Browse(_txtDataSetObs);
        }

        private void _btnDataSetVar_Click(object sender, EventArgs e)
        {
            Browse(_txtDataSetVar);
        }

        private void _btnIdentifications_Click(object sender, EventArgs e)
        {
            _lstCompounds.Items.AddRange(_lstAvailCompounds.SelectedItems.Cast<CompoundLibrary>().ToArray());
            UpdateAvailableCompoundsList();
            UpdateAutoIdentifyButton();
        }

        private void UpdateAvailableCompoundsList()
        {
            _lstAvailCompounds.Items.Clear();

            foreach (var cl in _compoundLibraries)
            {
                if (!_lstCompounds.Items.Contains(cl))
                {
                    _lstAvailCompounds.Items.Add(cl);
                }
            }
        }

        private void UpdateAvailableAdductsList()
        {
            _lstAvailableAdducts.Items.Clear();

            foreach (var cl in _adductLibraries)
            {
                if (!_lstAdducts.Items.Contains(cl))
                {
                    _lstAvailableAdducts.Items.Add(cl);
                }
            }
        }

        private void _btnAltVals_Click(object sender, EventArgs e)
        {
            Browse(_txtAltVals);
        }

        private void _btnCondInfo_Click(object sender, EventArgs e)
        {
            Browse(_txtCondInfo);
        }

        #endregion

        private void CheckTheBox(CheckBox cb, TextBox tb, Button bn)
        {
            tb.Enabled = cb.Checked;
            bn.Enabled = cb.Checked;

            if (cb.Checked && !_ignoreChanges && tb.TextLength == 0)
            {
                bn.PerformClick();
            }
        }

        private void _chkIdentifications_CheckedChanged(object sender, EventArgs e)
        {
            CheckTheBox(_chkIdentifications, _txtIdentifications, _btnIdentifications);
        }

        private void UpdateAutoIdentifyButton()
        {
            var e = EnumComboBox.Get(_lstLcmsMode, ELcmsMode.None);
            _chkAutoIdentify.Enabled = _lstCompounds.Items.Count != 0 && _lstAdducts.Items.Count != 0 && e != ELcmsMode.None;
            _chkAutoIdentify.Checked = _chkAutoIdentify.Enabled;
        }

        private void _chkAltVals_CheckedChanged(object sender, EventArgs e)
        {
            CheckTheBox(_chkAltVals, _txtAltVals, _btnAltVals);
        }

        private void _chkCondInfo_CheckedChanged(object sender, EventArgs e)
        {
            CheckTheBox(_chkCondInfo, _txtCondInfo, _btnCondInfo);
        }

        private void _btnHelp_LinkClicked(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
            _wizard.HelpText = splitContainer1.Panel2Collapsed ? "Show help" : "Hide help";
        }

        private void _btnDeleteWorkspace_Click(object sender, EventArgs e)
        {
            _historyDelete = true;
            _cmsRecentWorkspaces.Show(_btnDeleteWorkspace, 0, _btnDeleteWorkspace.Height);
        }

        private void _btnRecent_Click(object sender, EventArgs e)
        {
            _historyDelete = false;
            _cmsRecentWorkspaces.Show(_btnRecent, 0, _btnRecent.Height);
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            Control c = e.AssociatedControl;

            ShowControlHelp(c);

            e.Cancel = true;
        }

        private void ShowControlHelp(Control c)
        {
            string txt = _tipSideBar.GetToolTip(c);

            if (txt.StartsWith("*"))
            {
                if (txt.Length == 1)
                {
                    txt = "Hover the mouse over a control to view help";
                    textBox1.Visible = false;
                }
                else
                {
                    textBox1.Text = txt.Substring(1);
                    txt = UiControls.GetManText(txt.Substring(1));
                    textBox1.Visible = true;
                }
            }
            else
            {
                textBox1.Visible = false;
            }

            _txtHelp.Text = txt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MainSettings.Instance.RecentSessions.Count == 0)
            {
                BrowseForSession();
            }
            else
            {
                _cmsRecentSessions.Show(_btnReturnToSession, 0, _btnReturnToSession.Height);
            }
        }

        private void BrowseForSession()
        {
            string fileName = this.BrowseForFile(null, UiControls.EFileExtension.Sessions, FileDialogMode.Open, UiControls.EInitialFolder.Sessions);

            if (fileName != null)
            {
                LoadSession(fileName);
            }
        }

        private void LoadSession(string fn)
        {
            if (string.IsNullOrWhiteSpace(fn))
            {
                BrowseForSession();
                return;
            }

            _result = FrmDataLoad.Show(this, fn);

            if (_result == null)
            {
                FrmMsgBox.ShowError(this, UiControls.GetManText("Unable to load session"));
                return;
            }

            if (_result.FileNames == null)
            {
                _result.FileNames = new DataFileNames();
            }

            // Loaded ok!
            MainSettings.Instance.AddRecentSession(fn, _result.FileNames.Title);
            MainSettings.Instance.Save();

            if (_result.FileNames.AppVersion == null)
            {
                _result.FileNames.AppVersion = new Version();
            }

            if (_result.FileNames.AppVersion != UiControls.Version)
            {
                if (!FrmOldData.Show(this, _result.FileNames))
                {
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void exploreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "\"" + UiControls.StartupPath + "\"");
        }

        private void clearRPathrequiresRestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmMsgBox.ShowYesNo(this, "Restore Settings", "Restore settings to defaults and restart program?", Resources.MsgWarning))
            {
                UiControls.RestartProgram();
            }
        }               

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            _mnuBrowseWorkspace.Visible = !_historyDelete;
            _mnuBrowseWorkspaceSep.Visible = !_historyDelete;
        }

        private void _btnMostRecent_Click(object sender, EventArgs e)
        {
            LoadSession((string)_btnMostRecent.Tag);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _wizard.Page += 1;
        }

        private void _lstLcmsMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAutoIdentifyButton();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mnuDebug.Show(linkLabel1, new Point(0, 0), ToolStripDropDownDirection.AboveRight);
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiControls.RestartProgram();
        }

        public void UpdateCacheOfTypes()
        {
            string newFile = _txtCondInfo.Text;

            if (_typeCacheFileName != newFile)
            {
                _typeCacheIds.Clear();

                try
                {
                    _typeCacheFileName = newFile;

                    if (File.Exists(newFile))
                    {
                        _typeCacheNames = FrmDataLoad.LoadConditionInfo(newFile);
                    }
                    else
                    {
                        _typeCacheNames.Clear();
                    }

                    _typeCacheIds.AddRange(_typeCacheNames.Keys);
                }
                catch
                {
                    _typeCacheNames.Clear();
                }
            }
        }

        private ConditionBox<int> CreateExpConditionBox(TextBox textBox, Button button)
        {
            return new ListValueSet<int>()
            {
                Title = "Experimental Conditions",
                List = new TypeCacheIdsWrapper(this),
                Namer = ConditionBox_Namer,
                Describer = ConditionBox_Describer,
                Retriever = ConditionBox_Retriever,
            }.CreateConditionBox(textBox, button);
        }

        /// <summary>
        /// Wraps the list of type IDs to make a check to update the list from the
        /// users choices whenever the list is enumerated (e.g. by the options list).
        /// </summary>
        private class TypeCacheIdsWrapper : IEnumerable<int>
        {
            private FrmDataLoadQuery _owner;

            public TypeCacheIdsWrapper(FrmDataLoadQuery owner)
            {
                _owner = owner;
            }

            public IEnumerator<int> GetEnumerator()
            {
                _owner.UpdateCacheOfTypes();
                return _owner._typeCacheIds.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private bool ConditionBox_Retriever(string name, out int item)
        {
            if (name.StartsWith("TYPE_"))
            {
                return int.TryParse(name.Substring(5), out item);
            }

            return int.TryParse(name, out item);
        }

        private string ConditionBox_Describer(int item)
        {
            string name;

            if (_typeCacheNames.TryGetValue(item, out name))
            {
                return "Type: " + name + ", ID = " + item;
            }

            return "Type: Unnamed, ID = " + item;
        }

        private string ConditionBox_Namer(int item)
        {
            string name;

            if (_typeCacheNames.TryGetValue(item, out name))
            {
                return name;
            }

            return "Type_" + item;
        }

        private void _chkStatT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadDataFileNames(new DataFileNames());
        }

        private void resetdoNotShowAgainMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmMsgBox.ShowYesNo(this, "Restore Settings", "Clear \"do not show again\" choices and restart program?.", Resources.MsgWarning))
            {
                MainSettings.Instance.DoNotShowAgain.Clear();
                MainSettings.Instance.Save();
                UiControls.RestartProgram();
            }
        }

        private void ctlButton2_Click(object sender, EventArgs e)
        {
            RemoveSelected(_lstCompounds);
            UpdateAvailableCompoundsList();
            UpdateAutoIdentifyButton();
        }

        private void RemoveSelected(ListBox list)
        {
            int i = list.SelectedIndex;

            if (i != -1)
            {
                list.Items.RemoveAt(i);

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

        private void _btnAddCompoundLibrary_Click(object sender, EventArgs e)
        {
            CompoundLibrary sel = FrmSelectCompounds.Show(this);

            if (sel != null)
            {
                _lstCompounds.Items.Add(sel);

                UpdateAutoIdentifyButton();
            }
        }

        private void _btnAddAdduct_Click(object sender, EventArgs e)
        {
            _lstAdducts.Items.AddRange(_lstAvailableAdducts.SelectedItems.Cast<NamedItem<string>>().ToArray());
            UpdateAvailableAdductsList();
            UpdateAutoIdentifyButton();
        }

        private void ctlButton3_Click(object sender, EventArgs e)
        {
            RemoveSelected(_lstAdducts);
            UpdateAvailableAdductsList();
            UpdateAutoIdentifyButton();
        }

        private void _btnBrowseAdducts_Click(object sender, EventArgs e)
        {
            string fn = UiControls.BrowseForFile(this, null, UiControls.EFileExtension.Csv, FileDialogMode.Open, UiControls.EInitialFolder.None);

            if (fn != null)
            {
                _lstAdducts.Items.Add(new NamedItem<string>(fn, fn));
            }
        }

        private void _btnReconfigure_Click(object sender, EventArgs e)
        {
            _mnuDebug.Show(_btnReconfigure, new Point(_btnReconfigure.Width, 0), ToolStripDropDownDirection.AboveLeft);
        }

        private void editPathsAndLibrariesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmInitialSetup.Show(this, true))
            {
                UiControls.RestartProgram();
            }
        }

        private void _btnAddAllCompounds_Click(object sender, EventArgs e)
        {
            _lstCompounds.Items.AddRange(_lstAvailCompounds.Items.Cast<CompoundLibrary>().ToArray());
            UpdateAvailableCompoundsList();
            UpdateAutoIdentifyButton();
        }
    }
}
