//18_펜 생성 및 사용

/*
* LButton클릭시 사각형 출력
* 키보드로 R, G, B를 선택하면 "빨강", "녹색", "파랑" 펜으로 변경
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

COLORREF g_pencolor = RGB(0, 0, 0);
POINTS   g_pt = { -100, -100 };

void TitlePrint(HWND hwnd)
{
	TCHAR buf[50];
	wsprintf(buf, TEXT("현재 선택된 색상 : %d:%d:%d"),
		GetRValue(g_pencolor), GetGValue(g_pencolor), GetBValue(g_pencolor));

	SetWindowText(hwnd, buf);
}

void DrawPrint(HDC hdc)
{
	HPEN pen		= CreatePen(PS_SOLID, 2, g_pencolor);
	HBRUSH brush	= CreateSolidBrush(RGB(rand() % 256, rand() % 256, rand() % 256));

	HPEN oldpen		= (HPEN)SelectObject(hdc, pen);
	HBRUSH oldbrush	= (HBRUSH)SelectObject(hdc, brush);

	Rectangle(hdc, g_pt.x, g_pt.y, g_pt.x + 50, g_pt.y + 50);

	DeleteObject(SelectObject(hdc, oldpen));
	DeleteObject(SelectObject(hdc, oldbrush));
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		DrawPrint(hdc);

		EndPaint(hwnd, &ps);
		return 0;
	}
	case WM_LBUTTONDOWN:
	{
		g_pt = MAKEPOINTS(lParam);
		InvalidateRect(hwnd, 0, FALSE);

		return 0;
	}
	case WM_KEYDOWN:
	{
		if (wParam == 'R')
			g_pencolor = RGB(255, 0, 0);
		else if (wParam == 'G')
			g_pencolor = RGB(0, 255, 0);
		else if (wParam == 'B')
			g_pencolor = RGB(0, 0, 255);

		TitlePrint(hwnd);

		return 0;
	}
	case WM_CREATE:
	{
		TitlePrint(hwnd);
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