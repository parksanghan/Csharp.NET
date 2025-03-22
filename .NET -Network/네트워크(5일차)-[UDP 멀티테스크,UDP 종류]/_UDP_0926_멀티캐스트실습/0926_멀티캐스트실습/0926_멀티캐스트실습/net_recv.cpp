//net_recv.cpp
#include "std.h"

#define RECV_PORT	9000

static SOCKET recv_Socket;

//socket(), bind()
//동일주소 사용 가능
int net_recv_CreateSocket()
{
	WSADATA wsadata;

	if (WSAStartup(MAKEWORD(2, 2), &wsadata) != 0)
	{
		printf("Can't Initialize Socket !\n");
		return -1;
	}

	recv_Socket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);

	//---------------------동일 주소 사용 가능 ------------------
	BOOL optval = TRUE;
	int retval = setsockopt(recv_Socket, SOL_SOCKET, SO_REUSEADDR, (char*)&optval, sizeof(optval));
	if (retval == SOCKET_ERROR)
	{
		printf("SO_REUSEADDR!\n");
		return -1;
	}
	//------------------------------------------------------------

	SOCKADDR_IN addr;
	addr.sin_family = AF_INET;
	addr.sin_port = htons(RECV_PORT);
	addr.sin_addr.s_addr = INADDR_ANY;

	if (bind(recv_Socket, (SOCKADDR*)&addr, sizeof(addr)) == -1)
	{
		printf("Can't bind\n");
		return -1;
	}
	return 1;
}

//closesocket()
void net_recv_CloseSocket()
{
	closesocket(recv_Socket);
	WSACleanup();
}

int net_recv_GroupJoin(const char *ip)
{
	int n_ip;
	inet_pton(AF_INET, ip, &n_ip);  //<-------------------

	ip_mreq mreq;
	mreq.imr_multiaddr.s_addr = n_ip;
	mreq.imr_interface.s_addr = INADDR_ANY;
	int retval = setsockopt(recv_Socket, IPPROTO_IP, IP_ADD_MEMBERSHIP, (char*)&mreq, sizeof(mreq));
	if (retval == SOCKET_ERROR)
	{
		printf("가입 오류\n");
		return -1;
	}
	return 1;
}

int net_recv_GroupDrop(const char* ip)
{
	int n_ip;
	inet_pton(AF_INET, ip, &n_ip);  //<-------------------

	ip_mreq mreq;
	mreq.imr_multiaddr.s_addr = n_ip;
	mreq.imr_interface.s_addr = INADDR_ANY;
	int retval = setsockopt(recv_Socket, IPPROTO_IP, IP_DROP_MEMBERSHIP, (char*)&mreq, sizeof(mreq));
	if (retval == SOCKET_ERROR)
	{
		printf("탈퇴 오류\n");
		return -1;
	}
	return 1;
}

void net_recv_Run()
{
	uintptr_t  h = _beginthreadex(0, 0, RecvThread, 0, 0, 0);
	CloseHandle((HANDLE)h);
}

unsigned int __stdcall RecvThread(void *p)
{	
	TCHAR buf[4096] = { 0 };
	while (1)
	{
		memset(buf, 0, sizeof(buf));

		SOCKADDR_IN c_addr;
		int sz = sizeof(c_addr);		

		recvfrom(recv_Socket, (char*)buf, 4096, 0, (SOCKADDR*)&c_addr, &sz);

		con_RecvData(buf);
	}
	return 0;
}

