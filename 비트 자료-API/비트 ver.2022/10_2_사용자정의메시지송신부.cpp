//10_2_��������Ǹ޽����۽ź�(�ܼ�)

#include <Windows.h>
#include <tchar.h>
#include <stdio.h>

//1. �޽��� ����
#define WM_MYMESSAGE	WM_USER+100

int main()
{
	HWND hwnd = FindWindow(0, TEXT("0830"));
	if (hwnd == 0)
	{
		printf("���� ����\n");
		return 0;
	}

	int x, y;
	while (true)
	{
		printf("��ǥ �Է�(��) 10 20 : ");
		scanf_s("%d %d", &x, &y);
		//SendMessage(hwnd, WM_MYMESSAGE, rand() % 101 + 10, MAKELONG(x, y));
		PostMessage(hwnd, WM_MYMESSAGE, rand() % 101 + 10, MAKELONG(x, y));
	}
	return 0;
}