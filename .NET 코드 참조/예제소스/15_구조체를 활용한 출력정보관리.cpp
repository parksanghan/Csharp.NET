//15_구조체를 활용한 출력정보관리
/*
* 도형의 정보(도형의 타입, 좌표정보)를 관리하는 프로그램
*/

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <vector>
using namespace std;

struct Shape
{
	int type;		//1(사각형), 2(타원)		//키보드를 누를때 R ->1, E -> 2
	POINTS pt;		//좌표정보
};

//전역변수
vector<Shape>	g_shapes;			//저장정보
Shape			g_curshape;			//현재 설정정보

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_KEYDOWN:
	{
		if (wParam == 'R')
			g_curshape.type = 1;
		else if (wParam == 'E')
			g_curshape.type = 2;

		return 0;
	}
	case WM_LBUTTONUP:
	{
		g_curshape.pt = MAKEPOINTS(lParam);
		g_shapes.push_back(g_curshape);

		InvalidateRect(hwnd, 0, TRUE);
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
			Shape sh = g_shapes[i];
			if(sh.type == 1)
				Rectangle(hdc, sh.pt.x, sh.pt.y, sh.pt.x + 50, sh.pt.y + 50);
			else if(sh.type == 2)
				Ellipse(hdc, sh.pt.x, sh.pt.y, sh.pt.x + 50, sh.pt.y + 50);
		}

		EndPaint(hwnd, &ps);
		return 0;
	}

	case WM_CREATE: //초기화
	{
		g_curshape.type = 1;
		g_curshape.pt.x = 0;
		g_curshape.pt.y = 0;

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