#include <stdio.h>
#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>

#define SERVER_IP       "220.90.179.75"  //"127.0.0.1" : �ڽ� IP  
#define SERVER_PORT     9000

SOCKET sock;

int net_init();
void net_run();
void net_exit();

int main()
{
    if (net_init() == -1)
    {
        printf("���� ����\n");
        return 0;
    }

    printf("���� ����...........\n");
    net_run();

    net_exit();

    return 0;
}

int net_init()
{
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
    {
        printf("������ ���� �ʱ�ȭ ����!\n");
        return -1;
    }

    sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (sock == INVALID_SOCKET)
        return -1;

    int n_ip;
    inet_pton(AF_INET, SERVER_IP, &n_ip);  //<-------------------

    SOCKADDR_IN serveraddr;
    ZeroMemory(&serveraddr, sizeof(serveraddr));

    serveraddr.sin_family = AF_INET;
    serveraddr.sin_port = htons(SERVER_PORT);
    serveraddr.sin_addr.s_addr = n_ip;

    int retval = connect(sock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));

    if (retval == SOCKET_ERROR)
        return -1;

    return 0;
}

void net_run()
{
    char buf[512];

    while (1)
    {
        printf(">> ");      gets_s(buf, sizeof(buf));

        if (strlen(buf) == 0)   //���͸� ������ �ݺ��� ����
            break;

        int r = send(sock, buf, strlen(buf), 0);
        printf("\t ���� ����Ʈ ũ�� : %d byte\n", r);
    }
}

void net_exit()
{
    closesocket(sock);
    WSACleanup();
}
