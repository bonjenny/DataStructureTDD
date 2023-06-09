using DataStructure;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tests
{
    public class MySLinkedListTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddRemoveTest1()
        {
            MySLinkedList<int> list = new MySLinkedList<int>();

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
        public void ClearTest1()
        {
            MySLinkedList<int> list = new MySLinkedList<int>();

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
        public void ToArrayTest1()
        {
            MySLinkedList<int> list = new MySLinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(30);

            foreach (var item in list.ToArray()) {
                Console.WriteLine(item);
            }
        }
    }
}