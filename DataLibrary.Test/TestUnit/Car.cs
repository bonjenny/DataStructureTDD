using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Car : IComparable<Car> // IComparable이 구현되지 않으면 Ascending by Year는 오류!
    {
        public int Year;
        public string Make;

        public Car(int year, string make)
        {
            this.Year = year;
            this.Make = make;
        }

        public int CompareTo(Car other)
        {
            return this.Year - other.Year;    // Ascending by Year
        }

        public override string ToString()
        {
            return string.Format($"{this.Year} {this.Make}");
        }
    }
}
