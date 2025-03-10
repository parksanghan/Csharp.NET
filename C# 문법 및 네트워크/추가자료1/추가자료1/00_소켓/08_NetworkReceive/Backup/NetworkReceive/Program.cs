using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;  // SOAP 프로토콜 직렬화 사용

public class NetworkReceive
{
    public static void Main()
    {
        TcpListener server = new TcpListener(7000);  // TCP 7000번 포트 열기
        server.Start();

        TcpClient client = server.AcceptTcpClient();  // 외부에서 클라이언트 접속하면
        NetworkStream ns = client.GetStream();      // NetworkStream 객체를 얻어옴

        MemoryStream strm = new MemoryStream();  // 메모리 스트림 생성

        byte[] data = new byte[4];
        int recv_size = ns.Read(data, 0, 4);      // 수신할 스트림 데이터 크기 알아내기
        int size = BitConverter.ToInt32(data, 0);

        int receive = 0;

        while (size > 0)  // 상대방이 보낸 직렬화 스트림 읽기
        {
            data = new byte[1024];
            recv_size = ns.Read(data, 0, size);
            strm.Write(data, receive, recv_size);
            receive += recv_size;
            size -= recv_size;
        }

        strm.Position = 0;
        IFormatter formatter = new SoapFormatter();
        // 직렬화 인스턴스 복원 
        SerialExample ex1 = (SerialExample)formatter.Deserialize(strm);

        Console.WriteLine("ex1.id= " + ex1.id + " ex1.name= " + ex1.name);
        Console.WriteLine("ex1.kor=" + ex1.kor + " ex1.math=" + ex1.math + " ex1.eng=" + ex1.eng);
        Console.WriteLine("ex1.date  =" + ex1.date.ToString());

        strm.Close();
        ns.Close();
        client.Close();
        server.Stop();
    }
}
