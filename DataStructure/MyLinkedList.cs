using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class LinkedNode<T>
    {
        public T Data;
        public LinkedNode<T> Prev;
        public LinkedNode<T> Next;

        public LinkedNode(T data)
        {
            this.Data = data;
        }

        public LinkedNode(T data, LinkedNode<T> prev, LinkedNode<T> next)
        {
            this.Data = data;
            this.Prev = prev;
            this.Next = next;
        }
    }

    public class MyLinkedList<T> : IEnumerable<T>
    {
        private LinkedNode<T> _head;
        private LinkedNode<T> _tail;
        private IEqualityComparer<T> _equalityComparer;
        private int _size; // 현재 저장된 원소 개수

        #region [생성자]
        public MyLinkedList(IEqualityComparer<T> equalityComparer = null)
        {
            this._equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
        }
        #endregion

        #region [PROPERTIES]
        public int Count
        {
            get { return _size; }
        }

        public LinkedNode<T> First
        {
            get { return _head; }
        }

        public LinkedNode<T> Last
        {
            get { return _tail; }
        }
        #endregion

        #region [METHODS: AddFirst, AddLast]
        public void AddFirst(T data)
        {
            LinkedNode<T> newNode = new LinkedNode<T>(data);

            // HEAD가 null이면 자료구조에 첫 데이터가 추가되는 것이다
            if (_head == null) {
                _tail = _head = newNode;
            }

            // 이전 HEAD의 Prev를 새로 만들어진 노드를 바라보게 한다
            // HEAD의 Next는 이전의 HEAD를 바라보게 한다
            else {
                _head.Prev = newNode;
                newNode.Next = _head;
            }

            _head = newNode;
            _size++;
        }

        public void AddLast(T data)
        {
            LinkedNode<T> newNode = new LinkedNode<T>(data);

            // TAIL이 null이면 자료구조에 첫 데이터가 추가되는 것이다
            if (_tail == null) {
                _head = _tail = newNode;
            }

            // 이전 TAIL의 Next를 새로 만들어진 노드를 바라보게 한다
            // TAIL의 Prev는 이전의 TAIL을 바라보게 한다
            else {
                _tail.Next = newNode;
                newNode.Prev = _tail;
            }

            _tail = newNode;
            _size++;
        }
        #endregion

        #region [METHODS: Remove, RemoveFirst, Clear]
        public void Remove(LinkedNode<T> node)
        {
            // 1-1. node가 HEAD이자 TAIL: 본인이 마지막 노드
            if (node == _head && node == _tail) {
                _head = _tail = null;
                return;
            }
            
            // 2-1. node가 HEAD면 HEAD를 node로 변경
            // 2-2. HEAD가 아니면 node.Prev.Next의 위치를 node의 Next로 변경
            if (node == _head) _head = node.Next;
            else node.Prev.Next = node.Next;

            // 3-1. node가 TAIL이면 TAIL를 node로 변경
            // 3-2. node.Next.Prev의 위치를 node의 Prev로 변경
            if (node == _tail) _tail = node.Prev;
            else node.Next.Prev = node.Prev;

            // 3. 삭제가 되었으므로 size를 하나 감소시킨다
            _size--;
        }

        public bool Remove(T data)
        {
            var nodeToBeRemoved = Find(data);
            if (nodeToBeRemoved == null) return false;
            Remove(nodeToBeRemoved);
            return true;
        }

        public bool Remove(Predicate<LinkedNode<T>> match)
        {
            var nodeToBeRemoved = Find(match);
            if (nodeToBeRemoved == null) return false;
            Remove(nodeToBeRemoved);
            return true;
        }

        public T RemoveFirst()
        {
            T result = default(T);

            // 비어있는지 아닌지 체크
            if (_head != null) {
                result = _head.Data;
                Remove(_head);
            }

            return result;
        }

        public T RemoveLast()
        {
            T result = default(T);

            // 비어있는지 아닌지 체크
            if (_tail != null) {
                result = _tail.Data;
                Remove(_tail);
            }

            return result;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            _size = 0;
        }
        #endregion

        #region [Contains, Find, FindLast]
        public bool Contains(T data)
        {
            var node = Find(data);
            if (node == null) return false;
            return true;
        }

        public bool Contains(Predicate<LinkedNode<T>> match)
        {
            var node = Find(match);
            if (node == null) return false;
            return true;
        }

        public LinkedNode<T> Find(T data)
        {
            for (var currNode = _head; currNode != null; currNode = currNode.Next) {
                if (_equalityComparer.Equals(currNode.Data, data)) return currNode;
            }
            return null;
        }

        public LinkedNode<T> Find(Predicate<LinkedNode<T>> match)
        {
            for (var currNode = _head; currNode != null; currNode = currNode.Next) {
                if (match(currNode)) return currNode;
            }
            return null;
        }

        public LinkedNode<T> FindLast(T data)
        {
            for (var currNode = _tail; currNode != null; currNode = currNode.Prev) {
                if (_equalityComparer.Equals(currNode.Data, data)) return currNode;
            }
            return null;
        }

        public LinkedNode<T> FindLast(Predicate<LinkedNode<T>> match)
        {
            for (var currNode = _tail; currNode != null; currNode = currNode.Prev) {
                if (match(currNode)) return currNode;
            }
            return null;
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
            return new MyLinkedListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class MyLinkedListEnumerator : IEnumerator<T>
        {
            private MyLinkedList<T> _list;
            private LinkedNode<T> _node;
            private T _current;

            public MyLinkedListEnumerator(MyLinkedList<T> list)
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
