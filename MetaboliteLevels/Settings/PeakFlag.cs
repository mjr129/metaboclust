using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Viewers.Lists;
using System.ComponentModel;

namespace MetaboliteLevels.Settings
{
    /// <summary>
    /// Used to allow the user to "flag" variables with quick comments.
    /// </summary>
    [Serializable]
    public class PeakFlag : IVisualisable
    {
        private char _key;

        /// <summary>
        /// When this key is pressed on the main list the flag will be toggled.
        /// Pressing Ctrl+this key toggles this flag for all peaks in the list (under the current filter).
        /// This can be empty (null).
        /// </summary>
        public char Key { get { return _key; } set { _key = char.ToUpper(value); } }

        /// <summary>
        /// Sound to make when toggling the flag.
        /// </summary>
        public uint BeepFrequency { get; set; }

        /// <summary>
        /// Sound to make when toggling the flag.
        /// </summary>
        public uint BeepDuration { get; set; }

        /// <summary>
        /// Colour of the flag in lists.
        /// </summary>
        public Color Colour { get; set; }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        VisualClass IVisualisable.VisualClass => VisualClass.None;

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public string DisplayName => IVisualisableExtensions.FormatDisplayName(this);

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        string ITitlable.DefaultDisplayName => Key.ToString();

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public string OverrideDisplayName { get; set; }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public PeakFlag()
        {
            Init();
        }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public PeakFlag(string overrideDisplayName, char key, string comments, Color colour)
        {
            this.Key = key;
            this.OverrideDisplayName = overrideDisplayName;
            this.Comment = comments;
            this.Colour = colour;
        }

        /// <summary>
        /// Initialises default values.
        /// </summary>
        private void Init()
        {
            Key = '\0';
            BeepFrequency = 3000;
            BeepDuration = 100;
            Colour = UiControls.NextColour();
        }

        /// <summary>
        /// DESERIALISATION
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            Init();
        }

        /// <summary>
        /// OVERRIDES Object
        /// </summary>      
        public override string ToString()
        {
            return OverrideDisplayName;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>               
        UiControls.ImageListOrder IVisualisable.GetIcon()
        {
            return UiControls.ImageListOrder.Filter;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>     
        void IVisualisable.RequestContents(ContentsRequest list)
        {
            switch (list.Type)
            {
                case VisualClass.Peak:
                    list.Text = "Peaks marked with {0}";
                    list.AddRange(list.Core.Peaks.Where(z => z.CommentFlags.Contains(this)));
                    break;

                case VisualClass.Cluster:
                    list.Text = "Clusters containing peaks marked with {0}";
                    list.AddExtraColumn("Flag count", "Number of peaks in this cluster with the comment flag " + ToString());

                    foreach (Cluster c in list.Core.Clusters)
                    {
                        if (c.CommentFlags.ContainsKey(this))
                        {
                            list.Add(c, c.CommentFlags[this]);
                        }
                    }
                    break;

                case VisualClass.Pathway:
                    list.Text = "Pathways containing compounds annotated with peaks marked with {0}";
                    foreach (Pathway c in list.Core.Pathways)
                    {
                        if (c.Compounds.Any(z => z.Annotations.Any(zz => zz.Peak.CommentFlags.Contains(this))))
                        {
                            list.Add(c);
                        }
                    }
                    break;

                case VisualClass.Annotation:
                    list.Text = "Annotations for peaks peaks marked with {0}";
                    foreach (Annotation c in list.Core.Annotations)
                    {
                        if (c.Peak.CommentFlags.Contains(this))
                        {
                            list.Add(c);
                        }
                    }
                    break;

                case VisualClass.Assignment:
                    list.Text = "Assignments for peaks peaks marked with {0}";
                    foreach (Assignment c in list.Core.Assignments)
                    {
                        if (c.Peak.CommentFlags.Contains(this))
                        {
                            list.Add(c);
                        }
                    }
                    break;

                case VisualClass.Compound:
                    list.Text = "Compounds with annotations for peaks peaks marked with {0}";
                    foreach (Compound c in list.Core.Compounds)
                    {
                        if (c.Annotations.Any(z => z.Peak.CommentFlags.Contains(this)))
                        {
                            list.Add(c);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>     
        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            List<Column<PeakFlag>> result = new List<Column<PeakFlag>>();

            result.Add("Name", EColumn.Visible, z => z.OverrideDisplayName, z => z.Colour);
            result.Add("Key", EColumn.Visible, z => z.Key);
            result.Add("Enabled", z => z.Enabled);
            result.Add("Comments", z => z.Comment);
            result.Add("Colour", EColumn.None, z => UiControls.ColourToName(z.Colour), z => z.Colour);
            result.Add("Beep frequency", z => z.BeepFrequency);
            result.Add("Beep duration", z => z.BeepDuration);
            result.Add("Display name", EColumn.Advanced, z => z.DisplayName);

            return result;
        }

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>     
        public PeakFlag Clone()
        {
            return (PeakFlag)MemberwiseClone();
        }
    }
}
