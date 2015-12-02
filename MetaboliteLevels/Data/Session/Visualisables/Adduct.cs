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
        /// Adduct name (may be NULL - use DisplayName)
        /// </summary>
        public string Name;

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
        public string Title { get; set; }

        /// <summary>
        /// Annotations
        /// </summary>
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
            this.Name = name;
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
                return Name == null;
            }
        }

        /// <summary>
        /// Implements IVisualisable. 
        /// </summary>
        public string DisplayName
        {
            get
            {
                if (Title != null)
                {
                    return Title;
                }
                else if (Name == null)
                {
                    return "Unknown adduct";
                }
                else
                {
                    return Name;
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
        /// Implements IVisualisable. 
        /// </summary>
        public Image DisplayIcon
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
            yield return new InfoLine("Name", Name);
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
            result.Add("Name", true, λ => λ.Name);
            result.Add("Charge", false, λ => λ.Charge);
            result.Add("Mass", false, λ => λ.Mz);
            result.Add("Comment", false, λ => λ.Comment);
            return result;
        }

        public int GetIcon()
        {
            // IMAGE
            return UiControls.ImageListOrder.Adduct;
        }
    }
}
