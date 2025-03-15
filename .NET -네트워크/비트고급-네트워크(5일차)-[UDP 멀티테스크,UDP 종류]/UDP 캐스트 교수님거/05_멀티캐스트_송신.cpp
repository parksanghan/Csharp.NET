//05_멀티캐스트_송신.cpp
// 기본 코드 복사해 오세요.

#define _WINSOCK_DEPRECATED_NO_WARNINGS
#define WIN32_LEAN_AND_MEAN
#include <stdio.h>
#include <windows.h>	
#include <winsock2.h>	
#pragma comment(lib, "ws2_32.lib")

int main()
{
	WSADATA wsadata;

	if (WSAStartup(MAKEWORD(2, 2), &wsadata) != 0)
	{
		printf("Can't Initialize Socket !\n");
		return -1;
	}
	//--------------------------------------------
	SOCKET s = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);	

	while (1)
	{
		char ip[256];
		char msg[256];

		memset(ip, 0, 256);
		memset(msg, 0, 256);

		//printf("전송할 상대 IP : ");	gets_s(ip, sizeof(ip));
		strcpy_s(ip, sizeof(ip), "234.5.5.1");
		printf("전송할 문자열 : ");		gets_s(msg, sizeof(msg));

		SOCKADDR_IN addr;
		addr.sin_family = AF_INET;
		addr.sin_port = htons(4000);
		addr.sin_addr.s_addr = inet_addr(ip);

		sendto(s, msg, (int)strlen(msg) + 1, 0, (SOCKADDR*)&addr, sizeof(addr));
	}
	closesocket(s);
	//--------------------------------------------
	WSACleanup();
	
	return 0;
}