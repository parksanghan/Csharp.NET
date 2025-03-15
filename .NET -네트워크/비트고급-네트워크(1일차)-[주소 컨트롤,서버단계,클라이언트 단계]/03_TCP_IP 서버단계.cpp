#include <stdio.h>
#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>

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
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
        return -1;

    listenSock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (listenSock == INVALID_SOCKET)
        return -1;

    SOCKADDR_IN serveraddr;
    memset(&serveraddr, 0, sizeof(serveraddr));

    serveraddr.sin_family = AF_INET;
    serveraddr.sin_port = htons(9000);
    serveraddr.sin_addr.s_addr = htonl(INADDR_ANY);

    int retval = bind(listenSock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
    if (retval == SOCKET_ERROR)
        return -1;

    retval = listen(listenSock, SOMAXCONN);
    if (retval == SOCKET_ERROR)
        return -1;

    return 0;
}

void net_run()
{
    char buf[512];

    while (1)
    {
        SOCKADDR_IN clientaddr;
        int addrlen = sizeof(clientaddr);

        SOCKET clientSock = accept(listenSock, (SOCKADDR*)&clientaddr, &addrlen);
        if (clientSock == INVALID_SOCKET)
            break;

        char recvip[50];
        inet_ntop(AF_INET, &clientaddr.sin_addr, recvip, sizeof(recvip));
        int recvport = ntohs(clientaddr.sin_port);
        printf("\n>> 클라이언트 접속 - (%s:%d)\n", recvip, recvport);

        while (1)
        {
            int ret = recv(clientSock, buf, sizeof(buf), 0);
            if (ret > 0)
            {
                buf[ret] = '\0';
                printf("\t %s\n", buf);
            }
            else if (ret == 0)
            {
                printf("상대방이 소켓을 종료\n");
                break;
            }
            else
            {
                printf("상대방이 소켓을 종료하지 않고 프로그램을 종료함\n");
                break;
            }
        }
    }
}

void net_exit()
{
    closesocket(listenSock);
    WSACleanup();
}
