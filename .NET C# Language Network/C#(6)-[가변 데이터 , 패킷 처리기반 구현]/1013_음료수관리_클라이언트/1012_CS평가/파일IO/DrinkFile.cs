//MemberFile.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 클라이언트
{
    internal static class DrinkFile
    {
        //자동으로 static 맴버가 됨
        const string FILE_NAME = "drinnks.txt";

        public static void FileSave(List<Drink> drinks)
        {
            StreamWriter sw = new StreamWriter(FILE_NAME);

            //Head(메타데이터) : 몇개 저장?
            int size = drinks.Count;
            sw.WriteLine(size);

            //Data(실제 데이터)
            foreach (Drink drink in drinks)
            {
                //객체 직렬화 : 객체 -> 문자열 or byte배열
                string temp = string.Empty;
                temp += drink.Name + "#";
                temp += drink.Price;

                sw.WriteLine(temp);
            }
            sw.Close();
        }

        public static void FileLoad(List<Drink> drinks)
        {
            try
            {
                StreamReader sr = new StreamReader(FILE_NAME);

                //head정보 획득
                int size = int.Parse(sr.ReadLine());

                //본 데이터 획득
                for (int i = 0; i < size; i++)
                {
                    //역직렬화 : string -> Member
                    string temp = sr.ReadLine();    //홍길동#대전

                    string[] sp = temp.Split('#');  //#으로 자르기 
                    string name = sp[0];
                    int price   = int.Parse(sp[1]);

                    drinks.Add(new Drink(name, price));
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
