using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class MyList<T> : IEnumerable<T>
    {
        private IEqualityComparer<T> _comparer;
        private T[] _array;   // 할당된 배열을 가리키는 참조변수
        private int _size;         // 현재 저장된 원소 개수

        #region [생성자]
        public MyList(IEqualityComparer<T> equalityComparer = null)
            : this(4, equalityComparer) // 아래 생성자 대리호출
        {
        }

        public MyList(int capacity, IEqualityComparer<T> equalityComparer = null)
        {
            this._size = 0;
            this._array = new T[capacity];
            this._comparer = equalityComparer ?? EqualityComparer<T>.Default;
            //this._comparer = equalityComparer != null ? equalityComparer : EqualityComparer<T>.Default;
        }
        #endregion

        #region [Count, Capacity, EnsureCapacity]
        public int Count
        {
            get { return _size; } // 읽기전용
        }

        public int Capacity
        {
            get { return _array.Length; }
            set {
                if (value <= Capacity)
                    throw new ArgumentOutOfRangeException(); // 줄이지 못하게

                // TODO: 설정되는 크기로 새로운 배열 할당
                var newArray = new T[value];

                // 기존 원소를 새로운 배열로 복사
                Array.Copy(_array, 0, newArray, 0, this._size);

                // 할당된 배열을 가리키는 내부 참조변수를 새로운 배열로 변경
                this._array = newArray;
            }
        }

        private void EnsureCapacity()
        {
            int capacity = _array.Length;
            if (_size >= capacity) {
                this.Capacity = (capacity == 0 ? 4 : capacity * 2);
            }
        }
        #endregion

        #region [Index]
        // 외부에서 배열 요소에 접근을 위한 인덱서 프로퍼티
        public T this[int index]
        {
            get {
                if (index < 0 || index >= _size)
                    throw new IndexOutOfRangeException();
                return _array[index];
            }
            set {
                if (index < 0 || index >= _size)
                    throw new IndexOutOfRangeException();
                _array[index] = value;
            }
        }
        #endregion

        #region [Add, Insert]
        // 배열의 마지막에 원소 추가
        public void Add(T element)
        {
            // 배열 공간 체크, 부족할 시 resize
            EnsureCapacity();

            // 원소 추가
            _array[_size] = element;
            _size++;
        }

        // 해당 위치에 원소 추가
        public void Insert(int index, T element)
        {
            // 배열 공간 체크, 부족할 시 resize
            EnsureCapacity();

            // 맨 뒤에서부터 추가되려고 하는 위치까지 한칸씩 뒤로 데이터 이동
            for (int i = _size; i > index; i--) {
                _array[i] = _array[i - 1];
            }

            // 원소 추가
            _array[index] = element;
            _size++;
        }
        #endregion

        #region [RemoveAt, RemoveRange, Clear, Remove]
        // 해당 위치의 원소 삭제
        public void RemoveAt(int index)
        {
            RemoveRange(index, 1);
        }

        public void RemoveRange(int index, int count)
        {
            if (index < 0) throw new IndexOutOfRangeException();
            if (count < 0) throw new IndexOutOfRangeException();
            if (index + count > _size) throw new IndexOutOfRangeException();

            _size -= count;

            // 삭제하려는 위치부터 x칸씩 앞으로 데이터 이동
            for (int i = index; i < _size; i++) {
                _array[i] = _array[i + count];
            }
        }

        public void Clear()
        {
            // 2. Array.Clear(_array, 0, _size);
            // 3. RemoveRange(0, _size);
            if (_size != 0) _size = 0; // 1. size를 0으로 만들기
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index >= 0) {
                RemoveAt(index);
                return true;
            }
            return false;
        }
        #endregion

        #region [CopyTo]
        public void CopyTo(Array array)
        {
            CopyTo(array, 0);
        }

        public void CopyTo(Array array, int arrayIndex)
        {
            if (arrayIndex < 0) throw new IndexOutOfRangeException();
            if (arrayIndex > _size) throw new IndexOutOfRangeException();
            Array.Copy(array, arrayIndex, _array, 0, array.Length);
        }
        #endregion

        #region [Swap, ToArray, Contains]
        public void Swap(int i, int j)
        {
            if (i < 0 || i >= _size) throw new IndexOutOfRangeException();
            if (j < 0 || j >= _size) throw new IndexOutOfRangeException();

            var temp = _array[i];
            _array[i] = _array[j];
            _array[j] = temp;
        }

        public T[] ToArray()
        {
            var array = new T[_size];
            Array.Copy(_array, 0, array, 0, _size);
            return array;
        }
        
        public bool Contains(T item)
        {
            for (int index = 0; index < this._size; index++) {
                // if (this._array[index] == item) => Sometimes you will get a wrong result
                //if (this._array[index].Equals(item))
                if (_comparer.Equals(_array[index], item))
                    return true;
            }
            return false;
        }
        #endregion

        #region [IndexOf, LastIndexOf]
        public int IndexOf(T item)
        {
            return IndexOf(item, 0, _size);
        }

        public int IndexOf(T item, int startIndex)
        {
            return IndexOf(item, startIndex, _size - startIndex);
        }

        public int IndexOf(T item, int startIndex, int count)
        {
            if (startIndex < 0 || startIndex > this._size) throw new IndexOutOfRangeException();
            if (count < 0 || count + startIndex > this._size) throw new IndexOutOfRangeException();

            int endIndex = startIndex + count;

            for (int i = startIndex; i < endIndex; i++) {
                if (_comparer.Equals(_array[i], item)) return i;
            }
            return -1;
        }

        public int LastIndexOf(T item)
        {
            return LastIndexOf(item, _size - 1, _size);
        }

        public int LastIndexOf(T item, int startIndex)
        {
            return LastIndexOf(item, startIndex, startIndex + 1);
        }

        public int LastIndexOf(T item, int startIndex, int count)
        {
            int endIndex = startIndex - count + 1;

            if (startIndex < 0 || startIndex >= this._size) throw new IndexOutOfRangeException();
            if (count < 0 || count > startIndex + 1) throw new IndexOutOfRangeException();

            for (int i = startIndex; i >= endIndex; i--) {
                if (_comparer.Equals(_array[i], item)) return i;
            }
            return -1;
        }
        #endregion

        #region [BinarySearch]
        public int BinarySearch(T item)
        {
            return BinarySearch(item, Comparer<T>.Default);
        }

        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return Array.BinarySearch<T>(_array, 0, _size, item, comparer);
        }
        #endregion

        #region [Sort]
        public void Sort()
        {
            Sort(Comparer<T>.Default);
        }

        public void Sort(IComparer<T> comparer)
        {
            Array.Sort<T>(_array, 0, _size, comparer);
        }
        #endregion

        #region [IEnumerable, IEnumerable<T> 구현]
        public IEnumerator<T> GetEnumerator()
        {
            return new MyListEnumerator(this);
        }

        // IEnumerable<T> 인터페이스는 IEnumerable에서 상속되었으므로 IEnumerable 인터페이스에 대한 구현도 해줘야 한다.
        // 파라메터가 동일한 중복된 이름의 GetEnumerator() 메소드가 2개 있을 수 없으므로 IEnumerable 인터페이스의 메소드임을 명시해준다.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        // 내부에서만 사용가능하게 private 중첩 객체로 구현함.
        // 호출하는 쪽은 IEnumerator<T> 인터페이스를 사용하기 때문에 MyListEnumerator<T>를 밖으로 노출 할 이유가 없다.
        private class MyListEnumerator : IEnumerator<T>
        {
            private MyList<T> _list;
            private int _index;

            public MyListEnumerator(MyList<T> list)
            {
                this._list = list;
                this._index = -1;
            }

            public bool MoveNext()
            {
                return (++_index < this._list.Count);
            }

            public void Reset()
            {
                this._index = -1;
            }

            public void Dispose()
            {
            }

            object IEnumerator.Current {
                get { return Current; }
            }

            public T Current
            {
                get {
                    if (this._index >= this._list.Count) throw new IndexOutOfRangeException();
                    return this._list[_index];
                }
            }
        }
        #endregion
    }
}
