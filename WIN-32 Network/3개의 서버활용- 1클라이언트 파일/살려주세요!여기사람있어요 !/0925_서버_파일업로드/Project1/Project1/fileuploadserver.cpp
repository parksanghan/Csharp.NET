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

#define SERVER_PORT		7000

struct FILE_INFO
{
	TCHAR FileName[260]; // 전송할 파일 이름.
	int  size;			// 화일 크기
};


// 접속한 Client에게 파일을 전송 받는다!!!

unsigned int  __stdcall FileServer(void* p)
{
	SOCKET sock = (SOCKET)p;

	printf("전송 시작 준비\n");
	//1. recv : 파일 정보
	FILE_INFO  info;
	recv(sock, (char*)&info, sizeof(info), 0);

	TCHAR fileDir[100];
	wsprintf(fileDir, TEXT("C:\\wb_38\\%s"), info.FileName);


	_tcprintf_s(TEXT("전송 받을 자료 확인..... (%s - %dbyte)\n"), info.FileName, info.size);
	HANDLE hFile = CreateFile(fileDir, GENERIC_WRITE, FILE_SHARE_READ, 0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);



	DWORD errorCode = GetLastError();
	LPVOID errorMsg;

	// 오류 코드를 오류 메시지로 변환
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM,
		NULL,
		errorCode,
		0,
		(LPTSTR)&errorMsg,
		0,
		NULL
	);

	// 오류 메시지를 MessageBox로 표시
	MessageBox(0, (LPCTSTR)errorMsg, TEXT("Error"), MB_OK | MB_ICONERROR);

	// 메모리 해제
	LocalFree(errorMsg);








	int total = info.size;
	int current = 0;
	char buf[4096];
	_tcprintf_s(TEXT("파일 받기 시작.....%s (%d/%d)\n"), fileDir, current, total);
	while (total > current)
	{
		//받고
		int nRecv = recv(sock, buf, 4096, 0);
		if (nRecv <= 0) break;
		//쓰고
		DWORD len;
		WriteFile(hFile, buf, nRecv, &len, 0);
		//처리
		current += len;
		printf("파일 받는 중..... (%d/%d)\n", current, total);
	}
	if (current != total)printf("[결과] : 실패\n");
	else                    printf("[결과] : 성공\n");
	closesocket(sock);
	CloseHandle(hFile);
	printf("[소켓 통신 종료]\n");
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
















