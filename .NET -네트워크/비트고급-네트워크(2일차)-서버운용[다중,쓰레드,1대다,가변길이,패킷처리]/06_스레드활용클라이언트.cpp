//06_������Ȱ��Ŭ���̾�Ʈ.cpp
/*
Primary T : connect ���� ������ ���� send�� �ݺ�ó��
2nd     T : recv�� �ݺ� ó��
*/
//05_�������Ӱ����Ѽ������� ��밡��!
//07_1������ۼ������� ��밡��!
#include <stdio.h>

#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()...
#include <process.h>

#define SERVER_IP       "220.90.179.75"  //"127.0.0.1" : �ڽ� IP  
#define SERVER_PORT     9000
SOCKET sock;

int net_init();
void net_run();
void net_exit();
unsigned int __stdcall WorkThread(void* p);

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

    //������ ����
    CloseHandle((HANDLE)_beginthreadex(0, 0, WorkThread, (void*)sock, 0, 0));

    return 0;
}

void net_run()
{
    char buf[512];

    while (1)
    {
        //����� �Է�
        printf(">> ");      gets_s(buf, sizeof(buf));
        if (strlen(buf) == 0)   //���͸� ������ �ݺ��� ����
            break;

        //������ ����
        int r = send(sock, buf, (int)strlen(buf), 0);
        printf("\t ��������Ʈ ũ�� : %dbyte\n", r);       
    }
}

void net_exit()
{
    closesocket(sock);
    WSACleanup();
}

unsigned int __stdcall WorkThread(void* p)
{
    SOCKET clientSock = (SOCKET)p;

    char buf[512];

    while (1)
    {
        //�������� ���� �޽��� ����
        int ret = recv(clientSock, buf, sizeof(buf), 0);
        if (ret > 0)
        {
            buf[ret] = '\0'; //??
            printf("\t %s\n", buf);
        }
        else if (ret == 0)
        {
            printf("������ ��Ĺ�� ����\n");
            break;
        }
        else  // -1���� ����
        {
            printf("������ ������ �������� �ʰ� ���α׷��� ������\n");
            break;
        }
    }

    return 0;
}