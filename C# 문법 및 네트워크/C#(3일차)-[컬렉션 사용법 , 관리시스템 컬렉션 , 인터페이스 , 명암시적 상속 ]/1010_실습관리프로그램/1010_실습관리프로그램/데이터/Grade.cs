//Grade.cs
using System;

namespace _1010_실습관리프로그램
{
    internal class Grade
    {
		public int StuNumber	{ get; private set; }     //학생번호
		public int SubNumber	{ get; private set; }    //과목번호
		public int Jumsu		{ get; set; }           //점수

		#region 생성자
		public Grade(int stunumber, int subnumber, int jumsu)
		{
			StuNumber = stunumber;
			SubNumber = subnumber;
			Jumsu = jumsu;
		}
		#endregion

		#region 메서드

		public void Print()
		{
			Console.Write(StuNumber + ", ");
			Console.Write(SubNumber + ", ");
			Console.WriteLine(Jumsu);
		}

		public void Println()
		{
			Console.WriteLine("[학생번호] " + StuNumber);
			Console.WriteLine("[과목번호] " + SubNumber);
			Console.WriteLine("[학점  수] " + Jumsu);
		}

		public override string ToString()
		{
			return StuNumber + "\t" + SubNumber + "\t" + Jumsu;
		}

		#endregion
	}
}
