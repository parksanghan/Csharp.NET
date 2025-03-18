using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
Dispose() : 비관리 자원 소멸을 위해 약속된 메서드
 */ 
namespace _1005
{
    internal class _04_Dispose : IDisposable
    {
        public _04_Dispose()
        {
            Console.WriteLine("[비관리 자원] 파일 OPEN");
        }

        //접근 지정자 사용 불가!
        ~_04_Dispose()
        {
            Console.WriteLine("소멸자 호출");
            Dispose();
        }

        public void Dispose()
        {
            Console.WriteLine("[비관리 자원 소멸처리] 파일 CLOSE");
            //GC(가비지컬렉터). 객체 소멸시 소멸자 호출X
            GC.SuppressFinalize(this);
        }
    }
}
