using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Object 클래스의 가상함수
  1) Equlas()
     부모는 주소를 비교하도록 구현되어 있다.
     값 비교를 원할 때 재정의가 필요하다.
  2) ToString()
     부모는 타입명을 반환하도록 구현되어 있다.
     원하는 문자열을 반환하도록 재정의가 필요하다.
*/
namespace _1004
{
    class Sample
    {
        private int number;
        private string name;

        public Sample(int _number, string _name)
        {
            number = _number;
            name = _name;
        }
    
        public void Print()
        {
            Console.WriteLine(number + "\t" + name);
        }

        public override string ToString()
        {
            return number + "\t" + name;
        }
    }

    internal class Class7
    {
        //Equlas
        static void Exam1()
        {
            string s1 = "hello";
            string s2 = s1;
            string s3 = "hello"; // string.Format("hello");
            Console.WriteLine("s1.Equals(s2) => {0}", s1.Equals(s2));
            Console.WriteLine("s1.Equals(s3) => {0}", s1.Equals(s3));
            Console.WriteLine("object.ReferenceEquals(s1,s2) =>{0}",
            object.ReferenceEquals(s1, s2));
            Console.WriteLine("object.ReferenceEquals(s1,s3) =>{0}",
            object.ReferenceEquals(s1, s3));
        }

        //Equlas확인
        static void Exam2()
        {
            string s1 = "hello";
            string s2 = s1;
            string s3 = "hello";
            Console.WriteLine(s1 + "\t" + s2 + "\t" + s3);

            s1 = "문자열 변경";
            Console.WriteLine(s1 + "\t" + s2 + "\t" + s3);

        }
        
        //toString
        static void Exam3()
        {
            int num = 10;
            Console.WriteLine(num);  //int를 출력
            Console.WriteLine(num.ToString()); //정수를 문자열로 변경해서 출력
        }

        //toString
        static void Exam4()
        {
            Sample s1 = new Sample(10, "홍길동");
            s1.Print();

            //아래 2코드는 동일한 코드임!
            Console.WriteLine(s1.ToString());   //_1004.Sample
            Console.WriteLine(s1);              //암묵적으로 ToString()호출
        }
        
        static void Main(string[] args)
        {
            Exam4();
        }
    }
}
