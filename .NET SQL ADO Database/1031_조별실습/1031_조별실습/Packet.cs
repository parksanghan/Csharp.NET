using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1031_조별실습
{
      static  class Packet
    {
        public const string PACK_INSERT_ACK = "packinsertack";
        public const string PACK_SELECT_ACK = "packselectack";
        public const string PACK_SELECTALL_ACK = "packselectallack";
        public const string PACK_UPDATAE_ACK = "packupdateack";
        public const string PACK_DELETE_ACK = "packdeleteack";
        public static string Insert_Message(bool reuslt)
        {
            //string title, string image, 
            //string author ,  int discount , string publisher , string pudate ,
            //long isbn , string descrition)
            string packet = string.Empty;
            packet += PACK_INSERT_ACK + "\a";
            packet += reuslt ;
          
            return packet;
        }


        public static string Select_Message(bool reuslt,  Data book)
        {
            string packet = string.Empty;
            packet += PACK_SELECT_ACK + "\a";
            packet += reuslt + "#";
            packet += book.Title + "#";
            packet += book.Link + "#";
            packet += book.Image + "#";
            packet += book.Author + "#";
            packet += book.Discount + "#";
            packet += book.Publisher + "#";
            packet += book.Pubdate + "#";
            packet += book.Isbn + "#";
            packet += book.Description;
            return packet;


        }

        public static string Select_Message(bool reuslt )
        {
            string packet = string.Empty;
            packet += PACK_SELECT_ACK + "\a";
            packet += reuslt + "#";
          
            return packet;


        }

        public static string SelectAll_Message( List<Data> books)
        {
            string packet = string.Empty;
            packet += PACK_SELECTALL_ACK + "\a";
 ;

            foreach (Data book in books) 
            {
         
            
                packet += book.Title + "#";
                packet += book.Link + "#";
                packet += book.Image + "#";
                packet += book.Author + "#";
                packet += book.Discount + "#";
                packet += book.Publisher + "#";
                packet += book.Pubdate + "#";
                packet += book.Isbn + "#";
                packet += book.Description +"@";
            }
            return packet;
        }
        public static string Delete_MEssage(bool result)
        {
            string packet = string.Empty;
            packet += PACK_DELETE_ACK + "\a";
            packet += result + "#";
            return packet;
        }
        public static string Update_Message(bool result)
        {
            string packet = string.Empty;
            packet += PACK_UPDATAE_ACK + "\a";
            packet += result + "#";
            return packet;
        }

    }
}
