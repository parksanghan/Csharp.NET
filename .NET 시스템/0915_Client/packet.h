#pragma once
#include "std.h"

typedef struct tagPacket
{
	HWND hWnd; //Ŭ���̾�Ʈ�� �ڵ�
	TCHAR nickName[20];		//�̸�
	TCHAR msg[100];			//�޼���
	SYSTEMTIME dt;			//�ð�
} PACKET;

PACKET packet_CreatePacket(HWND hWnd, const TCHAR* nickName, const TCHAR* msg);
PACKET packet_CreatePacket(HWND hWnd, const TCHAR* nickName, const TCHAR* msg, SYSTEMTIME dt);