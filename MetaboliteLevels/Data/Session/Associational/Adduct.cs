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
    class Adduct : IAssociational
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
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// IMPLEMENTS IVisualisable
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
        /// Constructor
        /// </summary>
        public Adduct(string name, int charge, decimal mz)
        {
            this._defaultName = name;
            this.Charge = charge;
            this.Mz = mz;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// Unused (can't be disabled)
        /// </summary>
        bool INameable.Enabled { get { return true; } set { } }

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
        /// IMPLEMENTS IVisualisable
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
        /// IMPLEMENTS IVisualisable
        /// </summary>
        public string DisplayName
        {
            get
            {
                return IVisualisableExtensions.FormatDisplayName(this);
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
        VisualClass IAssociational.VisualClass
        {
            get { return VisualClass.Adduct; }
        }

        /// <summary>
        /// OVERRIDES Object
        /// </summary>
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>
        void IAssociational.RequestContents(ContentsRequest request)
        {
            switch (request.Type)
            {
                case VisualClass.Peak:
                    request.Text = "Peak using {0}.";
                    request.AddRange(Annotations.Select(z => z.Peak));
                    break;

                case VisualClass.Cluster:
                    break;

                case VisualClass.Compound:
                    request.Text = "Compounds using {0}.";
                    request.AddRange(Annotations.Select(z => z.Compound));
                    break;

                case VisualClass.Adduct:
                    break;

                case VisualClass.Pathway:
                    break;

                case VisualClass.Annotation:
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
        IEnumerable<Column> IVisualisable.GetColumns(Session.Core core)
        {                                                
            List<Column<Adduct>> result = new List<Column<Adduct>>();

            result.Add("Name", EColumn.Visible, λ => λ.DefaultDisplayName);
            result.Add("Comment", EColumn.None, λ => λ.Comment);
            result.Add("Charge", EColumn.None, λ => λ.Charge);
            result.Add("Mass", EColumn.None, λ => λ.Mz);
            result.Add("Annotations", EColumn.None, λ => λ.Annotations);

            core._annotationsMeta.ReadAllColumns(z => z.MetaInfo, result);

            return result;
        }

        /// <summary>
        /// IMPLEMENTS IVisualisable
        /// </summary>              
        UiControls.ImageListOrder IVisualisable.GetIcon()
        {
            // IMAGE
            return UiControls.ImageListOrder.Adduct;
        }
    }
}
