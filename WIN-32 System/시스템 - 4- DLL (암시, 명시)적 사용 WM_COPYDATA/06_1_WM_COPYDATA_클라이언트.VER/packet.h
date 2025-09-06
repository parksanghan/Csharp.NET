//packet.h
#pragma once

//서로 주고받는 데이터(packet) 정의
typedef struct tagPACKET
{
	HWND	hwnd;			//클라이언트 핸들
	TCHAR	nickname[20];	//대화명
	TCHAR   msg[100];		//메시지
	SYSTEMTIME dt;			//시간
}PACKET;

PACKET packet_CreatePacket(HWND h, const TCHAR* nn, const TCHAR* m);
PACKET packet_CreatePacket(HWND h, const TCHAR* nn, const TCHAR* m, SYSTEMTIME t);
