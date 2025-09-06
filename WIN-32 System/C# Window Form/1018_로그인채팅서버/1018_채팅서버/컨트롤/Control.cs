//Control.cs
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace _1018_채팅서버
{
	internal class Control : IDisposable
    {		
		private Server server = null;
		private const int SERVER_PORT  = 8000;

		private List<Member> members = new List<Member>();

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

		#region Callback - 네트워크에서 호출 : 적절히 수정
		public void RecvDel(Socket s, string msg) // 데이터를 전송한 소켓 정보 클라이언트 정보가 있음 
		{  // GameControl 에서  singleton 메서드를 통해 Server 클래스의 소켓 리스트를 가져오고 
			// 만약에 ?  여기서 패킷에 게임 접속에 대한 메시지 발생시  게임 유저 socket 리스트에 저장
			string[] sp = msg.Split('\a');
			if (sp[0] == "memberjoin")
				MemberJoin(s, sp[1]);
			else if (sp[0] == "login")
				LogIn(s, sp[1]);
		}
		// 조건에 대해 호출되는 함수 정의 
		// ** 

		
		public void LogDel(Socket s, Log log, string msg)
		{
			//string temp = string.Format(log.ToString() + "\t" + msg);
			//Console.WriteLine(temp);
		}
		#endregion

		#region 클라이언트가 요청한 기능 처리 메서드 - 수정 필요

		public void MemberJoin(Socket s, string msg) // 
		{
			Console.WriteLine("메시지 수신\n");
			Console.WriteLine(msg);

			try
			{
				string[] sp = msg.Split('#');

				string id	= sp[0];
				string pw	= sp[1];
				string name = sp[2];

				//연산~
				members.Add(new Member(id, pw, name));

				//응답처리
				string packet = Packet.MemberJoin(true, id, pw, name);
				server.SendData(s, packet);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				string packet = Packet.MemberJoin(false, "-", "-", "-");
				server.SendData(s, packet);
			}
		}

		public void LogIn(Socket s, string msg)
        {
			try
			{
				string[] sp = msg.Split('#');

				string id = sp[0];
				string pw = sp[1];

				//연산~
				Member member = members.Find(value => value.Id == id && value.Pw == pw);
				if (member == null)
					throw new Exception(sp[0]);	//ID

				//응답처리
				string packet = Packet.LogIn(true, member.Id, member.Pw, member.Name);
				server.SendData(s, packet);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				string packet = Packet.LogIn(false, ex.ToString(), "-", "-");
				server.SendData(s, packet);
			}
		}
		#endregion
	}
}
