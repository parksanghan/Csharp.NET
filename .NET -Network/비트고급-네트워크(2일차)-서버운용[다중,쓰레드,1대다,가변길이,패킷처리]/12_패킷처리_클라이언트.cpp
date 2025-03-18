//12_��Ŷó��Ŭ���̾�Ʈ.cpp

#include <stdio.h>
#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()...
#include <process.h>
#include "packet.h"

#define SERVER_IP       "220.90.179.75"  //"127.0.0.1" : �ڽ� IP  
#define SERVER_PORT     9000
SOCKET sock;

int net_init();
void net_run();
void net_exit();
unsigned int __stdcall WorkThread(void* p);

int recvn(SOCKET s, char* buf, int len, int flags);
int net_SendData(SOCKET s, char* msg, int size);
int net_RecvData(SOCKET s, char* msg, int size);

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
    
    int idx;
    while (1)
    {
        printf("--------------------------------------\n");
        printf("[1] �޽��� ���� [2] ������ ��û [3]����\n");
        printf("--------------------------------------\n");
        scanf_s("%d", &idx);
        char dummy = getchar();
        if (idx == 1)
        {
            PacketMessage1 pack;
            pack.flag = MESSAGE1;
            strcpy_s(pack.name, sizeof(pack.name), "ȫ�浿");
            printf("�޽��� �Է� : ");
            gets_s(pack.msg, sizeof(pack.msg));

            //������ ����
            int r = net_SendData(sock, (char*)&pack, sizeof(PacketMessage1));
            printf("\t ��������Ʈ ũ�� : %dbyte\n", r);
        }
        else if (idx == 2)
        {
            PacketMessage2 pack;
            pack.flag = MESSAGE2;
            strcpy_s(pack.name, sizeof(pack.name), "ȫ�浿");
            printf("���� �Է� : ");  scanf_s("%d", &pack.x);
            printf("���� �Է� : ");  scanf_s("%d", &pack.y);

            //������ ����
            int r = net_SendData(sock, (char*)&pack, sizeof(PacketMessage2));
            printf("\t ��������Ʈ ũ�� : %dbyte\n", r);
        }
        else if (idx == 3)
        {
            break;
        }
        
        Sleep(1000);
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
        int ret = net_RecvData(clientSock, buf, sizeof(buf));
        if (ret > 0)
        {
            //buf[ret] = '\0'; //??
           // printf("\t %s\n", buf);
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
        
        //----------������ ó�� �ڵ�----------------------------
        int* p = (int*)buf;
        if (*p == MESSAGE1)
        {
            PacketMessage1* pm = (PacketMessage1*)buf;
            //��ε�ĳ����
            printf("\t [%s] %s\n", pm->name, pm->msg);
        }
        else if (*p == MESSAGE2)
        {
            PacketMessage2* pm = (PacketMessage2*)buf;
            printf("%d + %d = %d\n", pm->x, pm->y, pm->result);
        }
        //-----------------------------------------------------      
    }

    return 0;
}


int net_SendData(SOCKET s, char* msg, int size)
{
    //���
    int ret = send(s, (char*)&size, sizeof(int), 0);
    //��������
    ret = send(s, msg, size, 0);

    return ret;
}

int net_RecvData(SOCKET s, char* msg, int size)
{
    int header;
    //1) ���(ũ��)
    int ret = recvn(s, (char*)&header, sizeof(int), 0);

    //2) �� ������
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