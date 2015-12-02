using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Utilities
{   
    class BiDictionary<T, U> : IEnumerable<KeyValuePair<T, U>>
    {
        Dictionary<T, U> _tu = new Dictionary<T, U>();
        Dictionary<U, T> _ut = new Dictionary<U, T>();

        public BiDictionary()
        {
            // NA
        }

        public BiDictionary(IEnumerable<KeyValuePair<T, U>> values)
        {
            foreach (var kvp in values)
            {
                Add(kvp.Key, kvp.Value);
            }
        }

        public BiDictionary(IEnumerable<KeyValuePair<U, T>> values)
        {
            foreach (var kvp in values)
            {
                Add(kvp.Key, kvp.Value);
            }
        }  

        public int Count
        {
            get
            {
                return _tu.Count;
            }
        }

        public bool Contains(T t)
        {
            return _tu.ContainsKey(t);
        }

        public bool Contains(U u)
        {
            return _ut.ContainsKey(u);
        }

        public bool TryGetValue(T t, out U u)
        {
            return _tu.TryGetValue(t, out u);
        }

        public bool TryGetValue(U u, out T t)
        {
            return _ut.TryGetValue(u, out t);
        }

        public void Clear()
        {
            _tu.Clear();
            _ut.Clear();
        }

        public ICollection<T> Keys { get { return _tu.Keys; } }

        public ICollection<U> Values { get { return _ut.Keys; } }

        public void RemoveKeys(Predicate<T> predicate)
        {
            List<T> toRemove = new List<T>();

            foreach (T t in _tu.Keys)
            {
                if (predicate(t))
                {
                    toRemove.Add(t);
                }
            }

            foreach (T t in toRemove)
            {
                Remove(t);
            }
        }

        public void RemoveValues(Predicate<U> predicate)
        {
            List<U> toRemove = new List<U>();

            foreach (U u in _ut.Keys)
            {
                if (predicate(u))
                {
                    toRemove.Add(u);
                }
            }

            foreach (U u in toRemove)
            {
                Remove(u);
            }
        }

        public bool Remove(T t)
        {
            if (_tu.ContainsKey(t))
            {
                _ut.Remove(_tu[t]);
                _tu.Remove(t);
                return true;
            }

            return false;
        }              

        public bool Remove(U u)
        {
            if (_ut.ContainsKey(u))
            {
                _tu.Remove(_ut[u]);
                _ut.Remove(u);
                return true;
            }

            return false;
        }
        
        public void Add(T t, U u)
        {
            _tu.Add(t, u);
            _ut.Add(u, t);
        }

        public void Add(U u, T t)
        {
            _tu.Add(t, u);
            _ut.Add(u, t);
        }

        public IEnumerator<KeyValuePair<T, U>> GetEnumerator()
        {
            return _tu.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public U this[T t]
        {
            get
            {
                return _tu[t];
            }
            set
            {
                if (_tu.ContainsKey(t))
                {
                    if (_ut.ContainsKey(value))
                    {
                        _tu[t] = value;
                        _ut[value] = t;
                    }
                    else
                    {
                        throw new KeyNotFoundException("Key not found: " + value.ToString() + " in U->T.");
                    }
                }
                else
                {
                    Add(t, value);
                }
            }
        }

        public T this[U u]
        {
            get
            {
                return _ut[u];
            }
            set
            {
                if (_ut.ContainsKey(u))
                {
                    if (_tu.ContainsKey(value))
                    {
                        _ut[u] = value;
                        _tu[value] = u;
                    }
                    else
                    {
                        throw new KeyNotFoundException("Key not found: " + value.ToString() + " in T->U.");
                    }
                }
                else
                {
                    Add(u, value);
                }
            }
        }
    }
}
