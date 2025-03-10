//MemberFile.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1011_회원관리프로그램
{
    internal static class MemberFile
    {
        //자동으로 static 맴버가 됨
        const string FILE_NAME = "members.txt";

        public static void FileSave(Dictionary<string, Member> members)
        {
            StreamWriter sw = new StreamWriter(FILE_NAME);   
            
            //Head(메타데이터) : 몇개 저장?
            int size = members.Count;
            sw.WriteLine(size);
           
            //Data(실제 데이터)
            foreach(Member member in members.Values)
            {
                //객체 직렬화 : 객체 -> 문자열 or byte배열
                string temp = string.Empty;
                temp += member.Name + "#";
                temp += member.Addr;

                sw.WriteLine(temp);
            }        
            sw.Close();
        }

        public static void FileLoad(Dictionary<string, Member> members)
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
                    string addr = sp[1];

                    Member member = new Member(name, addr);

                    members.Add(name, member);
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    
        public static void FileLogSave(object obj)
        {
            StreamWriter writer = File.AppendText("log.txt");
            writer.WriteLine(obj.ToString());
            writer.Close();
        }
    }
}
