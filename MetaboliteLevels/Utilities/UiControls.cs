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

namespace MetaboliteLevels.Utilities
{
    /// <summary>
    /// Miscellaneous functions.
    /// </summary>
    static class UiControls
    {
        // Fonts
        internal static Font normalFont;
        internal static Font largeFont;
        internal static Font boldFont;
        internal static Font italicFont;
        internal static Font strikeFont;
        internal static Font largeBoldFont;

        // Dictionaries
        internal static Dictionary<Version, string> BreakingVersions;
        internal static Dictionary<int, string> ColourNames;
        internal static int ColourIndex;

        // Random numbers
        public static Random Random;

        // A zero-width space
        // Yes it is really there!
        public const string ZEROSPACE = "​";
        
        private static string _startupPath;

       

        public static void ShowError(this ErrorProvider self, Control control, string text)
        {
            self.SetError(control, text);
            self.SetIconAlignment(control, ErrorIconAlignment.MiddleLeft);
        }

       

      
        public static string Title
        {
            get
            {
                var t = (AssemblyTitleAttribute)typeof(UiControls).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
                return t.Title;
            }
        }

        public static string Description
        {
            get
            {
                var t = (AssemblyDescriptionAttribute)typeof(UiControls).Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0];
                return t.Description;
            }
        }

        public static Version Version
        {
            get
            {
                return typeof(UiControls).Assembly.GetName().Version;
            }
        }

        public static string VersionString
        {
            get
            {
                return Version.ToString() + (IsDebug ? " (DEBUG BUILD)" : ".R");
            }
        }

        internal static void Initialise(Font Font)
        {
            largeFont = new Font("Segoe UI", 14, FontStyle.Regular);
            normalFont = new Font("Segoe UI", 9, FontStyle.Regular);
            boldFont = new Font(normalFont, FontStyle.Bold);
            largeBoldFont = new Font("Segoe UI", 14, FontStyle.Bold);
            italicFont = new Font(normalFont, FontStyle.Italic);
            strikeFont = new Font(normalFont, FontStyle.Strikeout);
            BreakingVersions = new Dictionary<Version, string>();
            BreakingVersions.Add(new Version(1, 0, 0, 3565), "Refactoring.");
            Random = new Random();
        }

       

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

        public static DialogResult ShowWithDim(Form owner, dynamic form)
        {
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
        }

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

        public static class ImageListOrder
        {
            // keep in tandem
            public const int Adduct = 0;
            public const int Compound = 1;
            public const int CompoundU = 2;
            public const int Info = 3;
            public const int InfoU = 4;
            public const int List = 5;
            public const int Pathway = 6;
            public const int Cluster = 7;
            public const int ClusterU = 8;
            public const int Variable = 9;
            public const int VariableU = 10;
            public const int Line = 11;
            public const int Assignment = 12;
            public const int Warning = 13;
            public const int TestFull = 14;
            public const int TestEmpty = 15;
            public const int Filter = 16;
            public const int Statistic = 17;
            public const int Point = 18;
            public const int File = 19;
        }

        internal static void PopulateImageList(ImageList il)
        {
            // keep in tandem
            il.Images.Add("0", Resources.ObjAdduct);
            il.Images.Add("1", Resources.ObjCompound);
            il.Images.Add("2", Resources.ObjCompoundU);
            il.Images.Add("3", Resources.ObjInfo);
            il.Images.Add("4", Resources.ObjInfoU);
            il.Images.Add("5", Resources.ObjList);
            il.Images.Add("6", Resources.ObjPathway);
            il.Images.Add("7", Resources.ObjCluster);
            il.Images.Add("8", Resources.ObjClusterU);
            il.Images.Add("9", Resources.ObjVariable);
            il.Images.Add("10", Resources.ObjVariableU);
            il.Images.Add("11", Resources.ObjLine);
            il.Images.Add("12", Resources.ObjAssignment);
            il.Images.Add("13", Resources.MnuWarning);
            il.Images.Add("14", Resources.TestFull);
            il.Images.Add("15", Resources.TestEmpty);
            il.Images.Add("16", Resources.SmallObjFilter);
            il.Images.Add("17", Resources.ObjStatistics);
            il.Images.Add("18", Resources.ObjPoint);
            il.Images.Add("19", Resources.MnuFile);
        }

        public static bool IsDesigning
        {
            get
            {
                return normalFont == null;
            }
        }

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

        internal static string GetManText(string topic)
        {
            StringBuilder sb = new StringBuilder();
            bool reading = false;
            string manFile = Path.Combine(UiControls.StartupPath, "Manual.dat");
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

        internal static void SetIcon(Form frm)
        {
            //frm.Icon = Resources.MetaboliteExplorer;
            frm.Icon = Resources.MainIcon2;
            //frm.Font = new Font("Segoe UI", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        internal static void CompensateForVisualStyles(Form frm)
        {
            if (!Application.RenderWithVisualStyles)
            {
                EnumerateControls<Button>(frm, CompensateForVisualStyles);
            }
        }

        private static void CompensateForVisualStyles(Button obj)
        {
            if (obj.FlatStyle == FlatStyle.Standard || obj.FlatStyle == FlatStyle.System)
            {
                obj.UseVisualStyleBackColor = false;
                obj.BackColor = Color.CornflowerBlue;
                obj.ForeColor = Color.White;
            }
        }

        internal static void Dispose(IDisposable d)
        {
            if (d != null)
            {
                d.Dispose();
            }
        }

        internal static Color NextColour()
        {
            return Maths.BasicColours[ColourIndex++ % Maths.BasicColours.Length];
        }

        public enum EFileExtension
        {
            //[Name("Sessions=mdat|MS-NRBF sessions=mdat-bin|Sesialised binary sessions=mdat-mbin|Serialied text format sessions=.mdat-tbin|Serialised fast binary sessions=mdat-fbin|Serialised compact binary sessions=mdat-cbin|XML sessions=mdat-xml|Data contact sessions=mdat-con")]
            [Name("All session types|Session files=mdat|MS-NRBF sessions=mdat-bin|Serialised sessions / binary=mdat-mbin|Serialied sessions / text=mdat-txt|Serialised sessions / fast binary=mdat-fbin|Serialised sessions / compact binary=mdat-cbin")]
            Sessions,

            //[Name("Results=mres|MS-NRBF results=mres-bin|Sesialised binary results=mres-mbin|Serialied text format results=.mres-tbin|Serialised fast binary sessions=mres-fbin|Serialised compact binary results=mres-cbin|XML results=mres-xml|Data contact results=mres-con")]
            [Name("All result types|Result files=mres|MS-NRBF results=mres-mbin|Serialised results / text=mres-txt|Serialised results / fast binary=mres-fbin|Serialised results / compact binary=mres-cbin")]
            EvaluationResults,

            [Name("Comma separated value files=csv|Text files=.txt")]
            Csv,

            [Name("R scripts=r|Text files=txt")]
            RScript,

            [Name("Supported image files|Portable network graphics=.png|Enhanced meta files=emf")]
            PngOrEmf,

            [Name("R data files=rdata")]
            RData
        }

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
        }

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

       

        internal static void RestartProgram()
        {
            Process.Start(Application.ExecutablePath);
            Application.Exit();
        }


       

        /// <summary>
        /// Invokes the object's constructor on itself, potentially initialising the object.
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
        internal static string GetNewFile(string v, string format = "-{0}")
        {
            int n = 0;

            string prefix = Path.Combine(Path.GetDirectoryName(v), Path.GetFileNameWithoutExtension(v));
            string suffix = Path.GetExtension(v);
            string fileName;

            do
            {
                n++;
                fileName = prefix + string.Format(format, n) + suffix;
            } while (File.Exists(fileName));

            return fileName;
        }

       

        /// <summary>
        /// (MJR) Converts an string to small caps.
        /// </summary>
        public static string ToSmallCaps(this string x)
        {
            //string caps = "ᴀʙᴄᴅᴇꜰɢʜɪᴊᴋʟᴍɴᴏᴘʀꜱᴛᴜᴠᴡʏᴢ";
            string caps = "ᴀʙᴄᴅᴇғɢʜɪᴊᴋʟᴍɴᴏᴘǫʀsᴛᴜᴠᴡxʏᴢ";
            char[] result = new char[x.Length];

            for (int n = 0; n < x.Length; n++)
            {
                char c = x[n];

                if (c >= 'a' && c <= 'z')
                {
                    result[n] = caps[c - 'a'];
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    result[n] = caps[c - 'A'];
                }
                else
                {
                    result[n] = c;
                }
            }

            return new string(result);
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

            return CreateSolidColourImage(ti.ShortName, color2, color1);
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
                    var sz = g.MeasureString(txt, UiControls.boldFont);

                    g.DrawString(txt, UiControls.boldFont, Brushes.White, bmp.Width / 2 - sz.Width / 2, bmp.Height / 2 - sz.Height / 2);
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
            bool fails = false;

            foreach (WeakReference<T> refr in self)
            {
                T t;

                if (refr.TryGetTarget(out t))
                {
                    result.Add(t);
                }
                else
                {
                    fails = true;
                }
            }

            strong = result;
            return fails;
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

      

    

     

        /// <summary>
        /// 
        /// </summary>
        public static string StartupPath
        {
            get
            {
                if (_startupPath == null)
                {
                    string rerouteFile = Path.Combine(Application.StartupPath, "reroute.txt");

                    if (File.Exists(rerouteFile))
                    {
                        _startupPath = File.ReadAllText(rerouteFile).Trim();
                    }
                    else
                    {
                        _startupPath = Application.StartupPath;
                    }
                }

                return _startupPath;
            }
        }

        /// <summary>
        /// Sets the startup path (REQUIRES APPLICATION RESTART).
        /// </summary>                                           
        public static void SetStartupPath(string text)
        {
            string rerouteFile = Path.Combine(Application.StartupPath, "reroute.txt");

            File.WriteAllText(rerouteFile, text);
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
