//net.cpp
#include "std.h"

//전역변수 : 대기소켓, 통신소켓들
SOCKET listenSock;
vector<SOCKET> sockets;

int net_init(int port)
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
    serveraddr.sin_port = htons(port);       //지역포트번호
    serveraddr.sin_addr.s_addr = htonl(INADDR_ANY); //지역IP 주소
    int retval = bind(listenSock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
    if (retval == SOCKET_ERROR)
        return -1;

    retval = listen(listenSock, SOMAXCONN);
    if (retval == SOCKET_ERROR)
        return -1;

    return 0;
}

void net_exit()
{
    closesocket(listenSock);
    WSACleanup();
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

unsigned int  __stdcall WorkThread(void* p)
{
    SOCKET clientSock = (SOCKET)p;

    char buf[4096]; //4kb
    while (1)
    {
        //수신 처리
        int ret = net_RecvData(clientSock, buf, sizeof(buf));
        if (ret > 0)
        {
            con_RecvData(clientSock, buf);  //주소 전달
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

int net_SendData(SOCKET s, char* msg, int size)
{
    //헤더
    int ret = send(s, (char*)&size, sizeof(int), 0);
    //본데이터
    ret = send(s, msg, size, 0);

    return ret;
}

//1. 접속한 모든 클라이언트에게 전송(본인 제외)
int net_SendAllData(SOCKET sock, char* msg, int size)
{  
    int ret = 0;
    for (int i = 0; i < sockets.size(); i++)
    {
        SOCKET s = sockets[i];
        if (sock != s)
        {
            ret = net_SendData(s, msg, size);
        }
    }
    printf("\t echo브로드캐스트전송 : [%dbyte]\n", size);
    return ret;
}

int net_RecvData(SOCKET s, char* msg, int size)
{
    int header;
    //1) 헤더(크기)
    int ret = recvn(s, (char*)&header, sizeof(int), 0);

    //2) 본 데이터
    ret = recvn(s, msg, header, 0);

    return ret;
}

int recvn(SOCKET s, char* buf, int len, int flags)
{
    int received;
    char* ptr = buf;
    int left = len;

    while (left > 0) {
        received = recv(s, ptr, left, flags);
        if (received == SOCKET_ERROR)
            return SOCKET_ERROR;
        else if (received == 0)
            break;
        left -= received;
        ptr += received;
    }
    return (len - left);
}