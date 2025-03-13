//07_1대다통신서버.cpp
 
//06_스레드활용클라이언트만 사용 가능
/*
클라이언트 접속 시 생성된 통신 소켓을 관리!

     전역변수에 통신 소켓들을 저장
      vector<SOCKET> sockets;

저장  : [ net_run() ] accept() 이후
        1)accept()
        2)정보출력
        3)저장!!!!!!!!!
        4)스레드 생성

삭제  : [WorkThread() ] recv() 이후 0보다 작거나 같은값반환
         반복문
            1) recv
            2.1) 출력
            2.2) break
            2.3) break
            3) send

          5) 삭제!
          6) 소켓close()

검색  : [WorkThread] 데이터를 수신한 후
        반복문
            1) recv
            2.1) 출력
            2.2) break
            2.3) break
            3) send --> sendAll!!!!!!!!!

          5) 삭제!
          6) 소켓close()
수정  : X

*/
#include <stdio.h>
#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()...
#include <process.h>        //_beginthread()....
#include <vector>
using namespace std;

//cmd >> ipconfig :  220.90.179.75

//전역변수 : 대기소켓, 통신소켓들
SOCKET listenSock;
vector<SOCKET> sockets;

int net_init();
void net_run();
void net_exit();

int main()
{
    if (net_init() == -1)
    {
        printf("초기화 오류\n");
        return 0;
    }

    printf("서버 구동...........\n");
    net_run();

    net_exit();

    return 0;
}

int net_init()
{
    //서버에서 문자열 치면  모든 소켓에게 echo 보낸다. 
    // 
    //1. 초기화
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
        return -1;

    //2. socket생성-> bind()주소할당 -> listen()망 연결    
    listenSock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (listenSock == INVALID_SOCKET)
        return -1;

    SOCKADDR_IN serveraddr;
    //ZeroMemory(&serveraddr, sizeof(serveraddr));  //API
    memset(&serveraddr, 0, sizeof(serveraddr));     //C

    serveraddr.sin_family = AF_INET;           //주소체계
    serveraddr.sin_port = htons(9000);       //지역포트번호
    serveraddr.sin_addr.s_addr = htonl(INADDR_ANY); //지역IP 주소
    int retval = bind(listenSock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
    if (retval == SOCKET_ERROR)
        return -1;

    retval = listen(listenSock, SOMAXCONN);
    if (retval == SOCKET_ERROR)
        return -1;

    return 0;
}

unsigned int  __stdcall WorkThread(void* p)
{
    SOCKET clientSock = (SOCKET)p;

    char buf[512];
    while (1)
    {
        //수신 처리
        int ret = recv(clientSock, buf, sizeof(buf), 0);
        if (ret > 0)
        {
            buf[ret] = '\0'; //??
            printf("\t %s\n", buf);
        }
        else if (ret == 0)
        {
            printf("상대방이 소캣을 종료\n");
            break;
        }
        else  // -1이하 에러
        {
            printf("상대방이 소켓을 종료하지 않고 프로그램을 종료함\n");
            break;
        }

        //접속한 모든 클라이언트에게 전송
        for (int i = 0; i < sockets.size(); i++)
        {
            SOCKET s = sockets[i];
            ret = send(s, buf, (int)strlen(buf), 0);
        }
        printf("\t echo브로드캐스트전송 : [%dbyte] %s\n", ret, buf);

        //보낸 사람에게만 전송
        //ret = send(clientSock, buf, (int)strlen(buf), 0);
        //printf("\t echo전송 : [%dbyte] %s\n", ret, buf);
    }

    //삭제
    for (int i = 0; i < sockets.size(); i++)
    {
        SOCKET s = sockets[i];
        if (s == clientSock)
        {
            sockets.erase(sockets.begin() + i);
            break;
        }
    }

    //소켓 close
    closesocket(clientSock);
    return 0;
}

// 요청을 할때마다 얻어온 소켓을 쓰레드로 넘김
void net_run()
{
    while (1)
    {
        //1. 대기큐에서 연결 요청 수락!
        SOCKADDR_IN clientaddr;
        int addrlen = sizeof(clientaddr);

        SOCKET clientSock = accept(listenSock, (SOCKADDR*)&clientaddr, &addrlen);
        if (clientSock == INVALID_SOCKET)
            continue; // break; // 

        //2. 정보 출력
        //clinetaddr.sin_addr : ip숫자(네트워크 바이트 정렬)
        //inet_ntoa() : 호스트 바이트 정렬로 변환 ->변환된 숫자를 문자열 형태 IP
        char recvip[50];
        inet_ntop(AF_INET, &clientaddr.sin_addr, recvip, sizeof(recvip));
        int recvport = ntohs(clientaddr.sin_port);
        printf("\n>> 클라이언트 접속 - (%s:%d)\n", recvip, recvport);

        //3. 통신 소켓 저장
        sockets.push_back(clientSock);

        //4. 통신 스레드 호출
        //스레드가 생성될 때 통신할 소켓을 전달!
        uintptr_t h1 = _beginthreadex(0, 0, WorkThread, (void*)clientSock, 0, 0);
        CloseHandle((HANDLE)h1);
    }
}

void net_exit()
{
    closesocket(listenSock);
    WSACleanup();
}