using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 시험1102
{
    public class Packet
    {

        const string PACK_INSERT = "insert";
        const string PACK_DELETE = "delete";
        const string PACK_UPDATE = "update";
        const string PACK_SELECTALL = "selctall";

        const string PACK_SELECT = "select";


         static public string insert(Exam e)
        { string packet = string.Empty;
            packet += PACK_INSERT + "\a";
            packet += e.ID + "#";
            packet += e.NAME + "#";
            packet += e.PHONE+"#";
            packet += e.Gender + "#";

            return packet;
        }

        static public string delete(int id)
        {
            string packet = string.Empty;
            packet += PACK_DELETE + "\a";
            packet += id;
         

            return packet;
        }

        static public string update(int id, string ph)
        {
            string packet = string.Empty;
            packet += PACK_UPDATE + "\a";
            packet += id + "#";

            packet += ph;

            return packet;
        }

        static public string selectall()
        {
            string packet = string.Empty;
            packet += PACK_SELECTALL + "\a";
 

            return packet;
        }
        static public string select(int id)
        {
            string packet = string.Empty;
            packet += PACK_SELECT + "\a";
            packet += id;

            return packet;
        }

    }
}
