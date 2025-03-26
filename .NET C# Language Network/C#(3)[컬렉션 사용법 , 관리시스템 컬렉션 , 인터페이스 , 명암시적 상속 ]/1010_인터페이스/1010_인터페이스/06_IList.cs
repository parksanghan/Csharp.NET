using System;
using System.Collections.Generic;

/*
IList : 선형자료구조(값 형식)
        배열, 연결리스트 

[자식클래스]
LinkedList<> : 이중연결리스트
*/
namespace _1010_인터페이스
{
    internal class _06_IList
    {
        LinkedList<int> ar = new LinkedList<int>();

        #region 저장
        //1)AddLast, AddFirst
        //2)AddAfter, AddBefore : 특정 위치에 저장(중간에 끼워 넣기)
        //           
        private void Insert()
        {
            try
            {
                PrintAll();

                Console.WriteLine("\n[Add] 1,2,3,4,5");
                for (int i = 1; i <= 5; i++)  //1...5
                    ar.AddLast(i);  //뒤로 넣는 함수
                PrintAll();         // 1 2 3 4 5 


                for (int i = 11; i <= 15; i++)  //11...15
                    ar.AddFirst(i);  //앞으로 넣는 함수
                PrintAll();         // 15 14 13 12 11 1 2 3 4 5 

                LinkedListNode<int> head =  ar.First;      //첫번째 노드
                ar.AddAfter(head, 111);
                PrintAll();         // 15 [111] 14 13 12 11 1 2 3 4 5 

                LinkedListNode<int> head1 = ar.First;      //첫번째 노드
                LinkedListNode<int>  cur = head1.Next.Next;
                ar.AddBefore(cur, 222);
                PrintAll();     // 15 111 [222] 14 13 12 11 1 2 3 4 5 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion 

        #region 검색
        private void Select()
        {
            LinkedListNode<int> cur = KeyToIdx(4);
            if (cur == null)
            {
                Console.WriteLine("없다\n");
            }
            else
            {
                Console.WriteLine("찾은 값 : " + cur.Value);
            }
        }

        //노드반환
        private LinkedListNode<int> KeyToIdx(int num)
        {
            LinkedListNode<int> cur = ar.First;

            while(cur != null)  
            {
                if (cur.Value == num)
                    return cur;                
                
                
                cur = cur.Next;
            }
            return null;
        }

        #endregion

        #region 수정
        private void Update()
        {
            LinkedListNode<int> cur = KeyToIdx(4);
            if (cur == null)
            {
                Console.WriteLine("없다\n");
            }
            else
            {
                cur.Value = 44;
                PrintAll();
            }
        }
        #endregion

        #region 삭제
        //Remove(객체전달)
        //RemoveFirst(), RemoveLast()
        public void Remove()
        {
            Console.WriteLine("\n44삭제요청");
            ar.Remove(44);

            ar.RemoveFirst();
            ar.RemoveLast();
            PrintAll();
        }
        #endregion

        #region 전체삭제(p103)
        private void Clear()
        {
            ar.Clear();
            PrintAll();
        }
        #endregion 

        #region 전체출력
        private void PrintAll()
        {
            Console.WriteLine("저장개수 : {0}", ar.Count);
            foreach (int o in ar)
            {
                Console.Write(o.ToString() + "\t");
            }
            Console.WriteLine();
        }
        #endregion 

        public void Exam()
        {
            Insert();
            Console.WriteLine();
            Select();
            Update();
            Remove();
            Clear();
        }
    }
}
