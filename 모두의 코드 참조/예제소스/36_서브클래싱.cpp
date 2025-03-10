//36_����Ŭ����.cpp
//���ڸ� �Է� ������ ����Ʈ ��Ʈ�� �����

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//----�ڽ� ID�� ����� ������ ����
#define IDC_EDIT1		1
#define IDC_STATIC1		2

HWND g_hedit1;
HWND g_static1;

WNDPROC old; // ������ EditBox�� �޼��� ó���Լ��� �ּҸ� ���� ����

LRESULT CALLBACK foo(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_CHAR:
		if ((wParam >= '0' && wParam <= '9') || wParam == 8)			
			return CallWindowProc(old, hwnd, msg, wParam, lParam);
		return 0; // ���� �̿��� ���� �����Ѵ�.
	}
	// ������ ��� �޼����� ������ �Լ��� �����Ѵ�.
	return CallWindowProc(old, hwnd, msg, wParam, lParam);
}


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		//�ڽ� ��Ʈ�� ����
	case WM_CREATE:
	{
		//�ڽ� ��Ʈ�� ����
		g_hedit1 = CreateWindow(TEXT("edit"), TEXT("���ڸ��Է°���"),
			WS_CHILD | WS_BORDER | WS_VISIBLE,
			20, 20, 200, 25, hwnd, (HMENU)IDC_EDIT1, GetModuleHandle(0), 0);

		g_static1 = CreateWindow(TEXT("static"), NULL,
			WS_CHILD | WS_BORDER | WS_VISIBLE,
			20, 50, 200, 25, hwnd, (HMENU)IDC_STATIC1, GetModuleHandle(0), 0);

		//����Ʈ��Ʈ���� ���ν����� ����!
		old = (WNDPROC)SetWindowLong(g_hedit1, GWL_WNDPROC, (LONG)foo);
		SetFocus(g_hedit1);
		return 0;
	}

	//���� �޽��� ����
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))  //�޴�������ID, �ڽ���Ʈ��ID
		{
		case IDC_EDIT1:  //���� �����ߴ°�?
		{
			if (HIWORD(wParam) == EN_CHANGE)  //��??
			{
				TCHAR buf[50];
				//GetWindowText(g_hedit1, buf, _countof(buf));
				//SetWindowText(hwnd, buf);

				//Dlg(Main������)�� Item(�ڽ� ��Ʈ��)�� ���ڸ� get!
				GetDlgItemText(hwnd, IDC_EDIT1, buf, _countof(buf));
				SetDlgItemText(hwnd, IDC_STATIC1, buf);
			}
			break;
		}
		}
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