//net.cpp
#include "std.h"

SOCKET sock;

int net_Start(const char* ip, int port)
{
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
    {
        printf("윈도우 소켓 초기화 실패!\n");
        return -1;
    }

    sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (sock == INVALID_SOCKET)
        return -1;

    int n_ip;
    inet_pton(AF_INET, ip, &n_ip);  //<-------------------

    SOCKADDR_IN serveraddr;
    ZeroMemory(&serveraddr, sizeof(serveraddr));

    serveraddr.sin_family = AF_INET;
    serveraddr.sin_port = htons(port);
    serveraddr.sin_addr.s_addr = n_ip;
    int retval = connect(sock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
    if (retval == SOCKET_ERROR)
        return -1;

    //스레드 생성
    CloseHandle((HANDLE)_beginthreadex(0, 0, WorkThread, (void*)sock, 0, 0));

    return 0;
}

void net_Stop()
{
    closesocket(sock);
    WSACleanup();
}

unsigned int __stdcall WorkThread(void* p)
{
    SOCKET clientSock = (SOCKET)p;

    char buf[4096];
    while (1)
    {
        //서버에서 보낸 메시지 수신
        int ret = net_RecvData(clientSock, buf, sizeof(buf));
        if (ret > 0)
        {
            con_RecvData(buf);
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
    return 0;
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

int net_SendData(const char* msg, int size)
{
    //헤더(4byte)
    int ret = send(sock, (char*)&size, sizeof(int), 0);

    //본데이터
    ret = send(sock, msg, size, 0);

    return ret;
}
