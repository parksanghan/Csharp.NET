//skeleton 코드
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

static POINT pts[] = { {200,10},{300,90},{250,200},{150,200},{100,90},{200,10} };
static POINT star[] = { {30,40},{240,150},{40,190},{140,20},{180,210} };

//다각선
void Print1(HDC hdc)
{
	//PEN
	Polyline(hdc, pts, 6);
}

//다각형
void Print2(HDC hdc)
{
	//PEN * BRUSH
	SelectObject(hdc, GetStockObject(LTGRAY_BRUSH));
	Polygon(hdc, pts, 5);
}

//채우기 모드
void Print3(HDC hdc)
{
	SelectObject(hdc, GetStockObject(LTGRAY_BRUSH));
	SetPolyFillMode(hdc, POLYFILL_LAST);
	Polygon(hdc, star, 5);
}

//삼각형
void Print4(HDC hdc)
{
	POINT points[] = { {100,100}, {125, 150}, {75,150 } };

	SelectObject(hdc, GetStockObject(LTGRAY_BRUSH));	
	Polygon(hdc, points, 3);
}


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{	
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);
		Print4(hdc);
		EndPaint(hwnd, &ps);
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