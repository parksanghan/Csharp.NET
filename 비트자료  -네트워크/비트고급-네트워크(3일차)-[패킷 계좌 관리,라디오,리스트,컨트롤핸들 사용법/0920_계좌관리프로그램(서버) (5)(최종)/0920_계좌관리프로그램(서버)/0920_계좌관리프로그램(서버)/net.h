//net.h
#pragma once

int net_init(int port);
void net_exit();

void net_run();

unsigned int  __stdcall WorkThread(void* p);

int net_SendData(SOCKET s, char* msg, int size);
int net_RecvData(SOCKET s, char* msg, int size);
int recvn(SOCKET s, char* buf, int len, int flags);