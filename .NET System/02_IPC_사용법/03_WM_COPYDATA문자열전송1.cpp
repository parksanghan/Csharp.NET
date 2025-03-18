//03_WM_COPYDATA문자열전송1.cpp
#include <windows.h> 
#include <stdio.h>
#include <tchar.h>
/*
void main() 
{
	//상대방 윈도우 핸들 얻기
	HWND hwnd = FindWindow(0, TEXT("B")); 
	if (hwnd == 0)
	{
		printf("B 윈도우를   먼저   실행해   주세요\n");      
		return;
	}

	//메시지 전송
	TCHAR buf[256] = { 0 };
	while (1) 
	{
		printf("B에게    전달한   메세지를   입력하세요   >> "); 
		_getts_s(buf, _countof(buf)); // 1줄입력   ?

		//_tprintf(TEXT("%s\n"), buf);
		//_tprintf(TEXT("%d\n"), (_tcslen(buf) + 1)*2);
		
		// 원격지로   Pointer를   전달하기   위한   메세지   - WM_COPYDATA 
		COPYDATASTRUCT cds;
		cds.lpData = buf;
		cds.cbData = (_tcslen(buf) + 1) * 2; //_tcslen(buf) + 1;  // 전달한   data 크기
		// 식별자
		cds.dwData = 1;
		
		// 전달할
		SendMessage( hwnd, WM_COPYDATA, 0,  (LPARAM)&cds);
	}
}
*/

void main()
{
	//상대방 윈도우 핸들 얻기
	HWND hwnd = FindWindow(0, TEXT("B"));
	if (hwnd == 0)
	{
		printf("B 윈도우를   먼저   실행해   주세요\n");
		return;
	}

	//메시지 전송
	char buf[256] = { 0 };
	while (1)
	{
		printf("B에게    전달한   메세지를   입력하세요   >> "); 
		gets_s(buf); // 1줄입력   ?

		// 원격지로   Pointer를   전달하기   위한   메세지   - WM_COPYDATA 
		COPYDATASTRUCT cds;
		cds.cbData = strlen(buf) + 1;  // 전달한   data 크기
		// 식별자
		cds.dwData = 1;
		cds.lpData = buf;          // 전달할   Pointer 
		SendMessage( hwnd, WM_COPYDATA, 0,  (LPARAM)&cds);
	}
}