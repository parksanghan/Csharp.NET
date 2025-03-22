//skeleton �ڵ�
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

static POINT pts[] = { {200,10},{300,90},{250,200},{150,200},{100,90},{200,10} };
static POINT star[] = { {30,40},{240,150},{40,190},{140,20},{180,210} };

//�ٰ���
void Print1(HDC hdc)
{
	//PEN
	Polyline(hdc, pts, 6);
}

//�ٰ���
void Print2(HDC hdc)
{
	//PEN * BRUSH
	SelectObject(hdc, GetStockObject(LTGRAY_BRUSH));
	Polygon(hdc, pts, 5);
}

//ä��� ���
void Print3(HDC hdc)
{
	SelectObject(hdc, GetStockObject(LTGRAY_BRUSH));
	SetPolyFillMode(hdc, POLYFILL_LAST);
	Polygon(hdc, star, 5);
}

//�ﰢ��
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