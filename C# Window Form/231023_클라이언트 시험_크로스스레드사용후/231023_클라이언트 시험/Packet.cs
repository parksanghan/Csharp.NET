using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _231023_클라이언트_시험
{
    internal static class Packet
    {
        public const string PACK_ADD = "drinkinsert";
        public const string PACK_PRINTALL = "drinkprintall";
        public const string PACK_SELECT = "drinkselect";
        public const string PACK_UPDATE = "drinkupdate";
        public const string PACK_DELETE = "drinkdelete";



        public static string PacketAdd(string userName, string drinkName, int drinkPrice) 
        {
            string temp = string.Empty;
            temp += PACK_ADD + "\a";
            temp += userName + "#";
            temp += drinkName + "#";
            temp += drinkPrice;

            return temp;
        }
        public static string PacketPrintAll(string userName)
        {
            string temp = string.Empty;
            temp += PACK_PRINTALL + "\a";
            temp += userName;

            return temp;
        }
        public static string PacketSelect(string userName, string drinkName)
        {
            string temp = string.Empty;
            temp += PACK_SELECT + "\a";
            temp += userName + "#";
            temp += drinkName;

            return temp;
        }
        public static string PacketUpdate(string userName, string drinkName, int newPrice)
        {
            string temp = string.Empty;
            temp += PACK_UPDATE + "\a";
            temp += userName + "#";
            temp += drinkName + "#";
            temp += newPrice;

            return temp;
        }
        public static string PacketDelete(string userName, string drinkName)
        {
            string temp = string.Empty;
            temp += PACK_DELETE + "\a";
            temp += userName + "#";
            temp += drinkName;

            return temp;
        }




    }
}
