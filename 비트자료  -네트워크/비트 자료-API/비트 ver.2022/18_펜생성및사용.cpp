//18_�� ���� �� ���

/*
* LButtonŬ���� �簢�� ���
* Ű����� R, G, B�� �����ϸ� "����", "���", "�Ķ�" ������ ����
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

COLORREF g_pencolor = RGB(0, 0, 0);
POINTS   g_pt = { -100, -100 };

void TitlePrint(HWND hwnd)
{
	TCHAR buf[50];
	wsprintf(buf, TEXT("���� ���õ� ���� : %d:%d:%d"),
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
	wc.style = 0;				//������ ��Ÿ��

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("0830"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);				//<-----------------(0)

	//�޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);		//<--------------------(0)
		DispatchMessage(&msg);
	}
	return 0;
}