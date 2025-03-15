//41_��޸�޸����ǽ�.cpp
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"

//1.����ü ����
struct SHAPE
{
	POINT pt;
	int size;
	COLORREF color;
};

//2.�������� 
SHAPE g_shape;

//A) ��� ���̾�α� ���ν���
BOOL CALLBACK Modal_DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static SHAPE* pshape = NULL;

	switch (msg)
	{
	case WM_INITDIALOG:
	{
		pshape = (SHAPE*)lParam;

		SetDlgItemInt(hDlg, IDC_EDIT1, pshape->pt.x, 0);
		SetDlgItemInt(hDlg, IDC_EDIT2, pshape->pt.y, 0);
		SetDlgItemInt(hDlg, IDC_EDIT3, pshape->size, 0);

		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDOK:
		{
			//��Ʈ�ѿ� ��µ� ������ ���޵� �ּҷ� ����...			
			pshape->pt.x = GetDlgItemInt(hDlg, IDC_EDIT1, 0, 0);
			pshape->pt.y = GetDlgItemInt(hDlg, IDC_EDIT2, 0, 0);
			pshape->size = GetDlgItemInt(hDlg, IDC_EDIT3, 0, 0);

			EndDialog(hDlg, IDOK);
			return TRUE;
		}

		case IDCANCEL:
			EndDialog(hDlg, IDCANCEL);
			return TRUE;
		}
	}
	}
	return FALSE;
}

//B) ��޸��� ���̾�α� ���ν���
//�ڽ� ������ �ڵ�
HWND g_child = NULL;

//����� ���� ���� �޽���
#define WM_APPLY WM_USER+100

//�ڽ������� ���ν���
/*
* [������]
* 1. �����ư Ŭ����(IDOK)
* 2. ��ũ�ѹٸ� �̵��� ��(WM_HSCROLL)
*/
BOOL CALLBACK Modaless_DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static SHAPE* pshape = NULL;
	static HWND hRed, hGreen, hBlue;
	static int Red, Green, Blue;
	int TempPos = 0;

	switch (msg)
	{
	case WM_INITDIALOG:
	{
		pshape = (SHAPE*)lParam;

		//�ڵ� ���
		hRed = GetDlgItem(hDlg, IDC_SCROLLBAR1);
		hGreen = GetDlgItem(hDlg, IDC_SCROLLBAR2);
		hBlue = GetDlgItem(hDlg, IDC_SCROLLBAR3);

		//Red, Green, Blue�� �� �ʱ�ȭ
		Red		= GetRValue(pshape->color);
		Green	= GetGValue(pshape->color);
		Blue	= GetBValue(pshape->color);
		
		//Edit��Ʈ��(����)
		SetDlgItemInt(hDlg, IDC_EDIT1, GetRValue(pshape->color), 0);
		SetDlgItemInt(hDlg, IDC_EDIT4, GetGValue(pshape->color), 0);
		SetDlgItemInt(hDlg, IDC_EDIT5, GetBValue(pshape->color), 0);
				

		//��ũ�ѹ�
		SetScrollRange(hRed, SB_CTL, 0, 255, TRUE);
		SetScrollPos(hRed, SB_CTL, GetRValue(pshape->color), TRUE);

		SetScrollRange(hGreen, SB_CTL, 0, 255, TRUE);
		SetScrollPos(hGreen, SB_CTL, GetGValue(pshape->color), TRUE);

		SetScrollRange(hBlue, SB_CTL, 0, 255, TRUE);
		SetScrollPos(hBlue, SB_CTL, GetBValue(pshape->color), TRUE);

		return TRUE;
	}
	
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDOK: //�����ư
		{
			//��Ʈ�ѿ� ��µ� ������ ���޵� �ּҷ� ����...
			//����Ʈ or ��ũ�ѹ�
			int r = GetDlgItemInt(hDlg, IDC_EDIT1, 0, 0);
			int g = GetDlgItemInt(hDlg, IDC_EDIT4, 0, 0);
			int b = GetDlgItemInt(hDlg, IDC_EDIT5, 0, 0);

			pshape->color = RGB(r, g, b);

			//���� �ʰ� ����� ����� �θ𿡰� ����(����)
			SendMessage(GetParent(hDlg), WM_APPLY, 0, 0);

			return TRUE;
		}

		case IDCANCEL:
			g_child = NULL; //<-------------------------------
			EndDialog(hDlg, IDCANCEL);
			return TRUE;
		}
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
			SetDlgItemInt(hDlg, IDC_EDIT1, Red,0);
		}
		if ((HWND)lParam == hGreen)
		{
			Green = TempPos;
			SetDlgItemInt(hDlg, IDC_EDIT4, Green, 0);
		}
		if ((HWND)lParam == hBlue)
		{
			Blue = TempPos;
			SetDlgItemInt(hDlg, IDC_EDIT5, Blue, 0);
		}

		//lParam : ��ũ�ѹ��� �ڵ� �ڵ�����
		SetScrollPos((HWND)lParam, SB_CTL, TempPos, TRUE);
		//----------------------------------------------------------------
		//��Ʈ�ѿ� ��µ� ������ ���޵� �ּҷ� ����...
			//����Ʈ or ��ũ�ѹ�
		int r = GetScrollPos(hRed, SB_CTL);
		int g = GetScrollPos(hGreen, SB_CTL);
		int b = GetScrollPos(hBlue, SB_CTL);
//		int r = GetDlgItemInt(hDlg, IDC_EDIT1, 0, 0);
//		int g = GetDlgItemInt(hDlg, IDC_EDIT4, 0, 0);
//		int b = GetDlgItemInt(hDlg, IDC_EDIT5, 0, 0);

		pshape->color = RGB(r, g, b);

		//���� �ʰ� ����� ����� �θ𿡰� ����(����)
		SendMessage(GetParent(hDlg), WM_APPLY, 0, 0);

		return 0;
	}
	}
	return FALSE;
}

//�θ� ���ν���
LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	//��޸������� ���޵Ǵ� ������
	static SHAPE temp;

	switch (msg)
	{
	case WM_APPLY:
	{		
		g_shape.color = temp.color;

		InvalidateRect(hwnd, 0, TRUE);

		return 0;
	}

	case WM_CREATE:
	{
		//3. �������� �ʱ�ȭ
		g_shape.pt.x = 100;
		g_shape.pt.y = 100;
		g_shape.size = 50;
		g_shape.color = RGB(255, 0, 0);
		return 0;
	}

	case WM_KEYDOWN:
	{
		if (wParam == 'Q')  //��� ����
		{
			SHAPE temp = g_shape;

			UINT ret = DialogBoxParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG2),
				hwnd, Modal_DlgProc, (LPARAM)&temp);
			if (ret == IDOK)
			{
				g_shape = temp;
				InvalidateRect(hwnd, 0, TRUE);
			}
		}
		else if (wParam == 'W')  //��޸��� ����
		{
			temp = g_shape; //����ü --> int Ÿ�� , �� = ��; 

			if (g_child == NULL)
			{
				g_child = CreateDialogParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG3),
					hwnd, Modaless_DlgProc, (LPARAM)&temp);
				ShowWindow(g_child, SW_SHOW);
			}
			else
			{
				SetFocus(g_child);
			}
		}
		return 0;
	}
	
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		HBRUSH br = CreateSolidBrush(g_shape.color);
		HBRUSH oldbr = (HBRUSH)SelectObject(hdc, br);

		Rectangle(hdc, g_shape.pt.x, g_shape.pt.y, 
			g_shape.pt.x + g_shape.size, g_shape.pt.y + g_shape.size);

		DeleteObject(SelectObject(hdc, oldbr));

		EndPaint(hwnd, &ps);
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