//12_패킷처리클라이언트.cpp

#include <stdio.h>
#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()...
#include <process.h>
#include "packet.h"

#define SERVER_IP       "220.90.179.75"  //"127.0.0.1" : 자신 IP  
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
        printf("연결 실패\n");
        return 0;
    }

    printf("연결 성공...........\n");
    net_run();

    net_exit();

    return 0;
}

int net_init()
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
    inet_pton(AF_INET, SERVER_IP, &n_ip);  //<-------------------

    SOCKADDR_IN serveraddr;
    ZeroMemory(&serveraddr, sizeof(serveraddr));

    serveraddr.sin_family = AF_INET;
    serveraddr.sin_port = htons(SERVER_PORT);
    serveraddr.sin_addr.s_addr = n_ip;
    int retval = connect(sock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
    if (retval == SOCKET_ERROR)
        return -1;

    //스레드 생성
    CloseHandle((HANDLE)_beginthreadex(0, 0, WorkThread, (void*)sock, 0, 0));

    return 0;
}

void net_run()
{
    
    int idx;
    while (1)
    {
        printf("--------------------------------------\n");
        printf("[1] 메시지 전송 [2] 연산결과 요청 [3]종료\n");
        printf("--------------------------------------\n");
        scanf_s("%d", &idx);
        char dummy = getchar();
        if (idx == 1)
        {
            PacketMessage1 pack;
            pack.flag = MESSAGE1;
            strcpy_s(pack.name, sizeof(pack.name), "홍길동");
            printf("메시지 입력 : ");
            gets_s(pack.msg, sizeof(pack.msg));

            //서버로 전송
            int r = net_SendData(sock, (char*)&pack, sizeof(PacketMessage1));
            printf("\t 보낸바이트 크기 : %dbyte\n", r);
        }
        else if (idx == 2)
        {
            PacketMessage2 pack;
            pack.flag = MESSAGE2;
            strcpy_s(pack.name, sizeof(pack.name), "홍길동");
            printf("정수 입력 : ");  scanf_s("%d", &pack.x);
            printf("정수 입력 : ");  scanf_s("%d", &pack.y);

            //서버로 전송
            int r = net_SendData(sock, (char*)&pack, sizeof(PacketMessage2));
            printf("\t 보낸바이트 크기 : %dbyte\n", r);
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
        //서버에서 보낸 메시지 수신
        int ret = net_RecvData(clientSock, buf, sizeof(buf));
        if (ret > 0)
        {
            //buf[ret] = '\0'; //??
           // printf("\t %s\n", buf);
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
        
        //----------데이터 처리 코드----------------------------
        int* p = (int*)buf;
        if (*p == MESSAGE1)
        {
            PacketMessage1* pm = (PacketMessage1*)buf;
            //브로드캐스팅
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
    //헤더
    int ret = send(s, (char*)&size, sizeof(int), 0);
    //본데이터
    ret = send(s, msg, size, 0);

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