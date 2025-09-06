//net_download.h
#pragma once

int net_recv_CreateSocket();
void net_recv_CloseSocket();

int net_recv_GroupJoin(const char* ip);
int net_recv_GroupDrop(const char* ip);

void net_recv_Run();
unsigned int __stdcall RecvThread(void* p);
