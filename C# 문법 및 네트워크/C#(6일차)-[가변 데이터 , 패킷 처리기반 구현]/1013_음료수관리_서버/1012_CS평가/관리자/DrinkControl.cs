//Control.cs
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace 서버
{
    internal class DrinkControl : IDisposable
    {
        private List<Drink> drinks = new List<Drink>();
        private Server server = null;

        #region 싱글톤(생성자에서 파일로드)
        public static DrinkControl Instance { get; private set; } = null;
        static DrinkControl()
        {
            Instance = new DrinkControl();
        }
        private DrinkControl() // 연결처리 생성자에서 함. 
        {
            DrinkFile.FileLoad(drinks);

            server = new Server(7000, RecvDel, LogDel);
            server.Start();
        }
        #endregion

        #region 소멸처리(파일저장)
        public void Dispose()
        {
            server.Stop();
            DrinkFile.FileSave(drinks);
            GC.SuppressFinalize(this);
        }

        ~DrinkControl()
        {
            Dispose();
        }
        #endregion

        #region Callback - 네트워크에서 호출
        public void RecvDel(Socket s, string msg)
        {
            string[] sp = msg.Split('\a');
            if (sp[0] == "pack_drinkinsert") Drink_Insert(s, sp[1]);
            else if (sp[0] == "pack_drinkselect") Drink_Select(s, sp[1]);
            else if (sp[0] == "pack_drinkupdate") Drink_Update(s, sp[1]);
            else if (sp[0] == "pack_drinkdelete") Drink_Delete(s, sp[1]);
            else if (sp[0] == "pack_drinkprintall") Drink_PrintAll(s);
        }

        public void LogDel(Socket s, Log log, string msg)
        {
            string temp = string.Format(log.ToString() + "\t" + msg);
            Console.WriteLine(temp);
        }
        #endregion

        #region 클라이언트가 요청한 기능 처리 메서드

        //F1) 저장기능(음료수명, 가격을 입력받아 List에 저장)
        public void Drink_Insert(Socket s, string msg)
        {
            Console.WriteLine("[음료수 저장]\n");

            string name = string.Empty;
            int price = 0;

            try
            {
                string[] sp = msg.Split('#');

                name = sp[0];
                price = int.Parse(sp[1]);

                Drink drink = new Drink(name, price);
                drinks.Add(drink);

                Console.WriteLine("음료수 저장 성공");

                //응답처리
                string packet = Packet.Drink_Insert(true, name, price);
                server.SendData(s, packet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                string packet = Packet.Drink_Insert(false, name, price);
                server.SendData(s, packet);
            }
        }

        //F2) 검색 기능(음료수명을 입력받아 검색하여 결과 출력)
        public void Drink_Select(Socket s, string msg)
        {
            Console.WriteLine("[성적 검색]\n");

            string name = string.Empty;
            try
            {
                string[] sp = msg.Split('#');
                name = sp[0];

                Drink drink = drinks.Find(obj => obj.Name == name);
                drink.Println();

                //응답처리
                string packet = Packet.Drink_Select(true, drink.Name, drink.Price);
                server.SendData(s, packet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                string packet = Packet.Drink_Select(false, name, 0);
                server.SendData(s, packet);
            }
        }

        //F3) 수정 기능(음료수명, 가격을 입력받아 해당 음료수를 찾아 입력받은 가격으로 변경)
        public void Drink_Update(Socket s, string msg)
        {
            Console.WriteLine("[음료수 가격 변경]\n");

            string name = string.Empty;
            int price = 0;
            int temp = 0;       //기존가격
            try
            {
                string[] sp = msg.Split('#');

                name = sp[0];
                price = int.Parse(sp[1]);

                Drink drink = drinks.Find(obj => obj.Name == name);
                temp = drink.Price;
                drink.Price = price;

                Console.WriteLine("가격이 변경되었습니다");

                //응답처리
                string packet = Packet.Drink_Update(true, drink.Name, temp, drink.Price);
                server.SendData(s, packet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                string packet = Packet.Drink_Update(false, name, temp, temp);
                server.SendData(s, packet);
            }
        }

        //F4) 삭제 기능(음료수명을 입력받아 해당 음료수를 찾고 List컬렉션에서 삭제)
        public void Drink_Delete(Socket s, string msg)
        {
            Console.WriteLine("[음료수 삭제]\n");

            string name = string.Empty;
            try
            {
                string[] sp = msg.Split('#');
                name = sp[0];

                Drink drink = drinks.Find(obj => obj.Name == name);
                drinks.Remove(drink);

                //응답처리
                string packet = Packet.Drink_Delete(true, drink.Name, drink.Price);
                server.SendData(s, packet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string packet = Packet.Drink_Delete(false, name, 0);
                server.SendData(s, packet);
            }
        }

        //부가기능) 음료수 전체 리스트 출력
        public void Drink_PrintAll(Socket s)
        {
            Console.WriteLine("저장개수 : {0}개\n", drinks.Count);

            for (int i = 0; i < drinks.Count; i++)
            {
                Drink drink = drinks[i];
                drink.Print();
            }
            Console.WriteLine();

            //응답처리
            string packet = Packet.Drink_PrintAll(drinks);
            server.SendData(s, packet);
        }

        #endregion
    }
}
