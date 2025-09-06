//39_모달리스 다이얼로그.cpp
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"


struct DATA
{
	TCHAR name[20];
	int age;
};

//자식 윈도의 핸들
HWND g_child = NULL;

//사용자 정의 통지 메시지
#define WM_APPLY WM_USER+100

//자식윈도우 프로시저
BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static DATA* pdata = NULL;

	switch (msg)
	{
	case WM_INITDIALOG:
	{
		pdata = (DATA*)lParam;

		SetDlgItemText(hDlg, IDC_EDIT1, pdata->name);
		SetDlgItemInt(hDlg, IDC_EDIT2, pdata->age, 0);

		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDOK:
		{
			//컨트롤에 출력된 정보를 전달된 주소로 전달...
			GetDlgItemText(hDlg, IDC_EDIT1, pdata->name, _countof(pdata->name));
			pdata->age = GetDlgItemInt(hDlg, IDC_EDIT2, 0, 0);

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
	}
	return FALSE;
}

//-----------전역변수 ----------
DATA g_data = { TEXT("홍길동"), 10 };

//부모윈도우 프로시저 
LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static DATA temp;

	switch (msg)
	{
	case WM_APPLY:
	{
		//자식이 변경된 정보를 전달해 줌..
		//여기서 처리!!!!
		_tcscpy_s(g_data.name, _countof(g_data.name), temp.name);
		g_data.age = temp.age;
		InvalidateRect(hwnd, 0, TRUE);

		return 0;
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		TextOut(hdc, 10, 10, g_data.name, _tcslen(g_data.name));
		TCHAR temp[20];
		wsprintf(temp, TEXT("%d"), g_data.age);
		TextOut(hdc, 10, 30, temp, _tcslen(temp));

		EndPaint(hwnd, &ps);
		return 0;
	}
	case WM_LBUTTONDOWN:	//모달리스 실행
	{		
		_tcscpy_s(temp.name, _countof(temp.name), g_data.name);
		temp.age = g_data.age;

		if (g_child == NULL)
		{
			g_child = CreateDialogParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG1),
				hwnd, DlgProc, (LPARAM)&temp);
			ShowWindow(g_child, SW_SHOW);
		}
		else
		{
			SetFocus(g_child);
		}		

		return 0;
	}
	case WM_CREATE:
	{
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