using DataStructure;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tests
{
    public class MyLinkedListTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddTest1()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(30);

            foreach (var item in list) {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void RemoveFirstTest1()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(30);

            foreach (var item in list) {
                Console.WriteLine(item);
            }

            list.RemoveFirst();

            foreach (var item in list) {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void RemoveLastTest1()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(30);

            foreach (var item in list) {
                Console.WriteLine(item);
            }

            list.RemoveLast();

            foreach (var item in list) {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void RemoveTest1()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(30);

            foreach (var item in list) {
                Console.WriteLine(item);
            }

            list.Remove(20);

            foreach (var item in list) {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void RemoveTest2()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(30);

            foreach (var item in list) {
                Console.WriteLine(item);
            }

            list.Remove(x => x.Data == 20);

            foreach (var item in list) {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void ClearTest1()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(30);

            foreach (var item in list) {
                Console.WriteLine(item);
            }

            list.Clear();

            foreach (var item in list) {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void ContainsTest1()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(30);

            foreach (var item in list) {
                Console.WriteLine(item);
            }

            Assert.IsTrue(list.Contains(20));
            Assert.IsFalse(list.Contains(40));
        }

        [Test]
        public void ContainsTest2()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(30);

            foreach (var item in list) {
                Console.WriteLine(item);
            }

            Assert.IsTrue(list.Contains(x => x.Data == 20));
            Assert.IsFalse(list.Contains(x => x.Data == 40));
        }

        [Test]
        public void FindTest1()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(20);

            foreach (var item in list) {
                Console.WriteLine(item);
            }

            Assert.AreEqual(list.Find(20).Data, 20);
            Assert.AreEqual(list.Find(20).Next.Data, 20);
        }

        [Test]
        public void FindTest2()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(20);

            foreach (var item in list) {
                Console.WriteLine(item);
            }

            Assert.AreEqual(list.Find(x => x.Data == 20).Data, 20);
            Assert.AreEqual(list.Find(x => x.Data == 20).Next.Data, 20);
        }

        [Test]
        public void FindLastTest1()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(20);

            foreach (var item in list) {
                Console.WriteLine(item);
            }

            Assert.AreEqual(list.FindLast(20).Data, 20);
            Assert.AreEqual(list.FindLast(20).Next, null);
        }

        [Test]
        public void FindLastTest2()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(20);

            foreach (var item in list) {
                Console.WriteLine(item);
            }

            Assert.AreEqual(list.FindLast(x => x.Data == 20).Data, 20);
            Assert.AreEqual(list.FindLast(x => x.Data == 20).Next, null);
        }

        [Test]
        public void ToArrayTest1()
        {
            MyLinkedList<int> list = new MyLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(30);

            foreach (var item in list.ToArray()) {
                Console.WriteLine(item);
            }
        }
    }
}