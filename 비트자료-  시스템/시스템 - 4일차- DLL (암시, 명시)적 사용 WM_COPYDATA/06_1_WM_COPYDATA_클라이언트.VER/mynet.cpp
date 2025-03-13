//mynet.cpp
#include "std.h"

HWND hServer;

/*
서버 찾기(서버윈도우핸들)
패킷구조체 생성
COPYDATASTRUCT 구조체 생성
SendMessage
*/
BOOL mynet_Connect(HWND hDlg, const TCHAR* s_handle, const TCHAR* nickname)
{
	hServer = FindWindow(0, s_handle);
	if (hServer == 0)
		return FALSE;

	PACKET pack = packet_CreatePacket(hDlg, nickname, TEXT(""));

	COPYDATASTRUCT cds;
	cds.dwData = 1;
	cds.cbData = sizeof(PACKET);
	cds.lpData = &pack;

	SendMessage(hServer, WM_COPYDATA, 0, (LPARAM)&cds);
	return TRUE;
}

BOOL mynet_DisConnect(HWND hDlg, const TCHAR* s_handle, const TCHAR* nickname)
{
	hServer = FindWindow(0, s_handle);
	if (hServer == 0)
		return FALSE;

	PACKET pack = packet_CreatePacket(hDlg, nickname, TEXT(""));

	COPYDATASTRUCT cds;
	cds.dwData = 2;
	cds.cbData = sizeof(PACKET);
	cds.lpData = &pack;

	SendMessage(hServer, WM_COPYDATA, 0, (LPARAM)&cds);
	return TRUE;
}

BOOL mynet_SendData(HWND hDlg, const TCHAR* s_handle, const TCHAR* nickname,
	const TCHAR* msg)
{
	hServer = FindWindow(0, s_handle);
	if (hServer == 0)
		return FALSE;

	PACKET pack = packet_CreatePacket(hDlg, nickname, msg);

	COPYDATASTRUCT cds;
	cds.dwData = 3;
	cds.cbData = sizeof(PACKET);
	cds.lpData = &pack;

	SendMessage(hServer, WM_COPYDATA, 0, (LPARAM)&cds);
	return TRUE;
}