//25_�ð�ȹ��
/*
* ���糯¥/�ð��� Ÿ��Ʋ�ٿ� ���
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

void Sample1(HWND hwnd)
{
	SYSTEMTIME st;
	GetLocalTime(&st);

	TCHAR buf[50];
	GetDateFormat(LOCALE_USER_DEFAULT, 0, &st, TEXT("yyyy�� MM�� dd��"), buf, 50);
	SetWindowText(hwnd, buf);
}

void Sample2(HWND hwnd)
{
	SYSTEMTIME st;
	GetLocalTime(&st);

	TCHAR buf[50];
	wsprintf(buf, TEXT("%04d-%02d-%02d"), st.wYear, st.wMonth, st.wDay);
	SetWindowText(hwnd, buf);
}

void Sample3(HWND hwnd)
{
	SYSTEMTIME st;
	GetLocalTime(&st);

	TCHAR buf[50];
	GetTimeFormat(LOCALE_USER_DEFAULT, 0, &st, TEXT("tt hh�� mm�� ss��"), buf, 50);
	SetWindowText(hwnd, buf);
}

void Sample4(HWND hwnd)
{
	SYSTEMTIME st;
	GetLocalTime(&st);

	TCHAR buf[50];
	wsprintf(buf, TEXT("%02d:%02d:%02d"), st.wHour, st.wMinute, st.wSecond);
	SetWindowText(hwnd, buf);
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_CREATE:
	{
		Sample4(hwnd);
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