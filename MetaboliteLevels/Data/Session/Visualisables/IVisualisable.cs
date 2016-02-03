using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Utilities;
using MetaboliteLevels.Viewers.Lists;

namespace MetaboliteLevels.Data.Visualisables
{
    interface ITitlable
    {
        /// <summary>
        /// Displayed name of this item.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Assigned name of this item.
        /// </summary>
        string DefaultDisplayName { get; }

        /// <summary>
        /// User overriden name of this item
        /// </summary>
        string OverrideDisplayName { get; set; }

        /// <summary>
        /// Comments applied to this item.
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// Is this object enabled?
        /// </summary>
        bool Enabled { get; set; }
    }

    /// <summary>
    /// Stuff that can appear in lists.
    /// Peaks...clusters...adducts...metabolites...pathways.
    /// </summary>
    interface IVisualisable : ITitlable
    {
        /// <summary>
        /// Icon for this item (as an index - see [UiControls]).
        /// </summary>
        UiControls.ImageListOrder GetIcon();

        /// <summary>
        /// VisualClass of IVisualisable.
        /// </summary>
        VisualClass VisualClass { get; }

        /// <summary>
        /// STATIC
        /// Gets columns
        /// </summary>
        IEnumerable<Column> GetColumns(Core core);

        /// <summary>
        /// Gets related items.
        /// </summary>
        void RequestContents(ContentsRequest list);
    }

    /// <summary>
    /// Classes able to provide previews for IVisualisable derivatives.
    /// </summary>
    interface IPreviewProvider
    {
        /// <summary>
        /// Provides a preview for the specified IVisualisable.
        /// </summary>
        Image ProvidePreview(Size size, IVisualisable p, IVisualisable p2);
    }

    /// <summary>
    /// Methods for IVisualisable.
    /// </summary>
    static class IVisualisableExtensions
    {
        // Can be used with QueryProperty to force search for specifics.
        public const string SYMBOL_PROPERTY = "prop:";

        /// <summary>
        /// Retrieves a request for contents with the specified parameters.
        /// </summary>
        public static ContentsRequest GetContents(this IVisualisable self, Core core, VisualClass type)
        {
            ContentsRequest cl = new ContentsRequest(core, self, type);
            self.RequestContents(cl);
            return cl;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>    
        internal static string GetDisplayName(string userTitle, string defaultName)
        {
            return string.IsNullOrEmpty(userTitle) ? defaultName : userTitle;
        }

        /// <summary>
        /// Gets the display name, or "nothing" if null.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetDisplayName(this IVisualisable self)
        {
            if (self == null)
            {
                return "nothing";
            }

            return self.DisplayName;
        }

        public static List<string> QueryProperties(this IVisualisable self, Core core)
        {
            List<string> result = new List<string>();
            result.AddRange(self.GetType().GetProperties().Select(z => SYMBOL_PROPERTY + z.Name));
            return result;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        public static object QueryProperty(this IVisualisable self, string property, Core core)
        {
            if (self == null)
            {
                return null;
            }

            bool isProperty = property.StartsWith(SYMBOL_PROPERTY);

            if (isProperty)
            {
                property = property.Substring(SYMBOL_PROPERTY.Length);
            }

            if (isProperty)
            {
                PropertyInfo p = self.GetType().GetProperty(property);

                if (p != null)
                {
                    return p.GetValue(self);
                }

                var f = self.GetType().GetField(property);

                if (f != null)
                {
                    return f.GetValue(self);
                }
            }

            Column column = ColumnManager.GetColumns(core, self).FirstOrDefault(z => z.Id.ToUpper() == property.ToUpper());

            if (column != null)
            {
                return column.GetRow(self);
            }   

            return "{Missing: " + property + "}";
        }
    }

    /// <summary>
    /// Request for contents from an IVisualisable.
    /// </summary>
    internal class ContentsRequest
    {
        /// <summary>
        /// Empty request.
        /// </summary>
        public static readonly ContentsRequest Empty = new ContentsRequest(null, null, (VisualClass)(-1));

        /// <summary>
        /// Request - Core
        /// </summary>
        public readonly Core Core;

        /// <summary>
        /// Request - Called upon
        /// </summary>
        public readonly IVisualisable Owner;

        /// <summary>
        /// Request - Type of results to get.
        /// </summary>
        public readonly VisualClass Type;

        /// <summary>
        /// Response - Title of the results.
        /// </summary>
        public string Text;

        /// <summary>
        /// Response - OBJECT and OPTIONAL EXTRA COLUMNS
        /// </summary>
        public readonly Dictionary<IVisualisable, object[]> Contents = new Dictionary<IVisualisable, object[]>();

        /// <summary>
        /// Response - titles of OPTIONAL EXTRA COLUMNS 
        /// </summary>
        private readonly List<Header> _headers = new List<Header>();

        public ContentsRequest(Core core, IVisualisable owner, VisualClass type)
        {
            this.Core = core;
            this.Owner = owner;
            this.Type = type;
        }

        public void AddHeader(string header, string description)
        {
            _headers.Add(new Header(header, description));
        }

        public void Add(IVisualisable item, params object[] meta)
        {
            Contents[item] = meta;
        }

        public IEnumerable<IVisualisable> Keys
        {
            get { return Contents.Keys; }
        }

        public void AddRange(IEnumerable<IVisualisable> items)
        {
            if (items != null)
            {
                foreach (IVisualisable item in items)
                {
                    Add(item);
                }
            }
        }

        internal IList<Header> Headers
        {
            get
            {
                return this._headers;
            }
        }

        internal void AddRangeWithCounts<T>(Counter<T> counts)
            where T : IVisualisable
        {
            foreach (var kvp in counts.Counts)
            {
                Add(kvp.Key, kvp.Value);
            }
        }

        public class Header
        {
            public readonly string Name;
            public readonly string Description;

            public Header(string header, string description)
            {
                this.Name = header;
                this.Description = description;
            }
        }
    }

    /// <summary>
    /// Field-value tuple.
    /// </summary>
    public class InfoLine
    {
        public string Field;
        public object Value;
        public bool IsMeta;

        public InfoLine(string p1, object p2, bool isMeta = false)
        {
            this.Field = p1;
            this.Value = p2;
            this.IsMeta = isMeta;
        }
    }

    /// <summary>
    /// Types of IVisualisable.
    /// </summary>
    public enum VisualClass
    {
        None,
        Peak,
        Cluster,
        Compound,
        Adduct,
        Pathway,
        Assignment,
        Annotation,
    }
}
