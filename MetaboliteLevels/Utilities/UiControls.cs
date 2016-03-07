// #define ENABLE_DIMMER

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MetaboliteLevels.Data.DataInfo;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Forms.Generic;
using MetaboliteLevels.Forms.Editing;
using MetaboliteLevels.Algorithms.Statistics.Statistics;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.General;
using System.Diagnostics;
using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Forms.Algorithms;
using Microsoft.Win32;
using MetaboliteLevels.Forms;

namespace MetaboliteLevels.Utilities
{
    /// <summary>
    /// Miscellaneous functions.
    /// </summary>
    internal static class UiControls
    {
        // Dictionaries
        public static Dictionary<Version, string> BreakingVersions;
        public static Dictionary<int, string> ColourNames;
        public static int ColourIndex;

        // Random numbers
        public static Random Random;

        // A zero-width space.
        // Yes it is really there! Don't delete it.
        public const string ZEROSPACE = "​";
        private const string STARTUPPATH_LOCAL = "reroute.txt";
        private const string STARTUPPATH_REGKEY = "Software\\MetaboliteLevels";
        private const string STARTUPPATH_REGVALUE = "WorkingDirectory";

        // Where the application stores its data.
        private static string __startupPath;
        private static EStartupPath __startupPathMode;

        public static readonly Color BackColour = Color.FromArgb(153, 180, 209); // Color.FromKnownColor(KnownColor.ActiveCaption);
        public static readonly Color ForeColour = Color.Black; // Color.FromKnownColor(KnownColor.ActiveCaptionText);
        public static readonly Color PreviewBackColour = Color.LightSteelBlue; // Color.FromKnownColor(KnownColor.ActiveCaption);
        public static readonly Color PreviewForeColour = Color.Black; // Color.FromKnownColor(KnownColor.ActiveCaptionText);

        public static readonly Color[] BrightColours =
            {
                Color.FromArgb(0,255,0),
                Color.FromArgb(255,0,0),
                Color.FromArgb(192,128,0),
                Color.FromArgb(255,0,192),
                Color.FromArgb(128,0,255),
                Color.FromArgb(0,128,255),
                Color.FromArgb(128,128,255),
                Color.FromArgb(255,128,128),
                Color.FromArgb(128,192,128),
                Color.FromArgb(128,128,128),
                Color.FromArgb(128,0,128),
                Color.FromArgb(128,128,0),
                Color.FromArgb(0,128,128),
                Color.FromArgb(0,0,255),
            };



        /// <summary>
        /// Initialises this class.
        /// REQUIRED.
        /// </summary>             
        internal static void Initialise(Font font)
        {
            FontHelper.Initialise(font);
            BreakingVersions = new Dictionary<Version, string>();
            BreakingVersions.Add(new Version(1, 0, 0, 4203), "Refactoring.");
            Random = new Random();
        }

        /// <summary>
        /// (MJR) Shows an error provider on a control, using the application default position.
        /// </summary>                                                                         
        public static void ShowError(this ErrorProvider self, Control control, string text)
        {
            self.SetError(control, text);
            self.SetIconAlignment(control, ErrorIconAlignment.MiddleLeft);
        }

        /// <summary>
        /// Adds a caption after the last item in the menu.
        /// </summary>                                     
        public static ToolStripLabel AddMenuCaptionFilename(ToolStripDropDownMenu destination, string fileName)
        {
            ToolStripLabel tsl = AddMenuCaption(destination, Path.GetFileName(fileName) + " (EXPLORE)");

            tsl.Tag = fileName;
            tsl.Text = Path.GetFileName(fileName); // Needed to set text with " (explore)" originally to get correct size
            tsl.MouseEnter += Tsl_MouseEnter;
            tsl.MouseLeave += Tsl_MouseLeave;
            tsl.ToolTipText = fileName + "\r\nClick to show in Windows Explorer";
            tsl.Click += Tsl_Click;

            return tsl;
        }

        public static ToolStripLabel AddMenuCaption(ToolStripDropDownMenu destination, string text)
        {
            ToolStripLabel tsl = new ToolStripLabel()
            {
                Text = text,
                Font = FontHelper.SmallRegularFont,
                ForeColor = Color.SteelBlue,
                Margin = new Padding(24, 0, 8, 8),
                Visible = true,
                LinkBehavior = LinkBehavior.HoverUnderline,
                LinkColor = Color.SteelBlue,
                IsLink = true,
                TextAlign = ContentAlignment.TopLeft,
            };

            tsl.AutoSize = false;
            tsl.Size = tsl.GetPreferredSize(Size.Empty);

            destination.Items.Add(tsl);
            return tsl;
        }

        private static void Tsl_MouseLeave(object sender, EventArgs e)
        {
            ToolStripLabel tsl = (ToolStripLabel)sender;

            tsl.Text = Path.GetFileName((string)tsl.Tag);
        }

        private static void Tsl_MouseEnter(object sender, EventArgs e)
        {
            ToolStripLabel tsl = (ToolStripLabel)sender;

            tsl.Text = Path.GetFileName((string)tsl.Tag) + " (EXPLORE)";
        }

        private static void Tsl_Click(object sender, EventArgs e)
        {
            ToolStripLabel tsl = (ToolStripLabel)sender;

            string fileName = (string)tsl.Tag;
            Form form = tsl.GetCurrentParent().FindForm();

            if (File.Exists(fileName))
            {
                ExploreTo(form, fileName);
            }
            else
            {
                string directory = Path.GetDirectoryName(fileName);

                if (Directory.Exists(directory))
                {
                    Explore(form, directory);
                }
                else
                {
                    FrmMsgBox.ShowError(form, $"Neither the file \"{Path.GetFileName(fileName)}\" nor the directory \"{directory}\" can be found.");
                }
            }
        }

        /// <summary>
        /// Gets the application title.
        /// </summary>                 
        public static string Title
        {
            get
            {
                var t = (AssemblyTitleAttribute)typeof(UiControls).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
                return t.Title;
            }
        }
        /// <summary>
        /// Gets the application desceription.
        /// </summary>          
        public static string Description
        {
            get
            {
                var t = (AssemblyDescriptionAttribute)typeof(UiControls).Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0];
                return t.Description;
            }
        }

        /// <summary>
        /// Gets the application version.
        /// </summary>          
        public static Version Version
        {
            get
            {
                return typeof(UiControls).Assembly.GetName().Version;
            }
        }

        /// <summary>
        /// Gets the application version, as a string, including whether it is a debug build.
        /// </summary>          
        public static string VersionString
        {
            get
            {
                return Version.ToString() + (IsDebug ? " (DEBUG BUILD)" : ".R");
            }
        }

        /// <summary>
        /// (MJR)
        /// </summary>
        internal static Dictionary<T, int> CreateIndexLookup<T>(this IEnumerable<T> self)
        {
            Dictionary<T, int> result = new Dictionary<T, int>();
            int n = 0;

            foreach (T t in self)
            {
                result.Add(t, n);
                ++n;
            }

            return result;
        }

        /// <summary>
        /// Converts a colour to its name.
        /// </summary>                    
        public static string ColourToName(Color colour)
        {
            if (colour.IsNamedColor)
            {
                return colour.Name;
            }

            if (ColourNames == null)
            {
                ColourNames = new Dictionary<int, string>();

                foreach (KnownColor kc in Enum.GetValues(typeof(KnownColor)))
                {
                    Color c = Color.FromKnownColor(kc);
                    ColourNames[c.ToArgb()] = c.Name;
                }
            }

            string name;
            if (ColourNames.TryGetValue(colour.ToArgb(), out name))
            {
                return name;
            }

            return colour.R.ToString() + ", " + colour.G + ", " + colour.B;
        }

        /// <summary>
        /// Sets the properties of an object to their [DefaultAttribute] value.
        /// </summary>                                                         
        public static void ApplyDefaultsFromAttributes(object obj)
        {
            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                var attr = property.GetCustomAttribute<DefaultValueAttribute>(true);

                if (attr != null)
                {
                    if (property.PropertyType == typeof(ParseElementCollection))
                    {
                        property.SetValue(obj, new ParseElementCollection(attr.Value.ToString()));
                    }
                    else
                    {
                        property.SetValue(obj, attr.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Shows a form, dimming the form behind it.
        /// </summary>                               
        public static DialogResult ShowWithDim(Form owner, dynamic form)
        {
#if ENABLE_DIMMER
            if (!owner.Visible)
            {
                return form.ShowDialog(owner);
            }

            DialogResult result;

            using (var dimmer = CreateDimmer(owner))
            {
                result = form.ShowDialog(dimmer);
            }

            owner.Focus();

            return result;
#else
            return form.ShowDialog(owner);
#endif
        }

#if ENABLE_DIMMER
        /// <summary>
        /// Creates the overlay that dims a form.
        /// </summary>  
        public static Form CreateDimmer(Form owner)
        {
            Form dimmer = new Form
            {
                BackColor = Color.DarkGray,
                Opacity = 0.50,
                FormBorderStyle = FormBorderStyle.None,
                ControlBox = false,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                AutoScaleMode = AutoScaleMode.None,
                Location = owner.Location,
                Size = owner.Size
            };

            dimmer.Show(owner);

            return dimmer;
        }
#endif

        /// <summary>
        /// Makes the form read-only by disabling text-boxes, etc.
        /// </summary>                                            
        internal static void MakeReadOnly(Control form, Control ignore = null)
        {
            foreach (Control control in UiControls.EnumerateControls(form, ignore))
            {
                TextBox textBox = control as TextBox;

                if (textBox != null)
                {
                    textBox.ReadOnly = true;
                    continue;
                }

                Button button = control as Button;

                if (button != null)
                {
                    if (button.DialogResult == DialogResult.Cancel)
                    {
                        button.Text = "Close";
                    }
                    else if (button.DialogResult == DialogResult.OK)
                    {
                        button.Visible = false;
                    }
                    else
                    {
                        button.Enabled = false;
                    }

                    continue;
                }

                CheckBox checkBox = control as CheckBox;

                if (checkBox != null)
                {
                    checkBox.AutoCheck = false;
                    continue;
                }

                RadioButton radioButton = control as RadioButton;

                if (radioButton != null)
                {
                    radioButton.AutoCheck = false;
                    continue;
                }

                ComboBox comboBox = control as ComboBox;

                if (comboBox != null)
                {
                    comboBox.Enabled = false;
                    continue;
                }
            }
        }

        /// <summary>
        /// Asserts the condition is true (regardless of build).
        /// </summary>                             
        internal static void Assert(bool p, string message = null)
        {
            if (!p)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }

                throw new Exception("Assert failed: " + (message ?? "No details provided"));
            }
        }

        internal static Image GetImage(ImageListOrder v, bool bold)
        {
            switch (v)
            {
                case ImageListOrder.Adduct: return bold ? Resources.ObjLAdduct : Resources.ObjAdduct;
                case ImageListOrder.Compound: return bold ? Resources.ObjLCompound : Resources.ObjCompound;
                case ImageListOrder.CompoundU: return bold ? Resources.ObjLCompoundU : Resources.ObjCompoundU;
                case ImageListOrder.Info: return bold ? Resources.ObjLInfo : Resources.ObjInfo;
                case ImageListOrder.InfoU: return bold ? Resources.ObjInfo : Resources.ObjInfoU; // No large
                case ImageListOrder.List: return bold ? Resources.ObjList : Resources.ObjList; // No large
                case ImageListOrder.Pathway: return bold ? Resources.ObjLPathway : Resources.ObjPathway;
                case ImageListOrder.Cluster: return bold ? Resources.ObjLCluster : Resources.ObjCluster;
                case ImageListOrder.ClusterU: return bold ? Resources.ObjCluster : Resources.ObjClusterU; // No large
                case ImageListOrder.Variable: return bold ? Resources.ObjVariable : Resources.ObjVariable;
                case ImageListOrder.VariableU: return bold ? Resources.ObjLVariableU : Resources.ObjVariableU;
                case ImageListOrder.Line: return bold ? Resources.ObjLine : Resources.ObjLine; // No large
                case ImageListOrder.Assignment: return bold ? Resources.ObjLAssignment : Resources.ObjAssignment;
                case ImageListOrder.Warning: return bold ? Resources.MnuWarning : Resources.MnuWarning;
                case ImageListOrder.TestFull: return bold ? Resources.TestFull : Resources.TestFull;
                case ImageListOrder.TestEmpty: return bold ? Resources.TestEmpty : Resources.TestEmpty;
                case ImageListOrder.Filter: return bold ? Resources.SmallObjFilter : Resources.SmallObjFilter;
                case ImageListOrder.Statistic: return bold ? Resources.ObjLStatistics : Resources.ObjStatistics;
                case ImageListOrder.Point: return bold ? Resources.ObjPoint : Resources.ObjPoint; // No large
                case ImageListOrder.File: return bold ? Resources.MnuFile : Resources.MnuFile;
                case ImageListOrder.ListSortUp: return bold ? Resources.ListSortUp : Resources.ListSortUp;
                case ImageListOrder.ListSortDown: return bold ? Resources.ListSortDown : Resources.ListSortDown;
                case ImageListOrder.ListFilter: return bold ? Resources.ListFilter : Resources.ListFilter;
                case ImageListOrder.ScriptInbuilt: return bold ? Resources.MnuDisable : Resources.MnuDisable;
                case ImageListOrder.ScriptFile: return bold ? Resources.MnuFile : Resources.MnuFile;
                default: throw new SwitchException(v);
            }
        }

        /// <summary>
        /// Order of images in the standard image list (see PopulateImageList).
        /// </summary>
        public enum ImageListOrder : int
        {
            Adduct,
            Compound,
            CompoundU,
            Info,
            InfoU,
            List,
            Pathway,
            Cluster,
            ClusterU,
            Variable,
            VariableU,
            Line,
            Assignment,
            Warning,
            TestFull,
            TestEmpty,
            Filter,
            Statistic,
            Point,
            File,
            ListSortUp,
            ListSortDown,
            ListFilter,
            ScriptInbuilt,
            ScriptFile,
        }

        /// <summary>
        /// Creates an image list with the common images (see ImageListOrder). 
        /// </summary>                                            
        internal static void PopulateImageList(ImageList il)
        {
            foreach (ImageListOrder n in Enum.GetValues(typeof(ImageListOrder)).Cast<ImageListOrder>().OrderBy(z => z))
            {
                il.Images.Add(n.ToString(), GetImage(n, false));
            }
        }

        /// <summary>
        /// Returns true in the designer.
        /// </summary>
        public static bool IsDesigning
        {
            get
            {
                return FontHelper.RegularFont == null;
            }
        }

        /// <summary>
        /// Returns true in a debug build.
        /// </summary>
        public static bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// Gets the manual text for the [topic].
        /// </summary>                           
        internal static string GetManText(string topic)
        {
            StringBuilder sb = new StringBuilder();
            bool reading = false;
            string manFile = Path.Combine(Application.StartupPath, "Manual.dat");
            topic = topic.ToUpper();

            if (!File.Exists(manFile))
            {
                throw new FileNotFoundException("The manual file appears to be missing:\r\n" + manFile, manFile);
            }

            using (StreamReader sr = new StreamReader(manFile))
            {
                while (!sr.EndOfStream)
                {
                    string l = sr.ReadLine();

                    if (l.ToUpper() == "[" + topic + "]")
                    {
                        reading = true;
                    }
                    else if (reading)
                    {
                        if (l.StartsWith("["))
                        {
                            break;
                        }

                        sb.AppendLine(l);
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Sets the forms icon to the main application icon.
        /// This is called on all forms.
        /// </summary>                  
        internal static void SetIcon(Form frm)
        {
            //frm.Icon = Resources.MetaboliteExplorer;
            frm.Icon = Resources.MainIcon2;
            //frm.Font = new Font("Segoe UI", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// Stops the application looking weird if VisualStyles are off.
        /// This is called on all forms but must be done AFTER control creation.
        /// </summary>                 
        internal static void CompensateForVisualStyles(Form frm)
        {
            if (!Application.RenderWithVisualStyles)
            {
                EnumerateControls<Button>(frm, CompensateForVisualStyles);
            }
        }

        /// <summary>
        /// Used by [CompensateForVisualStyles(Form)].
        /// </summary>                               
        private static void CompensateForVisualStyles(Button obj)
        {
            if (obj.FlatStyle == FlatStyle.Standard || obj.FlatStyle == FlatStyle.System)
            {
                obj.UseVisualStyleBackColor = false;
                obj.BackColor = Color.CornflowerBlue;
                obj.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// Disposes [d] if not null.
        /// </summary>               
        internal static void Dispose(IDisposable d)
        {
            if (d != null)
            {
                d.Dispose();
            }
        }

        /// <summary>
        /// Returns a new colour in sequence.
        /// </summary>                       
        internal static Color NextColour()
        {
            return Maths.BasicColours[ColourIndex++ % Maths.BasicColours.Length];
        }

        /// <summary>
        /// File extensions.
        /// </summary>
        public enum EFileExtension
        {
            //[Name("Sessions=mdat|MS-NRBF sessions=mdat-bin|Sesialised binary sessions=mdat-mbin|Serialied text format sessions=.mdat-tbin|Serialised fast binary sessions=mdat-fbin|Serialised compact binary sessions=mdat-cbin|XML sessions=mdat-xml|Data contact sessions=mdat-con")]
            [Name("All session types|Session files=mdat|MS-NRBF sessions=mdat-bin|Serialised sessions / binary=mdat-mbin|Serialied sessions / text=mdat-txt|Serialised sessions / fast binary=mdat-fbin|Serialised sessions / compact binary=mdat-cbin")]
            Sessions,

            //[Name("Results=mres|MS-NRBF results=mres-bin|Sesialised binary results=mres-mbin|Serialied text format results=.mres-tbin|Serialised fast binary sessions=mres-fbin|Serialised compact binary results=mres-cbin|XML results=mres-xml|Data contact results=mres-con")]
            [Name("All result types|Result files=mres|MS-NRBF results=mres-mbin|Serialised results / text=mres-txt|Serialised results / fast binary=mres-fbin|Serialised results / compact binary=mres-cbin")]
            EvaluationResults,

            [Name("Comma separated value files=csv|Text files=txt")]
            Csv,

            [Name("R scripts=r|Text files=txt")]
            RScript,

            [Name("Supported image files|Portable network graphics=png|Enhanced meta files=emf")]
            PngOrEmf,

            [Name("R data files=rdata")]
            RData
        }

        /// <summary>
        /// Application data folders.
        /// </summary>
        public enum EInitialFolder
        {
            None,

            [Name("Exported data")]
            ExportedData,

            [Name("Scripts\\Statistics")]
            FOLDER_STATISTICS,

            [Name("Scripts\\Metrics")]
            FOLDER_METRICS,

            [Name("Scripts\\Trends")]
            FOLDER_TRENDS,

            [Name("Scripts\\Clustering")]
            FOLDER_CLUSTERERS,

            [Name("Scripts\\Corrections")]
            FOLDER_CORRECTIONS,

            [Name("Compound databases")]
            CompondDatabases,

            [Name("Saved images")]
            SavedImages,

            [Name("Sessions")]
            Sessions,

            [Name("Evaluations")]
            Evaluations,

            [Name("Temporary")]
            Temporary,
        }

        /// <summary>
        /// Shows the browse file dialogue.
        /// </summary>                     
        public static string BrowseForFile(this Form owner, string fileName, EFileExtension extension, FileDialogMode mode, EInitialFolder initialFolder)
        {
            if (mode == FileDialogMode.Save)
            {
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    return fileName;
                }
                else
                {
                    mode = FileDialogMode.SaveAs;
                }
            }

            using (FileDialog ofd = ((mode == FileDialogMode.Open) ? (FileDialog)new OpenFileDialog() : (FileDialog)new SaveFileDialog()))
            {
                return Browse(owner, ofd, fileName, extension, initialFolder);
            }
        }

        /// <summary>
        /// Used by [BrowseForFile].
        /// </summary>                     
        private static string Browse(Form owner, FileDialog fd, string fileName, EFileExtension extension, EInitialFolder initialFolder)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                fd.InitialDirectory = Path.GetDirectoryName(fileName);
                fd.FileName = fileName;
            }
            else if (initialFolder != EInitialFolder.None)
            {
                string initialFolderName = GetOrCreateFixedFolder(initialFolder);
                fd.InitialDirectory = initialFolderName;
            }

            string finalFilterPrefix = null;
            string allFilters = "";
            string finalFilter = "";
            string filterFormat = extension.ToUiString();

            string[] filters = filterFormat.Split('|');

            foreach (string filter in filters)
            {
                string left;
                string right;

                if (StringHelper.SplitEquals(filter, out left, out right))
                {
                    if (finalFilter.Length != 0)
                    {
                        finalFilter += "|";
                    }

                    finalFilter += left + " (*." + right + ")|*." + right;

                    if (allFilters.Length != 0)
                    {
                        allFilters += ";";
                    }

                    allFilters += "*." + right;
                }
                else
                {
                    finalFilterPrefix = left;
                }
            }

            if (fd is OpenFileDialog)
            {
                if (finalFilterPrefix != null)
                {
                    finalFilterPrefix += " (" + allFilters + ")|" + allFilters;

                    if (finalFilter.Length != 0)
                    {
                        finalFilterPrefix += "|";
                    }

                    finalFilter = finalFilterPrefix + finalFilter;
                }

                finalFilter += "|All files (*.*)|*.*";
            }

            fd.Filter = finalFilter;

            if (ShowWithDim(owner, fd) == DialogResult.OK)
            {
                return fd.FileName;
            }

            return null;
        }

        /// <summary>
        /// Fixed application folders
        /// </summary>
        public static string GetOrCreateFixedFolder(EInitialFolder folderId)
        {
            string folder = Path.Combine(UiControls.StartupPath, "UserData", folderId.ToUiString());

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return folder;
        }



        /// <summary>
        /// (MJR) Shows a ContextMenuStrip at under [sender] if sender is a control, otherwise at the current mouse cursor position.
        /// </summary>
        internal static void ShowDropDown(this ContextMenuStrip self, object sender)
        {
            var c = sender as Control;

            if (c != null)
            {
                self.Show(c, 0, c.Height);
            }
            else
            {
                self.Show(Cursor.Position);
            }
        }

        /// <summary>
        /// Restarts the program.
        /// </summary>           
        internal static void RestartProgram()
        {
            Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        /// <summary>
        /// Invokes the object's constructor on itself, potentially initialising the object.
        /// Used in "OnDeserializing" methods to emulate XmlSerializer behaviour in [BinaryFormatter].
        /// </summary>
        internal static void InvokeConstructor(object self)
        {
            var constructor = self.GetType().GetConstructor(new Type[0]);

            constructor.Invoke(self, new object[0]);
        }

        /// <summary>
        /// (MJR) Converts a WinForms DialogResult to a WPF style boolean.
        /// </summary>
        public static bool? ToBoolean(this DialogResult self)
        {
            switch (self)
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                    return true;

                case DialogResult.Cancel:
                    return null;

                case DialogResult.No:
                    return false;

                default:
                    throw new InvalidOperationException("ToBool: " + self);
            }
        }

        /// <summary>
        /// Gets a sequentially numbered file.
        /// </summary>                        
        internal static string GetNewFile(string fileName, string format = "-{0}", bool checkOriginal = false)
        {
            int n = 0;

            string prefix = Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName));
            string suffix = Path.GetExtension(fileName);
            string result;

            if (checkOriginal)
            {
                if (!File.Exists(fileName))
                {
                    return fileName;
                }
            }

            do
            {
                n++;
                result = prefix + string.Format(format, n) + suffix;
            } while (File.Exists(result));

            return result;
        }



        internal static string GetTemporaryFile(string suffixAndExtension, Guid guid)
        {
            string dir = GetOrCreateFixedFolder(EInitialFolder.Temporary);
            string fn = guid.ToString() + suffixAndExtension;

            return Path.Combine(dir, fn);
        }

        /// <summary>
        /// Converts an image to grayscale.
        /// </summary>
        public static Bitmap MakeGrayScale(Image orig)
        {
            Bitmap r = new Bitmap(orig.Width, orig.Height);

            using (Graphics g = Graphics.FromImage(r))
            {
                // Grayscale
                ColorMatrix colorMatrix = new ColorMatrix(
                  new[]
                  { //            R,     G,   B, A, x
                     new[] {.3f, .3f, .3f, 0, 0},  // R
                     new[] {.3f, .3f, .3f, 0, 0},  // G
                     new[] {.3f, .3f, .3f, 0, 0},  // B
                     new float[] {0, 0, 0, 1, 0},        // A
                     new float[] {0, 0, 0, 0, 1}         // +
                  });

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);

                // Draw
                Rectangle ro = new Rectangle(0, 0, orig.Width, orig.Height);
                g.DrawImage(orig, ro, 0, 0, orig.Width, orig.Height, GraphicsUnit.Pixel, attributes);
            }

            return r;
        }

        public static Bitmap Inset(Image orig, Image inset, int width = 0, int height = 0)
        {
            Bitmap result = new Bitmap(orig);

            if (width == 0) width = inset.Width;
            if (height == 0) height = inset.Height;

            using (Graphics g = Graphics.FromImage(result))
            {
                Rectangle r = new Rectangle(result.Width - width, result.Height - height, width, height);

                g.DrawImage(inset, r);
            }

            return result;
        }

        /// <summary>
        /// Puts a crossout icon on an image.
        /// </summary>
        public static Bitmap Crossout(Image orig)
        {
            Bitmap result = new Bitmap(orig);

            using (Graphics g = Graphics.FromImage(result))
            {
                Rectangle r = new Rectangle(result.Width - 8, result.Height - 8, 8, 8);

                using (Pen p = new Pen(Color.FromArgb(255, 255, 255), 4))
                {
                    g.DrawLine(p, r.Left, r.Top, r.Right, r.Bottom);
                    g.DrawLine(p, r.Right, r.Top, r.Left, r.Bottom);
                }

                using (Pen p = new Pen(Color.FromArgb(0, 0, 0), 2))
                {
                    g.DrawLine(p, r.Left, r.Top, r.Right, r.Bottom);
                    g.DrawLine(p, r.Right, r.Top, r.Left, r.Bottom);
                }
            }

            return result;
        }


        /// <summary>
        /// Enumerates all controls within [ctrl] of type [T].
        /// </summary>
        internal static IEnumerable<Control> EnumerateControls(Control ctrl, Control ignore)
        {
            if (ctrl == ignore)
            {
                yield break;
            }

            foreach (Control ctrl2 in ctrl.Controls)
            {
                foreach (Control ctrl3 in EnumerateControls(ctrl2, ignore))
                {
                    yield return ctrl3;
                }
            }

            yield return ctrl;
        }

        /// <summary>
        /// Enumerates all controls within [ctrl] of type [T].
        /// </summary>
        internal static IEnumerable<T> EnumerateControls<T>(Control ctrl)
             where T : class
        {
            foreach (Control ctrl2 in ctrl.Controls)
            {
                foreach (T ctrl3 in EnumerateControls<T>(ctrl2))
                {
                    yield return ctrl3;
                }
            }

            T t = ctrl as T;

            if (t != null)
            {
                yield return t;
            }
        }

        /// <summary>
        /// Enumerates all controls within [ctrl] of type [T] and calls [action] on them.
        /// </summary>
        internal static void EnumerateControls<T>(Control ctrl, Action<T> action)
            where T : class
        {
            foreach (Control ctrl2 in ctrl.Controls)
            {
                EnumerateControls<T>(ctrl2, action);
            }

            T t = ctrl as T;

            if (t != null)
            {
                action(t);
            }
        }

        /// <summary>
        /// Creates a color1 image with a color2 border.
        /// </summary>
        internal static Image CreateSolidColourImage(bool isChecked, GroupInfoBase ti)
        {
            var color1 = ti.Colour;
            var color2 = ti.ColourLight;

            if (!isChecked)
            {
                color1 = Color.FromArgb(32, color1.R, color1.G, color1.B);
                color2 = Color.FromArgb(32, color1.R, color1.G, color1.B);
            }

            return CreateSolidColourImage(ti.DisplayShortName, color2, color1);
        }

        /// <summary>
        /// Creates an icon for a type named [text].
        /// </summary>
        internal static Image CreateSolidColourImage(string text, Color colorLight, Color color1)
        {
            Bitmap bmp = new Bitmap(20, 20);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(colorLight);

                using (Pen p = new Pen(color1))
                {
                    g.DrawRectangle(p, 0, 0, bmp.Width - 1, bmp.Height - 1);
                }

                using (Brush b = new SolidBrush(color1))
                {
                    g.FillRectangle(b, 4, 4, bmp.Width - 8, bmp.Height - 8);
                }

                if (text.Length != 0)
                {
                    string txt = text.Substring(0, 1);
                    var sz = g.MeasureString(txt, FontHelper.BoldFont);

                    g.DrawString(txt, FontHelper.BoldFont, Brushes.White, bmp.Width / 2 - sz.Width / 2, bmp.Height / 2 - sz.Height / 2);
                }
            }

            return bmp;
        }

        /// <summary>
        /// Blends two colours.
        /// </summary>
        internal static Color Blend(Color colorA, Color colorB, double amountA)
        {
            if (double.IsNaN(amountA) || double.IsInfinity(amountA))
            {
                // Error
                return Color.Pink;
            }
            else if (amountA < 0)
            {
                // Error
                return Color.Cyan;
            }
            else if (amountA > 1)
            {
                // Error
                return Color.Magenta;
            }

            return Color.FromArgb(Blend(colorA.A, colorB.A, amountA),
                                  Blend(colorA.R, colorB.R, amountA),
                                  Blend(colorA.G, colorB.G, amountA),
                                  Blend(colorA.B, colorB.B, amountA));
        }

        /// <summary>
        /// Blends two bytes.
        /// </summary>
        private static int Blend(byte byteA, byte byteB, double amountA)
        {
            return (int)(byteA + (byteB - byteA) * amountA);
        }

        /// <summary>
        /// Returns the trend message text.
        /// </summary>
        internal static string GetDefaultTrendMessage(Core core)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("The currently selected trend function is:");
            sb.AppendLine(" * " + core.AvgSmoother.ToString());

            return sb.ToString();
        }

        /// <summary>
        /// Browses for a folder.
        /// </summary>
        internal static string BrowseForFolder(Form owner, string path)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.InitialDirectory = path;
                sfd.FileName = "Select Directory";
                sfd.Filter = "Directories|*.*";
                sfd.OverwritePrompt = false;
                sfd.Title = "Select Directory";

                if (UiControls.ShowWithDim(owner, sfd) == DialogResult.OK)
                {
                    return Path.GetDirectoryName(sfd.FileName);
                }

                return null;
            }
        }

        /// <summary>
        /// (MJR) Attempts to get the contents of an enumerable of WeakReferences.
        /// </summary>
        /// <param name="self">The weak references</param>
        /// <param name="strong">All strong references successfully obtained, regardless of result</param>
        /// <returns>true if all elements extracted successfully, false otherwise</returns>
        internal static bool TryGetStrong<T>(this IEnumerable<WeakReference<T>> self, out List<T> strong)
            where T : class
        {
            List<T> result = new List<T>();
            bool success = true;

            foreach (WeakReference<T> refr in self)
            {
                T t;

                if (refr.TryGetTarget(out t))
                {
                    result.Add(t);
                }
                else
                {
                    success = false;
                }
            }

            strong = result;
            return success;
        }

        /// <summary>
        /// Gets the colour of a statistic.
        /// </summary>
        internal static Color StatisticColour(ConfigurationStatistic key, Dictionary<ConfigurationStatistic, double> values)
        {
            double value;

            if (!values.TryGetValue(key, out value))
            {
                return Color.Black;
            }

            return StatisticColour(value, key.Results.Min, key.Results.Max);
        }

        /// <summary>
        /// Gets the colour of a statistic.
        /// </summary>
        private static Color StatisticColour(double value, double min, double max)
        {
            if (double.IsNaN(value))
            {
                return Color.Black;
            }

            double pct = (value - min) / (max - min);

            return Blend(Color.Green, Color.Red, pct);
        }

        public enum EStartupPath
        {
            None,
            Local,
            User,
            Machine
        }

        public static EStartupPath StartupPathMode
        {
            get
            {
                if (__startupPath == null)
                {
                    FindStartupPath();
                }

                return __startupPathMode;
            }
        }

        /// <summary>
        ///Gets the application startuppath. 
        /// </summary>
        public static string StartupPath
        {
            get
            {
                if (__startupPath == null)
                {
                    FindStartupPath();
                }

                return __startupPath;
            }
        }

        /// <summary>
        /// Sets the startuppath variable.
        /// </summary>
        private static void FindStartupPath()
        {
            string rerouteFile = Path.Combine(Application.StartupPath, STARTUPPATH_LOCAL);

            if (File.Exists(rerouteFile))
            {
                __startupPath = File.ReadAllText(rerouteFile).Trim();
                __startupPathMode = EStartupPath.Local;
            }
            else
            {
                __startupPath = Registry.GetValue(Registry.CurrentUser.Name + "\\" + STARTUPPATH_REGKEY, STARTUPPATH_REGVALUE, null) as string;

                if (__startupPath != null)
                {
                    __startupPathMode = EStartupPath.User;
                }
                else
                {
                    __startupPath = Registry.GetValue(Registry.LocalMachine.Name + "\\" + STARTUPPATH_REGKEY, STARTUPPATH_REGVALUE, null) as string;

                    if (__startupPath != null)
                    {
                        __startupPathMode = EStartupPath.Machine;
                    }
                    else
                    {
                        __startupPath = Application.StartupPath;
                        __startupPathMode = EStartupPath.None;
                    }
                }
            }
        }

        internal static void ShowSessionInfo(Form owner, DataFileNames fileNames)
        {
            FrmInputLarge.ShowFixed(owner, UiControls.Title, "Current session information", Path.GetFileName(fileNames.Session), fileNames.GetDetails());
        }

        /// <summary>
        /// Opens the file browser with the specified file in focus.
        /// </summary>                                              
        internal static void ExploreTo(Form owner, string fileName)
        {
            try
            {
                Process.Start("explorer.exe", "/select,\"" + fileName + "\"");
            }
            catch (Exception ex)
            {
                FrmMsgBox.ShowError(owner, ex);
            }
        }

        /// <summary>
        /// Opens the file browser with the specified directory
        /// </summary>                                              
        internal static void Explore(Form owner, string fileName)
        {
            try
            {
                Process.Start("explorer.exe", "\"" + fileName + "\"");
            }
            catch (Exception ex)
            {
                FrmMsgBox.ShowError(owner, ex);
            }
        }

        /// <summary>
        /// Deletes a registry key, if it exists.
        /// </summary>                           
        private static void DeleteRegistryValue(RegistryKey root, string keyName, string valueName)
        {
            using (RegistryKey key = root.OpenSubKey(keyName, true))
            {
                if (key != null)
                {
                    key.DeleteValue(valueName);
                }
            }
        }

        /// <summary>
        /// Sets the startup path (REQUIRES APPLICATION RESTART).
        /// </summary>                                           
        public static void SetStartupPath(string text, EStartupPath mode)
        {
            // Delete existing
            string rerouteFile = Path.Combine(Application.StartupPath, STARTUPPATH_LOCAL);
            DeleteRegistryValue(Registry.CurrentUser, STARTUPPATH_REGKEY, STARTUPPATH_REGVALUE);
            DeleteRegistryValue(Registry.LocalMachine, STARTUPPATH_REGKEY, STARTUPPATH_REGVALUE);

            if (File.Exists(rerouteFile))
            {
                File.Delete(rerouteFile);
            }

            // Write new
            switch (mode)
            {
                case EStartupPath.Local:
                    File.WriteAllText(rerouteFile, text);
                    break;

                case EStartupPath.User:
                    Registry.SetValue(Registry.CurrentUser.Name + "\\" + STARTUPPATH_REGKEY, STARTUPPATH_REGVALUE, text, RegistryValueKind.String);
                    break;

                case EStartupPath.Machine:
                    Registry.SetValue(Registry.LocalMachine.Name + "\\" + STARTUPPATH_REGKEY, STARTUPPATH_REGVALUE, text, RegistryValueKind.String);
                    break;

                case EStartupPath.None:
                    break;

                default:
                    throw new SwitchException(mode);
            }
        }

        internal static void ShowAbout(Form owner)
        {
            FrmMsgBox.ShowInfo(owner, "About " + UiControls.Title, UiControls.GetManText("copyright").Replace("{productname}", UiControls.Title).Replace("{version}", UiControls.VersionString));
        }

        public static void DrawWatermark(Bitmap bmp, Core core, string watermark)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                string txt = core.CoreGuid.ToString() + "\r\n" + core.FileNames.Title + "\r\n" + watermark;
                g.DrawString(txt.ToUpper(), FontHelper.TinyRegularFont, Brushes.Silver, 0, 0);
            }
        }

        /// <summary>
        /// (EXTENSION) (MJR) Returns the object as a string, or an empty string if the object is null
        /// </summary>                                                                                
        public static string ToStringSafe<T>(this T self)
            where T : class
        {
            if (self == null)
            {
                return string.Empty;
            }

            return self.ToString();
        }
    }

    /// <summary>
    /// File dialog modes
    /// </summary>
    enum FileDialogMode
    {
        Open,
        Save,
        SaveAs,
    }

    /// <summary>
    /// Name attributes, for giving names to enum members.
    /// ([ComponentModel.DisplayNameAttribute] unfortunately doesn't work on enums)
    /// </summary>
    class NameAttribute : Attribute
    {
        public string Name { get; set; }

        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}
