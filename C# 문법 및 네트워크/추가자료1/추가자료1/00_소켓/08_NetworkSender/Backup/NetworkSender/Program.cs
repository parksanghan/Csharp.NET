using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;   // SOAP 프로토콜 형태로 직렬화 데이터 전송

public class NetworkSender
{
    public static void Main()
    {
        TcpClient server = new TcpClient("127.0.0.1", 7000);  // 서버에 접속
        NetworkStream ns = server.GetStream();               // NetworkStream 얻기

        SerialExample ex1 = new SerialExample();             // 직렬화 인스턴스 생성
        ex1.id = 1;
        ex1.name = "홍길동";
        ex1.kor = 80;
        ex1.math = 50;
        ex1.eng = 100;

        IFormatter formatter = new SoapFormatter();   // IFormatter 생성
        MemoryStream strm = new MemoryStream();   // 메모리 스트림 생성 

        formatter.Serialize(strm, ex1);    // 메모리 스트림에 직렬화 인스턴스 생성

        byte[] data = strm.GetBuffer();
        int data_size = (int)strm.Length;
        byte[] size = BitConverter.GetBytes(data_size);  // 메모리 스트림 전체 크기전송

        ns.Write(size, 0, 4);              // 전송할 데이터 사이즈 전송
        ns.Write(data, 0, data_size);      // 메모리 스트림에 있는 데이터 전송
        ns.Flush();

        strm.Close();
        ns.Close();
        server.Close();
    }
}

