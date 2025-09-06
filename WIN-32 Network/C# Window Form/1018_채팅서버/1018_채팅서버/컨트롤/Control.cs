//Control.cs
 
using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Sockets;

namespace _1018_채팅서버
{
	internal class Control : IDisposable
    {
		Action<Socket, string> del = null;// => template 기반의 대리자 
		List<Member> mem = new List<Member>();
		public void dd(Socket ds, string s)
		{
		 
		}
		private Server server = null;
		private const int SERVER_PORT  = 7000;

		#region 싱글톤(생성자에서 파일로드) - 수정X
		public static Control Instance { get; private set; } = null;
		static Control()
        {
			Instance = new Control();
        }
		private Control()
        {			
		}
		#endregion

		#region 소멸처리 - 수정 X
		public void Dispose()
		{
			server.Stop();
			GC.SuppressFinalize(this);
		}

		~Control()
        {
			Dispose();
		}
        #endregion

		# region 서버 시작 - 수정 X
        public void Run()
        {
			server = new Server(SERVER_PORT, RecvDel, LogDel);
			server.Start();
 

        }
	 
		#endregion
		public void servere()
		{
            IPEndPoint localEndPoint = (IPEndPoint)server.;
            IPAddress localIPAddress = localEndPoint.Address;


        }
        #region Callback - 네트워크에서 호출 : 적절히 수정
        public void RecvDel(Socket s, string msg)
		{
			 
			string[] sp = msg.Split('\a');
			if (sp[0] == "shortmessage")		ShortMessage(s, sp[1]);			
		}

		public void LogDel(Socket s, Log log, string msg)
		{
			//string temp = string.Format(log.ToString() + "\t" + msg);
			//Console.WriteLine(temp);
		}
		#endregion

		#region 클라이언트가 요청한 기능 처리 메서드 - 수정 필요

		public void ShortMessage(Socket s, string msg)
		{
			Console.WriteLine("메시지 수신\n");
			Console.WriteLine(msg);

			try
			{
				string[] sp = msg.Split('#');

				string idname = sp[0];
				string pwmsg	= sp[1];
				string name = sp[2];

				//응답처리
				string packet = Packet.ShortMessage(idname,pwmsg, name);
				server.SendAllData(s, packet, true);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}		
        #endregion
    }
}
