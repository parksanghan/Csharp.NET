//handler.cpp

#include "std.h"

POINTS			g_pt;

//현재 설정 정보
POINTS			g_curpt;
COLORREF		g_brush_color = RGB(255, 0, 0);

//도형 저장 정보
vector<SHAPE>	g_shape;


LRESULT OnCreate(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	return 0;
}

LRESULT OnDestroy(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	PostQuitMessage(0);
	return 0;
}

LRESULT OnMouseMove(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	g_pt = MAKEPOINTS(lParam);

	RECT rc = { 10,10, 200, 30 };
	InvalidateRect(hwnd, &rc, TRUE);

	return 0;
}

LRESULT OnPaint(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	PAINTSTRUCT ps;
	HDC hdc = BeginPaint(hwnd, &ps);

	draw_InfoPrint(hdc, g_pt, g_curpt);

	draw_ShapePrint(hdc, g_shape);

	EndPaint(hwnd, &ps);
	return 0;
}

LRESULT OnLButtonDown(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	g_curpt = MAKEPOINTS(lParam);

	RECT rc = { 10,30, 200, 60 };
	InvalidateRect(hwnd, &rc, TRUE);

	SHAPE sh = shape_CreateShape(g_curpt, g_brush_color);
	g_shape.push_back(sh);

	RECT rt = { g_curpt.x, g_curpt.y, g_curpt.x + 50, g_curpt.y + 50 };
	InvalidateRect(hwnd, &rt, FALSE);

	return 0;
}

LRESULT OnKeyDown(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	if (wParam == 'R')
	{
		g_brush_color = RGB(255, 0, 0);
	}
	else if (wParam == 'G')
	{
		g_brush_color = RGB(0, 255, 0);
	}
	else if (wParam == 'B')
	{
		g_brush_color = RGB(0, 0, 255);
	}
	else if (wParam == 'W')
	{
		g_brush_color = RGB(255, 255, 255);
	}
	return 0;
}