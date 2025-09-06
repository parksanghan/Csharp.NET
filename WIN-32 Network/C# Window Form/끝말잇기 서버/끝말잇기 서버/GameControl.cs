using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace 끝말잇기_서버
{
    internal class GameControl // 싱글톤 
    {
        public  List<Socket> gamers = new List<Socket>();
        public static GameControl Instance { get; private set; } = null;
        public List<GameMember> gameuser = new List<GameMember>();
        public static GameControl GetInstance()
        {
            if (Instance == null)
            {
                Instance = new GameControl();
            }
            return Instance;
        }
        public GameControl()
        {
           
        }
         
        // => Server 의 List<socket> sockets 를 메서드로 가져와 
        // Control 에서 GameControl 정의하여 함수를 통해 
        // 새로만든 List<Socket> gamer 에  클라이언트에서 보낸 게임접속 패킷에 id 값을 대조하며 순회 후 저장 






    }
}
