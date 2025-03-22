//05_윈도우 핸들사용
//C:\Program Files(x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\spyxx.exe
/*
* User객체(윈도우 핸들)
*  - 시스템에 전역적 핸들이다.
*/
//유니코드 --> 멀티바이트 변경처리
#include <Windows.h>	//Win32API사용 가능
#include <tchar.h>
#include <stdio.h>


int main()
{
	HWND hwnd = FindWindow(0, _TEXT("제목 없음 - Windows 메모장"));
	if (hwnd == 0)
	{
		printf("먼저 실행\n");
		return 0;
	}

	printf("메모장 - 윈도우 핸들 : %d\n", (int)hwnd);

	TCHAR c_name[60];
	GetClassName(hwnd, c_name, sizeof(c_name));
	printf("메모장 - 클래스명 : %s\n", c_name);

	GetWindowText(hwnd, c_name, sizeof(c_name));
	printf("메모장 - 윈도우명 : %s\n", c_name);

	RECT rc;
	GetWindowRect(hwnd, &rc);
	printf("메모장 - 위치(%d,%d)~(%d,%d)\n", rc.left, rc.top, rc.right, rc.bottom);
	printf("메모장 - 크기(%d * %d)\n", rc.right - rc.left, rc.bottom - rc.top);
	
	system("pause");

	MoveWindow(hwnd, 0, 0, 500, 500, FALSE);

	printf("윈도우 숨기기\n");
	system("pause");
	ShowWindow(hwnd, SW_HIDE);

	printf("윈도우 보여주기\n");
	system("pause");
	ShowWindow(hwnd, SW_SHOW);

	printf("윈도우 죽이기\n");
	system("pause");
	SendMessage(hwnd, WM_CLOSE, 0, 0);

	return 0;
}