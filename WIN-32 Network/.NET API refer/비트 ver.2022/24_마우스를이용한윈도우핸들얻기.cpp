//24_마우스를이용한 윈도우핸들얻기
/*
* FindWindow()       : 클래스명이나 윈도우명으로 윈도우를 검색!
* WindowFromPoint()  : 좌표(스크린좌표)로 부터 윈도우를 찾아 내겠다
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

void PrintWindowInfo(HWND hwnd)
{
	TCHAR Info[1024] = { 0 };
	TCHAR temp[256];
	RECT rcWin;
	GetClassName(hwnd, temp, 256);
	wsprintf(Info, TEXT("Window Class : %s\n"), temp);

	GetWindowText(hwnd, temp, 256);
	wsprintf(Info + wcslen(Info), TEXT("Window Caption : %s\n"), temp);

	GetWindowRect(hwnd, &rcWin);
	wsprintf(Info + wcslen(Info), TEXT("Window Position : (%d,%d)-(%d,%d)"),
		rcWin.left, rcWin.top, rcWin.right, rcWin.bottom);

	MessageBox(0, Info, TEXT("Window Info"), MB_OK);
}


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		SetCapture(hwnd);
		return 0;
	}
	case WM_LBUTTONUP:
	{
		if (GetCapture() == hwnd)
		{
			ReleaseCapture();

			POINT pt = { LOWORD(lParam), HIWORD(lParam) };	//클라이언트좌표
			ClientToScreen(hwnd, &pt);
			HWND hwndDest = WindowFromPoint(pt);

			//POINT pt;
			//GetCursorPos(&pt);	//현재 위치의 마우스 포인터 획득(스크린좌표)
			//HWND hwndDest = WindowFromPoint(pt);	//전달되어야 할 좌표는 스크린좌표
			PrintWindowInfo(hwndDest);

		}
		return 0;
	}
	//테스트(캡쳐가 되고 있는지 확인)
	case WM_MOUSEMOVE:
	{
		TCHAR temp[256];
		POINT pt = { LOWORD(lParam),  HIWORD(lParam) };
		wsprintf(temp, TEXT("Cursor Position : (%d, %d)"), pt.x, pt.y);
		SetWindowText(hwnd, temp);
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
	//윈도우 클래스 정의
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //펜, 브러쉬, 폰트
	wc.hCursor = LoadCursor(0, IDC_ARROW);//시스템
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //미리 만들어서 제공되는 프로시저(윈도우 공통 기능)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = 0;		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("0830"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);				//<-----------------(0)

	//메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);		//<--------------------(0)
		DispatchMessage(&msg);
	}
	return 0;
}