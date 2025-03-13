using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1023_시험
{
    internal class Packet
    {
        const string PACK_DRINK_SAVE = "drinkinsert";
        const string PACK_DRINK_PRINTALL = "drinkprintall";
        const string PACK_DRINK_SELECT = "drinkselect";
        const string PACK_DRINK_UPDATE = "drinkupdate";
        const string PACK_DRINK_DELETE = "drinkdelete";


        public static string Save(string name, string drname, int price)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_SAVE + "\a";
            packet += name + "#";
            packet += drname + "#";
            packet += price;

            return packet;
        }

        public static string PrintAll(string name)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_PRINTALL + "\a";
            packet += name;

            return packet;
        }

        public static string Select(string name, string drname)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_SELECT + "\a";
            packet += name + "#";
            packet += drname;

            return packet;
        }

        public static string Update(string name, string drname, int price)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_UPDATE + "\a";
            packet += name + "#";
            packet += drname + "#";
            packet += price;

            return packet;
        }

        public static string Delete(string name, string drname)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_DELETE + "\a";
            packet += name + "#";
            packet += drname;

            return packet;
        }
    }
}
