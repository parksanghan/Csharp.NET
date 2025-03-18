//28_리소스(아이콘)사용.cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>

#include "resource.h"

HICON g_hIcon[4] = {
	LoadIcon(GetModuleHandle(0), MAKEINTRESOURCE(IDI_ICON3)),
	LoadIcon(GetModuleHandle(0), MAKEINTRESOURCE(IDI_ICON4)),
	LoadIcon(GetModuleHandle(0), MAKEINTRESOURCE(IDI_ICON5)),
	LoadIcon(GetModuleHandle(0), MAKEINTRESOURCE(IDI_ICON6))
};

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_TIMER:
	{
		if (wParam == 1)  //timer ID
		{
			//ICON변경
			static int num = 0;

			num = (num + 1) % 4; //원형이동
			SetClassLong(hwnd, GCL_HICON, (LONG)g_hIcon[num]);
			

			//현재시간 출력
			SYSTEMTIME st;
			GetLocalTime(&st);
			TCHAR buf[20];
			wsprintf(buf, TEXT("%d:%d:%d"), st.wHour, st.wMinute, st.wSecond);
			SetWindowText(hwnd, buf);
		}

		return 0;
	}
	case WM_CREATE:
	{
		SetTimer(hwnd, 1, 1000, NULL);
		SendMessage(hwnd, WM_TIMER, 1, 0);
		return 0;
	}
	case WM_DESTROY:
	{
		KillTimer(hwnd, 1);
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
	//wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hIcon = LoadIcon(hInst, MAKEINTRESOURCE(IDI_ICON2));
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