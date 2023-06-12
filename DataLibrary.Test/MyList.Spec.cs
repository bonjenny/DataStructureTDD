using DataStructure;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tests
{
    public class MyListTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddTest1()
        {
            var list = new MyList<char>();
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
            var list = new MyList<char>();
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
            var list = new MyList<char>();
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
            var list = new MyList<char>();
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
            var list = new MyList<char>();
            list.Add('a');
            list.Add('b');
            list.Add('c');
            list.Add('d');
            list.Add('e');

            list.Clear();

            Assert.AreEqual(list.Count, 0);
        }

        [Test]
        public void ContainsTest1()
        {
            var list = new MyList<char>();
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
            var list = new MyList<char>();
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
            var list = new MyList<char>();
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
            var list = new MyList<char>();
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

        [Test]
        public void ContainsTest2()
        {
            var x = new MyList<string>();
            x.Add("Samsung");
            x.Add("Hyundae");
            x.Add("LG");

            Assert.IsFalse(x.Contains("hyundae")); // -> false
            Assert.IsTrue(x.Contains("Hyundae")); // -> true

            CheckValue("hello");
            CheckValue("Hello");
            CheckValue("HELLO");
        }

        private bool CheckValue(string value)
        {
            //if (value == "hello") return true;
            //if (value == "hello" || value == "Hello") return true;
            //if (value.ToUpper() == "HELLO") return true;
            if (value.Equals("hello", StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }

        [Test]
        public void ContainsTest3()
        {
            var x = new MyList<string>(new StringIgnoreCaseComparer());
            x.Add("Samsung");
            x.Add("Hyundae");
            x.Add("LG");

            for (int i = 0; i < x.Count; i++) {
                Console.WriteLine(x[i]);
            }

            //IEnumerable�� �������� ������ foreach�� ���� �Ұ���
            foreach (var item in x) { 
                Console.WriteLine(item);
            }

            IEnumerator<string> iterator = x.GetEnumerator();

            Assert.IsTrue(iterator.MoveNext()); // -> true
            Assert.IsTrue(iterator.MoveNext()); // -> true
            Assert.IsTrue(iterator.MoveNext()); // -> true
            Assert.IsFalse(iterator.MoveNext()); // -> false


            IEnumerable<string> enumerable = x as IEnumerable<string>;
            iterator.Reset();

            while (iterator.MoveNext()) {
                string item = iterator.Current;
                Console.WriteLine(item);
            }

            Assert.IsTrue(x.Contains("hyundae")); // -> false
            Assert.IsTrue(x.Contains("Hyundae")); // -> true
        }

        [Test]
        public void SortTest1()
        {
            // ���� ���ڸ� ����ϴ� ���� �׷��� �ʴ� ����� ��뿹��
            var list = new MyList<Car>();

            list.Add(new Car(1992, "Ford"));
            list.Add(new Car(1999, "Buick"));
            list.Add(new Car(1997, "Honda"));

            list.Sort();   //=> Sorted by Year (Ascending - IComparable ���)


            var iterator = list.GetEnumerator();

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(iterator.Current.ToString(), "1992 Ford");

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(iterator.Current.ToString(), "1997 Honda");

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(iterator.Current.ToString(), "1999 Buick");
            Assert.IsFalse(iterator.MoveNext());


            foreach (var item in list) {
                Console.WriteLine(item);  //=> Car ��ü ������ ���� ���ڿ��� ��µ� �� �ֵ��� Car��ü�� ToString() �޼ҵ带 �������ؾ� ��.
            }
        }

        [Test]
        public void SortTest2()
        {
            // ���� ���ڸ� ����ϴ� ���� �׷��� �ʴ� ����� ��뿹��
            var list = new MyList<Car>();

            list.Add(new Car(1992, "Ford"));
            list.Add(new Car(1999, "Buick"));
            list.Add(new Car(1997, "Honda"));

            list.Sort(new YearDescendingComparer());   //=> Sorted by Year (Descending - IComparable ���)


            var iterator = list.GetEnumerator();

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(iterator.Current.ToString(), "1999 Buick");

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(iterator.Current.ToString(), "1997 Honda");

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(iterator.Current.ToString(), "1992 Ford");

            Assert.IsFalse(iterator.MoveNext());


            foreach (var item in list) {
                Console.WriteLine(item);  //=> Car ��ü ������ ���� ���ڿ��� ��µ� �� �ֵ��� Car��ü�� ToString() �޼ҵ带 �������ؾ� ��.
            }
        }

        [Test]
        public void SortTest3()
        {
            var x = new MyList<string>(new StringIgnoreCaseComparer());
            x.Add("Samsung");
            x.Add("Hyundae");
            x.Add("LG");

            x.Sort();   //=> Sorted (Descending - IComparable ���)
            
            foreach (var item in x) {
                Console.WriteLine(item);  //=> Car ��ü ������ ���� ���ڿ��� ��µ� �� �ֵ��� Car��ü�� ToString() �޼ҵ带 �������ؾ� ��.
            }
        }

        [Test]
        public void SortTest4()
        {
            var x = new MyList<string>(new StringIgnoreCaseComparer());
            x.Add("Samsung");
            x.Add("Hyundae");
            x.Add("LG");

            x.Sort(new StringDescendingComparer());   //=> Sorted (Ascending - IComparable ���)

            foreach (var item in x) {
                Console.WriteLine(item);  //=> Car ��ü ������ ���� ���ڿ��� ��µ� �� �ֵ��� Car��ü�� ToString() �޼ҵ带 �������ؾ� ��.
            }
        }

        [Test]
        public void ContainsTest4()
        {
            var x = new MyList<string>(new StringIgnoreCaseComparer());
            x.Add("Samsung");
            x.Add("Hyundae");
            x.Add("LG");

            Assert.IsTrue(x.Contains<string>(item => item.StartsWith("S")));
            Assert.IsFalse(x.Contains<string>(item => item.StartsWith("A")));
        }

        [Test]
        public void FindTest1()
        {
            var x = new MyList<string>(new StringIgnoreCaseComparer());
            x.Add("Samsung");
            x.Add("Hyundae");
            x.Add("LG");

            Assert.AreEqual(x.Find<string>(item => item.StartsWith("S")), "Samsung");
            Assert.AreEqual(x.Find<string>(item => item.StartsWith("A")), null);

            Assert.AreEqual(x.FindIndex<string>(item => item.StartsWith("S")), 0);
            Assert.AreEqual(x.FindIndex<string>(1, item => item.StartsWith("S")), -1);
        }

        [Test]
        public void FindLastTest1()
        {
            var x = new MyList<string>(new StringIgnoreCaseComparer());
            x.Add("Samsung");
            x.Add("Hyundae");
            x.Add("LG");

            Assert.AreEqual(x.FindLast<string>(item => item.StartsWith("S")), "Samsung");
            Assert.AreEqual(x.FindLast<string>(item => item.StartsWith("A")), null);

            Assert.AreEqual(x.FindLastIndex<string>(item => item.StartsWith("S")), 0);
            Assert.AreEqual(x.FindLastIndex<string>(1, item => item.StartsWith("L")), -1);
        }

        [Test]
        public void RemoveTest2()
        {
            var x = new MyList<string>(new StringIgnoreCaseComparer()) {
                "Samsung",
                "Hyundae",
                "LG"
            };

            Assert.IsTrue(x.Remove<string>(item => item.StartsWith("S")));
            foreach (var item in x) {
                Console.WriteLine(item);
            }

            Assert.IsFalse(x.Remove<string>(item => item.StartsWith("A")));
            foreach (var item in x) {
                Console.WriteLine(item);
            }

            x = new MyList<string>(new StringIgnoreCaseComparer()) {
                "Samsung",
                "Hyundae",
                "LG"
            };

            Assert.AreEqual(x.RemoveAll<string>(item => item.Contains("a")), 2);
            Assert.AreEqual(x[0], "LG");
        }
    }

    public class StringIgnoreCaseComparer : IEqualityComparer<string>
    {
        // ����⵵�� �����簡 ��� ���ƾ� ���� ���� �ν��Ѵ�
        public bool Equals(string x, string y)
        {
            return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
        }

        // ����⵵�� ������ �� ��θ� �̿��Ͽ� �ؽ��ڵ带 �����Ѵ�
        public int GetHashCode(string obj)
        {
            return obj.GetHashCode() ^ obj.GetHashCode();
        }
    }

    public class YearDescendingComparer : IComparer<Car>
    {
        public int Compare(Car x, Car y)
        {
            return y.Year - x.Year;
        }
    }

    public class StringDescendingComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.Compare(y, x);
        }
    }
}