using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1005_계좌관리프로그램
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //App app = new App(); //LOGO
            //app.Run();
            //app.Dispose();      //Ending

            using (App app = new App())
            {
                app.Run();
            }                   //app.Dispose();
        }
    }
}
