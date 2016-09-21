using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Controls.Lists;
using MetaboliteLevels.Data.Session.General;
using MetaboliteLevels.Data.Session.Singular;
using MetaboliteLevels.Utilities;
using MSerialisers;

namespace MetaboliteLevels.Data.Session.Associational
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
                return _defaultName == null;
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public override string DefaultDisplayName
        {
            get
            {
                if (_defaultName == null)
                {
                    return "Unknown adduct";
                }
                else
                {
                    return _defaultName;
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
                    request.AddRange(Annotations.Select(z => z.Peak));
                    break;

                case EVisualClass.Cluster:
                    break;

                case EVisualClass.Compound:
                    request.Text = "Compounds using {0}.";
                    request.AddRange(Annotations.Select(z => z.Compound));
                    break;

                case EVisualClass.Adduct:
                    break;

                case EVisualClass.Pathway:
                    break;

                case EVisualClass.Annotation:
                    request.Text = "Annotations using {0}.";
                    request.AddRange(Annotations);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>              
        public override IEnumerable<Column> GetXColumns(Core core)
        {                                                
            List<Column<Adduct>> result = new List<Column<Adduct>>();       

            core._annotationsMeta.ReadAllColumns(z => z.MetaInfo, result);

            return result;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>              
        public override UiControls.ImageListOrder Icon=>UiControls.ImageListOrder.Adduct;
    }
}
