//41_모달모달리스실습.cpp
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"

//1.구조체 정의
struct SHAPE
{
	POINT pt;
	int size;
	COLORREF color;
};

//2.전역변수 
SHAPE g_shape;

//A) 모달 다이얼로그 프로시저
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
			//컨트롤에 출력된 정보를 전달된 주소로 전달...			
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

//B) 모달리스 다이얼로그 프로시저
//자식 윈도의 핸들
HWND g_child = NULL;

//사용자 정의 통지 메시지
#define WM_APPLY WM_USER+100

//자식윈도우 프로시저
/*
* [적용기능]
* 1. 적용버튼 클릭시(IDOK)
* 2. 스크롤바를 이동할 때(WM_HSCROLL)
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

		//핸들 얻기
		hRed = GetDlgItem(hDlg, IDC_SCROLLBAR1);
		hGreen = GetDlgItem(hDlg, IDC_SCROLLBAR2);
		hBlue = GetDlgItem(hDlg, IDC_SCROLLBAR3);

		//Red, Green, Blue의 값 초기화
		Red		= GetRValue(pshape->color);
		Green	= GetGValue(pshape->color);
		Blue	= GetBValue(pshape->color);
		
		//Edit컨트롤(숫자)
		SetDlgItemInt(hDlg, IDC_EDIT1, GetRValue(pshape->color), 0);
		SetDlgItemInt(hDlg, IDC_EDIT4, GetGValue(pshape->color), 0);
		SetDlgItemInt(hDlg, IDC_EDIT5, GetBValue(pshape->color), 0);
				

		//스크롤바
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
		case IDOK: //적용버튼
		{
			//컨트롤에 출력된 정보를 전달된 주소로 전달...
			//에디트 or 스크롤바
			int r = GetDlgItemInt(hDlg, IDC_EDIT1, 0, 0);
			int g = GetDlgItemInt(hDlg, IDC_EDIT4, 0, 0);
			int b = GetDlgItemInt(hDlg, IDC_EDIT5, 0, 0);

			pshape->color = RGB(r, g, b);

			//죽지 않고 변경된 사실을 부모에게 전달(통지)
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

		//5개의 위치값
		switch (LOWORD(wParam))
		{
		case SB_LINELEFT:	//왼쪽 화살표
			TempPos = max(0, TempPos - 1);		//2개의 인자중 큰 값 반환
			break;
		case SB_LINERIGHT:	//오른쪽 화살표
			TempPos = min(255, TempPos + 1);	//2개의 인자중 작은 값 반환
			break;
		case SB_PAGELEFT:	//왼쪽 화살표 ~ THUMB 
			TempPos = max(0, TempPos - 10);
			break;
		case SB_PAGERIGHT:	//THUMB~오른쪽 화살표   
			TempPos = min(255, TempPos + 10);
			break;
		case SB_THUMBTRACK:	//THUMB
			TempPos = HIWORD(wParam);
			break;
		}

		//Tempos가지고 있는 값을 스크롤에 적요
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

		//lParam : 스크롤바의 핸들 핸들정보
		SetScrollPos((HWND)lParam, SB_CTL, TempPos, TRUE);
		//----------------------------------------------------------------
		//컨트롤에 출력된 정보를 전달된 주소로 전달...
			//에디트 or 스크롤바
		int r = GetScrollPos(hRed, SB_CTL);
		int g = GetScrollPos(hGreen, SB_CTL);
		int b = GetScrollPos(hBlue, SB_CTL);
//		int r = GetDlgItemInt(hDlg, IDC_EDIT1, 0, 0);
//		int g = GetDlgItemInt(hDlg, IDC_EDIT4, 0, 0);
//		int b = GetDlgItemInt(hDlg, IDC_EDIT5, 0, 0);

		pshape->color = RGB(r, g, b);

		//죽지 않고 변경된 사실을 부모에게 전달(통지)
		SendMessage(GetParent(hDlg), WM_APPLY, 0, 0);

		return 0;
	}
	}
	return FALSE;
}

//부모 프로시저
LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	//모달리스에게 전달되는 데이터
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
		//3. 전역변수 초기화
		g_shape.pt.x = 100;
		g_shape.pt.y = 100;
		g_shape.size = 50;
		g_shape.color = RGB(255, 0, 0);
		return 0;
	}

	case WM_KEYDOWN:
	{
		if (wParam == 'Q')  //모달 실행
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
		else if (wParam == 'W')  //모달리스 실행
		{
			temp = g_shape; //구조체 --> int 타입 , 값 = 값; 

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
	//윈도우 클래스 정의
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //펜, 브러쉬, 폰트
	wc.hCursor = LoadCursor(0, IDC_ARROW);//시스템
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //미리 만들어서 제공되는 프로시저(윈도우 공통 기능)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = 0;		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("0830"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);				//<-----------------(0)

	//메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);		//<--------------------(0)
		DispatchMessage(&msg);
	}
	return 0;
}