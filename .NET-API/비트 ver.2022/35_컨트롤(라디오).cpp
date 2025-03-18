//35_��Ʈ��(����).cpp
//   ��ư��Ʈ���� Ȯ����(Ŭ�������� "button", ��Ÿ�� �߰�)
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//----�ڽ� ID�� ����� ������ ����
#define IDC_BUTTON1		1
#define IDC_BUTTON2		2
#define IDC_BUTTON3		3
#define IDC_BUTTON4		4
#define IDC_BUTTON5		5
#define IDC_BUTTON6		6

HWND g_r1, g_r2, g_r3, g_r4, g_r5, g_r6;


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static int type			= IDC_BUTTON1;
	static COLORREF color	= RGB(0, 255, 0);

	switch (msg)
	{
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		HBRUSH br = CreateSolidBrush(color);
		HBRUSH oldbr = (HBRUSH)SelectObject(hdc, br);

		if (type == IDC_BUTTON1)
		{
			Rectangle(hdc, 200, 100, 300, 200);
		}
		else if (type == IDC_BUTTON2)
		{
			Ellipse(hdc, 200, 100, 300, 200);
		}
		else if (type == IDC_BUTTON3)
		{
			MoveToEx(hdc, 200, 100, 0);
			LineTo(hdc, 300, 200);
		}
		DeleteObject(SelectObject(hdc, br));

		EndPaint(hwnd, &ps);
		return 0;
	}
	case WM_CREATE:
	{
		g_r1 = CreateWindow(TEXT("button"), TEXT("�簢��"),
			WS_CHILD | WS_BORDER | WS_VISIBLE | BS_AUTORADIOBUTTON |WS_GROUP,
			20, 20, 100, 30, hwnd, (HMENU)IDC_BUTTON1, GetModuleHandle(0), 0);

		g_r2 = CreateWindow(TEXT("button"), TEXT("Ÿ��"),
			WS_CHILD | WS_BORDER | WS_VISIBLE | BS_AUTORADIOBUTTON,
			20, 50, 100, 30, hwnd, (HMENU)IDC_BUTTON2, GetModuleHandle(0), 0);

		g_r3 = CreateWindow(TEXT("button"), TEXT("��"),
			WS_CHILD | WS_BORDER | WS_VISIBLE | BS_AUTORADIOBUTTON,
			20, 80, 100, 30, hwnd, (HMENU)IDC_BUTTON3, GetModuleHandle(0), 0);

		g_r4 = CreateWindow(TEXT("button"), TEXT("����"),
			WS_CHILD | WS_BORDER | WS_VISIBLE | BS_AUTORADIOBUTTON | WS_GROUP,
			20, 120, 100, 30, hwnd, (HMENU)IDC_BUTTON4, GetModuleHandle(0), 0);

		g_r5 = CreateWindow(TEXT("button"), TEXT("���"),
			WS_CHILD | WS_BORDER | WS_VISIBLE | BS_AUTORADIOBUTTON,
			20, 150, 100, 30, hwnd, (HMENU)IDC_BUTTON5, GetModuleHandle(0), 0);

		g_r6 = CreateWindow(TEXT("button"), TEXT("�Ķ�"),
			WS_CHILD | WS_BORDER | WS_VISIBLE | BS_AUTORADIOBUTTON,
			20, 180, 100, 30, hwnd, (HMENU)IDC_BUTTON6, GetModuleHandle(0), 0);

		//-------������ư �ʱ� ���� ----------------
		CheckRadioButton(hwnd, IDC_BUTTON1, IDC_BUTTON3, IDC_BUTTON1);
		CheckRadioButton(hwnd, IDC_BUTTON4, IDC_BUTTON6, IDC_BUTTON5);

		return 0;
	}

	//���� �޽��� ����
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDC_BUTTON1: type = IDC_BUTTON1;  break;
		case IDC_BUTTON2: type = IDC_BUTTON2;  break;
		case IDC_BUTTON3: type = IDC_BUTTON3;  break;
		case IDC_BUTTON4: color = RGB(255, 0, 0);  break;
		case IDC_BUTTON5: color = RGB(0, 255, 0);  break;
		case IDC_BUTTON6: color = RGB(0, 0, 255);  break;
		}
		InvalidateRect(hwnd, 0, TRUE);
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