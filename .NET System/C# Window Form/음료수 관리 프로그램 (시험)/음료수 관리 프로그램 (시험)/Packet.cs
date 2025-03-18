using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 음료수_관리_프로그램__시험_
{
    internal class Packet
    {
        const string PACK_DRINKINSERT = "drinkinsert";
        const string PACK_DRINKPRINTALL = "drinkprintall";
        const string PACK_DRINKSELECT = "drinkselect";
        const string PACK_DRINKUPDATE = "drinkupdate";
        const string PAKC_DRINKDELETE = "drinkdelete";

        public static string DrinkInsert(string myname,string d_name, int price)
        {
            string packet = string.Empty;
            packet += PACK_DRINKINSERT + "\a";
            packet += myname + "#";
            packet += d_name + "#";
            packet += price;

            return packet;
        }

        public static string DrinkPrintall(string myname)
        {
            string packet = string.Empty;
            packet += PACK_DRINKPRINTALL + "\a";
            packet += myname;

            return packet;
        }

        public static string DrinkSelect(string myname, string d_name)
        {
            string packet = string.Empty;
            packet += PACK_DRINKSELECT + "\a";
            packet += myname + "#";
            packet += d_name;

            return packet;
        }

        public static string DrinkUpdate(string myname, string d_name, int price)
        {
            string packet = string.Empty;
            packet += PACK_DRINKUPDATE + "\a";
            packet += myname + "#";
            packet += d_name + "#";
            packet += price;

            return packet;
        }

        public static string DrinkDelete(string myname, string d_name)
        {
            string packet = string.Empty;
            packet += PAKC_DRINKDELETE + "\a";
            packet += myname + "#";
            packet += d_name;

            return packet;
        }

    }
    
}
