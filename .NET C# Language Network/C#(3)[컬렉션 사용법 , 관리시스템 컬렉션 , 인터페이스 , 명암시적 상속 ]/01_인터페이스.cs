using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1010_인터페이스
{
    interface IStudy
    {
        void Study();
    }

    interface ITeach
    {
        void Teach();
    }

    //암시적 구현 상속
    internal class _01_인터페이스 : IStudy, ITeach
    {
        public void Study()
        {
            Console.WriteLine("공부하다");
        }

        public void Teach()
        {
            Console.WriteLine("강의하다");
        }
    }
}
