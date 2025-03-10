using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1006_상속기반관리프로그램
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App app = new App();
            app.Init();
            app.Run();
            app.Exit();
        }
    }
}
