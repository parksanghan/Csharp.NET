//01_유니캐스트_서버.cpp

#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <winsock2.h>	
#pragma comment(lib, "ws2_32.lib")
#include <stdlib.h>	
#include <stdio.h>
#include <tchar.h>

#define BUFSIZE 512


// 소켓 함수 오류 출력 후 종료
void err_quit(const TCHAR* msg)
{
	void* lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	MessageBox(NULL, (LPCTSTR)lpMsgBuf, msg, MB_ICONERROR);
	LocalFree(lpMsgBuf);
	exit(-1);
}

// 소켓 함수 오류 출력
void err_display(const TCHAR* msg)
{
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	MessageBox(NULL, (LPCTSTR)lpMsgBuf, msg, MB_ICONERROR);
	LocalFree(lpMsgBuf);
}

int main(int argc, char* argv[])
{
	int retval;

	// 윈속 초기화
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return -1;

	// socket()
	SOCKET sock = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
	if (sock == INVALID_SOCKET) 
		err_quit(TEXT("socket()"));

	// bind()
	SOCKADDR_IN			serveraddr;
	ZeroMemory(&serveraddr, sizeof(serveraddr));
	serveraddr.sin_family = AF_INET;
	serveraddr.sin_port = htons(9000);
	serveraddr.sin_addr.s_addr = htonl(INADDR_ANY);
	retval = bind(sock, (SOCKADDR*)&serveraddr,	sizeof(serveraddr));
	if (retval == SOCKET_ERROR)
		err_quit(TEXT("bind"));
	//--------------------------------------------------------------------

	//---- RUN ------------------------------------------------------------
	char			buf[BUFSIZE + 1];

	printf("서버 주소 : %s:%d\n", "220.90.179.75", 9000);
	while (1)
	{
		// 데이터 받기
		SOCKADDR_IN		clientaddr;
		int addrlen = sizeof(clientaddr);
		retval = recvfrom(sock, buf, BUFSIZE, 0,(SOCKADDR*)&clientaddr, &addrlen);
		if (retval == SOCKET_ERROR)
		{
			err_display(TEXT("recvfrom"));
			continue;
		}

		// 받은 데이터 출력
		buf[retval] = '\0';
		printf("[UDP/%s:%d] %s\n", inet_ntoa(clientaddr.sin_addr),
			ntohs(clientaddr.sin_port), buf);

		// 데이터 보내기(echo)
		retval = sendto(sock, buf, retval, 0,(SOCKADDR*)&clientaddr, sizeof(clientaddr));
		if (retval == SOCKET_ERROR)
		{
			err_display(TEXT("sendto"));
			continue;
		}
	}

	// closesocket()
	closesocket(sock);

	// 윈속 종료
	WSACleanup();

	return 0;
}
