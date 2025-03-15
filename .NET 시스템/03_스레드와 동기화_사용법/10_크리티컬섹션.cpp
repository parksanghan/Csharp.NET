//10_크리티컬섹션(임계영역)

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

int x;
CRITICAL_SECTION	g_cs;		//크리티컬섹션 변수 선언  -----------------1)

DWORD WINAPI ThreadFun1(LPVOID param)
{
	HDC hdc = GetDC((HWND)param);
	for (int i = 0; i < 100; i++)
	{
		EnterCriticalSection(&g_cs);
		x = 100;
		TextOut(hdc, x, 100, TEXT("강아지"), 3);
		LeaveCriticalSection(&g_cs);
	}
	ReleaseDC((HWND)param, hdc);
	return 0;
}

DWORD WINAPI ThreadFun2(LPVOID param)
{
	HDC hdc = GetDC((HWND)param);
	for (int i = 0; i < 100; i++)
	{
		EnterCriticalSection(&g_cs);			//--임계영역을 구성 (4)
		x = 200;
		TextOut(hdc, x, 200, TEXT("고양이"), 3);
		LeaveCriticalSection(&g_cs);
	}
	ReleaseDC((HWND)param, hdc);
	return 0;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{		
	case WM_LBUTTONDOWN:
	{
		DWORD ThreadID;
		HANDLE hThread1 = CreateThread(NULL, 0, ThreadFun1, hwnd, 0, &ThreadID);
		HANDLE hThread2 = CreateThread(NULL, 0, ThreadFun2, hwnd, 0, &ThreadID);

		CloseHandle(hThread1);
		CloseHandle(hThread2);
	}
	return 0;
	case WM_CREATE: 
	{
		InitializeCriticalSection(&g_cs);	//				임계영역 변수 초기화	 2)
		return 0;
	}
	case WM_DESTROY:
		DeleteCriticalSection(&g_cs);	//					 파괴		3)	
		PostQuitMessage(0);
		return 0;
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
	UpdateWindow(hwnd);

	//메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}