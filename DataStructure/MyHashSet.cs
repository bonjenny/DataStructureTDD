using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class MyHashSet
    {
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
                var hash = (_equalityComparer.GetHashCode() & 0x7fffffff) % bucketSize;
                return hash % bucketSize;
            }

            private MyLinkedList<T> FindBucketList(T item)
            {
                int index = GetBucketIndex(item, _bucket.Length);
                return this._bucket[index];
            }

            private void Resize(int capacity)
            {
                // 새로운 크기로 배열 새로 할당
                var newSize = HashHelpers.GetPrime(capacity);
                var newBucket = new MyLinkedList<T>[newSize];

                // 기존 버킷배열에 저장되어 있는 연결리스트 항목을 순회한다.(루프)
                for (int i = 0; i < _count; i++) {
                    var list = // TODO...
                    if (list != null)
                    {
                        foreach (var item in list)
                        {
                            // TODO: 현재 항목을 바뀐 배열 크기로 재해싱하여 버킷의 인덱스를 구한다.
                            // 해당 버킷에 이미 만들어진 연결리스트가 없다면 새로 만들고 버킷에 할당한다.
                            // 연결리스트에 현재 항목을 추가한다.
                        }
                    }
            }

            // 새로운 배열로 버킷(배열) 변수 정보 변경
            // TODO: _버켓은 new버켓
        }

        public bool Contains(T item)
        {
            var list = FindBucketList(item);
            if (list == null)
                return false;

            return list.Contains(item);
        }

        public bool Add(T item)
        {
            // 현재 데이터 개수가 해시 버킷 개수의 125% 가 넘으면 리사이징한다.
            if (_count >= _bucket.Length * HashHelpers.RESIZE_FACTOR)
            {
                Resize(_bucket.Length + HashHelpers.PRIME_FACTOR);
            }

            int index = GetBucketIndex(item, _bucket.Length);
            var list = _bucket[index];

            // TODO: 해당 버킷에 이미 만들어진 연결리스트가 없다면 새로 만들고 버킷에 할당한다.
            // 그렇지 않으면 연결리스트에 해당 항목이 이미 포함되어 있는지 검사 후 이미 추가된 값이면 false를 리턴한다.
            // 연결리스트의 마지막에 해당 항목을 추가하고 카운트값을 하나 늘린다.

            return true;
        }

        public void Remove(T item)
        {
            MyLinkedList<T> list = FindBucketList(item);

            if (list != null)
            {
                // TODO: 연결리스트에서 해당 항목을 찾은 후 있다면
                // 해당 노드를 연결리스트에서 삭제하고 카운트값을 하나 줄인 후 true 리턴.
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyHashSetEnumerator(this);
        }

        // IEnumerable 인터페이스 구현
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        private class MyHashSetEnumerator : IEnumerator<T>
        {
            private MyHashSet<T> _hset;
            private IEnumerator<T> _iterator;
            private int _index;

            public MyHashSetEnumerator(MyHashSet<T> hset)
            {
                this._hset = hset;
                this._index = 0;
                this._iterator = FindNextEnumerator();
            }


            // IEnumerator<T>
            //_________________________________________________________________________________________

            public T Current
            {
                get { return _iterator.Current; }
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public void Dispose()
            {
            }

            public void Reset()
            {
                _index = 0;
                _iterator = FindNextEnumerator();
            }

            private IEnumerator<T> FindNextEnumerator()
            {
                // TODO: 현재 인덱스가 해시셋의 버킷배열의 크기보다 작을때까지 반복한다.
                // 버킷배열에 할당된 연결리스트를 가져온 후 현재 인덱스를 하나 증가시킨다.
                // 연결리스트가 존재하고 리스트에 추가되어 있는 항목의 갯수가 0보다 크다면
                // 연결리스트의 GetEnumerator() 결과를 리턴한다.
                while (_index < _hset._bucket.Length)
                {
                    //TODO:
                }
            }

            public bool MoveNext()
            {
                // _iterator가 null이 아니고 _iterator의 MoveNext() 결과값이 false 일때까지
                // FindNextEnumerator를 호출하여 다음 버킷에 있는 연결리스트를 찾는다.
                while (TODO:..) {

                }

                // IEnumerator가 null이 아니면 MoveNext()가 성공한 것 이므로 Current를 호출할 수 있다.
                return _iterator != null;
            }
        }

        internal static class HashHelpers
        {
            private static readonly int[] _primes = new int[] {
                3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919
            };

            public static int PRIME_FACTOR = 4;
            public static decimal RESIZE_FACTOR = 1.25M;

            public static int GetPrime(int min)
            {
                for (int index = 0; index < _primes.Length; index++)
                {
                    int prime = _primes[index];
                    if (prime >= min)
                        return prime;
                }
                return min;
            }
        }
    }
}