//Subject.cs
using System;

namespace _1010_실습관리프로그램
{
    internal class Subject
    {
		public int Number { get; private set; }     //과목번호
		public string Name { get; private set; }    //과목명


		#region 생성자
		public Subject(int number, string name)
		{
			Number = number;
			Name = name;
		}
		#endregion

		#region 메서드

		public void Print()
		{
			Console.Write(Number + ", ");
			Console.WriteLine(Name);
		}

		public void Println()
		{
			Console.WriteLine("[과목번호] " + Number);
			Console.WriteLine("[과 목 명] " + Name);
		}

		public override string ToString()
		{
			return Number + "\t" + Name;
		}

		#endregion
	}
}
