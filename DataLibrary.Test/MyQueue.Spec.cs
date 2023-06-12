using DataStructure;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tests
{
    public class MyQueueTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void EnqueueTest1()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);

            foreach (var item in queue)
                Console.WriteLine(item);
        }

        [Test]
        public void PeekTest1()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);

            Assert.AreEqual(queue.Peek(), 10);
        }

        [Test]
        public void DequeueTest1()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);

            foreach (var item in queue)
                Console.WriteLine(item);

            Assert.AreEqual(queue.Dequeue(), 10);

            foreach (var item in queue)
                Console.WriteLine(item);
        }
    }
}