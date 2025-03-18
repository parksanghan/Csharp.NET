#pragma once
#include "std.h"

typedef struct tagPacket
{
	HWND hWnd; //클라이언트의 핸들
	TCHAR nickName[20];		//이름
	TCHAR msg[100];			//메세지
	SYSTEMTIME dt;			//시간
} PACKET;

PACKET packet_CreatePacket(HWND hWnd, const TCHAR* nickName, const TCHAR* msg);
PACKET packet_CreatePacket(HWND hWnd, const TCHAR* nickName, const TCHAR* msg, SYSTEMTIME dt);