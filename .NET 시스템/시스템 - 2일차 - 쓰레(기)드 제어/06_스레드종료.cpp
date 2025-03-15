//06_스레드종료.cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <commctrl.h>	

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

//프로그래스바 ID / HANDLE
#define IDC_PRO  1
HWND hPrg;

//Thread핸들
HANDLE hThread;

//thread 함수
//프로그래스바를 0...1000 이동!
DWORD WINAPI ThreadFunc1(LPVOID temp)
{
	//프로그래스바 컨트롤 핸들
	HWND hPrg = (HWND)temp;

	for (int i = 0; i < 1000; ++i) //0...1000
	{
		SendMessage(hPrg, PBM_SETPOS, i, 0); // 프로그래스 전진 
		for (int k = 0; k < 5000000; ++k); // 0 6개 - some work.!! 
	}

	return 0;	//카운트 0으로 
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		//스레드 생성 -> 커널 객체 생성[핸들2개 생성->프로세스내에...]
		DWORD ThreadID;
		hThread = CreateThread(0, 0, ThreadFunc1, hPrg, 0, &ThreadID); // 카운트:2 

		SetWindowText(hwnd, TEXT("스레드 생성"));

		//스레드 제어 안할래!!!
		//CloseHandle(hThread); // 카운트 : 1

		return TRUE;
	}
	case WM_RBUTTONDOWN:
	{
		DWORD code; 
		GetExitCodeThread(hThread, &code); 
		if (code == STILL_ACTIVE) 
		{ 
			SetWindowText(hwnd, TEXT("스레드 동작중"));
			TerminateThread(hThread, 100); 
			CloseHandle(hThread); 
		}
		else
		{
			SetWindowText(hwnd, TEXT("스레드 종료"));
		}

		return 0;
	}

	case WM_CREATE:	//시작점(초기화 처리, 비큐, )
	{
		hPrg = CreateWindow(PROGRESS_CLASS, TEXT(""),
			WS_CHILD | WS_VISIBLE | WS_BORDER | PBS_SMOOTH,
			10, 10, 500, 30, hwnd, (HMENU)IDC_PRO, 0, 0);

		//범위:0 ~1000 
		SendMessage(hPrg, PBM_SETRANGE32, 0, 1000);

		//초기위치:0으로 초기화.
		SendMessage(hPrg, PBM_SETPOS, 0, 0);

		return 0;
	}
	case WM_DESTROY: //끝점(종료 처리, 윈도우가 파괴됨)
	{
		PostQuitMessage(0);

		return 0;
	}
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//1. 윈도우 생성, 출력
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //펜, 브러쉬, 폰트
	wc.hCursor = LoadCursor(0, IDC_ARROW);//시스템
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //미리 만들어서 제공되는 프로시저(윈도우 공통 기능)
	wc.lpszClassName = CLASS_NAME;
	wc.lpszMenuName = 0;		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		CLASS_NAME, WINDOW_NAME, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 600, 600,
		//CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,  //시스템이 알아서 출력!
		0, 0, hInst, 0);

	ShowWindow(hwnd, nShowCmd);
	UpdateWindow(hwnd);

	//2. 메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}