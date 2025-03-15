//30_��Ʈ��(��ư).cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//----�ڽ� ID�� ����� ������ ����
#define IDC_BUTTON1		1
HWND g_hbutton;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	//��ư�� ���� ����
	case WM_LBUTTONDOWN:
	{
		SetWindowText(g_hbutton, TEXT("��ư���ں���"));
		MoveWindow(g_hbutton, 100, 100, 100, 25, TRUE);
		//ShowWindow(g_hbutton, SW_SHOW);
		return 0;
	}
	//�ڽ� ��Ʈ�� ����
	case WM_CREATE:
	{
		//�ڽ� ��Ʈ�� ����
		g_hbutton = CreateWindow(TEXT("button"), TEXT("Ŭ��!"),
			WS_CHILD | WS_BORDER | WS_VISIBLE| BS_PUSHBUTTON,
			20, 20, 100, 25, hwnd, (HMENU)IDC_BUTTON1, GetModuleHandle(0), 0);
		return 0;
	}

	//���� �޽��� ����
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))  //�޴�������ID, �ڽ���Ʈ��ID
		{
			case IDC_BUTTON1:  //Ŭ���ߴ�...
				SetWindowText(hwnd, TEXT("��ư Ŭ��!"));
				Sleep(1000);
				SetWindowText(hwnd, TEXT("0307"));
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