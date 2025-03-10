//38_��Ʈ��(��ũ�ѹ�).cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//----�ڽ� ID�� ����� ������ ����
#define ID_SCRRED		100
#define ID_SCRGREEN		101
#define ID_SCRBLUE		102

#define ID_EDITRED		103
#define ID_EDITGREEN	104
#define ID_EDITBLUE		105

HWND hRed, hGreen, hBlue;
HWND heRed, heGreen, heBlue;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	int TempPos;
	static int Red, Green, Blue;

	switch (msg)
	{
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))  //�޴�������ID, �ڽ���Ʈ��ID
		{
		case ID_EDITRED:  //���� �����ߴ°�?
		{
			if (HIWORD(wParam) == EN_CHANGE)  //��??
			{
				Red = GetDlgItemInt(hwnd, ID_EDITRED, 0, 0);
				SetScrollPos(hRed, SB_CTL, Red, TRUE);
				InvalidateRect(hwnd, 0, FALSE);
			}
			break;
		}
		case ID_EDITGREEN:  //���� �����ߴ°�?
		{
			if (HIWORD(wParam) == EN_KILLFOCUS)  //��Ŀ���� �Ҿ������
			{
				Green = GetDlgItemInt(hwnd, ID_EDITGREEN, 0, 0);
				SetScrollPos(hGreen, SB_CTL, Green, TRUE);
				InvalidateRect(hwnd, 0, FALSE);
			}
			break;
		}
		return 0;
		}
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		HBRUSH MyBrush = CreateSolidBrush(RGB(Red, Green, Blue));
		HBRUSH OldBrush = (HBRUSH)SelectObject(hdc, MyBrush);

		Rectangle(hdc, 10, 100, 410, 300);

		DeleteObject(SelectObject(hdc, OldBrush));

		EndPaint(hwnd, &ps);
		return 0;
	}
	//�ڽ� ��Ʈ�� ����
	case WM_CREATE:
	{
		hRed = CreateWindow(TEXT("scrollbar"), NULL,
			WS_CHILD | WS_VISIBLE | SBS_HORZ,
			10, 10, 400, 20, hwnd, (HMENU)ID_SCRRED, GetModuleHandle(0), NULL);

		hGreen = CreateWindow(TEXT("scrollbar"), NULL, WS_CHILD | WS_VISIBLE | SBS_HORZ,
			10, 40, 400, 20, hwnd, (HMENU)ID_SCRGREEN, GetModuleHandle(0), NULL);

		hBlue = CreateWindow(TEXT("scrollbar"), NULL, WS_CHILD | WS_VISIBLE | SBS_HORZ,
			10, 70, 400, 20, hwnd, (HMENU)ID_SCRBLUE, GetModuleHandle(0), NULL);

		heRed = CreateWindow(TEXT("edit"), TEXT("0"),
			WS_CHILD | WS_BORDER | WS_VISIBLE,
			420, 10, 40, 20, hwnd, (HMENU)ID_EDITRED, GetModuleHandle(0), 0);

		heGreen = CreateWindow(TEXT("edit"), TEXT("0"),
			WS_CHILD | WS_BORDER | WS_VISIBLE,
			420, 40, 40, 20, hwnd, (HMENU)ID_EDITGREEN, GetModuleHandle(0), 0);

		heBlue = CreateWindow(TEXT("edit"), TEXT("0"),
			WS_CHILD | WS_BORDER | WS_VISIBLE,
			420, 70, 40, 20, hwnd, (HMENU)ID_EDITBLUE, GetModuleHandle(0), 0);



		//--�ʱ�ȭ
		SetScrollRange(hRed, SB_CTL, 0, 255, TRUE);
		SetScrollPos(hRed, SB_CTL, 0, TRUE);

		SetScrollRange(hGreen, SB_CTL, 0, 255, TRUE);
		SetScrollPos(hGreen, SB_CTL, 0, TRUE);

		SetScrollRange(hBlue, SB_CTL, 0, 255, TRUE);
		SetScrollPos(hBlue, SB_CTL, 0, TRUE);

		SendMessage(heRed, EM_LIMITTEXT, 3, 0);

		return 0;
	}

	//��ũ�ѹٴ� �޽��� ����
	case WM_VSCROLL:
	{
		return 0;
	}

	case WM_HSCROLL:
	{
		if ((HWND)lParam == hRed)	TempPos = Red;
		if ((HWND)lParam == hGreen) TempPos = Green;
		if ((HWND)lParam == hBlue)	TempPos = Blue;

		//5���� ��ġ��
		switch (LOWORD(wParam))
		{
		case SB_LINELEFT:	//���� ȭ��ǥ
			TempPos = max(0, TempPos - 1);		//2���� ������ ū �� ��ȯ
			break;
		case SB_LINERIGHT:	//������ ȭ��ǥ
			TempPos = min(255, TempPos + 1);	//2���� ������ ���� �� ��ȯ
			break;
		case SB_PAGELEFT:	//���� ȭ��ǥ ~ THUMB 
			TempPos = max(0, TempPos - 10);
			break;
		case SB_PAGERIGHT:	//THUMB~������ ȭ��ǥ   
			TempPos = min(255, TempPos + 10);
			break;
		case SB_THUMBTRACK:	//THUMB
			TempPos = HIWORD(wParam);
			break;
		}

		//Tempos������ �ִ� ���� ��ũ�ѿ� ����
		if ((HWND)lParam == hRed)
		{
			Red = TempPos;

			TCHAR buf[20];
			wsprintf(buf, TEXT("%d"), Red);
			SetDlgItemText(hwnd, ID_EDITRED, buf);
		}
		if ((HWND)lParam == hGreen)
		{
			Green = TempPos;

			SetDlgItemInt(hwnd, ID_EDITGREEN, Green, 0);
		}
		if ((HWND)lParam == hBlue)
		{
			Blue = TempPos;

			SetDlgItemInt(hwnd, ID_EDITBLUE, Blue, 0);
		}

		//lParam : ��ũ�ѹ��� �ڵ� �ڵ�����
		SetScrollPos((HWND)lParam, SB_CTL, TempPos, TRUE);
		//----------------------------------------------------------------

		InvalidateRect(hwnd, NULL, FALSE);

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