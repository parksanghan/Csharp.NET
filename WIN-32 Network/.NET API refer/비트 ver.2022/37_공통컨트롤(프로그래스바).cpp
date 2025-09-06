//36_공통컨트롤(프로그레스바).cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <CommCtrl.h>
//#pragma comment(lib "comctrl32.lib")


//----자식 ID로 사용할 정보를 정의
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

		//반드시 초기 설정(범위 구성)
		SendMessage(g_pro1, PBM_SETRANGE32, 0, 100); // 0 ~ 100

		//아래 코드를 통해서 POSITION 셋팅이 가능
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