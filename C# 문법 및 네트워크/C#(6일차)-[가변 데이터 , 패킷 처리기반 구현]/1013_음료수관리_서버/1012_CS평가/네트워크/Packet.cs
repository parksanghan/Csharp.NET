//packet.cs

using System.Collections.Generic;

namespace 서버
{
    static class Packet
    {
        const string PACK_DRINK_INSERT = "pack_drinkinsert";
        const string PACK_DRINK_SELECT = "pack_drinkselect";
        const string PACK_DRINK_UPDATE = "pack_drinkupdate";
        const string PACK_DRINK_DELETE = "pack_drinkdelete";
        const string PACK_DRINK_PRINTALL = "pack_drinkprintall";

        public static string Drink_Insert(bool b, string name, int price)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_INSERT + "\a";
            packet += b + "#";
            packet += name + "#";
            packet += price;

            return packet;
        }

        public static string Drink_Select(bool b, string name, int price)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_SELECT + "\a";
            packet += b + "#";
            packet += name + "#";
            packet += price; 

            return packet;
        }

        public static string Drink_Update(bool b, string name, int price1, int price2)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_UPDATE + "\a";
            packet += b + "#";
            packet += name + "#";
            packet += price1 + "#";
            packet += price2;

            return packet;
        }

        public static string Drink_Delete(bool b, string name, int price)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_DELETE + "\a";
            packet += b + "#";
            packet += name + "#";
            packet += price; 

            return packet;
        }

        public static string Drink_PrintAll(List<Drink> drinks)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_PRINTALL + "\a";
            foreach(Drink drink in drinks)
            {
                packet += drink.Name + "#";
                packet += drink.Price + "@";
            }

            return packet;
        }
    }
}
