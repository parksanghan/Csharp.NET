// 12_11에 문자열 출력(TextOut).cpp

/*

전역변수

1) 누군가는 변수의 값을 변경

2) 누군가는 변수의 값을 활용

[추가 기능]

화면 좌상단에 아래 정보 출력

1) 실시간 마우스 좌표  WM_MOUSEMOVE(생성)   -> WM_PAINT(소비)

2) 마지막에 출력한 사각형 좌표 정보  WM_LBUTTONDOWN(생성) -> WM_PAINT(소비)

3) 저장된 데이터 개수  WM_LBUTTONDOWN(생성) -> WM_PAINT(소비)

*/

#pragma comment (linker, "/subsystem:windows") // "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <vector>

using namespace std;

// 전역 변수
vector<POINTS> points;
POINTS cur_point;
POINTS last_point;
int g_count;

#define CLASS_NAME TEXT("first")
#define WINDOW_NAME TEXT("0904")

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_MOUSEMOVE:
	{
		cur_point = MAKEPOINTS(lParam);

		// 형광등 효과
		RECT rc = { 10, 10, 200, 30 };
		InvalidateRect(hwnd, &rc, TRUE);

		return 0;
	}
	case WM_LBUTTONDOWN:
	{
		POINTS pt = MAKEPOINTS(lParam);
		points.push_back(pt);

		last_point = pt;
		g_count = (int)points.size();

		// 좌표 출력 무효화
		RECT rc = { 10, 30, 200, 90 };
		InvalidateRect(hwnd, &rc, TRUE);

		// 사각형 출력 무효화 처리
		RECT rt = { pt.x, pt.y, pt.x + 50, pt.y + 50 };
		InvalidateRect(hwnd, &rt, FALSE);

		TCHAR temp[50];
		wsprintf(temp, TEXT("저장개수 : %d개"), points.size());
		SetWindowText(hwnd, temp);

		return 0;
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		// 1. 도형 출력
		for (int i = 0; i < points.size(); i++)
		{
			POINTS pt = points[i];
			Rectangle(hdc, pt.x, pt.y, pt.x + 50, pt.y + 50);
		}

		// 2. 좌표 출력
		TCHAR buf[50];
		wsprintf(buf, TEXT("현재 좌표 -> %d:%d"), cur_point.x, cur_point.y);
		TextOut(hdc, 10, 10, buf, (int)_tcslen(buf));

		// 3. 마지막 좌표 출력
		wsprintf(buf, TEXT("마지막 좌표 -> %d:%d"), last_point.x, last_point.y);
		TextOut(hdc, 10, 30, buf, (int)_tcslen(buf));

		// 4. 도형 개수 출력
		wsprintf(buf, TEXT("저장 개수 -> %d개"), g_count);
		TextOut(hdc, 10, 50, buf, (int)_tcslen(buf));

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
	// 1. 윈도우 생성, 출력
	WNDCLASS wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);
	wc.hCursor = LoadCursor(0, IDC_ARROW);
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;
	wc.lpszClassName = CLASS_NAME;
	wc.lpszMenuName = 0;
	wc.style = 0;

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		CLASS_NAME, WINDOW_NAME, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 800, 800,
		0, 0, hInst, 0);

	ShowWindow(hwnd, nShowCmd);

	// 2. 메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		DispatchMessage(&msg);
	}

	return 0;
}
