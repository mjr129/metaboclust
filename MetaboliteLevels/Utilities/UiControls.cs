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
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Forms.Editing;
using System.Diagnostics;
using Microsoft.Win32;
using MetaboliteLevels.Forms;
using MGui;
using MGui.Datatypes;
using MGui.Helpers;
using MGui.Controls;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using MetaboliteLevels.Types.UI;
using MCharting;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Forms.Selection;
using MetaboliteLevels.Forms.Text;

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
        private static string _startupPath;
        private static EStartupPath _startupPathMode;

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

        public static void ColourMenuButtons( ToolStrip target)
        {
            foreach (var o in target.Items)
            {
                ToolStripItem tsi = o as ToolStripItem;

                if (tsi != null && tsi.ForeColor == Color.Purple)
                {
                    tsi.Image = UiControls.RecolourImage( tsi.Image );
                    tsi.ForeColor = UiControls.TitleForeColour;
                    tsi.Text = tsi.Text.ToUpper();
                }
            }
        }

        public static void CreateIcon(Series series, GroupInfoBase group)
        {
            GraphicsPath gp;

            series.Style.DrawPointsSize = 8;

            switch (group.GraphIcon)
            {
                case EGraphIcon.Default: gp = null; break;                              
                case EGraphIcon.Circle: gp = SeriesStyle.DrawPointsShapes.Circle; break;
                case EGraphIcon.Cross: gp = SeriesStyle.DrawPointsShapes.Cross; break;
                case EGraphIcon.Diamond: gp = SeriesStyle.DrawPointsShapes.Diamond; break;
                case EGraphIcon.HLine: gp = SeriesStyle.DrawPointsShapes.HLine; break;
                case EGraphIcon.Plus: gp = SeriesStyle.DrawPointsShapes.Plus; break;
                case EGraphIcon.Square: gp = SeriesStyle.DrawPointsShapes.Square; break;
                case EGraphIcon.Asterisk: gp = SeriesStyle.DrawPointsShapes.Asterisk; break;
                case EGraphIcon.Triangle: gp = SeriesStyle.DrawPointsShapes.Triangle; break;
                case EGraphIcon.InvertedTriangle: gp = SeriesStyle.DrawPointsShapes.InvertedTriangle; break;
                case EGraphIcon.VLine: gp = SeriesStyle.DrawPointsShapes.VLine; break;
                default: gp = null; break;
            }

            series.Style.DrawPointsShape = gp;

            switch (group.GraphIcon)
            {
                case Types.UI.EGraphIcon.Circle:
                case Types.UI.EGraphIcon.Default:
                case Types.UI.EGraphIcon.Diamond:
                case Types.UI.EGraphIcon.InvertedTriangle:
                case Types.UI.EGraphIcon.Square:
                case Types.UI.EGraphIcon.Triangle:
                    series.Style.DrawPoints = new SolidBrush( group.Colour );
                    break;

                case Types.UI.EGraphIcon.Asterisk:
                case Types.UI.EGraphIcon.Cross:
                case Types.UI.EGraphIcon.HLine:
                case Types.UI.EGraphIcon.Plus:
                case Types.UI.EGraphIcon.VLine:
                    series.Style.DrawPointsLine = new Pen( group.Colour, 3 );
                    break;

                default:
                    throw new SwitchException( group.GraphIcon );
            } 
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

            BitmapDataHelper data = new BitmapDataHelper( source );

            uint dc = colour.ToArgbU() & 0x00FFFFFF;

            for (int y = 0; y < data.Height; y++)
            {
                for (int x = 0; x < data.Width; x++)
                {
                    uint i = data[x, y];

                    i |= dc;

                    data[x, y] = i;
                }
            }

            return data.Unlock();
        }

        public static Bitmap EmboldenImage( Image source )
        {
            if (source == null)
            {
                return null;
            }

            BitmapDataHelper data = new BitmapDataHelper( source );
            BitmapDataHelper original = new BitmapDataHelper( source );

            for (int y = 0; y < data.Height; y++)
            {
                for (int x = 0; x < data.Width; x++)
                {
                    uint i = unchecked((uint)original[x, y]);

                    if ((i & 0xFF000000U) == 0U || (i == 0xFFFFFFFFU))
                    {
                        uint u = original.GetOrDefault( x, y - 1 );
                        uint d = original.GetOrDefault( x, y + 1 );
                        uint l = original.GetOrDefault( x - 1, y );
                        uint r = original.GetOrDefault( x + 1, y );
                        uint cmb = MaxC(u, d, l, r);


                        data[x, y] = cmb;
                    }
                }
            }

            return data.Unlock();
        }

        private static uint MaxC( params uint[] e )
        {    
            foreach (var x in e)
            {
                if ((x & 0xFF000000) != 0 && (x != 0xFFFFFFFF))
                {
                    return x;
                }
            }

            return e[0];
        }

        public static Bitmap OutlineImage( Image source, Color colour )
        {
            if (source == null)
            {
                return null;
            }

            BitmapDataHelper data = new BitmapDataHelper( source );
            BitmapDataHelper original = new BitmapDataHelper( source );

            uint dc = colour.ToArgbU();

            for (int y = 0; y < data.Height; y++)
            {
                for (int x = 0; x < data.Width; x++)
                {
                    uint i = original[x, y];
                    uint u = original.GetOrDefault( x, y - 1 );
                    uint d = original.GetOrDefault( x, y + 1 );
                    uint l = original.GetOrDefault( x - 1, y );
                    uint r = original.GetOrDefault( x + 1, y );

                    if (((i & 0xFF000000) == 0) && (((u | d | l | r) & 0xFF000000) != 0))
                    {
                        data[x, y] = dc;
                    }
                }
            }

            return data.Unlock();
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

        internal static Image GetImage( object @object, bool bold )
        {
            Image result = GetImage( @object );

            if (bold)
            {
                return EmboldenImage( result );
            }

            return result;
        }

        internal static Image GetImage( object @object )
        {              
            IIconProvider vis = @object as IIconProvider;

            return vis?.Icon ?? Resources.IconUnknown;
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

        internal static Image GetImage( EImageListOrder image, bool bold )
        {
            Image result = GetImage( image );

            if (bold)
            {
                return EmboldenImage( result );
            }

            return result;
        }

        internal static Image GetImage(EImageListOrder image)
        {                             
            switch (image)
            {
                case EImageListOrder.ListIconAdduct          : return Resources.ListIconAdduct;
                case EImageListOrder.IconAnnotationTentative     : return Resources.IconAnnotationTentative;
                case EImageListOrder.IconAnnotationAffirmed     : return Resources.IconAnnotationAffirmed;
                case EImageListOrder.IconAnnotationConfirmed     : return Resources.IconAnnotationConfirmed;
                case EImageListOrder.ListIconCompound       : return Resources.ListIconCompound;
                case EImageListOrder.ListIconCompoundTentative       : return Resources.ListIconCompoundTentative;
                case EImageListOrder.ListIconCompoundAffirmed       : return Resources.ListIconCompoundAffirmed;
                case EImageListOrder.ListIconCompoundConfirmed       : return Resources.ListIconCompoundConfirmed;
                case EImageListOrder.ListIconInformation            : return Resources.ListIconInformation;
                case EImageListOrder.ListIconInformationMeta           : return Resources.ListIconInformationMeta;      
                case EImageListOrder.IconList            : return Resources.IconList;                     
                case EImageListOrder.ListIconPathway         : return Resources.ListIconPathway;
                case EImageListOrder.ListIconCluster         : return Resources.ListIconCluster;
                case EImageListOrder.ListIconClusterInsignificant        : return Resources.ListIconClusterInsignificant; 
                case EImageListOrder.ListIconPeak       : return Resources.ListIconPeak;
                case EImageListOrder.ListIconPeakTentative       : return Resources.ListIconPeakTentative;
                case EImageListOrder.ListIconPeakAffirmed       : return Resources.ListIconPeakAffirmed;
                case EImageListOrder.ListIconPeakConfirmed       : return Resources.ListIconPeakConfirmed;
                case EImageListOrder.IconLine            : return Resources.IconLine;                     
                case EImageListOrder.ListIconVector      : return Resources.ListIconVector;
                case EImageListOrder.MnuWarning         : return Resources.MnuWarning;
                case EImageListOrder.ListIconTestFull        : return Resources.ListIconTestFull;
                case EImageListOrder.ListIconTestEmpty       : return Resources.ListIconTestEmpty;
                case EImageListOrder.MnuFilter          : return Resources.MnuFilter;
                case EImageListOrder.ListIconStatistics       : return Resources.ListIconStatistics;
                case EImageListOrder.IconPoint           : return Resources.IconPoint;                    
                case EImageListOrder.MnuFile            : return Resources.MnuFile;
                case EImageListOrder.ListIconSortUp      : return Resources.ListIconSortUp;                                  
                case EImageListOrder.ListIconSortDown    : return Resources.ListIconSortDown;
                case EImageListOrder.ListIconFilter      : return Resources.ListIconFilter;
                case EImageListOrder.IconBinary   : return Resources.IconBinary;
                case EImageListOrder.IconR      : return Resources.IconR;         
                case EImageListOrder.MnuGroup           : return Resources.MnuGroup;
                case EImageListOrder.IconMatrix          : return Resources.IconMatrix;
                case EImageListOrder.IconUnknown         : return Resources.IconUnknown;
                case EImageListOrder.MnuCancel           : return Resources.MnuCancel;
                case EImageListOrder.IconObservation     : return Resources.IconObservation;
                default                             : throw new SwitchException(image);
            }
        }

        /// <summary>
        /// Order of images in the standard image list (see <see cref="PopulateImageList"/>).
        /// </summary>
        public enum EImageListOrder : int
        {
            ListIconAdduct,     
            IconAnnotationTentative,
            IconAnnotationAffirmed,
            IconAnnotationConfirmed,
            ListIconCompound,
            ListIconCompoundTentative,
            ListIconCompoundAffirmed,
            ListIconCompoundConfirmed,
            ListIconInformation,
            ListIconInformationMeta,
            IconList,
            ListIconPathway,
            ListIconCluster,
            ListIconClusterInsignificant,
            ListIconPeak,
            ListIconPeakTentative,
            ListIconPeakAffirmed,
            ListIconPeakConfirmed,
            IconLine,
            ListIconVector,
            MnuWarning,
            ListIconTestFull,
            ListIconTestEmpty,
            MnuFilter,
            ListIconStatistics,
            IconPoint,
            MnuFile,
            ListIconSortUp,
            ListIconSortDown,
            ListIconFilter,
            IconBinary,
            IconR,   
            MnuGroup,
            IconMatrix,
            IconUnknown,
            MnuCancel,
            IconObservation
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
        internal static Image CreateExperimentalGroupImage(bool isChecked, GroupInfoBase group, bool large)
        {
            var color1 = group.Colour;
            var color2 = group.ColourLight;

            if (!isChecked)
            {
                color1 = Color.FromArgb(32, color1.R, color1.G, color1.B);
                color2 = Color.FromArgb(32, color1.R, color1.G, color1.B);
            }

            Color colour = group.Colour;
            Color colourLight = group.ColourLight;

            if (!isChecked)
            {
                colour = ColourHelper.Blend( colour, Color.Transparent, 0.75 );
                colourLight = ColourHelper.Blend( colourLight, Color.Transparent, 0.75 );
            }

            Bitmap result = RecolourImage( large ? Resources.IconGroups : Resources.MnuGroup, colour );

            string text = group.DefaultShortName; // todo: change!
            if (!string.IsNullOrEmpty( text ) || !isChecked)
            {
                using (Graphics g = Graphics.FromImage( result ))
                {
                    if (!string.IsNullOrEmpty( text ))
                    {
                        string txt = text.Substring( 0, 1 );
                        var sz = g.MeasureString( txt, FontHelper.SmallBoldFont );
                        float x = result.Width / 2 - sz.Width / 2;
                        float y = result.Height - sz.Height;

                        if (!large)
                        {
                            g.DrawString( txt, FontHelper.SmallBoldFont, Brushes.Black, x-1, y );
                            g.DrawString( txt, FontHelper.SmallBoldFont, Brushes.Black, x + 1, y );
                            g.DrawString( txt, FontHelper.SmallBoldFont, Brushes.Black, x , y-1 );
                            g.DrawString( txt, FontHelper.SmallBoldFont, Brushes.Black, x, y + 1 );
                        }
                                  
                        if (large)
                        {
                            g.DrawString( txt, FontHelper.SmallBoldFont, Brushes.White, result.Width / 2 - sz.Width / 2, result.Height / 2 );
                        }
                        else
                        {   
                            g.DrawString( txt, FontHelper.SmallBoldFont, Brushes.White, x,y );
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
        public static Color StatisticColour(double value, double min, double max)
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
                if (_startupPath == null)
                {
                    FindStartupPath();
                }

                return _startupPathMode;
            }
        }

        /// <summary>
        ///Gets the application startuppath. 
        /// </summary>
        public static string StartupPath
        {
            get
            {
                if (_startupPath == null)
                {
                    FindStartupPath();
                }

                return _startupPath;
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
                _startupPath = File.ReadAllText(rerouteFile).Trim();
                _startupPathMode = EStartupPath.Local;
            }
            else
            {
                _startupPath = Registry.GetValue(Registry.CurrentUser.Name + "\\" + STARTUPPATH_REGKEY, STARTUPPATH_REGVALUE, null) as string;

                if (_startupPath != null)
                {
                    _startupPathMode = EStartupPath.User;
                }
                else
                {
                    _startupPath = Registry.GetValue(Registry.LocalMachine.Name + "\\" + STARTUPPATH_REGKEY, STARTUPPATH_REGVALUE, null) as string;

                    if (_startupPath != null)
                    {
                        _startupPathMode = EStartupPath.Machine;
                    }
                    else
                    {
                        _startupPath = Application.StartupPath;
                        _startupPathMode = EStartupPath.None;
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
