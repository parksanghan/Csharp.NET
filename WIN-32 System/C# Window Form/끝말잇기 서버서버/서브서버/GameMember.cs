using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace 서브서버
{
    internal class GameMember
    {
        public string Id { get; set; }
        public bool Islose { get; set; }
 
        static int size = 0;

        public int Getsize () { return size; }
        public void Upsize() { size++; }
      
        public void Resetsize() { size = 0; }
        public GameMember(string id ) {
        

            Id = id;
            Islose = false;
            size++; // 인원추가마다 size 증가 
 

        }
    }
}
