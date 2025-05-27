using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    public class Packet
    {
        const string PACK_INSERT = "insert";
        const string PACK_DELETE = "delete";
        const string PACK_UPDATE = "update";
        const string PACK_PRINTALL = "printall";

        const string PACK_REQUEST = "requestall";
        static public string insertack(string t, string p , string w ,int pr)
        {
            string packet = string.Empty;
            packet += PACK_INSERT + "\a";
            packet += t + "#";
            packet += p + "#";
            packet += w + "#";
            packet += pr;

            return packet;
        }
        static public string deleteack(int index)
        {
            string packet = string.Empty;
            packet += PACK_DELETE + "\a";
           
            packet += index;
            return packet;
        }
        static public string updateeack(int index , int price)
        {
            string packet = string.Empty;
            packet += PACK_UPDATE + "\a";
            packet += index +"#";
            packet += price;
            return packet;
        }
        public static string printallack( )
        {
            string packet = string.Empty;
            packet += PACK_PRINTALL + "\a";
          
            return packet;
        }

    }


}
