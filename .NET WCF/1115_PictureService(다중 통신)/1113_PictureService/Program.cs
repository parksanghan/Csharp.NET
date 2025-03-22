using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _1113_PictureService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(PictureService)); 
            host.Open();

            Console.WriteLine("Press Any key to stop the service");
            Console.ReadKey(true);

            host.Close();
        }
    }
}
