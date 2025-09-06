using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 음료수_관리_프로그램__시험_
{
    internal class Drink
    {
        public string My_name { get; private set; }
        public string Drink_name { get; private set; }
        public int Drink_price { get; set; }

        public Drink(string my_name , string d_name, int d_price)
        {
            My_name = my_name;
            Drink_name = d_name;
            Drink_price = d_price;
        }

        public override string ToString()
        {
            return "[본인이름]" +My_name + "\t[음료수이름]" + Drink_name + "\t[가격]" + Drink_price;
        }
    }
}
