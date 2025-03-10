//33_��Ʈ��(�޺��ڽ�).cpp
//�޺��ڽ� & ����Ʈ�ڽ� ������.

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//----�ڽ� ID�� ����� ������ ����
#define IDC_COMBOBOX1		1
#define IDC_STATIC1			2

HWND g_combobox1;


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		//�ڽ� ��Ʈ�� ����
	case WM_CREATE:
	{
		//�ڽ� ��Ʈ�� ����
		CreateWindow(TEXT("static"), NULL,
			WS_CHILD | WS_BORDER | WS_VISIBLE,
			20, 20, 200, 25, hwnd, (HMENU)IDC_STATIC1, GetModuleHandle(0), 0);

		g_combobox1 = CreateWindow(TEXT("combobox"), NULL,
			WS_CHILD | WS_BORDER | WS_VISIBLE | CBS_DROPDOWN,
			20, 50, 200, 300, hwnd, (HMENU)IDC_COMBOBOX1, GetModuleHandle(0), 0);

		//---------- �޺� �ڽ��� ���ϴ� ���ڿ� ���� --------------
		SendMessage(g_combobox1, CB_ADDSTRING, 0, (LPARAM)TEXT("ù��°���ڿ�"));
		SendMessage(g_combobox1, CB_ADDSTRING, 0, (LPARAM)TEXT("�ι�°���ڿ�"));
		SendMessage(g_combobox1, CB_ADDSTRING, 0, (LPARAM)TEXT("����°���ڿ�"));
		SendMessage(g_combobox1, CB_ADDSTRING, 0, (LPARAM)TEXT("�׹�°���ڿ�"));

		//---------- �޺��ڽ� �ʱ�ȭ(���۽� ���ϴ� �ε��� ���) -----
		SendMessage(g_combobox1, CB_SETCURSEL, 2, 0);
		return 0;
	}

	//���� �޽��� ����
	case WM_COMMAND:
	{
		if (LOWORD(wParam) == IDC_COMBOBOX1)  //���� �����ߴ°�?
		{
			if (HIWORD(wParam) == CBN_SELCHANGE)  //��??
			{
				//1.������ �ε��� ȹ��
				int idx = SendMessage(g_combobox1, CB_GETCURSEL, 0, 0);

				//2.�ش� �ε����� ���ڿ� ȹ��
				TCHAR buf[100] = TEXT("");
				SendMessage(g_combobox1, CB_GETLBTEXT, idx, (LPARAM)buf);

				//static�� ���
				SetDlgItemText(hwnd, IDC_STATIC1, buf);
			}
			break;
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