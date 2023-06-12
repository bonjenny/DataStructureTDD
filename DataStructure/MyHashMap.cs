using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class MyHashMap<TValue> : IEnumerable<KeyValuePair<string, MyList<TValue>>>
    {
        private MyDictionary<string, MyList<TValue>> _dict;
        private MyList<string> _keyList;
        private int _count;

        // 생성자 =======================================================
        public MyHashMap(IEqualityComparer<string> equalityComparer = null) : this(3, equalityComparer) { }
        public MyHashMap(int capacity, IEqualityComparer<string> equalityComparer = null)
        {
            var comparer = equalityComparer ?? StringComparer.OrdinalIgnoreCase;
            this._dict = new MyDictionary<string, MyList<TValue>>(capacity, comparer);
            this._keyList = new MyList<string>(capacity, comparer);
        }

        // PROPERTIES ===================================================
        public int Count => this._keyList.Count;

        public TValue this[int index]
        {
            get => GetValue(_keyList[index]);
            set => SetValue(_keyList[index], value);
        }

        public TValue this[string key]
        {
            get => GetValue(key);
            set => SetValue(key, value);
        }

        //public IEnumerable<string> Keys => new MyMapKeyCollection(this._dict.Values);
        public IEnumerable<string> Keys => this._keyList;
        public IEnumerable<TValue> Values => GetAllValues();

        // METHODS =======================================================
        
        public TValue[] GetAllValues()
        {
            MyList<TValue> values = new MyList<TValue>();
            foreach (string key in this._keyList) {
                values.Add(GetValue(key));
            }
            return values.ToArray();
        }

        public TValue[] GetValues(string key)
        {
            var list = _dict.GetValue(key, false);
            if (list == null) return Array.Empty<TValue>();
            return list.ToArray();
        }

        protected TValue GetValue(string key)
        {
            var list = _dict.GetValue(key, false);
            if (list == null) return default(TValue);
            return list[0];
        }

        protected void SetValue(string key, TValue value)
        {
            if (_dict[key] == null) _keyList.Add(key);
            var list = _dict[key] = _dict[key] ?? new MyList<TValue>();
            list.Add(value);
        }
        public void Add(string key, TValue value) => SetValue(key, value);

        public bool Remove(string key)
        {
            if (!Remove(key)) return false;
            _keyList.Remove(key);
            return true;
        }

        // IEnumerable<T> 구현 ===============================================
        public IEnumerator<KeyValuePair<string, MyList<TValue>>> GetEnumerator() => _dict.GetEnumerator();
        // IEnumerable 구현 ===============================================
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        //// MyDictCollectionBase Class : IEnumerable<T> =============================================
        //private abstract class MyMapCollectionBase<TCurrent> : IEnumerable<TCurrent>
        //{
        //    protected MyList<TValue> _values;
        //    protected MyMapCollectionBase(MyList<TValue> values) => this._values = values;
        //    public abstract IEnumerator<TCurrent> GetEnumerator();
        //    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        //}

        //// MyDictKeyEnumerator Class : MyDictCollectionBase =========================================
        //private class MyMapKeyCollection : MyMapCollectionBase<string>
        //{
        //    public MyMapKeyCollection(MyList<TValue> values) : base(values) { }
        //    public override IEnumerator<string> GetEnumerator() => _values.GetEnumerator();
        //}
    }
}