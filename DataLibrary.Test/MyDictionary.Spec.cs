using DataStructure;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tests
{
    public class MyDictionaryTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void AddTest1()
        {
            // 초기 크기를 3으로 시작해서 중간에 Resizing이 되도록 테스트 한다.
            var x = new MyDictionary<string, string>(3);
            x.Add("10", "101010");
            x.Add("2", "222222");
            x.Add("30", "303030");
            x.Add("4", "444444");
            x.Add("50", "505050");
            // x.Add("30", "808080"); //=> 예외발생. 이미 중복된 값이므로 오류 발생
            x["30"] = "808080";    //=> 추가가 아닌 해당 키에 대한 값을 설정하는 것이므로 오류 없이 값 변경
            x.Remove("2");

            // Console.WriteLine(x["80"]); //=> 예외발생. 추가되지 않은 키로 검색했으로 오류가 발생한다.

            string result = null;
            if (x.TryGetValue("80", out result)) { //=> 추가되지 않은 키로 검색해도 오류가 발생하지 않는다.
                Console.WriteLine(result);
            }

            foreach (var key in x.Keys) { // 키만 출력
                Console.WriteLine(key);
            }

            foreach (var value in x.Values) { // 값만 출력
                Console.WriteLine(value);
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