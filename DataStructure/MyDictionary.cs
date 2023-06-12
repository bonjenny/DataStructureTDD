using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        internal MyLinkedList<KeyValuePair<TKey, TValue>>[] _bucket;
        private IEqualityComparer<TKey> _equalityComparer;
        private int _count;

        // 생성자 =======================================================
        public MyDictionary(IEqualityComparer<TKey> equalityComparer = null): this(3, equalityComparer) { }
        public MyDictionary(int capacity, IEqualityComparer<TKey> equalityComparer = null)
        {
            int size = HashHelpers.GetPrime(capacity);
            this._bucket = new MyLinkedList<KeyValuePair<TKey, TValue>>[size];
            this._equalityComparer = equalityComparer ?? EqualityComparer<TKey>.Default;
        }

        // PROPERTIES ===================================================
        public int Count => this._count;

        public TValue this[TKey key]
        {
            get => GetValue(key, true);
            set => SetValue(key, value, false);
        }

        public IEnumerable<TKey> Keys => new MyDictKeyCollection(this);
        public IEnumerable<TValue> Values => new MyDictValueCollection(this);

        // METHODS =======================================================
        internal int GetBucketIndex(TKey key, int bucketSize)
        {
            int hash = _equalityComparer.GetHashCode(key) & 0x7fffffff;
            return hash % bucketSize;
        }

        internal MyLinkedList<KeyValuePair<TKey, TValue>> FindBucketList(TKey key)
        {
            int index = GetBucketIndex(key, _bucket.Length);
            return this._bucket[index];
        }

        internal LinkedNode<KeyValuePair<TKey, TValue>> FindEntry(TKey key)
        {
            var list = FindBucketList(key);
            return FindEntry(list, key);
        }

        internal LinkedNode<KeyValuePair<TKey, TValue>> FindEntry(MyLinkedList<KeyValuePair<TKey, TValue>> list, TKey key)
        {
            if (list == null) return null;
            return list.Find((n) => _equalityComparer.Equals(n.Data.Key, key));
        }

        private void Resize() => Resize(_bucket.Length + HashHelpers.PRIME_FACTOR);
        private void Resize(int capacity)
        {
            var newSize = HashHelpers.GetPrime(capacity);
            var newBucket = new MyLinkedList<KeyValuePair<TKey, TValue>>[newSize];

            for (int i = 0; i < _bucket.Length; i++)
            {
                var list = _bucket[i];
                if (list == null) continue;
                foreach (var item in list)
                {
                    int index = GetBucketIndex(item.Key, newSize);
                    newBucket[index] = newBucket[index] ?? new MyLinkedList<KeyValuePair<TKey, TValue>>();
                    newBucket[index].AddLast(item);
                }
            }
            this._bucket = newBucket;
        }

        internal TValue GetValue(TKey key, bool raiseError = true)
        {
            var node = FindEntry(key);
            if (node != null) return node.Data.Value;
            if (!raiseError) return default(TValue);
            throw new ArgumentException("The key doesn't exist in the Dictionary.", key.ToString());
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var node = FindEntry(key);

            if (node != null) {
                value = node.Data.Value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        internal bool SetValue(TKey key, TValue value, bool raiseError = true)
        {
            if (_count >= _bucket.Length * HashHelpers.RESIZE_FACTOR) Resize();

            int index = GetBucketIndex(key, _bucket.Length);
            var list = _bucket[index] = _bucket[index] ?? new MyLinkedList<KeyValuePair<TKey, TValue>>();

            var node = FindEntry(key);
            if (node != null) {
                if (raiseError) throw new ArgumentException("An element with the same key already exists in the Dictionary.", key.ToString());
                node.Data = new KeyValuePair<TKey, TValue>(key, value);
                return false;
            }

            list.AddLast(new KeyValuePair<TKey, TValue>(key, value));
            this._count++;
            return true;
        }
        public bool Add(TKey key, TValue value) => SetValue(key, value);

        public bool Remove(TKey key)
        {
            var list = FindBucketList(key);

            //var nodeToBeRemoved = FindEntry(list, key);
            //if (nodeToBeRemoved == null) return false;
            if (!list.Remove(item => _equalityComparer.Equals(item.Data.Key, key))) return false;

            this._count--;
            return true;
        }

        // IEnumerable<T> 구현 ===============================================
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => new MyDictionaryEnumerator(this);

        // IEnumerable 구현 ===============================================
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        // NESTED Helper Class : MyDictEnumeratorBase ============================================
        private class MyDictionaryEnumerator : MyDictEnumeratorBase<KeyValuePair<TKey, TValue>>
        {
            public MyDictionaryEnumerator(MyDictionary<TKey, TValue> dict) : base(dict) { }
            public override KeyValuePair<TKey, TValue> Current => _iterator.Current;
        }

        // MyDictEnumeratorBase Class : IEnumerator<T> ============================================
        private abstract class MyDictEnumeratorBase<TCurrent> : IEnumerator<TCurrent>
        {
            protected int _index;
            protected MyDictionary<TKey, TValue> _dict;
            protected IEnumerator<KeyValuePair<TKey, TValue>> _iterator;

            public MyDictEnumeratorBase(MyDictionary<TKey, TValue> dict)
            {
                this._index = 0;
                this._dict = dict;
                this._iterator = FindNextEnumerator();
            }

            protected IEnumerator<KeyValuePair<TKey, TValue>> FindNextEnumerator()
            {
                while (_index < _dict._bucket.Length) {
                    var list = _dict._bucket[_index++];
                    if (list == null) continue;
                    if (list.Count > 0)
                        return list.GetEnumerator();
                }
                return null;
            }

            // IDisposable =============================================
            public void Dispose() { }

            // IEnumerator<T> =============================================
            public abstract TCurrent Current { get; }

            // IEnumerator =============================================
            object IEnumerator.Current => this.Current;

            public void Reset()
            {
                _index = 0;
                _iterator = FindNextEnumerator();
            }

            public bool MoveNext()
            {
                while (_iterator != null && !_iterator.MoveNext()) {
                    _iterator = FindNextEnumerator();
                }
                return _iterator != null;
            }
        }

        // MyDictKeyEnumerator Class : MyDictEnumeratorBase =========================================
        private class MyDictKeyEnumerator : MyDictEnumeratorBase<TKey>
        {
            public MyDictKeyEnumerator(MyDictionary<TKey, TValue> dict) : base(dict) { }
            public override TKey Current => _iterator.Current.Key;
        }

        // MyDictCollectionBase Class : IEnumerable<T> =============================================
        private abstract class MyDictCollectionBase<TCurrent> : IEnumerable<TCurrent>
        {
            protected MyDictionary<TKey, TValue> _dict;
            protected MyDictCollectionBase(MyDictionary<TKey, TValue> dict) => this._dict = dict;
            public abstract IEnumerator<TCurrent> GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }

        // MyDictKeyEnumerator Class : MyDictCollectionBase =========================================
        private class MyDictKeyCollection : MyDictCollectionBase<TKey>
        {
            public MyDictKeyCollection(MyDictionary<TKey, TValue> dict) : base(dict) { }
            public override IEnumerator<TKey> GetEnumerator() => new MyDictKeyEnumerator(_dict);

            private class MyDictKeyEnumerator : MyDictEnumeratorBase<TKey>
            {
                public MyDictKeyEnumerator(MyDictionary<TKey, TValue> dict) : base(dict) { }
                public override TKey Current => _iterator.Current.Key;
            }
        }

        // MyDictValueCollection Class : MyDictCollectionBase =========================================
        private class MyDictValueCollection : MyDictCollectionBase<TValue>
        {
            public MyDictValueCollection(MyDictionary<TKey, TValue> dict) : base(dict) { }
            public override IEnumerator<TValue> GetEnumerator() => new MyDictValueEnumerator(_dict);

            private class MyDictValueEnumerator : MyDictEnumeratorBase<TValue>
            {
                public MyDictValueEnumerator(MyDictionary<TKey, TValue> dict) : base(dict) { }
                public override TValue Current => _iterator.Current.Value;
            }
        }
    }
}