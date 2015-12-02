using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MetaboliteLevels.Settings
{
    class DefaultValueBag
    {
        private List<Value> _values = new List<Value>();
        private Dictionary<string, object> _defaultValueBag;
        private string _formId;

        public DefaultValueBag(Dictionary<string, object> defaultValueBag, Form form)
        {
            this._defaultValueBag = defaultValueBag;
            this._formId = form.GetType().Name;
        }

        public void Add<T>(string id, Func<T> getter, Action<T> setter)
        {
            _values.Add(new Value<T>(_formId + "\\" + id, _defaultValueBag, getter, setter));
        }

        public void LoadFromBag()
        {
            foreach (Value v in _values)
            {
                v.LoadFromBag();
            }
        }

        public void SaveToBag()
        {
            foreach (Value v in _values)
            {
                v.SaveToBag();
            }
        }

        private abstract class Value
        {
            private readonly string _id;
            private readonly Dictionary<string, object> _defaultValueBag;

            public Value(string id, Dictionary<string, object> bag)
            {
                this._id = id;
                this._defaultValueBag = bag;
            }

            internal void LoadFromBag()
            {
                object v;

                if (_defaultValueBag.TryGetValue(_id, out v))
                {
                    Load(v);
                }
            }

            internal void SaveToBag()
            {
                _defaultValueBag[_id] = Save();
            }

            protected abstract object Save();
            protected abstract void Load(object v);
        }

        private class Value<T> : Value
        {
            private readonly Func<T> Getter;
            private readonly Action<T> Setter;

            public Value(string id, Dictionary<string, object> bag, Func<T> getter, Action<T> setter)
                : base(id, bag)
            {
                this.Getter = getter;
                this.Setter = setter;
            }

            protected override object Save()
            {
                return Getter();
            }

            protected override void Load(object v)
            {
                Setter((T)v);
            }
        }
    }
}
