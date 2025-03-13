//17_�޴����ҽ�.cpp
//04_skeleton.cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

COLORREF g_color = RGB(255, 0, 0);
int g_width		 = 1;


LRESULT OnCommand(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam)) // ID
	{
	//����
	//������/�����ϱ�/�ҷ�����/���α׷�����
	case ID_40001:  break;
	case ID_40002:  break;
	case ID_40003:  break;
	case ID_40004: SendMessage(hwnd, WM_CLOSE, 0, 0);  break;

	//����
	//����/���/�Ķ�
	case ID_40006:  g_color = RGB(255, 0, 0); InvalidateRect(hwnd, 0, FALSE); break;
	case ID_40007:  g_color = RGB(0, 255, 0); InvalidateRect(hwnd, 0, FALSE); break;
	case ID_40008:  g_color = RGB(0, 0, 255); InvalidateRect(hwnd, 0, FALSE); break;

	//�β�
	case ID_40009:	g_width = 1; InvalidateRect(hwnd, 0, TRUE);  break;
	case ID_40010:	g_width = 3; InvalidateRect(hwnd, 0, TRUE);  break;
	case ID_40011:  g_width = 5; InvalidateRect(hwnd, 0, TRUE);  break;
	}
	return 0;
}

LRESULT OnInitMenuPopup(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	//�� �� �ϳ� ����Ͽ� �ڵ� ȹ��
	HMENU hMenu = GetMenu(hwnd); //hwnd�� ������ �޴� �ڵ� ȹ��
	hMenu = (HMENU)wParam;       //hwnd�� ������ �޴��� �ڵ��� wParam�� ���� ��

	//����
	EnableMenuItem(hMenu, ID_40006, g_color == RGB(255,0,0)? MF_GRAYED : MF_ENABLED);
	EnableMenuItem(hMenu, ID_40007, g_color == RGB(0, 255, 0) ? MF_GRAYED : MF_ENABLED);
	EnableMenuItem(hMenu, ID_40008, g_color == RGB(0, 0, 255) ? MF_GRAYED : MF_ENABLED);

	//�β�
	CheckMenuItem(hMenu, ID_40009, g_width == 1 ? MF_CHECKED : MF_UNCHECKED);
	CheckMenuItem(hMenu, ID_40010, g_width == 3 ? MF_CHECKED : MF_UNCHECKED);
	CheckMenuItem(hMenu, ID_40011, g_width == 5 ? MF_CHECKED : MF_UNCHECKED);

	return 0;
}

LRESULT OnContextMenu(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	HMENU hMenu = LoadMenu(GetModuleHandle(0), MAKEINTRESOURCE(IDR_MENU2));
	HMENU hSubMenu = GetSubMenu(hMenu, 0); 

	POINT pt = { LOWORD(lParam), HIWORD(lParam) }; //��ũ����ǥ
	TrackPopupMenu(hSubMenu, TPM_LEFTBUTTON, pt.x, pt.y, 0, hwnd, 0);

	return 0;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_COMMAND:		return OnCommand(hwnd, wParam, lParam);
	case WM_INITMENUPOPUP:  return OnInitMenuPopup(hwnd, wParam, lParam);
	case WM_CONTEXTMENU:	return OnContextMenu(hwnd, wParam, lParam);
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		HBRUSH hbr = CreateSolidBrush(g_color);
		HBRUSH old = (HBRUSH)SelectObject(hdc, hbr);

		HPEN pen = CreatePen(PS_SOLID, g_width, RGB(0, 0, 0));
		HPEN oldpen = (HPEN)SelectObject(hdc, pen);

		Rectangle(hdc, 10, 10, 110, 110);

		DeleteObject(SelectObject(hdc, old));
		DeleteObject(SelectObject(hdc, oldpen));

		EndPaint(hwnd, &ps);
		return 0;
	}


	case WM_CREATE:	//������(�ʱ�ȭ ó��, ��ť, )
	{
		return 0;
	}
	case WM_DESTROY: //����(���� ó��, �����찡 �ı���)
	{
		PostQuitMessage(0);
		return 0;
	}
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//1. ������ ����, ���
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //��, �귯��, ��Ʈ
	wc.hCursor = LoadCursor(0, IDC_ARROW);//�ý���
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)
	wc.lpszClassName = CLASS_NAME;
	wc.lpszMenuName = MAKEINTRESOURCE(IDR_MENU1);		//�޴� ���
	wc.style = 0;				//������ ��Ÿ��

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		CLASS_NAME, WINDOW_NAME, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 800, 800,
		//CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,  //�ý����� �˾Ƽ� ���!
		0, 0, hInst, 0);

	ShowWindow(hwnd, nShowCmd);
	UpdateWindow(hwnd);

	//2. �޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}