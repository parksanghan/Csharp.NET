using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1023_시험_클라이언트
{
    internal class Board
    {
        public string Name
        {
            get;
            private set;
        }
        public string Title
        {
            get;
            private set;
        }
        public string Write
        {
            get;
            set;
        }
        public DateTime Date
        {
            get; 
            set;
        }

        public Board(string name, string title, string write)
        {
            Name = name;
            Title= title;
            Write = write;
            Date = DateTime.Now;
        }
        public Board(string name, string title, string write, DateTime date)
        {
            Name = name;
            Title = title;
            Write = write;
            Date = date;
        }
        public override string ToString()
        {
            return  Name + "\t" + Title + "\t" + Write + "\t" + Date.ToShortTimeString();
        }

    }
}
