//04_브로드캐스트_클라이언트.cpp

#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <winsock2.h>	
#pragma comment(lib, "ws2_32.lib")
#include <stdlib.h>	
#include <stdio.h>

#define BUFSIZE 512

void err_quit(const TCHAR* msg)
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
	exit(-1);
}

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
	if (sock == INVALID_SOCKET) err_quit(TEXT("socket()"));

	// 브로드캐스팅 활성화*****************************************
	BOOL		bEnable = TRUE;
	retval = setsockopt(sock, SOL_SOCKET, SO_BROADCAST,
		(char*)&bEnable, sizeof(bEnable));
	if (retval == SOCKET_ERROR)
		err_quit(TEXT("setsockopt()"));

	SOCKADDR_IN remoteaddr;
	ZeroMemory(&remoteaddr, sizeof(remoteaddr));
	remoteaddr.sin_family = AF_INET;
	remoteaddr.sin_port = htons(9000);
	//-------------------------------------------------------
	remoteaddr.sin_addr.s_addr = htonl(INADDR_BROADCAST);	//***********
	//-------------------------------------------------------


	// 데이터 통신에 사용할 변수
	int len;
	char buf[BUFSIZE + 1];

	while (1)
	{
		// 데이터 입력
		printf("\n[보낼 데이터]");
		if (fgets(buf, BUFSIZE + 1, stdin) == NULL)
			break;

		// '\n' 문자 제거
		len = (int)strlen(buf);
		if (buf[len - 1] == '\n')
			buf[len - 1] = '\0';

		if (strlen(buf) == 0)
			break;

		// 데이터 보내기
		retval = sendto(sock, buf, (int)strlen(buf), 0,	(SOCKADDR*)&remoteaddr, sizeof(remoteaddr));
		if (retval == SOCKET_ERROR)
		{
			err_display(TEXT("sendto"));
			continue;
		}
		printf("%d바이트를 보냈습니다.\n", retval);
	}

	// closesocket()
	closesocket(sock);

	// 윈속 종료
	WSACleanup();
	return 0;
}
