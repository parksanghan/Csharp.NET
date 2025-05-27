using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp19
{
    public class Book
    {

        public string Title { get; set; }
        public string Pusblisher { get; set; }

        public string Writer { get; set; }

        public int Price { get; set; }

       public Book(string title , string pub  , string writ , int pro) 
        {
            Title = title;
            Pusblisher = pub;
                Writer = writ;
            Price = pro;
        }

       
    }
}
