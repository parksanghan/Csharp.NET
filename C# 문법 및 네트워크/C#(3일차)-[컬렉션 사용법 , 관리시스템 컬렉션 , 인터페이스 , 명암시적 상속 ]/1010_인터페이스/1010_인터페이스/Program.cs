using System;

namespace _1010_인터페이스
{
    internal class Program
    {
        #region 인터페이스 사용
        //암시적 구현상속 객체 사용 : 일반적인 클래스 사용법과 동일
        static void Exam1()
        {
            _01_인터페이스 sample = new _01_인터페이스();
            sample.Study();
            sample.Teach();
        }

        //명시적 구현상속 객체 사용 : 반드시 인터페이스를 이용해 맴버 사용!
        static void Exam2()
        {
            _02_인터페이스 sample = new _02_인터페이스();
            //에러!!
            //sample.Study();
            //sample.Teach();

            //업케스팅
            IStudy1 istudy = sample as IStudy1;
            if(istudy != null)
            {
                istudy.Study();
                istudy.Work();  //학생...
            }

            ITeach1 iteach = sample as ITeach1;
            if (iteach != null)
            {
                iteach.Teach();
                iteach.Work();  //강사...
            }

        }
        #endregion

        #region 컬렉션--------------------------------------
        static void Exam3()
        {
            _03_ICollection ic = new _03_ICollection();
            ic.Exam1();
            Console.WriteLine("\n\n");
            ic.Exam2();
        }

        //IList(배열)
        static void Exam4()
        {
            _04_IList list = new _04_IList();
            list.Exam();
        }

        //IList(배열)
        static void Exam5()
        {
            _05_IList list = new _05_IList();
            list.Exam();
        }

        //IList(연결리스트)
        static void Exam6()
        {
            _06_IList list = new _06_IList();
            list.Exam();
        }

        //ICollection(스택)
        static void Exam7()
        {
            _07_Stack stack = new _07_Stack();
            stack.Exam();
        }

        //ICollection(큐)
        static void Exam8()
        {
            _08_Queue queue = new _08_Queue();
            queue.Exam();
        }
        #endregion

        #region Find
        static void Exam9()
        {
            _09_Find.Exam6();
        }
        #endregion

        #region 컬렉션(Dictionary)
        static void Exam10()
        {
          _10_IDictionary dir = new _10_IDictionary();
            dir.Exam();
        }
        #endregion

        #region 정렬
        static void Exam11()
        {
            _12_Sort.Exam3();
        }
        #endregion 
        static void Main(string[] args)
        {
            Exam11();
        }
    }
}
