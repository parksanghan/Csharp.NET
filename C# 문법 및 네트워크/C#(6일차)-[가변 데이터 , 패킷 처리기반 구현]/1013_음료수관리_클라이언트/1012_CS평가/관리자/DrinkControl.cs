//Control.cs
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace 클라이언트
{
	internal class DrinkControl : IDisposable
    {
		List<Drink> drinks		= new List<Drink>();
		//"220.90.179.75";
		const string SERVER_IP = "127.0.0.1";
		const int SERVER_PORT	= 7000;
		private Client client	= null;

		#region 싱글톤(생성자에서 파일로드)
		public static DrinkControl Instance { get; private set; } = null;
		static DrinkControl()
        {
			Instance = new DrinkControl();
        }
		private DrinkControl()
        {
			client = new Client(SERVER_IP, SERVER_PORT, RecvDel, LogDel);
			if (client.Start() == false)
				return;
		}
		#endregion

		#region 소멸처리(파일저장)
		public void Dispose()
		{
			client.Stop();
			GC.SuppressFinalize(this);
		}

		~DrinkControl()
        {
			Dispose();
		}
		#endregion

		#region 네트워크 관련 코드 - Callback
		public void RecvDel(Socket s, string msg)
		{
			string[] sp = msg.Split('\a');
			if (sp[0] == "pack_drinkinsert")		Drink_Insert_Ack(sp[1]);
			else if(sp[0] == "pack_drinkselect")	Drink_Select_Ack(sp[1]);
			else if (sp[0] == "pack_drinkupdate")	Drink_Update_Ack(sp[1]);
			else if (sp[0] == "pack_drinkdelete")	Drink_Delete_Ack(sp[1]);
			else if (sp[0] == "pack_drinkprintall")	Drink_PrintAll_Ack(sp[1]);
		}

		public void LogDel(Socket s, Log log, string msg)
		{
			string temp = string.Format(log.ToString() + "\t" + msg);
			//Console.WriteLine(temp);
		}
		#endregion

		#region 기능 관련 메서드 & 기능 관련 응답 메서드

		//F1) 저장기능(음료수명, 가격을 입력받아 List에 저장)
		public void Drink_Insert()
		{
			Console.WriteLine("[음료수 저장]\n");
			Console.WriteLine("\t음료수명, 가격을 입력받아 List에 저장\n");

			try
			{
				string name = WbLib.InputString("음료수명");
				int price	= WbLib.InputInteger("가격");

				//패킷 생성 전송
				string packet = Packet.Drink_Insert(name, price);
				bool b = client.SendData(packet);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		//msg : true#음료수명#가격
		public void Drink_Insert_Ack(string msg)
        {
			string[] sp = msg.Split('#');
			bool b = bool.Parse(sp[0]);
			string name = sp[1];
			int price = int.Parse(sp[2]);

			if (b == true)
				Console.WriteLine("{0}, 저장 성공", name);
			else
				Console.WriteLine("{0}, 저장 실패", name);
        }


		//F2) 검색 기능(음료수명을 입력받아 검색하여 결과 출력)
		public void Drink_Select()
		{
			Console.WriteLine("[성적 검색]\n");
			Console.WriteLine("\t음료수명을 입력받아 검색하여 결과 출력\n");

			try
			{
				string name = WbLib.InputString("음료수명");

				//패킷 생성 전송
				string packet = Packet.Drink_Select(name);
				bool b = client.SendData(packet);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		//msg : true#음료수명#가격
		public void Drink_Select_Ack(string msg)
		{
			string[] sp = msg.Split('#');
			bool b = bool.Parse(sp[0]);
			string name = sp[1];
			int price = int.Parse(sp[2]);

			if (b == true)
            {
				Console.WriteLine("[제 품 명] " + name);
				Console.WriteLine("[제품가격] " + price + "원");
            }
			else
				Console.WriteLine("{0}, 검색 실패", name);
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

				//패킷 생성 전송
				string packet = Packet.Drink_Update(name, price);
				bool b = client.SendData(packet);

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		//msg : true#음료수명#기존가격#변경된가격
		public void Drink_Update_Ack(string msg)
		{
			string[] sp = msg.Split('#');
			bool b = bool.Parse(sp[0]);
			string name = sp[1];
			int price1 = int.Parse(sp[2]);
			int price2 = int.Parse(sp[3]);

			if (b == true)
			{
				Console.WriteLine("[제  품  명] " + name);
				Console.WriteLine("[기존  가격] " + price1 + "원");
				Console.WriteLine("[변경된가격] " + price2 + "원");
			}
			else
				Console.WriteLine("{0}, 변경 실패", name);
		}

		//F4) 삭제 기능(음료수명을 입력받아 해당 음료수를 찾고 List컬렉션에서 삭제)
		public void Drink_Delete()
		{
			Console.WriteLine("[음료수 삭제]\n");
			Console.WriteLine("\t음료수명을 입력받아 해당 음료수를 찾고 List컬렉션에서 삭제\n");

			try
			{
				string name = WbLib.InputString("음료수명");

				//패킷 생성 전송
				string packet = Packet.Drink_Delete(name);
				bool b = client.SendData(packet);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		//msg : true#음료수명#가격
		public void Drink_Delete_Ack(string msg)
		{
			string[] sp = msg.Split('#');
			bool b = bool.Parse(sp[0]);
			string name = sp[1];
			int price = int.Parse(sp[2]);

			if (b == true)
				Console.WriteLine("{0} 제품 삭제", name);
			else
				Console.WriteLine("{0}, 삭제 실패", name);
		}

		//부가기능) 음료수 전체 리스트 출력
		public void Drink_PrintAll()
		{
			Console.WriteLine("[전체 출력]\n");

			//패킷 생성 전송
			string packet = Packet.Drink_PrintAll();
			bool b = client.SendData(packet);
		}

		//msg : 음료수명#가격@음료수명#가격@음료수명#가격
		public void Drink_PrintAll_Ack(string msg)
		{
			string[] sp1 = msg.Split('@');
			for (int i=0; i< sp1.Length-1; i++)
			{				
				string[] sp2 = sp1[i].Split('#');
				Drink d = new Drink(sp2[0], int.Parse(sp2[1]));
				d.Print();
			}
		}

		#endregion

	}
}
