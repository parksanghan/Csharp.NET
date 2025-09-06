//33_컨트롤(콤보박스).cpp
//콤보박스 & 리스트박스 유사함.

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//----자식 ID로 사용할 정보를 정의
#define IDC_COMBOBOX1		1
#define IDC_STATIC1			2

HWND g_combobox1;


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		//자식 컨트를 생성
	case WM_CREATE:
	{
		//자식 컨트롤 생성
		CreateWindow(TEXT("static"), NULL,
			WS_CHILD | WS_BORDER | WS_VISIBLE,
			20, 20, 200, 25, hwnd, (HMENU)IDC_STATIC1, GetModuleHandle(0), 0);

		g_combobox1 = CreateWindow(TEXT("combobox"), NULL,
			WS_CHILD | WS_BORDER | WS_VISIBLE | CBS_DROPDOWN,
			20, 50, 200, 300, hwnd, (HMENU)IDC_COMBOBOX1, GetModuleHandle(0), 0);

		//---------- 콤보 박스에 원하는 문자열 저장 --------------
		SendMessage(g_combobox1, CB_ADDSTRING, 0, (LPARAM)TEXT("첫번째문자열"));
		SendMessage(g_combobox1, CB_ADDSTRING, 0, (LPARAM)TEXT("두번째문자열"));
		SendMessage(g_combobox1, CB_ADDSTRING, 0, (LPARAM)TEXT("세번째문자열"));
		SendMessage(g_combobox1, CB_ADDSTRING, 0, (LPARAM)TEXT("네번째문자열"));

		//---------- 콤보박스 초기화(시작시 원하는 인덱스 출력) -----
		SendMessage(g_combobox1, CB_SETCURSEL, 2, 0);
		return 0;
	}

	//통지 메시지 수신
	case WM_COMMAND:
	{
		if (LOWORD(wParam) == IDC_COMBOBOX1)  //누가 통지했는가?
		{
			if (HIWORD(wParam) == CBN_SELCHANGE)  //왜??
			{
				//1.선택한 인덱스 획득
				int idx = SendMessage(g_combobox1, CB_GETCURSEL, 0, 0);

				//2.해당 인덱스의 문자열 획득
				TCHAR buf[100] = TEXT("");
				SendMessage(g_combobox1, CB_GETLBTEXT, idx, (LPARAM)buf);

				//static에 출력
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