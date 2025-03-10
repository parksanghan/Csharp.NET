//Member.cs
using System;

//회원데이터[회원번호(100부터+1씩 자동증가), 이름, 전화번호, 가입일시]
namespace _1006_상속기반관리프로그램
{
    internal class Member
    {
		public int Number	{ get; private set; }
		public string Name	{ get; private set; }
		public string Phone { get; set;  }
		
		public DateTime Date { get; private set; }

		private static int s_number = 100;

        #region 생성자
        public Member(string _name, string _phone)
        {
			Number	= s_number;
			Name	= _name;
			Phone	= _phone;	
			Date	= DateTime.Now;
			s_number++;
        }

		public Member(string _name, string _phone, DateTime date)
		{
			Number = s_number;
			Name = _name;
			Phone = _phone;
			Date = date;
			s_number++;
		}

		#endregion

		#region 메서드(출력기능)

		public void Print()
        {
			Console.Write(Number + "\t");
			Console.Write(Name + "\t");
			Console.Write(Phone + "\t");
			Console.Write(Date.ToString() + "\t");
			Console.Write(Date.ToShortDateString() + "\t");
			Console.WriteLine(Date.ToShortTimeString() + "\t");
		}

		public void Println()
        {
			Console.WriteLine("[번    호] " + Number);
			Console.WriteLine("[이    름] " + Name);
			Console.WriteLine("[전화번호] " + Phone);
			Console.WriteLine("[일    시] " + Date.ToString());
			Console.WriteLine("[날    짜] " + Date.ToShortDateString());
			Console.WriteLine("[시    간] " + Date.ToShortTimeString());
		}
        #endregion 
    }
}
