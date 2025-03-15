using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1010_인터페이스
{
    public class NumberCompare : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            //return x - y; //오름차순
            return y - x;   //내림차순
        }
    }

    //데이터 클래스(p112)
    //클래스 정의기 기본 정렬 기준 설정
    public class Member : IComparable<Member>
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        //기본 정렬 기준!
        public int CompareTo(Member other)
        {
            //return Name.CompareTo(other.Name);  //오름차순
           return other.Name.CompareTo(Name); //내름차순
        }
    }

    //부가적 정렬기준 추가 생성
    public class MemberPhoneCompare : IComparer<Member>
    {
        public int Compare(Member x, Member y)
        {
            //return x.Phone.CompareTo(y.Phone);   //오름차순
            return y.Phone.CompareTo(x.Phone);      //내름차순
        }
    }

    internal class _12_Sort
    {
        //Sort, int, ASC(기본, 오름차순)
        public static void Exam1()
        {
            int[] arr = new int[10] { 20, 10, 5, 9, 11, 23, 8, 7, 6, 13 };

            //오름차순(ASC)
            Array.Sort(arr); //정적 메서드 Sort를 이용, 입력 인자는 1차원 배열
            foreach (int i in arr)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
        }


        //Sort, int, Desc(내림차순)
        public static void Exam2()
        {
            int[] arr = new int[10] { 20, 10, 5, 9, 11, 23, 8, 7, 6, 13 };

            Array.Sort(arr, new NumberCompare()); 
            foreach (int i in arr)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
        }
    
        public static void Exam3()
        {
            List<Member> members = new List<Member>
            {
                new Member(){Name = "최길동", Phone = "010-4444-4444"},
                new Member(){Name = "김길동", Phone = "010-3333-4444"},
                new Member(){Name = "허길동", Phone = "010-9999-4444"},
                new Member(){Name = "이길동", Phone = "010-6666-4444"},
            };

            //정렬기준제시 없다-->클래스 내에서 정해진 기준
            members.Sort();

            foreach(Member m in members)
            {
                Console.WriteLine(m.Name + "\t" + m.Phone);
            }

            Console.WriteLine("\n\n번호로 정렬");
            members.Sort(new MemberPhoneCompare());

            foreach (Member m in members)
            {
                Console.WriteLine(m.Name + "\t" + m.Phone);
            }
        }
    }
}
