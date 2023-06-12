using DataStructure;
using NUnit.Framework;
using System.Collections;

namespace Tests
{
    public class MyArrayListTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddTest1()
        {
            var list = new MyArrayList();
            list.Add('a');
            list.Add('b');
            list.Add('c');
            list.Add('d');
            list.Add('e');

            Assert.AreEqual(list.Count, 5);
            Assert.AreEqual(list[0], 'a');
        }

        [Test]
        public void InsertTest1()
        {
            var list = new MyArrayList();
            list.Add('a');
            list.Add('b');
            list.Add('c');
            list.Add('d');
            list.Add('e');

            list.Insert(2, 'z');

            Assert.AreEqual(list[1], 'b');
            Assert.AreEqual(list[2], 'z');
            Assert.AreEqual(list[3], 'c');
        }

        [Test]
        public void RemoveAtTest1()
        {
            var list = new MyArrayList();
            list.Add('a');
            list.Add('b');
            list.Add('c');
            list.Add('d');
            list.Add('e');

            list.RemoveAt(2);

            Assert.AreEqual(list[1], 'b');
            Assert.AreEqual(list[2], 'd');
            Assert.AreEqual(list[3], 'e');
        }

        [Test]
        public void RemoveRangeTest1()
        {
            var list = new MyArrayList();
            list.Add('a');
            list.Add('b');
            list.Add('c');
            list.Add('d');
            list.Add('e');

            list.RemoveRange(2, 2);

            Assert.AreEqual(list[0], 'a');
            Assert.AreEqual(list[1], 'b');
            Assert.AreEqual(list[2], 'e');
        }

        [Test]
        public void ClearTest1()
        {
            var list = new MyArrayList();
            list.Add('a');
            list.Add('b');
            list.Add('c');
            list.Add('d');
            list.Add('e');

            list.Clear();

            Assert.AreEqual(list.Count, 0);
        }

        [Test]
        public void SwapTest1()
        {
            var list = new MyArrayList();
            list.Add('a');
            list.Add('b');
            list.Add('c');
            list.Add('d');
            list.Add('e');

            list.Swap(1, 2);

            Assert.AreEqual(list[1], 'c');
            Assert.AreEqual(list[2], 'b');
        }

        [Test]
        public void ContainsTest1()
        {
            var list = new MyArrayList();
            list.Add('a');
            list.Add('b');
            list.Add('c');
            list.Add('d');
            list.Add('e');

            Assert.AreEqual(list.Contains('c'), true);
            Assert.AreEqual(list.Contains('x'), false);
            Assert.IsTrue(list.Contains('c'));
            Assert.IsFalse(list.Contains('x'));
        }

        [Test]
        public void IndexOfTest1()
        {
            var list = new MyArrayList();
            list.Add('a');
            list.Add('b');
            list.Add('c');
            list.Add('c');
            list.Add('e');

            Assert.AreEqual(list.IndexOf('c'), 2);
            Assert.AreEqual(list.IndexOf('e'), 4);
        }

        [Test]
        public void LastIndexOfTest1()
        {
            var list = new MyArrayList();
            list.Add('a');
            list.Add('b');
            list.Add('c');
            list.Add('c');
            list.Add('e');

            Assert.AreEqual(list.LastIndexOf('c'), 3);
            Assert.AreEqual(list.LastIndexOf('e'), 4);
        }

        [Test]
        public void RemoveTest1()
        {
            var list = new MyArrayList();
            list.Add('a');
            list.Add('b');
            list.Add('c');
            list.Add('d');
            list.Add('e');

            list.Remove('f');

            Assert.IsFalse(list.Contains('f'));
            Assert.AreEqual(list.IndexOf('f'), -1);
            
            Assert.IsFalse(list.Remove('f'));


            Assert.AreEqual(list.IndexOf('e'), 4);

            Assert.IsTrue(list.Remove('e'));

            Assert.IsFalse(list.Contains('e'));
            Assert.AreEqual(list.IndexOf('e'), -1);
        }
    }
}