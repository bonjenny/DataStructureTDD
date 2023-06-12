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
            // 초기 크기를 3으로 시작해서 중간에 Resizing이 되도록 테스트
            var x = new MyHashMap<string>(3);    
            x.Add("10", "101010");
            x.Add("2", "222222");
            x.Add("30", "303030");
            x.Add("4", "444444");
            x.Add("50", "505050");
            x.Add("30", "808080");         //=> 예외발생없음. 이미 중복된 키가 있더라도 그룹핑되어 추가된다.

            Console.WriteLine(x[3]);       //=> 3번째 인덱스, 즉 "4"의 값인 "444444"가 출력된다.

            Console.WriteLine(x["80"]);    //=> 해당 키가 추가되어 있지 않더라도 예외발생하지 않는다.
            Console.WriteLine(x["30"]);
            Console.WriteLine(string.Join(", ", x.GetValues("30")));

            foreach (var item in x.Keys) {
                Console.WriteLine(item);
            }

            foreach (var item in x.GetAllValues()) {
                Console.WriteLine(item);
            }

            foreach (var item in x) { // 키와 값 쌍을 출력
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