//start.cpp
//skeleton �ڵ�
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

	//����(�յ��ϰ�)
	int ymove = (rc.bottom / (yline+1));
	for (int i = 1; i <= yline; i++)
	{
		MoveToEx(hdc, 0, ymove *i, NULL);
		LineTo(hdc, rc.right, ymove *i);
	}

	//����(�յ��ϰ�)
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

//��� ����
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

//��޸��� ����
LINEDATA g_data = { xline, yline, penColor };	//<=========================
HWND g_child = NULL;


//�ڽ� ����
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

//�޽��� ó��
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
	//������ Ŭ���� ����
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //��, �귯��, ��Ʈ
	wc.hCursor = LoadCursor(0, IDC_ARROW);//�ý���
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = 0;		//�޴� ���
	wc.style = CS_HREDRAW | CS_VREDRAW;				//������ ��Ÿ��

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("0830"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);

	//�޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}