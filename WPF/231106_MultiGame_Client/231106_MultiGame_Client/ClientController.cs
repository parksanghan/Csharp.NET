using _231018_WBNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _231106_MultiGame_Client
{
    internal class ClientController
    {
        #region [싱글톤]
        public static ClientController instance;
        static ClientController() 
        {
            instance = new ClientController();
        }
        private ClientController() 
        {
            client = new Client(SERVER_IP, PORT, ClientRecv, ClientLog);
        }
        #endregion

        string SERVER_IP = "10.101.156.73";
        int PORT = 9000;

        public static string id;
        public static string name;

        Client client;
        public FormMain formMain;


        public void Load(FormMain form)
        {
            formMain = form;
            if (client.Start() == false)
            {
                MessageBox.Show("네트워크 연결에 실패했습니다. 잠시 후 다시 시도해주세요.");
                Application.Exit();
            }
        }

        public void ServerSend(string packet) 
        {
            lock (client)
            {
                client.SendData(packet);
            };
        }

        public void ClientRecv(string msg)
        {
            try
            {
                string[] sp = msg.Split('\r');

                PacketServerType packetType = (PacketServerType)Enum.Parse(typeof(PacketServerType), sp[0]);

                switch (packetType)
                {
                    case PacketServerType.USER_LOGIN_RESULT:
                        {
                            AckLogIn(sp[1]);
                        }
                        break;
                    case PacketServerType.USER_LOGOUT_RESULT:
                        {
                            AckLogOut(sp[1]);
                        }
                        break;
                    case PacketServerType.USER_SIGNUP_RESULT:
                        {
                            AckSignUp(sp[1]);
                        }
                        break;
                    case PacketServerType.ROOM_GETIN_RESULT:
                        {
                            AckRoomGetIn(sp[1]);
                        }
                        break;
                    case PacketServerType.ROOM_LIST:
                        {
                            AckRoomList(sp[1]);
                        }
                        break;
                    case PacketServerType.ROOM_MAKE_RESULT:
                        {
                            AckRoomMake(sp[1]);
                        }
                        break;

                    case PacketServerType.ROOM_GETOUT_RESULT:
                        {
                            AckRoomGetOut(sp[1]);
                        }
                        break;
                    case PacketServerType.INGAME_TICK:
                        {
                            AckIngameTick(sp[1]);
                        }
                        break;
                    case PacketServerType.INGAME_MESSAGE:
                        {
                            AckIngameMessage(sp[1]);
                        }
                        break;
                    case PacketServerType.INGAME_SOUND:
                        {
                            AckIngameSound(sp[1]);
                        }
                        break;
                    case PacketServerType.INGAME_SHAKE:
                        {
                            AckIngameShake(sp[1]);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ClientRecv :" + ex.Source + ex.StackTrace + ex.Message + " \r\n\r\n" + msg);
            }
        }
        public void ClientLog(LogType logType, string msg) 
        {
            
        }


        void AckLogIn(string msg)
        {
            string[] sp = msg.Split('#');
            bool isSucceed = bool.Parse(sp[0]);
            string data = sp[1];

            if (!isSucceed)
            {
                MessageBox.Show(data);
                return;
            }

            name = data;

            Action del = () =>
            {
                formMain.ShowMenuControl();
            };
            formMain.Invoke(del);
        }
        void AckLogOut(string msg) 
        {

            string[] sp = msg.Split('#');
            bool isSucceed = bool.Parse(sp[0]);
            string data = sp[1];

            if (!isSucceed)
            {
                MessageBox.Show(data);
                return;
            }


            Action del = () =>
            {
                formMain.ShowLoginControl();
            };
            formMain.Invoke(del);
        }
        void AckSignUp(string msg)
        {
            string[] sp = msg.Split('#');
            bool isSucceed = bool.Parse(sp[0]);
            string data = sp[1];

            if (!isSucceed)
            {
                MessageBox.Show(data);
                return;
            }

            MessageBox.Show("회원가입 성공");
        }
        void AckRoomList(string msg) 
        {
            if (formMain.mainMenu == null) return;
            List<string> list = new List<string>();

            string[] sp = msg.Split('@');
            foreach (string subString in sp) 
            {
                if (subString == string.Empty) continue;

                string[] sp2 = subString.Split('#');

                string item = $"{sp2[0]}({sp2[1]})";

                list.Add(item);
            }

            formMain.mainMenu.UpdateRoomList(list);
        }
        void AckRoomGetIn(string msg)
        {
            string[] sp = msg.Split('#');
            bool isSucceed = bool.Parse(sp[0]);
            string data = sp[1];

            if (!isSucceed)
            {
                MessageBox.Show(data);
                return;
            }

            Action del = () =>
            {
                formMain.ShowIngameControl();
            };
            formMain.Invoke(del);
        }
        void AckRoomMake(string msg) 
        {
            string[] sp = msg.Split('#');
            bool isSucceed = bool.Parse(sp[0]);
            string data = sp[1];

            if (!isSucceed)
            {
                MessageBox.Show(data);
                return;
            }

            Action del = () =>
            {
                formMain.ShowIngameControl();
            };
            formMain.Invoke(del);
        }
        void AckRoomGetOut(string msg) 
        {
            string[] sp = msg.Split('#');
            bool isSucceed = bool.Parse(sp[0]);
            string data = sp[1];

            if (!isSucceed)
            {
                MessageBox.Show(data);
                return;
            }

            Action del = () =>
            {
                formMain.ShowMenuControl();
            };
            formMain.Invoke(del);
        }
        void AckIngameTick(string msg)
        {
            if (formMain.ingameCon == null) throw new Exception("불가능한 명령, 인게임 상황이 아닌데 인게임 패킷을 받음");
            
            //인게임 데이터 전달
            formMain.ingameCon.drawEngine.LoadDraw(msg);
        }
        void AckIngameMessage(string msg)
        {
            if (formMain.ingameCon == null) throw new Exception("불가능한 명령, 인게임 상황이 아닌데 인게임 패킷을 받음");

            //인게임 메세지 전달

            formMain.ingameCon.drawEngine.LoadMessage(msg);
        }

        void AckIngameShake(string msg)
        {
            string[] sp = msg.Split('#');
            float strength = float.Parse(sp[0]);

            formMain.ingameCon.ShakeCamera(strength);
        }
        void AckIngameSound(string msg) 
        {
            string[] sp = msg.Split('#');
            SoundType type = (SoundType)int.Parse(sp[0]);
            float pointX = float.Parse(sp[1]);
            float pointY = float.Parse(sp[2]);

            formMain.ingameCon.PlaySound(type, new Vector2(pointX, pointY));
        }
    }
}
