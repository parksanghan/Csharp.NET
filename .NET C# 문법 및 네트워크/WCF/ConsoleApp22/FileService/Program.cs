using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Uri uri = new Uri(ConfigurationManager.AppSettings["addes"]);
            //Contract-> Setting
            //Binding -> App.Config
            ServiceHost host = new ServiceHost(typeof(FileServices), uri);

            //오픈
            host.Open();
            Console.WriteLine("파일 서비스를 시작합니다. {0}", uri.ToString());
            Console.WriteLine("http://127.0.0.1:7000/GetFile");
            Console.WriteLine("멈추시려면 엔터를 눌러주세요..");
            Console.ReadLine();
            //서비스
            host.Abort();
            host.Close();
        }
    }
}
