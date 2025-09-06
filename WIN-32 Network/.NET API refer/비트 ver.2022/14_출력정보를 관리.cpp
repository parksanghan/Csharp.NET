//14_출력정보를 관리하는 프로그램.

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <vector>
using namespace std;

//전역변수
vector<POINTS> g_shapes;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONUP:
	{
		POINTS pt = MAKEPOINTS(lParam);
		g_shapes.push_back(pt);

		//전체 무효화(화면 지우기)
		//InvalidateRect(hwnd, 0, TRUE);

		//전체 무효화(화면은 지우지 않음)
		//InvalidateRect(hwnd, 0, FALSE);

		//일부 무효화(화면 지우기)
		RECT rc = { pt.x, pt.y, pt.x + 50, pt.y + 50 };
		//InvalidateRect(hwnd, &rc, TRUE);

		//일부 무효화(화면 지우지 않음) --> 최고의 효율성
		InvalidateRect(hwnd, &rc, FALSE);
		
		return 0;
	}
	
	case WM_RBUTTONDOWN:
	{
		//마지막에 저장된 데이터 삭제
		if (g_shapes.size() <= 0)
			return 0;

		g_shapes.pop_back();
		InvalidateRect(hwnd, 0, TRUE);

		return 0;
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);	//무효화처리 더 들어감

		for (int i = 0; i < (int)g_shapes.size(); i++)
		{
			POINTS pt = g_shapes[i];
			Rectangle(hdc, pt.x, pt.y, pt.x + 50, pt.y + 50);
		}		

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