using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1010_실습관리프로그램
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (App app = new App())
            {
                app.Run();
            }                   //app.Dispose();

        }
    }
}
