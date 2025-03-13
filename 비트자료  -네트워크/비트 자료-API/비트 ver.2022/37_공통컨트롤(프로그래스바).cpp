//36_������Ʈ��(���α׷�����).cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <CommCtrl.h>
//#pragma comment(lib "comctrl32.lib")


//----�ڽ� ID�� ����� ������ ����
#define IDC_PROGRESS	1
#define IDC_STATIC1		2

HWND g_pro1;


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{	
	case WM_TIMER:
	{
		int pos = (rand() % 11) * 10; //0~10 --> 0~100
		SendMessage(g_pro1, PBM_SETPOS, pos, 0);

		TCHAR buf[20];
		wsprintf(buf, TEXT("%d%%"), pos);
		SetDlgItemText(hwnd, IDC_STATIC1, buf);
		return 0;
	}
	case WM_CREATE:
	{
		CreateWindow(TEXT("static"), TEXT("10%"),
			WS_CHILD | WS_BORDER | WS_VISIBLE,
			20, 20, 40, 20, hwnd, (HMENU)IDC_STATIC1, GetModuleHandle(0), 0);

		g_pro1 = CreateWindow(TEXT("msctls_progress32"), NULL,
			WS_CHILD | WS_BORDER | WS_VISIBLE ,//| PBS_SMOOTH,
			60, 20, 300, 20, hwnd, (HMENU)IDC_PROGRESS, GetModuleHandle(0), 0);

		//�ݵ�� �ʱ� ����(���� ����)
		SendMessage(g_pro1, PBM_SETRANGE32, 0, 100); // 0 ~ 100

		//�Ʒ� �ڵ带 ���ؼ� POSITION ������ ����
		SendMessage(g_pro1, PBM_SETPOS, 10, 0);  //WPARAM

		SetTimer(hwnd, 1, 500, NULL);

		return 0;
	}

	case WM_DESTROY:
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