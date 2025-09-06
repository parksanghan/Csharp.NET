using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace 서브서버
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            GameControl _control = GameControl.Instance;
        
                _control.Run();
            Console.ReadLine();
 
        }
    }
}
