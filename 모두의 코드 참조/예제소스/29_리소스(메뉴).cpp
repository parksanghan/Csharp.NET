//29_���ҽ�(�޴�).cpp

#define _CRT_SECURE_NO_WARNINGS
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"
#include <stdio.h>

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static COLORREF color = RGB(0, 0, 0); //����

	switch (msg)
	{
	//���콺 �����ʹ�ư Ŭ���� ContextMenu�� ����
	case WM_CONTEXTMENU:
	{
		//���� �޴��� Ȱ���ؼ� ó��
		//HMENU hmenu = GetMenu(hwnd);
		//HMENU hsubmenu = GetSubMenu(hmenu, 1);

		//���� ���� �޴��� Ȱ���ؼ� ó��
		HMENU hmenu = LoadMenu(GetModuleHandle(0), MAKEINTRESOURCE(IDR_MENU2));
		HMENU hsubmenu = GetSubMenu(hmenu, 0);

		POINT pt = { LOWORD(lParam), HIWORD(lParam) };  //��ũ����ǥ
		TCHAR buf[20];
		wsprintf(buf, TEXT("%d,%d"), pt.x, pt.y);
		SetWindowText(hwnd, buf);
		TrackPopupMenu(hsubmenu, TPM_LEFTBUTTON, pt.x, pt.y, 0, hwnd, 0);

		return 0;
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		HBRUSH hbr = CreateSolidBrush(color);
		HBRUSH oldb = (HBRUSH)SelectObject(hdc, hbr);

		Rectangle(hdc, 10, 10, 110, 110);

		DeleteObject(SelectObject(hdc, oldb));

		EndPaint(hwnd, &ps);
		return 0;
	}

	//�޴������� UIó�� (�޴����� �޴��� Ŭ������ �� POPUP�� �ϱ� �� �߻�)
	//CheckMenuItem�� �����ϸ� �����ϰ� ��� ����!
	case WM_INITMENUPOPUP:
	{
		HMENU hmenu = GetMenu(hwnd);
		EnableMenuItem(hmenu, ID_40002, color == RGB(255,0,0)?	TRUE:FALSE);
		EnableMenuItem(hmenu, ID_40003, color == RGB(0, 255, 0) ? TRUE : FALSE);
		EnableMenuItem(hmenu, ID_40004, color == RGB(0, 0, 255) ? TRUE : FALSE);
		EnableMenuItem(hmenu, ID_40005, color == RGB(0, 0, 0) ? TRUE : FALSE);
		return 0;
	}
	//�޴� Ŭ������ �� ȣ��Ǵ� �޽���
	case WM_COMMAND:
	{
		switch(LOWORD(wParam))  //�޴�ID
		{
		case ID_40001:  SendMessage(hwnd, WM_CLOSE, 0, 0); break;
		case ID_40002:  color = RGB(255, 0, 0); break;
		case ID_40003:  color = RGB(0, 255, 0); break;
		case ID_40004:  color = RGB(0, 0, 255); break;
		case ID_40005:  color = RGB(0, 0, 0);	break;
		}

		RECT rc = { 0,0,110,110 };
		InvalidateRect(hwnd, &rc, FALSE);

		return 0;
	}

	//�޴� ���� ó��
	case WM_LBUTTONDOWN:
	{
		static HMENU hmenu = 0;  

		if (hmenu == 0)
		{
			hmenu = GetMenu(hwnd); //���� �����쿡 ������ �޴��� �ڵ� ȹ��
			SetMenu(hwnd, 0);	   //���� �����쿡 �޴� ����(�޴��� ���ŵ�)		
		}
		else
		{
			SetMenu(hwnd, hmenu);
			hmenu = 0;
		}
		return 0;
	}
	case WM_CREATE:
	{
		return 0;
	}

	case WM_DESTROY:
	{
		PostQuitMessage(0);
		return 0;
	}
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
//	_tfreopen(_T("CONOUT$"), _T("w"), stdout);
//	_tprintf(_T("TEST"));
	//_tsetlocale(LC_ALL, _T("");

	//������ Ŭ���� ����
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //��, �귯��, ��Ʈ
	wc.hCursor = LoadCursor(0, IDC_ARROW);//�ý���
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = 0;// MAKEINTRESOURCE(IDR_MENU1);		//�޴� ���
	wc.style = 0;				//������ ��Ÿ��

	RegisterClass(&wc);

	//�ڽ��� ���ҽ��� �ִ� ���ҽ� �ڵ� ���(LoadIcon, LoadCursor, LoadMenu)
	HMENU hmenu = LoadMenu(hInst, MAKEINTRESOURCE(IDR_MENU1));

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("0830"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, hmenu, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);				//<-----------------(0)

	//�޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);		//<--------------------(0)
		DispatchMessage(&msg);
	}
	return 0;
}