using System;

namespace _1023_시험_클라이언트
{
    internal class Packet
    {
        const string PACK_BOARDINSERT = "boardinsert";
        const string PACK_BOARDPRINTALL = "boardprintall";
        const string PACK_BOARDSELECT = "boardselect";
        const string PACK_BOARDUPDATE = "boardupdate";
        const string PACK_BOARDDELETE = "boarddelete";

        public static string BoardInsert(string name, string boardname, string write, DateTime date)
        {
            string packet = string.Empty;

            packet += PACK_BOARDINSERT + "\a";
            packet += name + "#";
            packet += boardname + "#";
            packet += write + "#";
            packet += date;

            return packet;
        }
        public static string BoardPrintAll()
        {
            string packet = string.Empty;

            packet += PACK_BOARDPRINTALL + "\a";

            return packet;
        }
        public static string BoardSelect(string boardname)
        {
            string packet = string.Empty;

            packet += PACK_BOARDSELECT + "\a";            
            packet += boardname;

            return packet;
        }
        public static string BoardUpdate(string boardname, string changename, DateTime date)
        {
            string packet = string.Empty;

            packet += PACK_BOARDUPDATE + "\a";
            packet += boardname + "#";
            packet += changename + "#";
            packet += date;

            return packet;
        }
        public static string BoardDelete(string boardname)
        {
            string packet = string.Empty;

            packet += PACK_BOARDDELETE + "\a";           
            packet += boardname;

            return packet;
        }
    }
}
