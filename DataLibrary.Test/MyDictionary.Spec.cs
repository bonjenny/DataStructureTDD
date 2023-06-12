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
            // �ʱ� ũ�⸦ 3���� �����ؼ� �߰��� Resizing�� �ǵ��� �׽�Ʈ �Ѵ�.
            var x = new MyDictionary<string, string>(3);
            x.Add("10", "101010");
            x.Add("2", "222222");
            x.Add("30", "303030");
            x.Add("4", "444444");
            x.Add("50", "505050");
            // x.Add("30", "808080"); //=> ���ܹ߻�. �̹� �ߺ��� ���̹Ƿ� ���� �߻�
            x["30"] = "808080";    //=> �߰��� �ƴ� �ش� Ű�� ���� ���� �����ϴ� ���̹Ƿ� ���� ���� �� ����
            x.Remove("2");

            // Console.WriteLine(x["80"]); //=> ���ܹ߻�. �߰����� ���� Ű�� �˻������� ������ �߻��Ѵ�.

            string result = null;
            if (x.TryGetValue("80", out result)) { //=> �߰����� ���� Ű�� �˻��ص� ������ �߻����� �ʴ´�.
                Console.WriteLine(result);
            }

            foreach (var key in x.Keys) { // Ű�� ���
                Console.WriteLine(key);
            }

            foreach (var value in x.Values) { // ���� ���
                Console.WriteLine(value);
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