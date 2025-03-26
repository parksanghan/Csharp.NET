//Student.cs
using System;

namespace _1010_실습관리프로그램
{
	internal class Student
	{		 
		public int Number { get; private set; }		//학생번호
		public string Name { get; private set; }	//이름
		public string Major { get; set; }			//학과명
			

		#region 생성자
		public Student(int number, string name, string major)
		{
			Number = number;
			Name = name;
			Major = major;
		}
		#endregion

		#region 메서드
		
		public void Print()
		{
			Console.Write(Number + ", ");
			Console.Write(Name + ", ");
			Console.WriteLine(Major);
		}

		public void Println()
        {
			Console.WriteLine("[학  번] " + Number);
			Console.WriteLine("[이  름] " + Name);
			Console.WriteLine("[학과명] " + Major);
		}

        public override string ToString()
        {
            return Number + "\t" + Name + "\t" + Major;
        }

        #endregion
    }
}
