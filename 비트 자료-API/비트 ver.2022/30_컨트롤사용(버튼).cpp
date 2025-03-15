//30_컨트롤(버튼).cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//----자식 ID로 사용할 정보를 정의
#define IDC_BUTTON1		1
HWND g_hbutton;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	//버튼의 글자 변경
	case WM_LBUTTONDOWN:
	{
		SetWindowText(g_hbutton, TEXT("버튼글자변경"));
		MoveWindow(g_hbutton, 100, 100, 100, 25, TRUE);
		//ShowWindow(g_hbutton, SW_SHOW);
		return 0;
	}
	//자식 컨트를 생성
	case WM_CREATE:
	{
		//자식 컨트롤 생성
		g_hbutton = CreateWindow(TEXT("button"), TEXT("클릭!"),
			WS_CHILD | WS_BORDER | WS_VISIBLE| BS_PUSHBUTTON,
			20, 20, 100, 25, hwnd, (HMENU)IDC_BUTTON1, GetModuleHandle(0), 0);
		return 0;
	}

	//통지 메시지 수신
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))  //메뉴아이템ID, 자식컨트롤ID
		{
			case IDC_BUTTON1:  //클릭했다...
				SetWindowText(hwnd, TEXT("버튼 클릭!"));
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