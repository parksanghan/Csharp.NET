//03_��������.cpp
//02_�ڵ带 ����(L Button Down �ڵ带 ����ó��)
/*
* �ݵ�� Primary Thread�� �޽��� �⺻ �帧�� �����ϰ� ������ �� �ֵ��� ó��
* [�޽���ť->�޽�������->�޽������ν���]
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

void fun1(LPVOID temp)		//LPVOID : void* :�ּ��������(8byte, 64bit)
{
	HWND h = (HWND)temp;	//��������� ũ�⿡ ���� ���� ����!!!

	BYTE Blue = 0;
	HBRUSH hBrush, hOldBrush;

	HDC hdc = GetDC(h);
	while (true)
	{
		Blue++;
		Sleep(1);	//0.01�� ����
		hBrush = CreateSolidBrush(RGB(0, 0, Blue));
		hOldBrush = (HBRUSH)SelectObject(hdc, hBrush);
		Rectangle(hdc, 100, 100, 300, 400);
		DeleteObject(SelectObject(hdc, hOldBrush));
	}

	ReleaseDC(h, hdc);
}

//thread �Լ�
DWORD WINAPI ThreadFunc1(LPVOID temp)
{
	HWND hwnd = (HWND)temp;

	fun1(hwnd);

	return 0;	//ī��Ʈ 0���� 
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		//������ ���� -> Ŀ�� ��ü ����[�ڵ�2�� ����->���μ�������...]
		DWORD ThreadID;
		HANDLE hThread = CreateThread(0, 0, ThreadFunc1, hwnd, 0, &ThreadID); // ī��Ʈ:2 

		//������ ���� ���ҷ�!!!
		CloseHandle(hThread); // ī��Ʈ : 1

		return TRUE;
	}
	case WM_RBUTTONDOWN:
	{
		POINTS pt = MAKEPOINTS(lParam);

		HDC hdc = GetDC(hwnd);
		Rectangle(hdc, pt.x, pt.y, pt.x + 50, pt.y + 50);
		ReleaseDC(hwnd, hdc);

		return 0;
	}

	case WM_CREATE:	//������(�ʱ�ȭ ó��, ��ť, )
	{
		return 0;
	}
	case WM_DESTROY: //����(���� ó��, �����찡 �ı���)
	{
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