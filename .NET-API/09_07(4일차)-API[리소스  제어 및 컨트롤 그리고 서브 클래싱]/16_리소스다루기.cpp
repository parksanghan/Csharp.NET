//16_���ҽ��ٷ��.cpp
//page116) ������
//page118) Ŀ�� 
//  PurPose  :  �����ܰ� �޴��� ID ���� ���� ���ҽ��� �ٷ��.
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"		//<-----------------

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

RECT g_rects[3] = { {10,10,60,60}, {80, 10, 130, 60}, {150, 10, 200, 60} };

HCURSOR g_h1 = LoadCursorFromFile(TEXT("c:\\windows\\cursors\\size1_i.cur"));
HCURSOR g_h2 = LoadCursorFromFile(TEXT("c:\\windows\\cursors\\size1_il.cur"));

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_MOUSEMOVE:
	{
		POINT pt = { LOWORD(lParam), HIWORD(lParam) };			// ��ũ�� ��ǥ �Ҵ�.

		HDC hdc = GetDC(hwnd);  // �ڵ� ���׽�Ʈ �Ҵ� 

		//���콺 ��ġ�� �ȼ� ���� ��������
		COLORREF color = GetPixel(hdc, pt.x, pt.y); // ���� 
		int r = GetRValue(color);
		int g = GetGValue(color);
		int b = GetBValue(color);
		
		//�簢���� ȹ���� ���� ���
		HBRUSH hbr = CreateSolidBrush(RGB(r, g, b));
		HBRUSH old = (HBRUSH)SelectObject(hdc, hbr);
		Rectangle(hdc, 10, 70, 200, 150);
		DeleteObject(SelectObject(hdc, old));		

		//R,G,B �� ���
		TCHAR temp[50];
		wsprintf(temp, TEXT("%03d, %03d, %03d"), r, g, b);
		TextOut(hdc, 10, 220, temp, _tcslen(temp));

		ReleaseDC(hwnd, hdc);

		return 0;
	}
	case WM_SETCURSOR:
	{
		int code = LOWORD(lParam); // x ��ǥ���� �޴´� .
		
		POINT pt;
		GetCursorPos(&pt);
		ScreenToClient(hwnd, &pt); //  Ŭ���̾�Ʈ x ��ǥ�� ��´�.

		if (code == HTCLIENT && PtInRect(&g_rects[0], pt)) //Ŭ���̾�Ʈ �����̸鼭  pt x ��ǥ�� ù��° �簢�� �ȿ� �ִٸ� 
		{
			SetCursor(LoadCursor(0, IDC_ARROW)); // Ŀ�� �������� ȭ��� �ٲ۴�.
			return TRUE;	//<--
		}
		if (code == HTCLIENT && PtInRect(&g_rects[1], pt)) 
		{
			SetCursor(LoadCursor(0, IDC_ARROW));
			return TRUE;	//<--
		}
		if (code == HTCLIENT && PtInRect(&g_rects[2], pt))
		{
			SetCursor(LoadCursor(0, IDC_ARROW));
			return TRUE;	//<--
		}

		//---������Ŀ�� ����-----------------------------------------
		if (code == HTLEFT || code == HTRIGHT) // ���콺�� ���� �׵θ� �� ������ �׵θ��� �ִٸ� 
		{
			SetCursor(g_h2); // g_h2�� Ŀ���� �ٲ۴�.
			return TRUE;
		}
		else if (code == HTTOP || code == HTBOTTOM) // ���콺�� ���� �׵θ� �� �Ʒ��� �׵θ��� �ִٸ� 
		{
			SetCursor(g_h1);
			return TRUE;
		}


		return DefWindowProc(hwnd, msg, wParam, lParam);
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		//ICON ���
		HICON hIcon = LoadIcon(GetModuleHandle(0), MAKEINTRESOURCE(IDI_ICON1));
		DrawIcon(hdc, 10, 10, hIcon);

		//�簢�� 3�� ���
		HBRUSH r_br = CreateSolidBrush(RGB(255, 0, 0));
		HBRUSH g_br = CreateSolidBrush(RGB(0, 255, 0));
		HBRUSH b_br = CreateSolidBrush(RGB(0, 0, 255));

		
		HBRUSH old = (HBRUSH)SelectObject(hdc, r_br);
		Rectangle(hdc, g_rects[0].left, g_rects[0].top, g_rects[0].right, g_rects[0].bottom);

		(HBRUSH)SelectObject(hdc, g_br);
		Rectangle(hdc, g_rects[1].left, g_rects[1].top, g_rects[1].right, g_rects[1].bottom);

		
		(HBRUSH)SelectObject(hdc, b_br);
		Rectangle(hdc, g_rects[2].left, g_rects[2].top, g_rects[2].right, g_rects[2].bottom);



		SelectObject(hdc, old);		//<---���� ������ �귯��..
		DeleteObject(r_br);
		DeleteObject(g_br);
		DeleteObject(b_br);

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
	wc.hCursor  = LoadCursor(0, IDC_CROSS);//�ý���
	wc.hIcon	= LoadIcon(hInst, MAKEINTRESOURCE(IDI_ICON1));
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)
	wc.lpszClassName = CLASS_NAME;
	wc.lpszMenuName = 0;		//�޴� ���
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