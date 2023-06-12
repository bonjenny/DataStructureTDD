using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    internal static class HashHelpers
    {
        private static readonly int[] _primes = new int[] {
            3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919
        };

        public static int PRIME_FACTOR = 4;
        public static decimal RESIZE_FACTOR = 1.25M;

        public static int GetPrime(int min)
        {
            for (int index = 0; index < _primes.Length; index++) {
                int prime = _primes[index];
                if (prime >= min)
                    return prime;
            }
            return min;
        }
    }

    public class MyHashSet<T> : IEnumerable<T>
    {

        private MyLinkedList<T>[] _bucket;
        private IEqualityComparer<T> _equalityComparer;
        private int _count;

        // 생성자 =======================================================
        public MyHashSet(IEqualityComparer<T> equalityComparer = null) : this(3, equalityComparer) { }
        public MyHashSet(int capacity, IEqualityComparer<T> equalityComparer = null)
        {
            int size = HashHelpers.GetPrime(capacity);
            this._bucket = new MyLinkedList<T>[size];
            this._equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
        }

        // PROPERTIES ===================================================
        public int Count => this._count;

        // METHODS =======================================================
        private int GetBucketIndex(T item, int bucketSize)
        {
            var hash = _equalityComparer.GetHashCode(item) & 0x7fffffff;
            return hash % bucketSize;
        }

        private MyLinkedList<T> FindBucketList(T item)
        {
            int index = GetBucketIndex(item, _bucket.Length);
            return this._bucket[index];
        }

        private void Resize()
        {
            var newSize = HashHelpers.GetPrime(_bucket.Length + HashHelpers.PRIME_FACTOR);
            var newBucket = new MyLinkedList<T>[newSize];

            for (int i = 0; i < _bucket.Length; i++) {
                var list = _bucket[i];
                if (list == null) continue;
                foreach (var item in list) {
                    int index = GetBucketIndex(item, newSize);
                    newBucket[index] = newBucket[index] ?? new MyLinkedList<T>(_equalityComparer);
                    newBucket[index].AddLast(item);
                }
            }
            this._bucket = newBucket;
        }

        public bool Contains(T item)
        {
            var list = FindBucketList(item);
            if (list == null) return false;
            return list.Contains(item);
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
        public IEnumerator<T> GetEnumerator()=> new MyHashSetEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private class MyHashSetEnumerator : IEnumerator<T>
        {
            private int _index;
            private MyHashSet<T> _hset;
            private IEnumerator<T> _iterator;

            public MyHashSetEnumerator(MyHashSet<T> hset)
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