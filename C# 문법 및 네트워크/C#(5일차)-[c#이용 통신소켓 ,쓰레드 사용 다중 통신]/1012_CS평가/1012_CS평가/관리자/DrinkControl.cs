//Control.cs
using System;
using System.Collections.Generic;

namespace _1012_CS평가
{
	internal class DrinkControl : IDisposable
    {
		List<Drink> drinks = new List<Drink>();

		#region 싱글톤(생성자에서 파일로드)
		public static DrinkControl Instance { get; private set; } = null;
		static DrinkControl()
        {
			Instance = new DrinkControl();
        }
		private DrinkControl()
        {
			DrinkFile.FileLoad(drinks);
		}
		#endregion

		#region 소멸처리(파일저장)
		public void Dispose()
		{
			DrinkFile.FileSave(drinks);
			GC.SuppressFinalize(this);
		}

		~DrinkControl()
        {
			Dispose();
		}
		#endregion

		#region 기능 관련 메서드

		//F1) 저장기능(음료수명, 가격을 입력받아 List에 저장)
		public void Drink_Insert()
		{
			Console.WriteLine("[음료수 저장]\n");
			Console.WriteLine("\t음료수명, 가격을 입력받아 List에 저장\n");

			try
			{
				string name = WbLib.InputString("음료수명");
				int price	= WbLib.InputInteger("가격");

				Drink drink = new Drink(name, price);
				drinks.Add(drink);

				Console.WriteLine("음료수 저장 성공");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		//F2) 검색 기능(음료수명을 입력받아 검색하여 결과 출력)
		public void Drink_Select()
		{
			Console.WriteLine("[성적 검색]\n");
			Console.WriteLine("\t음료수명을 입력받아 검색하여 결과 출력\n");

			try
			{
				string name = WbLib.InputString("음료수명");

				Drink drink = drinks.Find(obj => obj.Name == name);
				drink.Println();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		//F3) 수정 기능(음료수명, 가격을 입력받아 해당 음료수를 찾아 입력받은 가격으로 변경)
		public void Drink_Update()
		{
			Console.WriteLine("[음료수 가격 변경]\n");
			Console.WriteLine("\t음료수명, 가격을 입력받아 해당 음료수를 찾아 입력받은 가격으로 변경\n");

			try
			{
				string name = WbLib.InputString("음료수명");
				int price = WbLib.InputInteger("가격");

				Drink drink = drinks.Find(obj => obj.Name == name);
				drink.Price = price;

				Console.WriteLine("가격이 변경되었습니다");

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
		
		//F4) 삭제 기능(음료수명을 입력받아 해당 음료수를 찾고 List컬렉션에서 삭제)
		public void Drink_Delete()
		{
			Console.WriteLine("[음료수 삭제]\n");
			Console.WriteLine("\t음료수명을 입력받아 해당 음료수를 찾고 List컬렉션에서 삭제\n");

			try
			{
				string name = WbLib.InputString("음료수명");

				Drink drink = drinks.Find(obj => obj.Name == name); 
				drinks.Remove(drink);

				Console.WriteLine("음료수 삭제 성공");	
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		//부가기능) 음료수 전체 리스트 출력
		public void Drink_PrintAll()
		{
			Console.WriteLine("저장개수 : {0}개\n", drinks.Count);

			for (int i = 0; i < drinks.Count; i++)
			{
				Drink drink = drinks[i];
				drink.Print();
			}
			Console.WriteLine();
		}

        #endregion
    }
}
