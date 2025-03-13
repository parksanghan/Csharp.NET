#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <stdio.h>
#include <WinSock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()

// IP ��ȯ (���ڿ�, ����)
// inet_addr: ���ڿ� -> ����
// inet_ntoa: ���� -> ���ڿ�

int exam1()
{
    // 1. �ʱ�ȭ
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
    {
        printf("������ ���� �ʱ�ȭ ����!\n");
        return -1;
    }
    printf("������ ���� �ʱ�ȭ ����!\n");

    // �ּ� ��ȯ
    // ���ڿ� �ּ� -> ����
    char ipaddr[50] = "230.200.12.5";
    int n_ip = inet_addr(ipaddr);

    printf("IP ���ڿ� �ּ�: %s\n", ipaddr);
    printf("IP ���ڿ� �ּ� -> ����: 0x%08X\n\n", n_ip);

    // ���� -> ���ڿ� �ּ�
    IN_ADDR in_addr;
    in_addr.S_un.S_addr = n_ip;
    printf("IP ���� -> ���ڿ� �ּ�: %s\n", inet_ntoa(in_addr));

    // 2. ���� ó��
    WSACleanup();

    return 0;
}

// IN_ADDR: ���� ����� ����ü�� ���� �ִ�.
// �̸� �̿��ؼ� ���� <-> ���ڿ� ��ȯ�� ���ϰ� �� �� �ִ�.

void exam2()
{
    IN_ADDR in_addr;

    in_addr.S_un.S_addr = 0x01020304;

    printf("%d, %d, %d, %d\n",
        in_addr.S_un.S_un_b.s_b1,
        in_addr.S_un.S_un_b.s_b2,
        in_addr.S_un.S_un_b.s_b3,
        in_addr.S_un.S_un_b.s_b4);

    char buf[20];
    sprintf_s(buf, "%d.%d.%d.%d", in_addr.S_un.S_un_b.s_b1,
        in_addr.S_un.S_un_b.s_b2,
        in_addr.S_un.S_un_b.s_b3,
        in_addr.S_un.S_un_b.s_b4);

    printf("%s\n", buf);
}

// IPv4 -> Ipv4 or IPv6
int exam3()
{
    // 1. �ʱ�ȭ
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
    {
        printf("������ ���� �ʱ�ȭ ����!\n");
        return -1;
    }
    printf("������ ���� �ʱ�ȭ ����!\n");

    // �ּ� ��ȯ
    // ���ڿ� �ּ� -> ����
    char ipaddr[50] = "230.200.12.5";
    int n_ip;
    inet_pton(AF_INET, ipaddr, &n_ip);

    printf("IP ���ڿ� �ּ�: %s\n", ipaddr);
    printf("IP ���ڿ� �ּ� -> ����: 0x%08X\n\n", n_ip);

    // ���� -> ���ڿ� �ּ�
    IN_ADDR in_addr;
    in_addr.S_un.S_addr = n_ip;

    char recvip[50];
    inet_ntop(AF_INET, &in_addr, recvip, sizeof(recvip));

    printf("IP ���� -> ���ڿ� �ּ�: %s\n", recvip);

    // 2. ���� ó��
    WSACleanup();

    return 0;
}

// ����Ʈ ����
// Host(L or B) -> Net(B)
// Net(B) -> Host(L or B)

void exam4()
{
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
    {
        printf("������ ���� �ʱ�ȭ ����!\n");
        return;
    }

    unsigned short us = 0x1234;
    unsigned long ul = 0x12345678;

    // ȣ��Ʈ ����Ʈ�� ��Ʈ��ũ ����Ʈ�� ��ȯ�Ѵ�.
    printf("0x%08X -> 0x%08X\n", us, htons(us));
    printf("0x%08X -> 0x%08X\n", ul, htonl(ul));

    unsigned short n_us = htons(us);
    unsigned long n_ul = htonl(ul);

    // ��Ʈ��ũ ����Ʈ�� ȣ��Ʈ ����Ʈ�� ��ȯ�Ѵ�.
    printf("0x%08X -> 0x%08X\n", n_us, ntohs(n_us));
    printf("0x%08X -> 0x%08X\n", n_ul, ntohl(n_ul));

    WSACleanup();
}

// �ּ� ����ϱ�
// ��ǥ ����ü: SOCKADDR
// ��� ����ü: SOCKADDR_IN

void exam5()
{
    SOCKADDR_IN addr;

    // 1. IPv4
    addr.sin_family = AF_INET;

    // 2. Host -> Net �����ؼ� ����
    addr.sin_port = htons(9000);

    // 3. ���ڿ� ���� IP
    char ipaddr[50] = "127.0.0.1";

    // 3.1 ���ڿ��� ���ڷ� ����
    int h_ip;
    inet_pton(AF_INET, ipaddr, &h_ip);

    // 3.2 ������ IP�� Net ����Ʈ ������ ����
    int n_ip = htonl(h_ip);

    // 3.3 IP�� ǥ���ϴ� ����ü Ÿ�Կ� ����
    IN_ADDR in_addr;
    in_addr.S_un.S_addr = n_ip;

    // 3.4 IP�� ����
    addr.sin_addr = in_addr;

    // ����ü�� ���� �Լ��� ����!(H -> N �����ؼ� ����!)
}

int main()
{
    exam5();

    return 0;
}