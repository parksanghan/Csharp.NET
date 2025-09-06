#include "std.h"

//서로 주고받는 데이터(packet) 정의


PACKET packet_CreatePacket(HWND hWnd, const TCHAR* nickName, const TCHAR* msg)
{
	PACKET pk;

	pk.hWnd = hWnd;
	_tcscpy_s(pk.nickName, _countof(pk.nickName), nickName);
	_tcscpy_s(pk.msg, _countof(pk.msg), msg);
	GetLocalTime(&pk.dt);

	return pk;
}

PACKET packet_CreatePacket(HWND hWnd, const TCHAR* nickName, const TCHAR* msg, SYSTEMTIME dt)
{
	PACKET pk;

	pk.hWnd = hWnd;
	_tcscpy_s(pk.nickName, _countof(pk.nickName), nickName);
	_tcscpy_s(pk.msg, _countof(pk.msg), msg);
	pk.dt = dt;

	return pk;
}

