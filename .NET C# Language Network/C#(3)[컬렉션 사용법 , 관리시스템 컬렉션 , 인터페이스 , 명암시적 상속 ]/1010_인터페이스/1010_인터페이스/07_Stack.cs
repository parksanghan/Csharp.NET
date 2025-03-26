using System;
using System.Collections.Generic;

/*
ICollectino : 선형자료구조(값 형식)
        스택, 큐 

[자식클래스]
Stack<> : 스택 ( LIFO, Last In First Out )
*/
namespace _1010_인터페이스
{
    internal class _07_Stack
    {
        Stack<int> s = new Stack<int>();        

        #region 저장
        //1)Insert
        private void Insert()
        {
            try
            {
                PrintAll();

                Console.WriteLine("\n[Add] 1,2,3,4,5");
                for (int i = 1; i <= 5; i++)  //1...5
                    s.Push(i);      //앞으로 저장 됨
                PrintAll();         // 5  4  3  2  1              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 삭제
        //Remove(객체전달)
        //RemoveFirst(), RemoveLast()
        public void Remove()
        {
            int value = s.Pop();
            Console.Write("삭제 : [{0}] >> ", value);
            PrintAll();
            
            value = s.Pop();
            Console.Write("\n삭제 : [{0}] >> ", value);
            PrintAll();
        }
        #endregion

        #region 삭제할 위치의 데이터 확인 Peek
        public void Peek()
        {
            int value = s.Peek();
            Console.WriteLine("\nTOP 위치의 값 : {0}", value);
        }
        #endregion

        #region 전체 초기화
        public void Clear()
        {
            s.Clear();
            PrintAll();
        }
        #endregion

        #region 전체출력
        private void PrintAll()
        {
            Console.WriteLine("저장개수 : {0}", s.Count);
            foreach (int o in s)
            {
                Console.Write(o.ToString() + "\t");
            }
            Console.WriteLine();
        }
        #endregion

        public void Exam()
        {
            Insert();   Console.WriteLine();
            Remove();   Console.WriteLine();
            Peek();     Console.WriteLine();
            Clear();
        }
    }
}
