using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 끝말잇기_서버
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Control con = Control.Instance;
            con.Run();
            Console.ReadLine();
            
        }
    }
}
