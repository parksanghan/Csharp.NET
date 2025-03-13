//start.cpp
//skeleton 코드
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"
#include "modaldlgproc.h"
#include "modalessdlgproc.h"
#include "LineData.h"

int yline = 10;
int xline = 5;
int penColor = 2;  //0~4 (0, 50, 100, 150, 200)

LRESULT OnPaint(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	PAINTSTRUCT ps;
	HDC hdc = BeginPaint(hwnd, &ps);

	HPEN hpen = CreatePen(PS_SOLID, 1, RGB(penColor * 50, penColor * 50, penColor * 50));
	HPEN oldPen = (HPEN)SelectObject(hdc, hpen);

	RECT rc;
	GetClientRect(hwnd, &rc);

	//가로(균등하게)
	int ymove = (rc.bottom / (yline+1));
	for (int i = 1; i <= yline; i++)
	{
		MoveToEx(hdc, 0, ymove *i, NULL);
		LineTo(hdc, rc.right, ymove *i);
	}

	//세로(균등하게)
	int xmove = (rc.right / (xline + 1));
	for (int i = 1; i <= xline; i++)
	{
		MoveToEx(hdc, xmove * i, 0, NULL);
		LineTo(hdc, xmove * i, rc.bottom );
	}

	DeleteObject(SelectObject(hdc, oldPen));
	EndPaint(hwnd, &ps);
	return 0;
}

//모달 예제
LRESULT OnLButtonDown(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	LINEDATA data = { xline, yline, penColor };

	UINT ret = DialogBoxParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG1),
		hwnd, ModalDlgProc, (LPARAM)&data);
	if (ret == IDOK)
	{
		yline = data.yline;
		xline = data.xline;
		penColor = data.penColor;
		InvalidateRect(hwnd, 0, TRUE);
	}
	return 0;
}

//모달리스 예제
LINEDATA g_data = { xline, yline, penColor };	//<=========================
HWND g_child = NULL;


//자식 생성
LRESULT OnRButtonDown(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	if (g_child == NULL)
	{
		g_child = CreateDialogParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG1),
			hwnd, ModalessDlgProc, (LPARAM)&g_data);
		ShowWindow(g_child, SW_SHOW);
	}
	else
	{
		SetFocus(g_child);
	}

	return 0;
}

//메시지 처리
LRESULT OnApply(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	yline = g_data.yline;
	xline = g_data.xline;
	penColor = g_data.penColor;
	InvalidateRect(hwnd, 0, TRUE);

	return 0;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_APPLY:			return OnApply(hwnd, wParam, lParam);
	case WM_RBUTTONDOWN:	return OnRButtonDown(hwnd, wParam, lParam);
	case WM_LBUTTONDOWN:	return OnLButtonDown(hwnd, wParam, lParam);
	case WM_PAINT:			return OnPaint(hwnd, wParam, lParam);
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
	wc.style = CS_HREDRAW | CS_VREDRAW;				//윈도우 스타일

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