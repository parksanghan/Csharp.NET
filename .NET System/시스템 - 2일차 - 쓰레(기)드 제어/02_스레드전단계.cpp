//02_���������ܰ�.cpp
/*
���ÿ� 2���� ���� ó���� 
1) ȭ���� ��� ����ó��(Ÿ�̸� Ȱ��)
2) ���콺 �̺�Ʈ ó��(Primary Thread ���� ó��)

* �ݵ�� Primary Thread�� �޽��� �⺻ �帧�� �����ϰ� ������ �� �ֵ��� ó��
* [�޽���ť->�޽�������->�޽������ν���]
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static int blue = 0;	//���� �������� -> ���������޸𸮿� ����
							//WndProc �Լ��� ����ǵ� �����ִ�.

	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		POINTS pt = MAKEPOINTS(lParam);

		HDC hdc = GetDC(hwnd);		
		Rectangle(hdc, pt.x, pt.y, pt.x + 50, pt.y + 50); 
		ReleaseDC(hwnd, hdc);

		return TRUE;
	}
	case WM_TIMER:	//1000 -> 1�� ���� �ݺ� ȣ��!
	{
		blue += 5;
		InvalidateRect(hwnd, 0, FALSE);

		return TRUE;
	}	
	case WM_PAINT:
	{
		PAINTSTRUCT ps; 
		HDC hdc = BeginPaint(hwnd, &ps);

		HBRUSH hbrush = CreateSolidBrush(RGB(0, 0, blue));
		HBRUSH hOld = (HBRUSH)SelectObject(hdc, hbrush); 
		Rectangle(hdc, 100, 100, 400, 400); 
		DeleteObject(SelectObject(hdc, hOld));

		EndPaint(hwnd, &ps);
		return TRUE;
	}
	case WM_CREATE:	//������(�ʱ�ȭ ó��, ��ť, )
	{
		SetTimer(hwnd, 1000, 1, 0);

		return 0;
	}
	case WM_DESTROY: //����(���� ó��, �����찡 �ı���)
	{
		KillTimer(hwnd, 1);

		PostQuitMessage(0);

		return 0;
	}
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//1. ������ ����, ���
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //��, �귯��, ��Ʈ
	wc.hCursor = LoadCursor(0, IDC_ARROW);//�ý���
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)
	wc.lpszClassName = CLASS_NAME;
	wc.lpszMenuName = 0;		//�޴� ���
	wc.style = 0;				//������ ��Ÿ��

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		CLASS_NAME, WINDOW_NAME, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 600, 600,
		//CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,  //�ý����� �˾Ƽ� ���!
		0, 0, hInst, 0);

	ShowWindow(hwnd, nShowCmd);
	UpdateWindow(hwnd);

	//2. �޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}