//09_��������Ǹ޽���
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//1. �޽��� ����
#define WM_MYMESSAGE	WM_USER+100
#define WM_MYMESSAGE1	WM_USER+101

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		//3. �޽��� ó��(���������� �̿��ؼ� �簢�� ���)
	case WM_MYMESSAGE:
	{
		int size = wParam;
		POINTS pt = MAKEPOINTS(lParam);

		HDC hdc = GetDC(hwnd);  

		Rectangle(hdc, pt.x, pt.y, pt.x + size, pt.y + size);

		ReleaseDC(hwnd, hdc);   
		MessageBox(hwnd, _TEXT("MyMessage���� ��"), _TEXT("�˸�"), MB_OK);
		return 11;
	}
	case WM_MYMESSAGE1:
	{
		int size = wParam;
		POINTS pt = MAKEPOINTS(lParam);

		HDC hdc = GetDC(hwnd);

		Rectangle(hdc, pt.x, pt.y, pt.x + size, pt.y + size);

		ReleaseDC(hwnd, hdc);
		MessageBox(hwnd, _TEXT("MyMessage1���� ��"), _TEXT("�˸�"), MB_OK);
		return 0;
	}
	case WM_LBUTTONDOWN:
	{
		//2. �޽��� ����
		int value = SendMessage(hwnd, WM_MYMESSAGE, 50, MAKELONG(100,200));
		TCHAR temp[10];
		wsprintf(temp, TEXT("%d"), value);
		MessageBox(hwnd, temp, _TEXT("�˸�"), MB_OK);
		return 0;
	}
	case WM_RBUTTONDOWN:
	{
		bool b = PostMessage(hwnd, WM_MYMESSAGE1, 50, MAKELONG(200, 100));
		MessageBox(hwnd, TEXT("PostMessage������"), _TEXT("�˸�"), MB_OK);
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