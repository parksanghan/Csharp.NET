//11_2_마우스메시지송신부(콘솔)

#include <Windows.h>
#include <tchar.h>
#include <stdio.h>

int main()
{
	HWND hwnd = FindWindow(0, TEXT("0830"));
	if (hwnd == 0)
	{
		printf("먼저 실행\n");
		return 0;
	}

	while (true)
	{
		int x = rand() % 500;
		int y = rand() % 500;
		printf("전송 : %d, %d\n", x, y);
		SendMessage(hwnd, WM_LBUTTONDOWN, rand() % 101 + 10, MAKELONG(x, y));
		//PostMessage(hwnd, WM_MYMESSAGE, rand() % 101 + 10, MAKELONG(x, y));

		Sleep(1000);
	}
	return 0;
}