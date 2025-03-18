//02_스레드전단계.cpp
/*
동시에 2개의 일이 처리됨 
1) 화면을 계속 갱신처리(타이머 활용)
2) 마우스 이벤트 처리(Primary Thread 에서 처리)

* 반드시 Primary Thread는 메시지 기본 흐름이 원할하게 동작할 수 있도록 처리
* [메시지큐->메시지루프->메시지프로시저]
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static int blue = 0;	//정적 지역변수 -> 전역정적메모리에 저장
							//WndProc 함수가 종료되도 살이있다.

	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		POINTS pt = MAKEPOINTS(lParam);

		HDC hdc = GetDC(hwnd);		
		Rectangle(hdc, pt.x, pt.y, pt.x + 50, pt.y + 50); 
		ReleaseDC(hwnd, hdc);

		return TRUE;
	}
	case WM_TIMER:	//1000 -> 1초 간격 반복 호출!
	{
		blue += 5;
		InvalidateRect(hwnd, 0, FALSE);

		return TRUE;
	}	
	case WM_PAINT:
	{
		PAINTSTRUCT ps; 
		HDC hdc = BeginPaint(hwnd, &ps);

		HBRUSH hbrush = CreateSolidBrush(RGB(0, 0, blue));
		HBRUSH hOld = (HBRUSH)SelectObject(hdc, hbrush); 
		Rectangle(hdc, 100, 100, 400, 400); 
		DeleteObject(SelectObject(hdc, hOld));

		EndPaint(hwnd, &ps);
		return TRUE;
	}
	case WM_CREATE:	//시작점(초기화 처리, 비큐, )
	{
		SetTimer(hwnd, 1000, 1, 0);

		return 0;
	}
	case WM_DESTROY: //끝점(종료 처리, 윈도우가 파괴됨)
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
	wc.lpszMenuName = 0;		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		CLASS_NAME, WINDOW_NAME, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 600, 600,
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