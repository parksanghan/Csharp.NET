//17_메뉴리소스.cpp
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
	//파일
	//새파일/저장하기/불러오기/프로그램종료
	case ID_40001:  break;
	case ID_40002:  break;
	case ID_40003:  break;
	case ID_40004: SendMessage(hwnd, WM_CLOSE, 0, 0);  break;

	//색상
	//빨강/녹색/파랑
	case ID_40006:  g_color = RGB(255, 0, 0); InvalidateRect(hwnd, 0, FALSE); break;
	case ID_40007:  g_color = RGB(0, 255, 0); InvalidateRect(hwnd, 0, FALSE); break;
	case ID_40008:  g_color = RGB(0, 0, 255); InvalidateRect(hwnd, 0, FALSE); break;

	//두께
	case ID_40009:	g_width = 1; InvalidateRect(hwnd, 0, TRUE);  break;
	case ID_40010:	g_width = 3; InvalidateRect(hwnd, 0, TRUE);  break;
	case ID_40011:  g_width = 5; InvalidateRect(hwnd, 0, TRUE);  break;
	}
	return 0;
}

LRESULT OnInitMenuPopup(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	//둘 중 하나 사용하여 핸들 획득
	HMENU hMenu = GetMenu(hwnd); //hwnd에 부착된 메뉴 핸들 획득
	hMenu = (HMENU)wParam;       //hwnd에 부착된 메뉴의 핸들이 wParam에 전달 됨

	//색상
	EnableMenuItem(hMenu, ID_40006, g_color == RGB(255,0,0)? MF_GRAYED : MF_ENABLED);
	EnableMenuItem(hMenu, ID_40007, g_color == RGB(0, 255, 0) ? MF_GRAYED : MF_ENABLED);
	EnableMenuItem(hMenu, ID_40008, g_color == RGB(0, 0, 255) ? MF_GRAYED : MF_ENABLED);

	//두께
	CheckMenuItem(hMenu, ID_40009, g_width == 1 ? MF_CHECKED : MF_UNCHECKED);
	CheckMenuItem(hMenu, ID_40010, g_width == 3 ? MF_CHECKED : MF_UNCHECKED);
	CheckMenuItem(hMenu, ID_40011, g_width == 5 ? MF_CHECKED : MF_UNCHECKED);

	return 0;
}

LRESULT OnContextMenu(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	HMENU hMenu = LoadMenu(GetModuleHandle(0), MAKEINTRESOURCE(IDR_MENU2));
	HMENU hSubMenu = GetSubMenu(hMenu, 0); 

	POINT pt = { LOWORD(lParam), HIWORD(lParam) }; //스크린좌표
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
	wc.hCursor = LoadCursor(0, IDC_ARROW);//시스템
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //미리 만들어서 제공되는 프로시저(윈도우 공통 기능)
	wc.lpszClassName = CLASS_NAME;
	wc.lpszMenuName = MAKEINTRESOURCE(IDR_MENU1);		//메뉴 등록
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