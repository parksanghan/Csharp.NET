//03_�����带 �̿��Ͽ� ���� �ذ�
/*
* A��� ����� 1���� -> 2����->3������ �Ϸ��ؾ� ���� �����ִ�.
* B��� ������� 2������ ��Ź
* C��� ������� 3������ ��Ź.
* 
* ������� Ư�� �Լ��� ���� ȣ��Ǵ� ������ �帧..
*/

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//������ ���� �Լ�
DWORD WINAPI ThreadFunc1(LPVOID temp) //unsigned long __stdcall ThreadFunc1(void* temp);
{
	HDC	hdc;
	BYTE Blue = 0;
	HBRUSH hBrush, hOldBrush;
	HWND h = (HWND)temp;
	hdc = GetDC(h);
	while (1)
	{
		Blue++;
		Sleep(1);
		hBrush = CreateSolidBrush(RGB(0, 0, Blue));
		hOldBrush = (HBRUSH)SelectObject(hdc, hBrush);
		Rectangle(hdc, 100, 100, 300, 400);

		SelectObject(hdc, hOldBrush);
		DeleteObject(hBrush);
	}
	ReleaseDC(h, hdc);
}//


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static HANDLE hThread;
	static DWORD  ThreadID;
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		//fun1(hwnd);
		hThread = CreateThread(0, 0, ThreadFunc1, hwnd, 0, &ThreadID); //T UseCount: 2
		//�����带 �����ϱ� �ȴ�.
		CloseHandle(hThread);
	}
	return 0;
	case WM_RBUTTONDOWN:
	{
		HDC hdc = GetDC(hwnd);
		Ellipse(hdc, LOWORD(lParam), HIWORD(lParam), LOWORD(lParam) + 20,
			HIWORD(lParam) + 20);
		ReleaseDC(hwnd, hdc);
	}
	return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
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