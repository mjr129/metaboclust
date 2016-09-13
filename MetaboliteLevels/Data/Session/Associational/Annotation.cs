using System;
using System.Collections.Generic;
using System.Drawing;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using MGui.Datatypes;

namespace MetaboliteLevels.Data.Visualisables
{
    enum EAnnotation
    {
        Tentative = 0,
        Affirmative = 1,
        Confirmed=2,
    }

    /// <summary>
    /// An annotation;
    /// </summary>
    [Serializable]
    class Annotation : IAssociational
    {
        public readonly Peak Peak;
        public readonly Compound Compound;
        public readonly Adduct Adduct;

        /// <summary>
        /// Superfluous information in source file
        /// </summary>
        public readonly MetaInfoCollection Meta = new MetaInfoCollection();

        public readonly EAnnotation Status;

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
        public Annotation(Peak peak, Compound compound, Adduct adduct, EAnnotation attributes)
        {
            this.Peak = peak;
            this.Compound = compound;
            this.Adduct = adduct;
            this.Status = attributes;
        }

        /// <summary>
        /// Unused (can't be disabled)
        /// </summary>
        bool INameable.Hidden { get { return false; } set { } }

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
                return IVisualisableExtensions.FormatDisplayName(this);
            }
        }    

        EVisualClass IAssociational.VisualClass
        {
            get
            {
                return EVisualClass.Annotation;
            }
        }

        UiControls.ImageListOrder IVisualisable.GetIcon()
        {
            switch (this.Status)
            {
                case EAnnotation.Tentative:
                    return UiControls.ImageListOrder.AnnotationT;

                case EAnnotation.Affirmative:
                    return UiControls.ImageListOrder.AnnotationA;

                case EAnnotation.Confirmed:
                    return UiControls.ImageListOrder.AnnotationC;

                default:
                    throw new SwitchException( this.Status );
            }                                                
        }

        IEnumerable<Column> IVisualisable.GetColumns( Core core )
        {
            List<Column<Annotation>> columns = new List<Column<Annotation>>();

            columns.Add( "Name", EColumn.Visible, z => z.DisplayName );

            columns.AddSubObject( core, "Peak", z => z.Peak );
            columns.AddSubObject( core, "Compound", z => z.Compound );
            columns.AddSubObject( core, "Adduct", z => z.Adduct );
            columns.Add( "Confirmation", z => z.Status );

            core._annotationsMeta.ReadAllColumns<Annotation>( z => z.Meta, columns );

            return columns;
        }

        void IAssociational.RequestContents( ContentsRequest list )
        {
            switch (list.Type)
            {
                case EVisualClass.Adduct:
                    list.Text = "Adduct for annotation {0}";
                    list.Add( Adduct );
                    break;

                case EVisualClass.Annotation:
                    ((IAssociational)Peak).RequestContents( list );
                    ((IAssociational)Compound).RequestContents( list );
                    list.Text = "Annotations with same peaks/compounds to {0}";
                    break;

                case EVisualClass.Assignment:
                    ((IAssociational)Peak).RequestContents( list );
                    break;

                case EVisualClass.Cluster:
                    ((IAssociational)Peak).RequestContents( list );
                    break;

                case EVisualClass.Compound:
                    list.Text = "Compound for annotation {0}";
                    list.Add( Compound );
                    break;

                case EVisualClass.Pathway:
                    ((IAssociational)Peak).RequestContents( list );
                    ((IAssociational)Compound).RequestContents( list );
                    break;

                case EVisualClass.Peak:
                    list.Text = "Peak for annotation {0}";
                    list.Add( Peak );
                    break;
            }
        }
    }
}
