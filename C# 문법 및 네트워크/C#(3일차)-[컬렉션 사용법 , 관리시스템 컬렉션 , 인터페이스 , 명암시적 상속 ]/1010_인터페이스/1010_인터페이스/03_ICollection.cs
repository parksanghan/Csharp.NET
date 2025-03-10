using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
IEnumerable : 열거기능 -> foreach문에서 사용가능케 함
 
interface ICollection : IEnumerable
- void CopyTo(Array array, int index);  //보관된 요소들을 array에 복사
- int Count  { get;  }                  //보관된 요소 개수 가져오기

[약속의 결과]
* ICollection 자식들은 foreach문 사용 가능하다.!
*                      요소의 개수 : Count 
*                      복사기능    : CopyTo()
 */

namespace _1010_인터페이스
{
    internal class _03_ICollection
    {
        //p100 (Count)
        public void Exam1()
        {
            //배열(Array - 고정)
            int[] arr = new int[3] { 1, 2, 4};

            //배열(ArrayList - 확장)
            ArrayList ar = new ArrayList();
            ar.Add(1);
            ar.Add(2);
            ar.Add(3);
            ar.Add(4);

            //View를 통해 출력 요청
            View(arr);
            View(ar);
        }

        //p101(CopyTo)
        public void Exam2()
        {
            int[] srcarr = new int[3] { 1, 2, 4 };
            int[] dstarr = new int[5] { 11, 12, 131, 4, 15 };
            View(dstarr);

            srcarr.CopyTo(dstarr, 1); //11 1 2  4  15
            View(dstarr);
        }


        //ICollection 자식들을 받을 수 있다.
        //IEnumerable 구현상속이 되어 있기 때문에 foreach사용이 가능!
        private void View(ICollection ic) 
        {
            Console.WriteLine("저장개수 : {0}" , ic.Count);
            foreach(object o in ic)
            {
                Console.Write(o.ToString() + "\t");
            }
            Console.WriteLine();
        }
    }
}
