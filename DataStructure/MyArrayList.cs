using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class MyArrayList
    {
        private object[] _array;   // 할당된 배열을 가리키는 참조변수
        private int _size;         // 현재 저장된 원소 개수

        // 생성자
        public MyArrayList()
            : this(4) // 아래 생성자 대리호출
        {
        }

        public MyArrayList(int capacity)
        {
            this._size = 0;
            this._array = new object[capacity];
        }

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
                var newArray = new object[value];

                // 기존 원소를 새로운 배열로 복사
                Array.Copy(_array, 0, newArray, 0, this._size);

                // 할당된 배열을 가리키는 내부 참조변수를 새로운 배열로 변경
                this._array = newArray;
            }
        }

        // 외부에서 배열 요소에 접근을 위한 인덱서 프로퍼티
        public object this[int index]
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

        private void EnsureCapacity()
        {
            int capacity = _array.Length;
            if (_size >= capacity) {
                this.Capacity = (capacity == 0 ? 4 : capacity * 2);
            }
        }

        // 배열의 마지막에 원소 추가
        public void Add(object element)
        {
            // 배열 공간 체크, 부족할 시 resize
            EnsureCapacity();

            // 원소 추가
            _array[_size] = element;
            _size++;
        }

        // 해당 위치에 원소 추가
        public void Insert(int index, object element)
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

        public void Swap(int i, int j)
        {
            if (i < 0 || i >= _size) throw new IndexOutOfRangeException();
            if (j < 0 || j >= _size) throw new IndexOutOfRangeException();

            var temp = _array[i];
            _array[i] = _array[j];
            _array[j] = temp;
        }

        public object[] ToArray()
        {
            var array = new object[_size];
            Array.Copy(_array, 0, array, 0, _size);
            return array;
        }

        public bool Contains(object item)
        {
            for (int index = 0; index < this._size; index++) {
                // if (this._array[index] == item) => Sometimes you will get a wrong result
                if (this._array[index].Equals(item))
                    return true;
            }
            return false;
        }

        public int IndexOf(object item)
        {
            return IndexOf(item, 0, _size);
        }

        public int IndexOf(object item, int startIndex)
        {
            return IndexOf(item, startIndex, _size - startIndex);
        }

        public int IndexOf(object item, int startIndex, int count)
        {
            if (startIndex < 0 || startIndex > this._size) throw new IndexOutOfRangeException();
            if (count < 0 || count + startIndex > this._size) throw new IndexOutOfRangeException();

            int endIndex = startIndex + count;

            for (int i = startIndex; i < endIndex; i++) {
                if (_array[i].Equals(item)) return i;
            }
            return -1;
        }

        public int LastIndexOf(object item)
        {
            return LastIndexOf(item, _size - 1, _size);
        }

        public int LastIndexOf(object item, int startIndex)
        {
            return LastIndexOf(item, startIndex, startIndex + 1);
        }

        public int LastIndexOf(object item, int startIndex, int count)
        {
            if (startIndex < 0 || startIndex >= this._size) throw new IndexOutOfRangeException();
            if (count < 0 || startIndex + 1 < count) throw new IndexOutOfRangeException();

            int endIndex = startIndex - count;

            for (int i = startIndex; i > endIndex; i--) {
                if (_array[i].Equals(item)) return i;
            }
            return -1;
        }

        public bool Remove(object item)
        {
            var index = IndexOf(item);
            if (index >= 0) {
                RemoveAt(index);
                return true;
            }
            return false;
        }
    }
}
