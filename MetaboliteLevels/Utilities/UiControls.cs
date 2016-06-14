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
using MGui;
using MGui.Datatypes;
using MGui.Helpers;
using MetaboliteLevels.Forms.Startup;
using MGui.Controls;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using MetaboliteLevels.Data.Visualisables;

namespace MetaboliteLevels.Utilities
{
    /// <summary>
    /// Miscellaneous functions.
    /// </summary>
    internal static class UiControls
    {
        // Dictionaries
        public static Dictionary<Version, string> VersionHistory;
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

        public static readonly Color TitleBackColour = Color.White; // Color.FromKnownColor(KnownColor.ActiveCaption);
        public static readonly Color TitleForeColour = Color.Purple; // Color.FromKnownColor(KnownColor.ActiveCaptionText);
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

        public static bool IsValid(this double d)
        {
            return !double.IsNaN(d) && !double.IsInfinity(d);
        }

        /// <summary>
        /// Initialises this class.
        /// REQUIRED.
        /// </summary>             
        internal static void Initialise(Font font)
        {
            FontHelper.Initialise(font);
            VersionHistory = new Dictionary<Version, string>();
            Version currentVersion = null;
            StringBuilder sb = new StringBuilder();

            string mrsn = "MetaboliteLevels.VersionHistory.txt";
            var mrs = Assembly.GetCallingAssembly().GetManifestResourceStream(mrsn );

            if (mrs == null)
            {
                throw new InvalidOperationException( "Failed to retrieve the manifest resource stream: " + mrsn + "." );
            }

            using (StreamReader sr = new StreamReader( mrs ))
            {
                while (!sr.EndOfStream)
                {
                    string l = sr.ReadLine();

                    if (l.StartsWith( "VERSION " ))
                    {
                        if (currentVersion != null)
                        {
                            VersionHistory.Add( currentVersion, sb.ToString().Trim() );
                            sb.Clear();
                        }

                        string[] e = l.Substring( 8 ).Split( '.', ',' );
                        currentVersion = new Version( int.Parse( e[0] ), int.Parse( e[1] ), int.Parse( e[2] ), int.Parse( e[3] ) );
                    }
                    else
                    {
                        sb.AppendLine( l );
                    }
                }

                if (currentVersion != null)
                {
                    VersionHistory.Add( currentVersion, sb.ToString().Trim() );
                    sb.Clear();
                }
            }
                
            Random = new Random();
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

        public static Bitmap RecolourImage( Image source )
        {
            return RecolourImage( source, TitleForeColour );
        }

        public static Bitmap RecolourImage( Image source, Color colour )
        {
            if (source == null)
            {
                return null;
            }

            Bitmap result = new Bitmap( source );
            Rectangle entirity = new Rectangle( 0, 0, result.Width, result.Height );

            BitmapData bmpData = result.LockBits( entirity, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb );

            int strideInInts = bmpData.Stride / sizeof( UInt32 );
            int sizeInInts = bmpData.Height * strideInInts;
            Int32[] data = new Int32[sizeInInts];

            // Marshal to avoid unsafe code...
            Marshal.Copy( bmpData.Scan0, data, 0, sizeInInts );

            int dc = colour.ToArgb() & 0x00FFFFFF;

            for (int y = 0; y < bmpData.Height; y++)
            {
                for (int x = 0; x < bmpData.Width; x++)
                {
                    int i = x + (y * strideInInts);

                    int c = data[i];

                    c |= dc;

                    data[i] = c;
                }
            }
            
            Marshal.Copy( data, 0, bmpData.Scan0, data.Length );

            result.UnlockBits( bmpData );

            return result;
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
        /// Like Path.GetFilename but doesn't error for invalid characters.
        /// </summary>                                                     
        public static string GetFileName( string key )
        {
            int last = key.LastIndexOf( '\\' );

            if (last == -1)
            {
                return key;
            }

            return key.Substring( last + 1 );
        }

        /// <summary>
        /// Like Path.GetDirectory but doesn't error for invalid characters.
        /// </summary>                                                     
        public static string GetDirectory( string key )
        {
            int last = key.LastIndexOf( '\\' );

            if (last == -1)
            {
                return string.Empty;
            }

            return key.Substring( 0, last );
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

        internal static void DrawHBar( Graphics graphics, Control control )
        {
            const int h = 3;
            Rectangle r = new Rectangle( 0, control.Height - h, control.Width, h );

            using (Brush b = new LinearGradientBrush( r, UiControls.TitleBackColour, UiControls.TitleForeColour,180, true ))
            {
                graphics.FillRectangle( b, r );
            }
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
            foreach (Control control in FormHelper.EnumerateControls(form, ignore))
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
        internal static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }

                throw new InvalidOperationException("Assert failed: " + (message ?? "No details provided"));
            }
        }

        internal static Image GetImage(ImageListOrder v, bool bold)
        {
            switch (v)
            {
                case ImageListOrder.Adduct: return bold ? Resources.IconAdduct : Resources.ListIconAdduct;
                case ImageListOrder.Compound: return bold ? Resources.IconCompound : Resources.ListIconCompoundWithAnnotation;
                case ImageListOrder.CompoundU: return bold ? Resources.IconCompound : Resources.ListIconCompound;
                case ImageListOrder.Info: return bold ? Resources.IconInformation : Resources.ListIconInformation;
                case ImageListOrder.InfoU: return bold ? Resources.ListIconInformation : Resources.ListIconInformationMeta; // No large
                case ImageListOrder.List: return bold ? Resources.IconList : Resources.IconList; // No large
                case ImageListOrder.Pathway: return bold ? Resources.IconPathway : Resources.ListIconPathway;
                case ImageListOrder.Cluster: return bold ? Resources.IconCluster : Resources.ListIconCluster;
                case ImageListOrder.ClusterU: return bold ? Resources.ListIconCluster : Resources.ListIconClusterInsignificant; // No large
                case ImageListOrder.Variable: return bold ? Resources.ListIconPeakWithAnnotation : Resources.ListIconPeakWithAnnotation;
                case ImageListOrder.VariableU: return bold ? Resources.IconPeak : Resources.ListIconPeak;
                case ImageListOrder.Line: return bold ? Resources.IconLine : Resources.IconLine; // No large
                case ImageListOrder.Assignment: return bold ? Resources.IconVector : Resources.ListIconVector;
                case ImageListOrder.Warning: return bold ? Resources.MnuWarning : Resources.MnuWarning;
                case ImageListOrder.TestFull: return bold ? Resources.ListIconTestFull : Resources.ListIconTestFull;
                case ImageListOrder.TestEmpty: return bold ? Resources.ListIconTestEmpty : Resources.ListIconTestEmpty;
                case ImageListOrder.Filter: return bold ? Resources.MnuFilter : Resources.MnuFilter;
                case ImageListOrder.Statistic: return bold ? Resources.IconStatistics : Resources.ListIconStatistics;
                case ImageListOrder.Point: return bold ? Resources.IconPoint : Resources.IconPoint; // No large
                case ImageListOrder.File: return bold ? Resources.MnuFile : Resources.MnuFile;
                case ImageListOrder.ListSortUp: return bold ? Resources.ListIconSortUp : Resources.ListIconSortUp;
                case ImageListOrder.ListSortDown: return bold ? Resources.ListIconSortDown : Resources.ListIconSortDown;
                case ImageListOrder.ListFilter: return bold ? Resources.ListIconFilter : Resources.ListIconFilter;
                case ImageListOrder.ScriptInbuilt: return bold ? Resources.IconBinary : Resources.IconBinary;
                case ImageListOrder.ScriptFile: return bold ? Resources.IconR : Resources.IconR;
                case ImageListOrder.ScriptMathDotNet: return bold ? Resources.IconMathNet : Resources.IconMathNet;
                case ImageListOrder.Group: return bold ? Resources.MnuGroup : Resources.MnuGroup;
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
            ScriptMathDotNet,
            Group,
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
        /// Sets the forms icon to the main application icon.
        /// This is called on all forms.
        /// </summary>                  
        internal static void SetIcon(Form frm)
        {
            frm.Icon = Resources.MainIcon;   
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
        internal static void RestartProgram(Form owner)
        {
            StartProcess( owner, Application.ExecutablePath );
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
        /// Creates a color1 image with a color2 border.
        /// </summary>
        internal static Image CreateExperimentalGroupImage(bool isChecked, GroupInfoBase ti, bool large)
        {
            var color1 = ti.Colour;
            var color2 = ti.ColourLight;

            if (!isChecked)
            {
                color1 = Color.FromArgb(32, color1.R, color1.G, color1.B);
                color2 = Color.FromArgb(32, color1.R, color1.G, color1.B);
            }

            Color colour = ti.Colour;
            Color colourLight = ti.ColourLight;

            if (!isChecked)
            {
                colour = ColourHelper.Blend( colour, Color.Transparent, 0.75 );
                colourLight = ColourHelper.Blend( colourLight, Color.Transparent, 0.75 );
            }

            Bitmap result = RecolourImage( large ? Resources.IconGroups : Resources.MnuGroup, colour );

            string text = ti.DisplayShortName;
            if (!string.IsNullOrEmpty( text ) || !isChecked)
            {
                using (Graphics g = Graphics.FromImage( result ))
                {
                    if (!string.IsNullOrEmpty( text ))
                    {
                        string txt = text.Substring( 0, 1 );
                        var sz = g.MeasureString( txt, FontHelper.SmallBoldFont );

                        using (Brush b = new SolidBrush( colourLight ))
                        {
                            g.DrawString( txt, FontHelper.SmallBoldFont, Brushes.White, result.Width / 2 - sz.Width / 2, result.Height / 2 );
                        }
                    }

                    if (!isChecked)
                    {
                        using (Pen p = new Pen( TitleForeColour, 2.5f ))
                        {
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.DrawLine( p, 0, 0, result.Width, result.Height );
                            g.DrawLine( p, result.Width, 0, 0, result.Height );
                        }
                    }
                }
            }

            return result;
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

                if (!string.IsNullOrEmpty(text))
                {
                    string txt = text.Substring(0, 1);
                    var sz = g.MeasureString(txt, FontHelper.BoldFont);

                    g.DrawString(txt, FontHelper.BoldFont, Brushes.White, bmp.Width / 2 - sz.Width / 2, bmp.Height / 2 - sz.Height / 2);
                }
            }

            return bmp;
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
        /// Shows the about dialogue.
        /// </summary>                  
        internal static void ShowAbout( Form owner )
        {
            AboutForm.Show(
                owner,
                Assembly.GetExecutingAssembly(),
                "Additional icons made by Freepik, Google, Appzgear, Vectorgraphit from www.flaticon.com are licensed by CC BY 3.0.",
                "https://bitbucket.org/mjr129/metabolitelevels"
                );
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

            return ColourHelper.Blend(Color.Green, Color.Red, pct);
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
            FrmInputMultiLine.ShowFixed(owner, UiControls.Title, "Current session information", Path.GetFileName(fileNames.Session), fileNames.GetDetails());
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

        public static void DrawWatermark(Bitmap bmp, Core core, string watermark)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                string txt = core.CoreGuid.ToString() + "\r\n" + core.FileNames.Title + "\r\n" + watermark;
                g.DrawString(txt.ToUpper(), FontHelper.TinyRegularFont, Brushes.Silver, 0, 0);
            }
        }

        internal static bool StartProcess( Form owner, string path )
        {
            try
            {
                Process.Start( path );
                return true;
            }
            catch (Exception ex)
            {
                FrmMsgBox.ShowError( owner, path, ex );
                return false;
            }
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
}
