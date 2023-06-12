﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class SLinkedNode<T>
    {
        public T Data;
        public SLinkedNode<T> Next;

        public SLinkedNode(T data)
        {
            this.Data = data;
        }

        public SLinkedNode(T data, SLinkedNode<T> next)
        {
            this.Data = data;
            this.Next = next;
        }
    }

    public class MySLinkedList<T> : IEnumerable<T>
    {
        private SLinkedNode<T> _head;
        private SLinkedNode<T> _tail;
        private int _size; // 현재 저장된 원소 개수

        #region [생성자]
        public MySLinkedList()
        {
        }
        #endregion

        #region [PROPERTIES]
        public int Count
        {
            get { return _size; }
        }

        public SLinkedNode<T> First
        {
            get { return _head; }
        }

        public SLinkedNode<T> Last
        {
            get { return _tail; }
        }
        #endregion

        #region [METHODS: AddFirst, AddLast]
        public void AddFirst(T data)
        {
            SLinkedNode<T> newNode = new SLinkedNode<T>(data, _head);

            // TAIL이 null이면 자료구조에 첫 데이터가 추가되는 것이다
            // 그러므로 HEAD와 TAIL이 같도록 TAIL도 설정한다
            if (_tail == null) {
                _head = _tail = newNode;
            }

            // HEAD를 새로 만든 노드로 변경한다
            else {
                _head = newNode;
            }

            _size++;
        }

        public void AddLast(T data)
        {
            SLinkedNode<T> newNode = new SLinkedNode<T>(data);
            
            // TAIL의 Next를 새로 만들어진 노드를 바라보게 한다
            if (_tail != null) {
                _tail.Next = newNode;
                _tail = newNode;
            }
            
            // TAIL이 null이면 자료구조에 첫 데이터가 추가되는 것이다
            // 그러므로 HEAD와 TAIL이 같도록 HEAD도 설정한다
            else {
                _head = _tail = newNode;
            }

            _size++;
        }
        #endregion

        #region [METHODS: RemoveFirst, Clear]
        public T RemoveFirst()
        {
            T result = default(T);

            if (_head != null) {
                result = _head.Data;

                // 1. HEAD 위치를 다음 node로 변경한다
                _head = _head.Next;
            
                // 2. 만약 변경된 HEAD가 null이면 TAIL도 null로 변경한다
                if (_head == null)  _tail = null;

                // 3. 삭제가 되었으므로 size를 하나 감소시킨다
                _size--;
            }

            return result;
        }

        public void Clear()
        {
            while (_head != null) { RemoveFirst(); }
        }
        #endregion

        #region [METHODS: ToArray]
        public T[] ToArray()
        {
            T[] objArray = new T[_size];
            int i = 0;

            // 연결리스트를 순회하며 배열에 값을 복사한다
            for (var currNode = _head; currNode != null; currNode = currNode.Next) {
                objArray[i++] = currNode.Data;
            }
            return objArray;
        }
        #endregion
        
        #region [IEnumerator<T> 구현]
        public IEnumerator<T> GetEnumerator()
        {
            return new MySLinkedListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class MySLinkedListEnumerator : IEnumerator<T>
        {
            private MySLinkedList<T> _list;
            private SLinkedNode<T> _node;
            private T _current;

            public MySLinkedListEnumerator(MySLinkedList<T> list)
            {
                this._list = list;
                this._node = list.First;
                this._current = default(T);
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public T Current
            {
                get { return _current; }
            }

            public bool MoveNext()
            {
                if (_node != null) {
                    // current와 node를 설정 
                    _current = _node.Data;
                    _node = _node.Next;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                // current와 node를 초기화 시킨다.
                _node = _list.First;
                _current = default(T);
            }

            public void Dispose()
            {
            }
        }
        #endregion
    }
}
