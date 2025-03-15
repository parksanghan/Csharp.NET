using System;
using System.Collections.Generic;

/*
ICollectino : 선형자료구조(값 형식)
        스택, 큐 

[자식클래스]
Queue<> : 큐 ( FIFO, First In First Out )
*/

namespace _1010_인터페이스
{
    internal class _08_Queue
    {
        Queue<int> q = new Queue<int>();

        #region 저장
        //1)Enqueue(put)
        private void Insert()
        {
            try
            {
                PrintAll();

                Console.WriteLine("\n[Add] 1,2,3,4,5");
                for (int i = 1; i <= 5; i++)  //1...5
                    q.Enqueue(i);      //뒤로 저장 됨
                PrintAll();         // 1  2  3  4  5
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 삭제
        //Dequeue()
        public void Remove()
        {
            int value = q.Dequeue();
            Console.Write("삭제 : [{0}] >> ", value);
            PrintAll();

            value = q.Dequeue();
            Console.Write("\n삭제 : [{0}] >> ", value);
            PrintAll();
        }
        #endregion

        #region 삭제할 위치의 데이터 확인 Peek
        public void Peek()
        {
            int value = q.Peek();
            Console.WriteLine("\nFront 위치의 값 : {0}", value);
        }
        #endregion

        #region 전체 초기화
        public void Clear()
        {
            q.Clear();
            PrintAll();
        }
        #endregion

        #region 전체출력
        private void PrintAll()
        {
            Console.WriteLine("저장개수 : {0}", q.Count);
            foreach (int o in q)
            {
                Console.Write(o.ToString() + "\t");
            }
            Console.WriteLine();
        }
        #endregion

        public void Exam()
        {
            Insert(); Console.WriteLine();
            Remove(); Console.WriteLine();
            Peek(); Console.WriteLine();
            Clear();
        }

    }
}
