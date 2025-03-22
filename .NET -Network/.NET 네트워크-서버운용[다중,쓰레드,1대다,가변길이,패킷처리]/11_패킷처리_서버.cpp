//11_��Ŷó��_����.cpp
#define MESSAGE1  1
#define MESSAGE2  2

typedef struct PacketMessage1
{
    int flag;
    char name[20];
    char msg[100];
} MSG1;

typedef struct PacketMessage2
{
    int flag;
    char name[20];
    int x;
    int y;
    int result;
} MSG2;

#include <stdio.h>
#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()...
#include <process.h>        //_beginthread()....
#include <vector>
using namespace std;

#include "packet.h"

//cmd >> ipconfig :  220.90.179.75

//�������� : ������, ��ż��ϵ�
SOCKET listenSock;
vector<SOCKET> sockets;

int net_init();
void net_run();
void net_exit();

int recvn(SOCKET s, char* buf, int len, int flags);
int net_SendData(SOCKET s, char* msg, int size);
int net_RecvData(SOCKET s, char* msg, int size);

int main()
{
    if (net_init() == -1)
    {
        printf("�ʱ�ȭ ����\n");
        return 0;
    }

    printf("���� ����...........\n");
    net_run();

    net_exit();

    return 0;
}

int net_init()
{
    //1. �ʱ�ȭ
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
        return -1;

    //2. socket����-> bind()�ּ��Ҵ� -> listen()�� ����    
    listenSock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (listenSock == INVALID_SOCKET)
        return -1;

    SOCKADDR_IN serveraddr;
    //ZeroMemory(&serveraddr, sizeof(serveraddr));  //API
    memset(&serveraddr, 0, sizeof(serveraddr));     //C

    serveraddr.sin_family = AF_INET;           //�ּ�ü��
    serveraddr.sin_port = htons(9000);       //������Ʈ��ȣ
    serveraddr.sin_addr.s_addr = htonl(INADDR_ANY); //����IP �ּ�
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
        //���� ó��
        int ret = net_RecvData(clientSock, buf, sizeof(buf));
        if (ret > 0)
        {
            //buf[ret] = '\0'; //??
            //printf("\t %s\n", buf);
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
        int* p = (int*)buf; // ���� �ּ� ������ 
        if (*p == MESSAGE1)
        {
            PacketMessage1* pm = (PacketMessage1*)buf;
            

            PacketMessage1* buf = (PacketMessage1*)buf;
            buf->flag; 
            buf->msg;
            buf->name;

            //��ε�ĳ����
            printf("\t [%s] %s\n", pm->name, pm->msg);

            //������ ��� Ŭ���̾�Ʈ���� ����
            for (int i = 0; i < sockets.size(); i++)
            {
                SOCKET s = sockets[i];
                ret = net_SendData(s, buf, sizeof(PacketMessage1));
            }
            printf("\t echo��ε�ĳ��Ʈ���� : [%dbyte]\n", ret);

        }
        else if (*p == MESSAGE2)
        {
            PacketMessage2* pm = (PacketMessage2*)buf;
            //���� ������Ը� ���� ��� ��ȯ
            pm->result = pm->x + pm->y;
            ret = net_SendData(clientSock, buf, sizeof(PacketMessage2));
            printf("\t echo���� : [%dbyte]\n", ret);
        }
        //-------------------------------------------------------     
    }

    //����
    for (int i = 0; i < sockets.size(); i++)
    {
        SOCKET s = sockets[i];
        if (s == clientSock)
        {
            sockets.erase(sockets.begin() + i);
            break;
        }
    }

    //���� close
    closesocket(clientSock);
    return 0;
}

void net_run()
{
    while (1)
    {
        //1. ���ť���� ���� ��û ����!
        SOCKADDR_IN clientaddr;
        int addrlen = sizeof(clientaddr);

        SOCKET clientSock = accept(listenSock, (SOCKADDR*)&clientaddr, &addrlen);
        if (clientSock == INVALID_SOCKET)
            continue; // break; // 

        //2. ���� ���
        //clinetaddr.sin_addr : ip����(��Ʈ��ũ ����Ʈ ����)
        //inet_ntoa() : ȣ��Ʈ ����Ʈ ���ķ� ��ȯ ->��ȯ�� ���ڸ� ���ڿ� ���� IP
        char recvip[50];
        inet_ntop(AF_INET, &clientaddr.sin_addr, recvip, sizeof(recvip));
        int recvport = ntohs(clientaddr.sin_port);
        printf("\n>> Ŭ���̾�Ʈ ���� - (%s:%d)\n", recvip, recvport);

        //3. ��� ���� ����
        sockets.push_back(clientSock);

        //4. ��� ������ ȣ��
        //�����尡 ������ �� ����� ������ ����!
        uintptr_t h1 = _beginthreadex(0, 0, WorkThread, (void*)clientSock, 0, 0);
        CloseHandle((HANDLE)h1);
    }
}

void net_exit()
{
    closesocket(listenSock);
    WSACleanup();
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