//16_���ڿ��׵������

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

POINTS g_pt;

//���ڿ� ���(���콺 ��ġ ���)
void PrintText(HDC hdc) 
{
	TCHAR buf[50] = _TEXT("");
	wsprintf(buf, TEXT("���콺 ��ǥ : %04d, %04d"), g_pt.x, g_pt.y);
	TextOut(hdc, 10, 10, buf, _tcslen(buf));
}

//�� ���
void PixelPrint(HDC hdc)
{
	SetPixel(hdc, g_pt.x, g_pt.y, RGB(0, 0, 255));
}

//���� ���
void LinePrint(HDC hdc)
{
	MoveToEx(hdc, 100, 100, 0);  //������
	LineTo(hdc, 200, 200);  //����
}

//�簢�� Ÿ��
void RectAndEllipsePrint(HDC hdc)
{
	Rectangle(hdc, 20, 20, 120, 120);
	Ellipse(hdc, 20, 20, 120, 120);
}

//�ﰢ�����

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_KEYDOWN:
	{
		HDC hdc = GetDC(hwnd);
		
		if (wParam == 'L')
			LinePrint(hdc);
		else if (wParam == 'R' || wParam == 'E')
			RectAndEllipsePrint(hdc);

		ReleaseDC(hwnd, hdc);
		return 0;
	}
	case WM_MOUSEMOVE:
	{
		g_pt = MAKEPOINTS(lParam);		
		InvalidateRect(hwnd, 0, false);		
		return 0;
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		PrintText(hdc);
		PixelPrint(hdc);

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