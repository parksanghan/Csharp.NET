using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _231023_클라이언트_시험
{
    internal class Drink
    {
        public string name { get; private set; }
        public int price { get; set; }

        public string owner;

        public Drink(string name, int price)
        {
            this.name = name;
            this.price = price;
            owner = null;
        }

        public Drink(string owner, string name, int price)
        {
            this.owner = owner;
            this.name = name;
            this.price = price;
            
        }

        public override string ToString()
        {
            return owner +"\t"+name + "\t" + price + "원";
        }
    }
}
