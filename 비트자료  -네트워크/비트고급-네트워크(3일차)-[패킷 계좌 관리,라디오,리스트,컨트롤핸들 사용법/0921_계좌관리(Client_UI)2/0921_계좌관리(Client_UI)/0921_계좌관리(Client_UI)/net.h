//net.h
#pragma once

//����, ����
int net_Start(const char* ip, int port);
void net_Stop();

//����
unsigned int __stdcall WorkThread(void* p);
int net_RecvData(SOCKET s, char* msg, int size);
int recvn(SOCKET s, char* buf, int len, int flags);


//�۽�
int net_SendData(const char* msg, int size);