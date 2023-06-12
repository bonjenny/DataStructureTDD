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
        private MyLinkedList<KeyValuePair<TKey, TValue>>[] _bucket;
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
            get { return GetValue(key, true); }
            set { SetValue(key, value, false); }
        }

        // METHODS =======================================================
        internal int GetBucketIndex(TKey key, int bucketSize)
        {
            var hash = _equalityComparer.GetHashCode(key) & 0x7fffffff;
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
            if (list == null) return null;
            return list.Find((n) => _equalityComparer.Equals(n.Data.Key, key));
        }

        internal TValue GetValue(TKey key, bool raiseError)
        {
            var node = FindEntry(key);
            if (node != null) return node.Data.Value;
            if (!raiseError) return default(TValue);
            throw new ArgumentException("The key doesn't exist in the Dictionary.", key.ToString());
        }

        private void Resize()
        {
            var newSize = HashHelpers.GetPrime(_bucket.Length + HashHelpers.PRIME_FACTOR);
            var newBucket = new MyLinkedList<KeyValuePair<TKey, TValue>>[newSize];

            for (int i = 0; i < _bucket.Length; i++) {
                var list = _bucket[i];
                if (list == null) continue;
                foreach (var item in list) {
                    int index = GetBucketIndex(item.Key, newSize);
                    newBucket[index] = newBucket[index] ?? new MyLinkedList<KeyValuePair<TKey, TValue>>(_equalityComparer);
                    newBucket[index].AddLast(item);
                }
            }
            this._bucket = newBucket;
        }

        internal bool SetValue(TKey key, TValue value, bool raiseError)
        {
            if (_count >= _bucket.Length * HashHelpers.RESIZE_FACTOR) Resize();

            int index = GetBucketIndex(key, _bucket.Length);
            var list = _bucket[index];

            if (list == null) {
                // TODO: 해당 버킷에 이미 만들어진 연결리스트가 없다면 새로 만들고 버킷에 할당한다.
            }
            else {
                var node = // TODO: EqualityComparer를 이용하여 list에 key와 같은 중복된 항목이 있는지 찾는다.
            if (node != null) { // 중복된 값이 있는 경우
                    if (raiseError) {
                        throw new ArgumentException("An element with the same key already exists in the Dictionary.", key.ToString());
                    }

                    // 기존에 저장되어 있던 값을 새로 설정되는 값으로 변경한다.
                    node.Data = new KeyValuePair<TKey, TValue>(key, value);
                    return false;
                }
            }

            // TODO: 연결리스트의 마지막에 해당 항목을 추가하고 카운트값을 하나 늘린다.

            return true;
        }

        public bool Add(T item)
        {
            if (_count >= _bucket.Length * HashHelpers.RESIZE_FACTOR) Resize();

            int index = GetBucketIndex(item, _bucket.Length);
            var list = _bucket[index] = _bucket[index] ?? new MyLinkedList<T>(_equalityComparer);

            if (list.Contains(item)) return false;
            list.AddLast(item);

            this._count++;
            return true;
        }

        public void Remove(T item)
        {
            MyLinkedList<T> list = FindBucketList(item);
            if (list == null) return;

            var nodeToBeRemoved = list.Find(item);
            if (nodeToBeRemoved == null) return;

            list.Remove(nodeToBeRemoved);
            this._count--;
        }

        // IEnumerable 구현 ===============================================
        public IEnumerator<T> GetEnumerator()=> new MyDictionaryEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private class MyDictionaryEnumerator : IEnumerator<T>
        {
            private int _index;
            private MyDictionary<T> _hset;
            private IEnumerator<T> _iterator;

            public MyDictionaryEnumerator(MyDictionary<T> hset)
            {
                this._index = 0;
                this._hset = hset;
                this._iterator = FindNextEnumerator();
            }

            public T Current => _iterator.Current;
            object IEnumerator.Current => this.Current;

            public void Dispose() { }

            public void Reset()
            {
                _index = 0;
                _iterator = FindNextEnumerator();
            }

            private IEnumerator<T> FindNextEnumerator()
            {
                while (_index < _hset._bucket.Length) {
                    var list = _hset._bucket[_index++];
                    if (list == null) continue;
                    if (list.Count > 0)
                        return list.GetEnumerator();
                }
                return null;
            }

            public bool MoveNext()
            {
                while (_iterator != null && !_iterator.MoveNext()) {
                    _iterator = FindNextEnumerator();
                }
                return _iterator != null;
            }
        }
    }
}