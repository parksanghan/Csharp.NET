//09_사용자정의메시지
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//1. 메시지 정의
#define WM_MYMESSAGE	WM_USER+100
#define WM_MYMESSAGE1	WM_USER+101

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		//3. 메시지 처리(전달정보를 이용해서 사각형 출력)
	case WM_MYMESSAGE:
	{
		int size = wParam;
		POINTS pt = MAKEPOINTS(lParam);

		HDC hdc = GetDC(hwnd);  

		Rectangle(hdc, pt.x, pt.y, pt.x + size, pt.y + size);

		ReleaseDC(hwnd, hdc);   
		MessageBox(hwnd, _TEXT("MyMessage종료 전"), _TEXT("알림"), MB_OK);
		return 11;
	}
	case WM_MYMESSAGE1:
	{
		int size = wParam;
		POINTS pt = MAKEPOINTS(lParam);

		HDC hdc = GetDC(hwnd);

		Rectangle(hdc, pt.x, pt.y, pt.x + size, pt.y + size);

		ReleaseDC(hwnd, hdc);
		MessageBox(hwnd, _TEXT("MyMessage1종료 전"), _TEXT("알림"), MB_OK);
		return 0;
	}
	case WM_LBUTTONDOWN:
	{
		//2. 메시지 전송
		int value = SendMessage(hwnd, WM_MYMESSAGE, 50, MAKELONG(100,200));
		TCHAR temp[10];
		wsprintf(temp, TEXT("%d"), value);
		MessageBox(hwnd, temp, _TEXT("알림"), MB_OK);
		return 0;
	}
	case WM_RBUTTONDOWN:
	{
		bool b = PostMessage(hwnd, WM_MYMESSAGE1, 50, MAKELONG(200, 100));
		MessageBox(hwnd, TEXT("PostMessage전송후"), _TEXT("알림"), MB_OK);
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