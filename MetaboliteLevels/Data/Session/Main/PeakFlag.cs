using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MGui.Helpers;

namespace MetaboliteLevels.Data.Session.Main
{
    /// <summary>
    /// Used to allow the user to "flag" variables with quick comments.
    /// </summary>
    [Serializable]
    internal class PeakFlag : Associational
    {
        private char _key;

        /// <summary>
        /// When this key is pressed on the main list the flag will be toggled.
        /// Pressing Ctrl+this key toggles this flag for all peaks in the list (under the current filter).
        /// This can be empty (null).
        /// </summary>
        [XColumn(EColumn.Visible)]
        public char Key { get { return this._key; } set { this._key = char.ToUpper(value); } }

        /// <summary>
        /// Sound to make when toggling the flag.
        /// </summary>
        [XColumn]
        public uint BeepFrequency { get; set; }

        /// <summary>
        /// Sound to make when toggling the flag.
        /// </summary>
        [XColumn]
        public uint BeepDuration { get; set; }

        /// <summary>
        /// Colour of the flag in lists.
        /// </summary>
        public Color Colour { get; set; }

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public override EVisualClass AssociationalClass => EVisualClass.None;

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>
        public override string DefaultDisplayName => this.Key.ToString();      

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public PeakFlag()
        {
            this.Init();
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
            this.Key = '\0';
            this.BeepFrequency = 3000;
            this.BeepDuration = 100;
            this.Colour = UiControls.NextColour();
        }

        /// <summary>
        /// DESERIALISATION
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            this.Init();
        }         

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>               
        public override Image Icon=> Resources.ListIconFlag;

        /// <summary>
        /// IMPLEMENTS IVisualisable.
        /// </summary>     
        protected override void OnFindAssociations(ContentsRequest list)
        {
            switch (list.Type)
            {
                case EVisualClass.Peak:
                    list.Text = "Peaks marked with {0}";
                    list.AddRange(list.Core.Peaks.Where(z => z.CommentFlags.Contains(this)));
                    break;

                case EVisualClass.Cluster:
                    list.Text = "Clusters containing peaks marked with {0}";
                    list.AddExtraColumn("Flag count", "Number of peaks in this cluster with the comment flag " + this.ToString());

                    foreach (Cluster c in list.Core.Clusters)
                    {
                        if (c.CommentFlags.ContainsKey(this))
                        {
                            list.Add(c, c.CommentFlags[this]);
                        }
                    }
                    break;

                case EVisualClass.Pathway:
                    list.Text = "Pathways containing compounds annotated with peaks marked with {0}";
                    foreach (Pathway c in list.Core.Pathways)
                    {
                        if (c.Compounds.Any(z => z.Annotations.Any(zz => zz.Peak.CommentFlags.Contains(this))))
                        {
                            list.Add(c);
                        }
                    }
                    break;

                case EVisualClass.Annotation:
                    list.Text = "Annotations for peaks peaks marked with {0}";
                    foreach (Annotation c in list.Core.Annotations)
                    {
                        if (c.Peak.CommentFlags.Contains(this))
                        {
                            list.Add(c);
                        }
                    }
                    break;

                case EVisualClass.Assignment:
                    list.Text = "Assignments for peaks peaks marked with {0}";
                    foreach (Assignment c in list.Core.Assignments)
                    {
                        if (c.Peak.CommentFlags.Contains(this))
                        {
                            list.Add(c);
                        }
                    }
                    break;

                case EVisualClass.Compound:
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
        public override void GetXColumns( CustomColumnRequest request )
        {
            var result = request.Results.Cast< PeakFlag>();
                                                                                             
            result.Add( "Colour", EColumn.None, z => ColourHelper.ColourToName( z.Colour ), z => z.Colour );
        }

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>     
        public PeakFlag Clone()
        {
            return (PeakFlag)this.MemberwiseClone();
        }
    }
}
