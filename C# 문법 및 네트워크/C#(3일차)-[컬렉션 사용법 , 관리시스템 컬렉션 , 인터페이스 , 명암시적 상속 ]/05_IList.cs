using System;
using System.Collections.Generic;  //template, 일반화

/*
IList : 선형자료구조(값 형식)
        배열, 연결리스트 

[자식클래스]
List<> : 템플릿 기반 자동확장 배열
*/
namespace _1010_인터페이스
{
    internal class _05_IList
    {
        List<int> ar = new List<int>();

        #region 저장
        //1)Add (c++의 push_back과 동일) : 자동확장되면서 0 --> N(뒤로)
        //2)Insert : 특정 위치에 저장(중간에 끼워 넣기)
        //            0 ~ Count 범위에서만 저장가능
        private void Insert()
        {
            try
            {
                PrintAll();

                Console.WriteLine("\n[Add] 1,2,3,4,5");
                for (int i = 1; i <= 5; i++)  //1...5
                    ar.Add(i);

                PrintAll();

                Console.WriteLine("\n[Insert] 2 -> 10");
                //Insert(저장할위치, 값)
                ar.Insert(2, 10);
                PrintAll();

                Console.WriteLine("\n[Insert] 6 -> 20 (없는 인덱스)");
                ar.Insert(6, 20);
                PrintAll();

                //삽입 인덱스가 범위를 벗어났습니다
                Console.WriteLine("\n[Insert] 10 -> 20 (없는 인덱스)");
                ar.Insert(10, 20);
                PrintAll();
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
            int idx = KeyToIdx(4);
            if (idx == -1)
            {
                Console.WriteLine("없다\n");
            }
            else
            {
                int value = ar[idx];       //*****************
                Console.WriteLine("찾은 값 : " + value);
            }
        }

        private int KeyToIdx(int num)
        {
            for (int i = 0; i < ar.Count; i++)
            {
                int value = ar[i];      //***********************
                if (value == num)
                    return i;
            }
            return -1;
        }


        #endregion

        #region 수정
        private void Update()
        {
            int idx = KeyToIdx(4);
            if (idx == -1)
            {
                Console.WriteLine("없다\n");
            }
            else
            {
                ar[idx] = 44;
                Console.WriteLine("수정성공");
                PrintAll();
            }
        }
        #endregion

        #region 삭제(p103)
        //Remove(객체전달)
        //RemoveAt(인덱스)
        public void Remove()
        {
            Console.WriteLine("\n1번 인덱스 전달->삭제 요청");
            ar.RemoveAt(1);
            PrintAll();

            Console.WriteLine("\n44삭제요청");
            ar.Remove(44);
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
            foreach (int o in ar)           //*********
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
