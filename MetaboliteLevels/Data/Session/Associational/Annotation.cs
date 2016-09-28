using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Utilities;
using MGui.Datatypes;

namespace MetaboliteLevels.Data.Session.Associational
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
        [XColumn()]
        public readonly Peak Peak;
        [XColumn()]
        public readonly Compound Compound;
        [XColumn()]
        public readonly Adduct Adduct;

        /// <summary>
        /// Superfluous information in source file
        /// </summary>
        public readonly MetaInfoCollection Meta = new MetaInfoCollection();

        [XColumn()]
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

        protected override void OnFindAssociations( ContentsRequest list )
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

        public override void GetXColumns( ColumnCollection list, Core core )
        {
            ColumnCollection<Annotation> results = list .Cast< Annotation>();

            core._annotationsMeta.ReadAllColumns<Annotation>( z => z.Meta, results );
        }
    }
}
