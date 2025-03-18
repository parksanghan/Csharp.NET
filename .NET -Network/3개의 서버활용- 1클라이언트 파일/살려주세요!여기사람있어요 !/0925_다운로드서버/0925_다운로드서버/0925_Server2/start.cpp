//fileserver2

#define _WINSOCK_DEPRECATED_NO_WARNINGS
#define WIN32_LEAN_AND_MEAN
#include <stdio.h>
#include <windows.h>	
#include <winsock2.h>	
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()...
#include <process.h>        //_beginthread()....
#include <tchar.h>
#include <stdlib.h>

#define SERVER_PORT		8000




struct FILE_INFO
{
	TCHAR FileName[260]; // 전송할 파일 이름.
	int  size;			// 화일 크기
};

// 접속한 Client에게 파일을 전송한다.
unsigned int  __stdcall FileServer(void* p)
{
	SOCKET s = (SOCKET)p;

	//1. recv : 파일 정보
	TCHAR filename[20] = {0};
	recv(s, (char*)filename, 40, 0);

	TCHAR fileDir[200];
	wsprintf(fileDir, TEXT("c:\\wb38\\%s"), filename);

	//2. 전송할 파일Open
	HANDLE hFile = CreateFile(fileDir, GENERIC_READ, FILE_SHARE_READ,
		0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0);

	if (hFile == INVALID_HANDLE_VALUE)
	{
		printf("Can't Open File\n");
		closesocket(s);
		return 0;
	}

	//1. 파일 이름, 파일 크기를 전송한다.
	DWORD size1;
	DWORD size2 = GetFileSize(hFile, &size1);

	FILE_INFO fi;
	_tcscpy_s(fi.FileName, _countof(fi.FileName), filename);
	fi.size = size2;

	send(s, (char*)&fi, sizeof(fi), 0);

	//-------------------------------------------------
	// 3. 화일 데이터 전송(한번 읽고 보내는 크기 : 4096byte)
	//    4k 이상 되는 파일일 경우 반복해서 보내게 됨!
	//    - 파일크기 : size2
	int total = size2; // 전송할 전체 크기
	int current = 0;   // 전송한 크기
	int nRead = 0;
	TCHAR buf[4096];    // 4k 버퍼.

	while (total > current)
	{
		DWORD len;
		nRead = ReadFile(hFile, buf, 4096, &len, 0);

		if (len <= 0) break;

		int nSend = send(s, (char*)buf, len, 0);
		if (nSend <= 0) break;

		current += nSend;
	}
	if (total != current)	printf("전송에러\n");
	else					printf("전송완료 \n");

	closesocket(s);
	CloseHandle(hFile);
	return 0;
}


int main()
{
	//1. 초기화
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return -1;

	//2. socket생성-> bind()주소할당 -> listen()망 연결    
	SOCKET listenSock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (listenSock == INVALID_SOCKET)
		return -1;

	SOCKADDR_IN serveraddr;
	//ZeroMemory(&serveraddr, sizeof(serveraddr));  //API
	memset(&serveraddr, 0, sizeof(serveraddr));     //C

	serveraddr.sin_family = AF_INET;           //주소체계
	serveraddr.sin_port = htons(SERVER_PORT);       //지역포트번호
	serveraddr.sin_addr.s_addr = htonl(INADDR_ANY); //지역IP 주소
	int retval = bind(listenSock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
	if (retval == SOCKET_ERROR)
		return -1;

	retval = listen(listenSock, SOMAXCONN);
	if (retval == SOCKET_ERROR)
		return -1;

	while (1)
	{
		//1. 대기큐에서 연결 요청 수락!
		SOCKADDR_IN clientaddr;
		int addrlen = sizeof(clientaddr);

		SOCKET clientSock = accept(listenSock, (SOCKADDR*)&clientaddr, &addrlen);
		if (clientSock == INVALID_SOCKET)
			continue; // break; // 

		//2. 정보 출력
		char recvip[50];
		inet_ntop(AF_INET, &clientaddr.sin_addr, recvip, sizeof(recvip));
		int recvport = ntohs(clientaddr.sin_port);
		printf("\n>> 클라이언트 접속 - (%s:%d)\n", recvip, recvport);

		//3. 통신 스레드 호출
		//스레드가 생성될 때 통신할 소켓을 전달!
		uintptr_t h1 = _beginthreadex(0, 0, FileServer, (void*)clientSock, 0, 0);
		CloseHandle((HANDLE)h1);
	}

	closesocket(listenSock);
	WSACleanup();
}
















