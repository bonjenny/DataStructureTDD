using DataStructure;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tests
{
    public class MyStackTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void PushTest()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);

            foreach (var item in stack)
                Console.WriteLine(item);
        }

        [Test]
        public void PeekTest1()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);

            Assert.AreEqual(stack.Peek(), 30);
        }

        [Test]
        public void PopTest1()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);

            foreach (var item in stack)
                Console.WriteLine(item);

            Assert.AreEqual(stack.Pop(), 30);

            foreach (var item in stack)
                Console.WriteLine(item);
        }
    }
}