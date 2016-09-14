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
    class Annotation : Associational
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
        /// Constructor
        /// </summary> 
        public Annotation(Peak peak, Compound compound, Adduct adduct, EAnnotation attributes)
        {
            this.Peak = peak;
            this.Compound = compound;
            this.Adduct = adduct;
            this.Status = attributes;
        }

        public override EPrevent SupportsHide => EPrevent.Hide;

        public override string DefaultDisplayName=> Compound.DisplayName;      

        public override EVisualClass AssociationalClass=>EVisualClass.Annotation;

        public override UiControls.ImageListOrder Icon
        {
            get
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
        }

        public override IEnumerable<Column> GetColumns( Core core )
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

        public override void FindAssociations( ContentsRequest list )
        {
            switch (list.Type)
            {
                case EVisualClass.Adduct:
                    list.Text = "Adduct for annotation {0}";
                    list.Add( Adduct );
                    break;

                case EVisualClass.Annotation:
                    ((Associational)Peak).FindAssociations( list );
                    ((Associational)Compound).FindAssociations( list );
                    list.Text = "Annotations with same peaks/compounds to {0}";
                    break;

                case EVisualClass.Assignment:
                    ((Associational)Peak).FindAssociations( list );
                    break;

                case EVisualClass.Cluster:
                    ((Associational)Peak).FindAssociations( list );
                    break;

                case EVisualClass.Compound:
                    list.Text = "Compound for annotation {0}";
                    list.Add( Compound );
                    break;

                case EVisualClass.Pathway:
                    ((Associational)Peak).FindAssociations( list );
                    ((Associational)Compound).FindAssociations( list );
                    break;

                case EVisualClass.Peak:
                    list.Text = "Peak for annotation {0}";
                    list.Add( Peak );
                    break;
            }
        }
    }
}
