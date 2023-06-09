using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class MyStack<T> : IEnumerable<T>
    {
        private MyLinkedList<T> _list;
        private IEqualityComparer<T> _equalityComparer;

        // 생성자 =======================================================
        public MyStack(IEqualityComparer<T> equalityComparer = null)
        {
            this._equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
            _list = new MyLinkedList<T>(this._equalityComparer);
        }

        // PROPERTIES ===================================================
        public int Count => _list.Count;

        // METHODS =======================================================
        public void Push(T data) => _list.AddLast(data);
        public T Pop() => _list.RemoveLast();
        public T Peek() => _list.Last.Data;
        public void Clear() => _list.Clear();
        public T[] ToArray() => _list.ToArray();

        // IEnumerable 구현 ===============================================
        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
