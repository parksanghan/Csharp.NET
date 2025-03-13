#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <stdio.h>
#include <WinSock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()

// IP 변환 (문자열, 정수)
// inet_addr: 문자열 -> 정수
// inet_ntoa: 정수 -> 문자열

int exam1()
{
    // 1. 초기화
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
    {
        printf("윈도우 소켓 초기화 실패!\n");
        return -1;
    }
    printf("윈도우 소켓 초기화 성공!\n");

    // 주소 변환
    // 문자열 주소 -> 정수
    char ipaddr[50] = "230.200.12.5";
    int n_ip = inet_addr(ipaddr);

    printf("IP 문자열 주소: %s\n", ipaddr);
    printf("IP 문자열 주소 -> 정수: 0x%08X\n\n", n_ip);

    // 정수 -> 문자열 주소
    IN_ADDR in_addr;
    in_addr.S_un.S_addr = n_ip;
    printf("IP 정수 -> 문자열 주소: %s\n", inet_ntoa(in_addr));

    // 2. 종료 처리
    WSACleanup();

    return 0;
}

// IN_ADDR: 내부 멤버로 공용체를 갖고 있다.
// 이를 이용해서 정수 <-> 문자열 변환을 편리하게 할 수 있다.

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
    // 1. 초기화
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
    {
        printf("윈도우 소켓 초기화 실패!\n");
        return -1;
    }
    printf("윈도우 소켓 초기화 성공!\n");

    // 주소 변환
    // 문자열 주소 -> 정수
    char ipaddr[50] = "230.200.12.5";
    int n_ip;
    inet_pton(AF_INET, ipaddr, &n_ip);

    printf("IP 문자열 주소: %s\n", ipaddr);
    printf("IP 문자열 주소 -> 정수: 0x%08X\n\n", n_ip);

    // 정수 -> 문자열 주소
    IN_ADDR in_addr;
    in_addr.S_un.S_addr = n_ip;

    char recvip[50];
    inet_ntop(AF_INET, &in_addr, recvip, sizeof(recvip));

    printf("IP 정수 -> 문자열 주소: %s\n", recvip);

    // 2. 종료 처리
    WSACleanup();

    return 0;
}

// 바이트 정렬
// Host(L or B) -> Net(B)
// Net(B) -> Host(L or B)

void exam4()
{
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
    {
        printf("윈도우 소켓 초기화 실패!\n");
        return;
    }

    unsigned short us = 0x1234;
    unsigned long ul = 0x12345678;

    // 호스트 바이트를 네트워크 바이트로 변환한다.
    printf("0x%08X -> 0x%08X\n", us, htons(us));
    printf("0x%08X -> 0x%08X\n", ul, htonl(ul));

    unsigned short n_us = htons(us);
    unsigned long n_ul = htonl(ul);

    // 네트워크 바이트를 호스트 바이트로 변환한다.
    printf("0x%08X -> 0x%08X\n", n_us, ntohs(n_us));
    printf("0x%08X -> 0x%08X\n", n_ul, ntohl(n_ul));

    WSACleanup();
}

// 주소 사용하기
// 대표 구조체: SOCKADDR
// 사용 구조체: SOCKADDR_IN

void exam5()
{
    SOCKADDR_IN addr;

    // 1. IPv4
    addr.sin_family = AF_INET;

    // 2. Host -> Net 변경해서 전달
    addr.sin_port = htons(9000);

    // 3. 문자열 형태 IP
    char ipaddr[50] = "127.0.0.1";

    // 3.1 문자열을 숫자로 변경
    int h_ip;
    inet_pton(AF_INET, ipaddr, &h_ip);

    // 3.2 숫자형 IP를 Net 바이트 오더로 변경
    int n_ip = htonl(h_ip);

    // 3.3 IP를 표현하는 구조체 타입에 저장
    IN_ADDR in_addr;
    in_addr.S_un.S_addr = n_ip;

    // 3.4 IP를 저장
    addr.sin_addr = in_addr;

    // 구조체를 소켓 함수에 전달!(H -> N 변경해서 전달!)
}

int main()
{
    exam5();

    return 0;
}