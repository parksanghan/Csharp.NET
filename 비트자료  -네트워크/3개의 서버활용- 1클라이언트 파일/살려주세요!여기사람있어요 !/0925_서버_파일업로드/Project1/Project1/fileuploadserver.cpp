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
	TCHAR FileName[260]; // ������ ���� �̸�.
	int  size;			// ȭ�� ũ��
};


// ������ Client���� ������ ���� �޴´�!!!

unsigned int  __stdcall FileServer(void* p)
{
	SOCKET sock = (SOCKET)p;

	printf("���� ���� �غ�\n");
	//1. recv : ���� ����
	FILE_INFO  info;
	recv(sock, (char*)&info, sizeof(info), 0);

	TCHAR fileDir[100];
	wsprintf(fileDir, TEXT("C:\\wb_38\\%s"), info.FileName);


	_tcprintf_s(TEXT("���� ���� �ڷ� Ȯ��..... (%s - %dbyte)\n"), info.FileName, info.size);
	HANDLE hFile = CreateFile(fileDir, GENERIC_WRITE, FILE_SHARE_READ, 0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);



	DWORD errorCode = GetLastError();
	LPVOID errorMsg;

	// ���� �ڵ带 ���� �޽����� ��ȯ
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM,
		NULL,
		errorCode,
		0,
		(LPTSTR)&errorMsg,
		0,
		NULL
	);

	// ���� �޽����� MessageBox�� ǥ��
	MessageBox(0, (LPCTSTR)errorMsg, TEXT("Error"), MB_OK | MB_ICONERROR);

	// �޸� ����
	LocalFree(errorMsg);








	int total = info.size;
	int current = 0;
	char buf[4096];
	_tcprintf_s(TEXT("���� �ޱ� ����.....%s (%d/%d)\n"), fileDir, current, total);
	while (total > current)
	{
		//�ް�
		int nRecv = recv(sock, buf, 4096, 0);
		if (nRecv <= 0) break;
		//����
		DWORD len;
		WriteFile(hFile, buf, nRecv, &len, 0);
		//ó��
		current += len;
		printf("���� �޴� ��..... (%d/%d)\n", current, total);
	}
	if (current != total)printf("[���] : ����\n");
	else                    printf("[���] : ����\n");
	closesocket(sock);
	CloseHandle(hFile);
	printf("[���� ��� ����]\n");
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
















