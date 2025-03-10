using System;
using System.Collections;

/*
[p106]
IDictionary  
- 키와 값을 pair형태로 저장관리
- 검색목적
  이진탐색트리, 해시테이블
*/ 

namespace _1010_인터페이스
{
    internal class _10_IDictionary
    {
        Hashtable ht = new Hashtable();

        //저장
        //1) Add(키, 값);
        //2) [키] = 값;
        public void Insert()
        {
            ht.Add("홍길동", "율도국");
            ht["장언휴"] = "이에이치";
            PrintAll1();

            Console.WriteLine("\n같은 key를 저장할 경우..");
            //ht.Add("홍길동", "동일한키에러발생"); //동일한키 저장불가
            ht["장언휴"] = "동일한키를사용하면값이변경됨";
            PrintAll1();
        }

        //삭제(p108)
        //Remove("키")
        public void Remove()
        {
            Console.WriteLine("\n\n삭제 후");
            ht.Remove("홍길동");
            PrintAll1();
        }

        //수정
        //[키] = 값;
        public void Update()
        {
            Console.WriteLine("\n\n수정 후");
            ht["장언휴"] = "값 변경";
            PrintAll1();
        }

        //검색
        public void Select()
        {
            string value = (string)ht["장언휴"];
            Console.WriteLine("검색결과 : " + value);
        }

        #region 출력
        //키와 값을 모두 출력
        public void PrintAll1()
        {
            foreach(DictionaryEntry entry in ht)
            {
                Console.WriteLine(entry.Key + "\t" + entry.Value);
            }
        }

        //키만 열거
        public void PrintAll2()
        {
            foreach (string str in ht.Keys)
            {
                Console.Write(str + "\t");
            }
            Console.WriteLine();
        }

        //값만 열거
        public void PrintAll3()
        {
            foreach (string str in ht.Values)
            {
                Console.Write(str + "\t");
            }
            Console.WriteLine();
        }

        #endregion 

        public void Exam()
        {
            Insert();
            Remove();
            Update();
            Select();
        }
    }
}
