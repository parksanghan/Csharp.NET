//net.h
#pragma once

//시작, 종료
int net_Start(const char* ip, int port);
void net_Stop();

//수신
unsigned int __stdcall WorkThread(void* p);
int net_RecvData(SOCKET s, char* msg, int size);
int recvn(SOCKET s, char* buf, int len, int flags);


//송신
int net_SendData(const char* msg, int size);