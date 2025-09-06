//net_send.h
#pragma once

int net_send_CreateSocket();
void net_send_CloseSocket();

int net_send_SendData(const char* ip, const char* msg, int size);
