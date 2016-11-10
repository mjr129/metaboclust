using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MetaboliteLevels.Data.Session.Associational;
using MetaboliteLevels.Gui.Controls.Lists;

namespace MetaboliteLevels.Utilities
{
    [Serializable]
    internal sealed class MetaInfoCollection
    {
        private string[] _contents = new string[0];

        public void Write(MetaInfoHeader headers, string key, string value)
        {
            int index = headers.GetOrCreateIndex(key);

            if (_contents.Length < index + 1)
            {
                Array.Resize(ref _contents, index + 1);
            }
                                     
            _contents[index] = value;
        }

        public string Read(int index)
        {
            if (index >= _contents.Length)
            {
                return null;
            }

            return _contents[index];
        }
    }

    [Serializable]
    internal sealed class MetaInfoHeader
    {
        private readonly Dictionary<string, int> _indices = new Dictionary<string, int>();
        private string[] _headers = new string[0];

        internal int GetOrCreateIndex(string key)
        {
            int index;

            if (!_indices.TryGetValue(key, out index))
            {
                index = _headers.Length;
                Array.Resize(ref _headers, _headers.Length + 1);
                _headers[index] = key;
                _indices.Add(key, index);
            }

            return index;
        }

        internal int GetIndex( string key )
        {
            int index;

            if (!_indices.TryGetValue( key, out index ))
            {
                return -1;
            }

            return index;
        }

        public string[] Headers
        {
            get { return _headers; }
        }      

        internal void ReadAllColumns<T>(Converter<T, MetaInfoCollection> collectionRetriever, ColumnCollection columns)
            where T : Visualisable
        {
            for (int index = 0; index < this._headers.Length; index++)
            {
                string header = this._headers[index];
                int __closure = index;
                const string description = "This column represents meta data provided by the user.";
                columns.Add(new Column<T>("Meta\\" + header, EColumn.IsMeta, description, λ => collectionRetriever(λ).Read(__closure), null));
            }
        }
                   
        internal string GetValue(MetaInfoCollection collection, string key)
        {
            int index;

            if (!_indices.TryGetValue(key, out index))
            {
                return null;
            }

            return collection.Read(index);
        }
    }
}
