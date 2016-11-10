using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Gui.Controls.Lists;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MSerialisers;

namespace MetaboliteLevels.Data.Session.Main
{
    /// <summary>
    /// Adducts
    /// aka. LC-MS adducts
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    internal class Adduct : Associational
    {
        /// <summary>
        /// Adduct name (may be NULL)
        /// </summary>
        private readonly string _defaultName;

        /// <summary>
        /// Charge
        /// </summary>
        [XColumn]
        public int Charge;

        /// <summary>
        /// m/z
        /// (m/z not mass - i.e. for both 1H+ and 2H+ this is 1.007)
        /// </summary>
        [XColumn]
        public decimal Mz;

        /// <summary>
        /// Annotations
        /// </summary>
        [XColumn]
        public List<Annotation> Annotations = new List<Annotation>();

        /// <summary>
        /// Meta info
        /// </summary>
        public readonly MetaInfoCollection MetaInfo = new MetaInfoCollection();

        /// <summary>
        /// Constructor
        /// </summary>
        public Adduct(string name, int charge, decimal mz)
        {
            this._defaultName = name;
            this.Charge = charge;
            this.Mz = mz;
        }

        public override EPrevent SupportsHide => EPrevent.Hide;

        /// <summary>
        /// Is this an empty adduct?
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this._defaultName == null;
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override string DefaultDisplayName
        {
            get
            {
                if (this._defaultName == null)
                {
                    return "Unknown adduct";
                }
                else
                {
                    return this._defaultName;
                }
            }
        }         

        /// <summary>
        /// Creates an empty adduct (placeholder)
        /// </summary>             
        public static Adduct CreateEmpty()
        {
            return new Adduct(null, 0, 0);
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override EVisualClass AssociationalClass
        {
            get { return EVisualClass.Adduct; }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        protected override void OnFindAssociations(ContentsRequest request)
        {
            switch (request.Type)
            {
                case EVisualClass.Peak:
                    request.Text = "Peak using {0}.";
                    request.AddRange(this.Annotations.Select(z => z.Peak));
                    break;

                case EVisualClass.Cluster:
                    break;

                case EVisualClass.Compound:
                    request.Text = "Compounds using {0}.";
                    request.AddRange(this.Annotations.Select(z => z.Compound));
                    break;

                case EVisualClass.Adduct:
                    break;

                case EVisualClass.Pathway:
                    break;

                case EVisualClass.Annotation:
                    request.Text = "Annotations using {0}.";
                    request.AddRange(this.Annotations);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>              
        public override void GetXColumns( CustomColumnRequest request )
        {
            ColumnCollection<Adduct> result = request.Results.Cast<Adduct>();       

            request.Core._annotationsMeta.ReadAllColumns<Adduct>(z => z.MetaInfo, result);
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>              
        public override Image Icon=> Resources.ListIconAdduct;
    }
}
