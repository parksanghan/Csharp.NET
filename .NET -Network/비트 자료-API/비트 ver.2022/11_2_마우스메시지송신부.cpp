//11_2_���콺�޽����۽ź�(�ܼ�)

#include <Windows.h>
#include <tchar.h>
#include <stdio.h>

int main()
{
	HWND hwnd = FindWindow(0, TEXT("0830"));
	if (hwnd == 0)
	{
		printf("���� ����\n");
		return 0;
	}

	while (true)
	{
		int x = rand() % 500;
		int y = rand() % 500;
		printf("���� : %d, %d\n", x, y);
		SendMessage(hwnd, WM_LBUTTONDOWN, rand() % 101 + 10, MAKELONG(x, y));
		//PostMessage(hwnd, WM_MYMESSAGE, rand() % 101 + 10, MAKELONG(x, y));

		Sleep(1000);
	}
	return 0;
}