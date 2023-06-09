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
            // 초기 크기를 3으로 시작해서 중간에 Resizing이 되도록 테스트 한다.
            var x = new MyHashSet<int>(3);
            Assert.IsTrue(x.Add(10));
            Assert.IsTrue(x.Add(2));
            Assert.IsTrue(x.Add(30));
            Assert.IsTrue(x.Add(4));
            Assert.IsTrue(x.Add(50));
            Assert.IsFalse(x.Add(30));
            // => false. 이미 중복된 값이므로 추가되지 않는다.

            //foreach (var item in x) {
            //    Console.WriteLine(item);
            //}

            var xEnumerator = x.GetEnumerator();
            while (xEnumerator.MoveNext()) {
                Console.WriteLine(xEnumerator.Current);
            }
        }
    }
}