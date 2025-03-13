//05_�������Ӱ����Ѽ���.cpp
//04_echoŬ���̾�Ʈ�� �����׽�Ʈ.
/*
------ Primary Thread���� ó��-----
1. accept
2. �������� ���
3. ��Ž����� ����
------------------------------------

* ����Ǹ� �����Ǵ� ���� ������
--------��� ������ ----------------
1. recv
2. send
* ����� �� ��Ĺ close()
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

        //�۽� ó��
        ret = send(clientSock, buf, (int)strlen(buf), 0);
        printf("\t echo���� : [%dbyte] %s\n", ret, buf);
    }

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

        // Ŭ���̾�Ʈ �ּ� �ް� accept �� �ް� ���������� �Ѵ� �ְ� �ְ� 
        //
        char recvip[50];
        inet_ntop(AF_INET, &clientaddr.sin_addr, recvip, sizeof(recvip));
        int recvport = ntohs(clientaddr.sin_port);
        printf("\n>> Ŭ���̾�Ʈ ���� - (%s:%d)\n", recvip, recvport);

        //3. ��� ������ ȣ��
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