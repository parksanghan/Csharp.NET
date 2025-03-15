//net.cpp
#include "std.h"

#define SEND_PORT		9000

static SOCKET send_Socket;

int net_send_CreateSocket()
{
	WSADATA wsadata;

	if (WSAStartup(MAKEWORD(2, 2), &wsadata) != 0)
	{
		printf("Can't Initialize Socket !\n");
		return -1;
	}

	send_Socket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
	return 1;
}

void net_send_CloseSocket()
{
	closesocket(send_Socket);
	WSACleanup();
}

int net_send_SendData(const char* ip, const char* msg, int size)
{
	int n_ip;
	inet_pton(AF_INET, ip, &n_ip);  //<-------------------

	SOCKADDR_IN addr;
	addr.sin_family = AF_INET;
	addr.sin_port = htons(SEND_PORT);
	addr.sin_addr.s_addr = n_ip;

	int r = sendto(send_Socket, msg, size, 0, (SOCKADDR*)&addr, sizeof(addr));
	return r;
}
