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

            //IEnumerable을 구현하지 않으면 foreach문 수행 불가능
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
            // 형식 비교자를 사용하는 경우와 그렇지 않는 경우의 사용예제
            var list = new MyList<Car>();

            list.Add(new Car(1992, "Ford"));
            list.Add(new Car(1999, "Buick"));
            list.Add(new Car(1997, "Honda"));

            list.Sort();   //=> Sorted by Year (Ascending - IComparable 사용)


            var iterator = list.GetEnumerator();

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(iterator.Current.ToString(), "1992 Ford");

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(iterator.Current.ToString(), "1997 Honda");

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(iterator.Current.ToString(), "1999 Buick");
            Assert.IsFalse(iterator.MoveNext());


            foreach (var item in list) {
                Console.WriteLine(item);  //=> Car 객체 내부의 값이 문자열로 출력될 수 있도록 Car객체의 ToString() 메소드를 재정의해야 함.
            }
        }

        [Test]
        public void SortTest2()
        {
            // 형식 비교자를 사용하는 경우와 그렇지 않는 경우의 사용예제
            var list = new MyList<Car>();

            list.Add(new Car(1992, "Ford"));
            list.Add(new Car(1999, "Buick"));
            list.Add(new Car(1997, "Honda"));

            list.Sort(new YearDescendingComparer());   //=> Sorted by Year (Descending - IComparable 사용)


            var iterator = list.GetEnumerator();

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(iterator.Current.ToString(), "1999 Buick");

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(iterator.Current.ToString(), "1997 Honda");

            Assert.IsTrue(iterator.MoveNext());
            Assert.AreEqual(iterator.Current.ToString(), "1992 Ford");

            Assert.IsFalse(iterator.MoveNext());


            foreach (var item in list) {
                Console.WriteLine(item);  //=> Car 객체 내부의 값이 문자열로 출력될 수 있도록 Car객체의 ToString() 메소드를 재정의해야 함.
            }
        }

        [Test]
        public void SortTest3()
        {
            var x = new MyList<string>(new StringIgnoreCaseComparer());
            x.Add("Samsung");
            x.Add("Hyundae");
            x.Add("LG");

            x.Sort();   //=> Sorted (Descending - IComparable 사용)
            
            foreach (var item in x) {
                Console.WriteLine(item);  //=> Car 객체 내부의 값이 문자열로 출력될 수 있도록 Car객체의 ToString() 메소드를 재정의해야 함.
            }
        }

        [Test]
        public void SortTest4()
        {
            var x = new MyList<string>(new StringIgnoreCaseComparer());
            x.Add("Samsung");
            x.Add("Hyundae");
            x.Add("LG");

            x.Sort(new StringDescendingComparer());   //=> Sorted (Ascending - IComparable 사용)

            foreach (var item in x) {
                Console.WriteLine(item);  //=> Car 객체 내부의 값이 문자열로 출력될 수 있도록 Car객체의 ToString() 메소드를 재정의해야 함.
            }
        }
    }

    public class StringIgnoreCaseComparer : IEqualityComparer<string>
    {
        // 생산년도와 제조사가 모두 같아야 같은 차로 인식한다
        public bool Equals(string x, string y)
        {
            return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
        }

        // 생산년도와 제조사 값 모두를 이용하여 해싱코드를 생성한다
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