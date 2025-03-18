using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1017_회원관리프로그램.파일IO
{
    internal class WbFile
    {
        //자동으로 static 맴버가 됨
        const string FILE_NAME = "members.txt";

        public static void FileSave(List<Member> members)
        {
            StreamWriter sw = new StreamWriter(FILE_NAME);

            //Head(메타데이터) : 몇개 저장?
            int size = members.Count;
            sw.WriteLine(size);

            //Data(실제 데이터)
            foreach (Member member in members)
            {
                //객체 직렬화 : 객체 -> 문자열 or byte배열
                string temp = string.Empty;
                temp += member.Id + "#";
                temp += member.Name + "#";
                temp += member.Phone + "#";
                temp += member.Gender + "#";
                temp += member.Date;

                sw.WriteLine(temp);
            }
            sw.Close();
        }

        public static void FileLoad(List<Member> members)
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

                    int id          = int.Parse(sp[0]);
                    string name     = sp[1];
                    string phone    = sp[2];
                    bool gender     = bool.Parse(sp[3]);
                    DateTime date   = DateTime.Parse(sp[4]);

                    members.Add(new Member(id, name, phone, gender, date));
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
