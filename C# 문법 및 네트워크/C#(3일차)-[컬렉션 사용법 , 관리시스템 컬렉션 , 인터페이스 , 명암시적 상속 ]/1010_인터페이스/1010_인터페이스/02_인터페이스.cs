using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1010_인터페이스
{
    interface IStudy1
    {
        void Study();
        void Work();
    }

    interface ITeach1
    {
        void Teach();
        void Work();
    }

    //명시적 구현 상속
    internal class _02_인터페이스 : IStudy1, ITeach1
    {
        void IStudy1.Study()
        {
            Console.WriteLine("공부하다");
        }
        void IStudy1.Work()
        {
            Console.WriteLine("학생이 일을하다");
        }

        void ITeach1.Teach()
        {
            Console.WriteLine("강의하다");
        }

        void ITeach1.Work()
        {
            Console.WriteLine("강사가 일을하다");
        }
    }
}
