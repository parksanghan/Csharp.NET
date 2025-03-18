using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
[값 형식과 참조 형식]
 */ 
namespace _1004
{
    struct S_Member  //값 형식
    {
        public int Number;
        public string Name;
        public S_Member(int Number, string Name)
        {
            this.Number = Number;
            this.Name = Name;
        }
    }

    class C_Member  //참조 형식
    {
        public int Number;
        public string Name;
        public C_Member(int Number, string Name)
        {
            this.Number = Number;
            this.Name = Name;
        }
    }

    class Program   
    {
        static void Main(string[] args)
        {
            //값 형식!
            //스택에 각각 값을 저장
            S_Member s_Member1 = new S_Member(10, "김길동");
            S_Member s_Member2 = new S_Member(20, "이길동");

            Console.WriteLine(s_Member1.Number + ", " + s_Member1.Name );
            Console.WriteLine(s_Member2.Number + ", " + s_Member2.Name + "\n");

            s_Member1 = s_Member2;
            Console.WriteLine(s_Member1.Number + ", " + s_Member1.Name);
            Console.WriteLine(s_Member2.Number + ", " + s_Member2.Name + "\n");

            s_Member1.Number = 30;
            s_Member1.Name = "이름변경";
            Console.WriteLine(s_Member1.Number + ", " + s_Member1.Name);
            Console.WriteLine(s_Member2.Number + ", " + s_Member2.Name + "\n");

            Console.WriteLine("\n\n참조형식*********************");
            //참조 형식!
            C_Member c_Member1 = new C_Member(10, "김길동");
            C_Member c_Member2 = new C_Member(20, "이길동");

            Console.WriteLine(c_Member1.Number + ", " + c_Member1.Name);
            Console.WriteLine(c_Member2.Number + ", " + c_Member2.Name + "\n");

            c_Member1 = c_Member2;
            Console.WriteLine(c_Member1.Number + ", " + c_Member1.Name);
            Console.WriteLine(c_Member2.Number + ", " + c_Member2.Name + "\n");

            c_Member1.Number = 30;
            c_Member1.Name = "이름변경";
            Console.WriteLine(c_Member1.Number + ", " + c_Member1.Name);
            Console.WriteLine(c_Member2.Number + ", " + c_Member2.Name + "\n");
        }
    }
   
}
