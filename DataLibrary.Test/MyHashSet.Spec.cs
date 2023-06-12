using DataStructure;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tests
{
    public class MyHashSetTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void AddTest1()
        {
            // �ʱ� ũ�⸦ 3���� �����ؼ� �߰��� Resizing�� �ǵ��� �׽�Ʈ �Ѵ�.
            var x = new MyHashSet<int>(3);
            Assert.IsTrue(x.Add(10));
            Assert.IsTrue(x.Add(2));
            Assert.IsTrue(x.Add(30));
            Assert.IsTrue(x.Add(4));
            Assert.IsTrue(x.Add(50));
            Assert.IsFalse(x.Add(30));
            // => false. �̹� �ߺ��� ���̹Ƿ� �߰����� �ʴ´�.

            foreach (var item in x) {
                Console.WriteLine(item);
            }

            var xEnumerator = x.GetEnumerator();
            while (xEnumerator.MoveNext()) {
                Console.WriteLine(xEnumerator.Current);
            }
        }
    }
}