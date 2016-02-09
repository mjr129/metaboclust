using System;
using System.Collections.Generic;
using System.Drawing;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Data.Visualisables
{
    /// <summary>
    /// An annotation;
    /// </summary>
    [Serializable]
    class Annotation : IVisualisable
    {
        public readonly Peak Peak;
        public readonly Compound Compound;
        public readonly Adduct Adduct;

        /// <summary>
        /// Superfluous information in source file
        /// </summary>
        public readonly MetaInfoCollection Meta = new MetaInfoCollection();

        /// <summary>
        /// User modifiable comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// User modifiable comment
        /// </summary>
        public string OverrideDisplayName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary> 
        public Annotation(Peak peak, Compound compound, Adduct adduct)
        {
            this.Peak = peak;
            this.Compound = compound;
            this.Adduct = adduct;
        }

        /// <summary>
        /// Unused (can't be disabled)
        /// </summary>
        bool ITitlable.Enabled { get { return true; } set { } }

        public string DefaultDisplayName
        {
            get
            {
                return Compound.DisplayName;
            }
        }

        public string DisplayName
        {
            get
            {
                return IVisualisableExtensions.FormatDisplayName(OverrideDisplayName, Compound.DisplayName);
            }
        }    

        VisualClass IVisualisable.VisualClass
        {
            get
            {
                return VisualClass.Annotation;
            }
        }

        UiControls.ImageListOrder IVisualisable.GetIcon()
        {
            return UiControls.ImageListOrder.Compound;
        }          

        IEnumerable<Column> IVisualisable.GetColumns(Core core)
        {
            List<Column<Annotation>> columns = new List<Column<Annotation>>();

            columns.Add("Name", EColumn.Visible, z => z.DisplayName);

            columns.AddSubObject(core, "Peak", z => z.Peak);
            columns.AddSubObject(core, "Compound", z => z.Compound);
            columns.AddSubObject(core, "Adduct", z => z.Adduct);

            core._annotationsMeta.ReadAllColumns<Annotation>(z => z.Meta, columns);

            return columns;
        }

        void IVisualisable.RequestContents(ContentsRequest list)
        {
            switch (list.Type)
            {
                case VisualClass.Adduct:
                    list.Text = "Adduct for annotation {0}";
                    list.Add(Adduct);
                    break;

                case VisualClass.Annotation:
                    ((IVisualisable)Peak).RequestContents(list);
                    ((IVisualisable)Compound).RequestContents(list);
                    list.Text = "Annotations with same peaks/compounds to {0}";
                    break;

                case VisualClass.Assignment:
                    ((IVisualisable)Peak).RequestContents(list);
                    break;

                case VisualClass.Cluster:
                    ((IVisualisable)Peak).RequestContents(list);
                    break;

                case VisualClass.Compound:
                    list.Text = "Compound for annotation {0}";
                    list.Add(Compound);
                    break;

                case VisualClass.Pathway:
                    ((IVisualisable)Peak).RequestContents(list);
                    ((IVisualisable)Compound).RequestContents(list);
                    break;

                case VisualClass.Peak:
                    list.Text = "Peak for annotation {0}";
                    list.Add(Peak);
                    break;
            }
        }
    }
}
