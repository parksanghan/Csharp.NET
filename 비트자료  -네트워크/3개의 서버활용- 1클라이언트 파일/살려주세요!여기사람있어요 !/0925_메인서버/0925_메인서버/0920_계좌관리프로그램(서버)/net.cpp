//net.cpp
#include "std.h"

//�������� : ������, ��ż��ϵ�
SOCKET listenSock;
vector<SOCKET> sockets;

int net_init(int port)
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
    serveraddr.sin_port = htons(port);       //������Ʈ��ȣ
    serveraddr.sin_addr.s_addr = htonl(INADDR_ANY); //����IP �ּ�
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

unsigned int  __stdcall WorkThread(void* p)
{
    SOCKET clientSock = (SOCKET)p;

    char buf[4096]; //4kb
    while (1)
    {
        //���� ó��
        int ret = net_RecvData(clientSock, buf, sizeof(buf));
        if (ret > 0)
        {
            con_RecvData(clientSock, buf);  //�ּ� ����
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

int net_SendData(SOCKET s, char* msg, int size)
{
    //���
    int ret = send(s, (char*)&size, sizeof(int), 0);
    //��������
    ret = send(s, msg, size, 0);

    return ret;
}

//1. ������ ��� Ŭ���̾�Ʈ���� ����(���� ����)
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
    printf("\t echo��ε�ĳ��Ʈ���� : [%dbyte]\n", size);
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