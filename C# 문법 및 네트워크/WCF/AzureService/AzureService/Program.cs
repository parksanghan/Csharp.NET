using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Configuration;

namespace AzureService
{
    class Program
    {
        public static void Main(string[] args)
        {
            Uri uri = new Uri(ConfigurationManager.AppSettings["addx"]);
            //Contract-> Setting
            //Binding -> App.Config
            ServiceHost host = new ServiceHost(typeof(CAzureService), uri);

            //오픈
            host.Open();
            Console.WriteLine("파일 서비스를 시작합니다. {0}", uri.ToString());
            Console.WriteLine("http://127.0.0.1:7000/GetAzure");
            Console.WriteLine("멈추시려면 엔터를 눌러주세요..");
            Console.ReadLine();

            //서비스
            host.Abort();
            host.Close();
        }
    }
}
