using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp19
{
    public class Packet
    {
        const string PACK_INSERT = "insertack";
        const string PACK_DELETE = "deleteack";
        const string PACK_UPDATE = "updateack";
        const string PACK_PRINTALL = "printallack";

        static public string insertack (bool reuslt)
        {
            string packet = string.Empty;
            packet += PACK_INSERT+"\a";
            packet += reuslt;
            return packet;
        }
        static public string deleteack(bool reuslt)
        {
            string packet = string.Empty;
            packet += PACK_DELETE + "\a";
            packet += reuslt;
            return packet;
        }
        static public string updateeack(bool reuslt)
        {
            string packet = string.Empty;
            packet += PACK_UPDATE + "\a";
            packet += reuslt;
            return packet;
        }
        public static string printallack(List<Book> bookes)
        {
            string packet = string.Empty;
            packet += PACK_PRINTALL + "\a";
            foreach (Book book in bookes)
            {
                packet += book.Title + "#";
                packet += book.Pusblisher + "#";
                packet += book.Writer + "#";
                packet += book.Price + "@";

            }
            return packet;
        }

    }
}
