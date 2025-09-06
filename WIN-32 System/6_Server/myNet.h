//mynet.h
#pragma once
#include "std.h"


vector<PACKET> mynet_Connect(void* pData);
vector<PACKET> mynet_Disconnect(void* pData);
void mynet_RecvData(void* pData);