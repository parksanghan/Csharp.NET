//packet.cs

namespace 클라이언트
{
    static class Packet
    {
        const string PACK_DRINK_INSERT      = "pack_drinkinsert";
        const string PACK_DRINK_SELECT      = "pack_drinkselect";
        const string PACK_DRINK_UPDATE      = "pack_drinkupdate";
        const string PACK_DRINK_DELETE      = "pack_drinkdelete";
        const string PACK_DRINK_PRINTALL    = "pack_drinkprintall";

        public static string Drink_Insert(string name, int price)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_INSERT + "\a";
            packet += name + "#";
            packet += price;

            return packet;
        }

        public static string Drink_Select(string name)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_SELECT + "\a";
            packet += name;

            return packet;
        }

        public static string Drink_Update(string name, int price)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_UPDATE + "\a";
            packet += name + "#";
            packet += price;

            return packet;
        }
         
        public static string Drink_Delete(string name)
        {
            string packet = string.Empty;

            packet += PACK_DRINK_DELETE + "\a";
            packet += name;

            return packet;
        }

        public static string Drink_PrintAll()
        {
            string packet = string.Empty;

            packet += PACK_DRINK_PRINTALL + "\a";

            return packet;
        }
    }
}
