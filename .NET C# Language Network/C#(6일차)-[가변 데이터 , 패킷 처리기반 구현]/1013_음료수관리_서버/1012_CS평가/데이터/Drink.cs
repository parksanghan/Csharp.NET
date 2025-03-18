//Grade.cs
using System;

namespace 서버
{
    internal class Drink
    {
		public string Name	{ get; private set; }     //제품명
		public int Price	{ get; set; }    //가격
		

		#region 생성자
		public Drink(string name, int price)
		{
			Name = name;
			Price = price;
		}
		#endregion

		#region 메서드

		public void Print()
		{
			Console.Write(Name + "\t");
			Console.WriteLine(Price + "원");
		}

		public void Println()
		{
			Console.WriteLine("[음료수명] " + Name);
			Console.WriteLine("[가    격] " + Price);
		}

		#endregion
	}
}
