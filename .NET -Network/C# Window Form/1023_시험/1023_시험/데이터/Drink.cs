using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1023_시험
{
    internal class Drink
    {
        #region 프로퍼티
        public string Name
        {
            get;
            private set;
        }
        public int Price
        {
            get;
            set;
        }
        #endregion
        public Drink(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }
}
