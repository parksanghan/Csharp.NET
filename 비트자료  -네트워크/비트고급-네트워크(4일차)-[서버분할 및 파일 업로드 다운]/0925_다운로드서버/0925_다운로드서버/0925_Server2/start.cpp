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
	TCHAR FileName[260]; // ������ ���� �̸�.
	int  size;			// ȭ�� ũ��
};

// ������ Client���� ������ �����Ѵ�.
unsigned int  __stdcall FileServer(void* p)
{
	SOCKET s = (SOCKET)p;

	//1. recv : ���� ����
	TCHAR filename[20] = {0};
	recv(s, (char*)filename, 40, 0);

	TCHAR fileDir[200];
	wsprintf(fileDir, TEXT("c:\\wb38\\%s"), filename);

	//2. ������ ����Open
	HANDLE hFile = CreateFile(fileDir, GENERIC_READ, FILE_SHARE_READ,
		0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0);

	if (hFile == INVALID_HANDLE_VALUE)
	{
		printf("Can't Open File\n");
		closesocket(s);
		return 0;
	}

	//1. ���� �̸�, ���� ũ�⸦ �����Ѵ�.
	DWORD size1;
	DWORD size2 = GetFileSize(hFile, &size1);

	FILE_INFO fi;
	_tcscpy_s(fi.FileName, _countof(fi.FileName), filename);
	fi.size = size2;

	send(s, (char*)&fi, sizeof(fi), 0);

	//-------------------------------------------------
	// 3. ȭ�� ������ ����(�ѹ� �а� ������ ũ�� : 4096byte)
	//    4k �̻� �Ǵ� ������ ��� �ݺ��ؼ� ������ ��!
	//    - ����ũ�� : size2
	int total = size2; // ������ ��ü ũ��
	int current = 0;   // ������ ũ��
	int nRead = 0;
	TCHAR buf[4096];    // 4k ����.

	while (total > current)
	{
		DWORD len;
		nRead = ReadFile(hFile, buf, 4096, &len, 0);

		if (len <= 0) break;

		int nSend = send(s, (char*)buf, len, 0);
		if (nSend <= 0) break;

		current += nSend;
	}
	if (total != current)	printf("���ۿ���\n");
	else					printf("���ۿϷ� \n");

	closesocket(s);
	CloseHandle(hFile);
	return 0;
}


int main()
{
	//1. �ʱ�ȭ
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return -1;

	//2. socket����-> bind()�ּ��Ҵ� -> listen()�� ����    
	SOCKET listenSock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (listenSock == INVALID_SOCKET)
		return -1;

	SOCKADDR_IN serveraddr;
	//ZeroMemory(&serveraddr, sizeof(serveraddr));  //API
	memset(&serveraddr, 0, sizeof(serveraddr));     //C

	serveraddr.sin_family = AF_INET;           //�ּ�ü��
	serveraddr.sin_port = htons(SERVER_PORT);       //������Ʈ��ȣ
	serveraddr.sin_addr.s_addr = htonl(INADDR_ANY); //����IP �ּ�
	int retval = bind(listenSock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
	if (retval == SOCKET_ERROR)
		return -1;

	retval = listen(listenSock, SOMAXCONN);
	if (retval == SOCKET_ERROR)
		return -1;

	while (1)
	{
		//1. ���ť���� ���� ��û ����!
		SOCKADDR_IN clientaddr;
		int addrlen = sizeof(clientaddr);

		SOCKET clientSock = accept(listenSock, (SOCKADDR*)&clientaddr, &addrlen);
		if (clientSock == INVALID_SOCKET)
			continue; // break; // 

		//2. ���� ���
		char recvip[50];
		inet_ntop(AF_INET, &clientaddr.sin_addr, recvip, sizeof(recvip));
		int recvport = ntohs(clientaddr.sin_port);
		printf("\n>> Ŭ���̾�Ʈ ���� - (%s:%d)\n", recvip, recvport);

		//3. ��� ������ ȣ��
		//�����尡 ������ �� ����� ������ ����!
		uintptr_t h1 = _beginthreadex(0, 0, FileServer, (void*)clientSock, 0, 0);
		CloseHandle((HANDLE)h1);
	}

	closesocket(listenSock);
	WSACleanup();
}
















