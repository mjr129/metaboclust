using System;
using System.Collections.Generic;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Properties;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;
using System.Drawing;
using MSerialisers;
using System.Linq;

namespace MetaboliteLevels.Data.Visualisables
{
    /// <summary>
    /// Adducts
    /// aka. LC-MS adducts
    /// </summary>
    [Serializable]
    [DeferSerialisation]
    class Adduct : IVisualisable
    {
        /// <summary>
        /// Adduct name (may be NULL)
        /// </summary>
        private readonly string _defaultName;

        /// <summary>
        /// Charge
        /// </summary>
        public int Charge;

        /// <summary>
        /// m/z
        /// (m/z not mass - i.e. for both 1H+ and 2H+ this is 1.007)
        /// </summary>
        public decimal Mz;

        /// <summary>
        /// User comments.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// User comments.
        /// </summary>
        public string OverrideDisplayName { get; set; }

        /// <summary>
        /// Annotations
        /// </summary>
        public List<Annotation> Annotations = new List<Annotation>();

        /// <summary>
        /// Meta info
        /// </summary>
        public readonly MetaInfoCollection MetaInfo = new MetaInfoCollection();

        /// <summary>
        /// Unused (can't be disabled)
        /// </summary>
        bool ITitlable.Enabled { get { return true; } set { } }

        /// <summary>
        /// Constructor
        /// </summary>
        public Adduct(string name, int charge, decimal mz)
        {
            this._defaultName = name;
            this.Charge = charge;
            this.Mz = mz;
        }

        /// <summary>
        /// Is this an empty adduct?
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return DefaultDisplayName == null;
            }
        }

        /// <summary>
        /// Default display name.
        /// </summary>
        public string DefaultDisplayName
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
        /// Implements IVisualisable. 
        /// </summary>
        public string DisplayName
        {
            get
            {
                return IVisualisableExtensions.GetDisplayName(OverrideDisplayName, DefaultDisplayName);
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
        /// Implements IVisualisable. 
        /// </summary>
        public Image REMOVE_THIS_FUNCTION
        {
            get { return Resources.ObjLAdduct; }
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public IEnumerable<InfoLine> GetInformation(Core core)
        {
            yield return new InfoLine("Charge", Charge);
            yield return new InfoLine("Comment", Comment);
            yield return new InfoLine("Display name", DisplayName);
            yield return new InfoLine("Mass", Mz);
            yield return new InfoLine("Name", DefaultDisplayName);
            yield return new InfoLine("Class", VisualClass);

            foreach (InfoLine il in core._adductsMeta.ReadAll(this.MetaInfo))
            {
                yield return il;
            }
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public IEnumerable<InfoLine> GetStatistics(Core core)
        {
            return new InfoLine[0];
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public VisualClass VisualClass
        {
            get { return VisualClass.Adduct; }
        }

        /// <summary>
        /// Debugging.
        /// </summary>
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public void RequestContents(ContentsRequest request)
        {
            switch (request.Type)
            {
                case VisualClass.Peak:
                    request.Text = "Peak using the adduct {0}.";
                    request.AddRange(Annotations.Select(z => z.Peak));
                    break;

                case VisualClass.Cluster:
                    break;

                case VisualClass.Compound:
                    request.Text = "Compounds using the adduct {0}.";
                    request.AddRange(Annotations.Select(z => z.Compound));
                    break;

                case VisualClass.Adduct:
                    break;

                case VisualClass.Pathway:
                    break;

                case VisualClass.Annotation:
                    request.Text = "Annotations using the adduct {0}.";
                    request.AddRange(Annotations);
                    break;

                default:
                    break;
            }
        }

        public IEnumerable<Column> GetColumns(Session.Core core)
        {
            List<Column<Adduct>> result = new List<Column<Adduct>>();
            result.Add("Name", true, λ => λ.DefaultDisplayName);
            result.Add("Charge", false, λ => λ.Charge);
            result.Add("Mass", false, λ => λ.Mz);
            result.Add("Comment", false, λ => λ.Comment);
            return result;
        }

        public UiControls.ImageListOrder GetIcon()
        {
            // IMAGE
            return UiControls.ImageListOrder.Adduct;
        }
    }
}
