//05_다중접속가능한서버.cpp
//04_echo클라이언트로 연결테스트.
/*
------ Primary Thread에서 처리-----
1. accept
2. 연결정보 출력
3. 통신스레드 생성
------------------------------------

* 연결되면 생성되는 전용 스레드
--------통신 스레드 ----------------
1. recv
2. send
* 종료될 때 소캣 close()
-------------------------------------
*/
#include <stdio.h>

#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()...
#include <process.h>        //_beginthread()....

//cmd >> ipconfig :  220.90.179.75
SOCKET listenSock;

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

        //송신 처리
        ret = send(clientSock, buf, (int)strlen(buf), 0);
        printf("\t echo전송 : [%dbyte] %s\n", ret, buf);
    }

    closesocket(clientSock);
    return 0;
}


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

        // 클ㄹ이언트 주소 받고 accept 로 받고 담긴정보대로 둘다 넣고 넣고 
        //
        char recvip[50];
        inet_ntop(AF_INET, &clientaddr.sin_addr, recvip, sizeof(recvip));
        int recvport = ntohs(clientaddr.sin_port);
        printf("\n>> 클라이언트 접속 - (%s:%d)\n", recvip, recvport);

        //3. 통신 스레드 호출
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