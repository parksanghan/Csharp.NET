using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace _1120_testSer
{
    internal class Program
    {
        WBdb db = new WBdb();
        static void Main(string[] args)
        {
            Uri uri = new Uri(ConfigurationManager.AppSettings["addr"]);

            ServiceHost host = new ServiceHost(typeof(Testserivce), uri);

            //오픈
            host.Open();
            Console.WriteLine("서비스를 시작합니다. {0}", uri.ToString());
            Console.WriteLine("멈추시려면 엔터를 눌러주세요..");
            Console.ReadLine();
            

            //서비스
            host.Abort();
            host.Close();
        }
    }
}
