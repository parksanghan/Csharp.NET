using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace 끝말잇기_서버
{
    internal class GameMember
    {
        public Socket sock { get; set; }
        public string Id { get; private set; } // Member에서 받음
        public DateTime Dt { get; private set; }



        public GameMember(Socket sock, string id, DateTime dt)
        {
            this.sock = sock;
            Id = id;
            Dt = dt;
        }
     
    }
}
