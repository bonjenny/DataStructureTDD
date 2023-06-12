using DataStructure;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tests
{
    public class MyHashMapTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void AddTest1()
        {
            // �ʱ� ũ�⸦ 3���� �����ؼ� �߰��� Resizing�� �ǵ��� �׽�Ʈ
            var x = new MyHashMap<string>(3);    
            x.Add("10", "101010");
            x.Add("2", "222222");
            x.Add("30", "303030");
            x.Add("4", "444444");
            x.Add("50", "505050");
            x.Add("30", "808080");         //=> ���ܹ߻�����. �̹� �ߺ��� Ű�� �ִ��� �׷��εǾ� �߰��ȴ�.

            Console.WriteLine(x[3]);       //=> 3��° �ε���, �� "4"�� ���� "444444"�� ��µȴ�.

            Console.WriteLine(x["80"]);    //=> �ش� Ű�� �߰��Ǿ� ���� �ʴ��� ���ܹ߻����� �ʴ´�.
            Console.WriteLine(x["30"]);
            Console.WriteLine(string.Join(", ", x.GetValues("30")));

            foreach (var item in x.Keys) {
                Console.WriteLine(item);
            }

            foreach (var item in x.GetAllValues()) {
                Console.WriteLine(item);
            }

            foreach (var item in x) { // Ű�� �� ���� ���
                Console.WriteLine(string.Format("{0} = {1}", item.Key, item.Value));
            }

            var xEnumerator = x.GetEnumerator();
            while (xEnumerator.MoveNext()) {
                Console.WriteLine(string.Format("{0} = {1}",
                    xEnumerator.Current.Key, xEnumerator.Current.Value));
            }
        }
    }
}