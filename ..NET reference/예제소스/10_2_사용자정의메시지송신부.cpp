//10_2_사용자정의메시지송신부(콘솔)

#include <Windows.h>
#include <tchar.h>
#include <stdio.h>

//1. 메시지 정의
#define WM_MYMESSAGE	WM_USER+100

int main()
{
	HWND hwnd = FindWindow(0, TEXT("0830"));
	if (hwnd == 0)
	{
		printf("먼저 실행\n");
		return 0;
	}

	int x, y;
	while (true)
	{
		printf("좌표 입력(예) 10 20 : ");
		scanf_s("%d %d", &x, &y);
		//SendMessage(hwnd, WM_MYMESSAGE, rand() % 101 + 10, MAKELONG(x, y));
		PostMessage(hwnd, WM_MYMESSAGE, rand() % 101 + 10, MAKELONG(x, y));
	}
	return 0;
}