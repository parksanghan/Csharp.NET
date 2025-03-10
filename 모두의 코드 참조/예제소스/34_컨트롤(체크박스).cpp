//34_��Ʈ��(üũ�ڽ�).cpp
//   ��ư��Ʈ���� Ȯ����(Ŭ�������� "button", ��Ÿ�� �߰�)
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//----�ڽ� ID�� ����� ������ ����
#define IDC_BUTTON1		1
#define IDC_BUTTON2		2

HWND g_button1, g_button2;


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static BOOL isbrush = FALSE;

	switch (msg)
	{
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		HBRUSH br;
		if (isbrush == TRUE)
		{
			br = CreateSolidBrush(RGB(255, 0, 0));
		}
		else
		{
			br = CreateSolidBrush(RGB(255, 255, 255));
		}

		HBRUSH oldbr = (HBRUSH)SelectObject(hdc, br);

		if (SendMessage(g_button1, BM_GETCHECK, 0, 0) == BST_CHECKED)
		{
			Rectangle(hdc, 200, 10, 310, 110);
		}
		else
		{
			Ellipse(hdc, 200, 10, 310, 110);
		}

		DeleteObject(SelectObject(hdc, oldbr));

		EndPaint(hwnd, &ps);
		return 0;
	}
	case WM_CREATE:
	{
		g_button1 = CreateWindow(TEXT("button"), TEXT("�簢�����"),
			WS_CHILD | WS_BORDER | WS_VISIBLE | BS_AUTOCHECKBOX,
			20, 20, 100, 25, hwnd, (HMENU)IDC_BUTTON1, GetModuleHandle(0), 0);

		g_button2 = CreateWindow(TEXT("button"), TEXT("�귯�����"),
			WS_CHILD | WS_BORDER | WS_VISIBLE | BS_AUTOCHECKBOX,
			20, 50, 100, 25, hwnd, (HMENU)IDC_BUTTON2, GetModuleHandle(0), 0);
		
		return 0;
	}

	//���� �޽��� ����
	case WM_COMMAND:
	{
		if (LOWORD(wParam) == IDC_BUTTON1)  //���� �����ߴ°�?
		{
			if (HIWORD(wParam) == BN_CLICKED)  //��??
			{
				InvalidateRect(hwnd, 0, TRUE);
			}
			break;
		}
		else if (LOWORD(wParam) == IDC_BUTTON2) //Ŭ������ �����ϰ� ������..
		{
			//���
			isbrush = !isbrush;

			InvalidateRect(hwnd, 0, TRUE);
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