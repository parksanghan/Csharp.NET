//25_시간획득
/*
* 현재날짜/시간을 타이틀바에 출력
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

void Sample1(HWND hwnd)
{
	SYSTEMTIME st;
	GetLocalTime(&st);

	TCHAR buf[50];
	GetDateFormat(LOCALE_USER_DEFAULT, 0, &st, TEXT("yyyy년 MM월 dd일"), buf, 50);
	SetWindowText(hwnd, buf);
}

void Sample2(HWND hwnd)
{
	SYSTEMTIME st;
	GetLocalTime(&st);

	TCHAR buf[50];
	wsprintf(buf, TEXT("%04d-%02d-%02d"), st.wYear, st.wMonth, st.wDay);
	SetWindowText(hwnd, buf);
}

void Sample3(HWND hwnd)
{
	SYSTEMTIME st;
	GetLocalTime(&st);

	TCHAR buf[50];
	GetTimeFormat(LOCALE_USER_DEFAULT, 0, &st, TEXT("tt hh시 mm분 ss초"), buf, 50);
	SetWindowText(hwnd, buf);
}

void Sample4(HWND hwnd)
{
	SYSTEMTIME st;
	GetLocalTime(&st);

	TCHAR buf[50];
	wsprintf(buf, TEXT("%02d:%02d:%02d"), st.wHour, st.wMinute, st.wSecond);
	SetWindowText(hwnd, buf);
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_CREATE:
	{
		Sample4(hwnd);
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