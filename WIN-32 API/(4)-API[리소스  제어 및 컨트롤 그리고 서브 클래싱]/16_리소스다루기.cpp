//16_리소스다루기.cpp
//page116) 아이콘
//page118) 커서 
//  PurPose  :  아이콘과 메뉴를 ID 값을 통해 리소스를 다룬다.
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
		POINT pt = { LOWORD(lParam), HIWORD(lParam) };			// 스크린 좌표 할당.

		HDC hdc = GetDC(hwnd);  // 핸들 콘테스트 할당 

		//마우스 위치의 픽셀 색상 가져오기
		COLORREF color = GetPixel(hdc, pt.x, pt.y); // 현재 
		int r = GetRValue(color);
		int g = GetGValue(color);
		int b = GetBValue(color);
		
		//사각형에 획득한 색상 출력
		HBRUSH hbr = CreateSolidBrush(RGB(r, g, b));
		HBRUSH old = (HBRUSH)SelectObject(hdc, hbr);
		Rectangle(hdc, 10, 70, 200, 150);
		DeleteObject(SelectObject(hdc, old));		

		//R,G,B 값 출력
		TCHAR temp[50];
		wsprintf(temp, TEXT("%03d, %03d, %03d"), r, g, b);
		TextOut(hdc, 10, 220, temp, _tcslen(temp));

		ReleaseDC(hwnd, hdc);

		return 0;
	}
	case WM_SETCURSOR:
	{
		int code = LOWORD(lParam); // x 좌표값만 받는다 .
		
		POINT pt;
		GetCursorPos(&pt);
		ScreenToClient(hwnd, &pt); //  클라이언트 x 좌표를 얻는다.

		if (code == HTCLIENT && PtInRect(&g_rects[0], pt)) //클라이언트 영역이면서  pt x 좌표가 첫번째 사각형 안에 있다면 
		{
			SetCursor(LoadCursor(0, IDC_ARROW)); // 커서 아이콘을 화살로 바꾼다.
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

		//---프레임커서 변경-----------------------------------------
		if (code == HTLEFT || code == HTRIGHT) // 마우스가 왼쪽 테두리 및 오른쪽 테두리에 있다면 
		{
			SetCursor(g_h2); // g_h2의 커서로 바꾼다.
			return TRUE;
		}
		else if (code == HTTOP || code == HTBOTTOM) // 마우스가 위쪽 테두리 및 아래쪽 테두리에 있다면 
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

		//ICON 출력
		HICON hIcon = LoadIcon(GetModuleHandle(0), MAKEINTRESOURCE(IDI_ICON1));
		DrawIcon(hdc, 10, 10, hIcon);

		//사각형 3개 출력
		HBRUSH r_br = CreateSolidBrush(RGB(255, 0, 0));
		HBRUSH g_br = CreateSolidBrush(RGB(0, 255, 0));
		HBRUSH b_br = CreateSolidBrush(RGB(0, 0, 255));

		
		HBRUSH old = (HBRUSH)SelectObject(hdc, r_br);
		Rectangle(hdc, g_rects[0].left, g_rects[0].top, g_rects[0].right, g_rects[0].bottom);

		(HBRUSH)SelectObject(hdc, g_br);
		Rectangle(hdc, g_rects[1].left, g_rects[1].top, g_rects[1].right, g_rects[1].bottom);

		
		(HBRUSH)SelectObject(hdc, b_br);
		Rectangle(hdc, g_rects[2].left, g_rects[2].top, g_rects[2].right, g_rects[2].bottom);



		SelectObject(hdc, old);		//<---최초 상태의 브러쉬..
		DeleteObject(r_br);
		DeleteObject(g_br);
		DeleteObject(b_br);

		EndPaint(hwnd, &ps);

		return 0;
	}
	case WM_CREATE:	//시작점(초기화 처리, 비큐, )
	{

		return 0;
	}
	case WM_DESTROY: //끝점(종료 처리, 윈도우가 파괴됨)
	{
		PostQuitMessage(0);
		return 0;
	}
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//1. 윈도우 생성, 출력
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //펜, 브러쉬, 폰트
	wc.hCursor  = LoadCursor(0, IDC_CROSS);//시스템
	wc.hIcon	= LoadIcon(hInst, MAKEINTRESOURCE(IDI_ICON1));
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //미리 만들어서 제공되는 프로시저(윈도우 공통 기능)
	wc.lpszClassName = CLASS_NAME;
	wc.lpszMenuName = 0;		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		CLASS_NAME, WINDOW_NAME, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 800, 800,
		//CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,  //시스템이 알아서 출력!
		0, 0, hInst, 0);

	ShowWindow(hwnd, nShowCmd);
	UpdateWindow(hwnd);

	//2. 메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}