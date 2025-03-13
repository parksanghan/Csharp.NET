//packet.cpp

#include "std.h"

PACKET packet_CreatePacket(HWND h, const TCHAR* nn, const TCHAR* m)
{
	PACKET pack;

	pack.hwnd = h;
	_tcscpy_s(pack.nickname, _countof(pack.nickname), nn);
	_tcscpy_s(pack.msg, _countof(pack.msg), m);
	GetLocalTime(&pack.dt);

	return pack;
}

PACKET packet_CreatePacket(HWND h, const TCHAR* nn, const TCHAR* m, SYSTEMTIME t)
{
	PACKET pack;

	pack.hwnd = h;
	_tcscpy_s(pack.nickname, _countof(pack.nickname), nn);
	_tcscpy_s(pack.msg, _countof(pack.msg), m);
	pack.dt = t;	//****

	return pack;
}