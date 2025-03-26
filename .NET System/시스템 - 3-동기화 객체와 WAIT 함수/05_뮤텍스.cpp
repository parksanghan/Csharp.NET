//05_뮤텍스.cpp
//기존 코드 변경 -> 동일 프로세스 내 스레드간 동기화 처리!
/*
소유권

1) 생성 및 열기 CreateMutex / OpenMutext
2) 소유권 반납  ReleaseMutex
   - 가지고 있는 스레드만 반납
*/

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

//전역변수[누구나 접근 가능]
//멀티스레드 환경-서로 다른 스레드도 접근 가능
int x;

HANDLE g_Mutex;

//100,100 : 강아지
DWORD WINAPI ThreadFun1(LPVOID param)
{
	HDC hdc = GetDC((HWND)param);

	//신호등!
	/*
	* hMutex : Signal상태(아무도 소유하지 않고 있다)
	*          통과하게 되고(해당 소유권을 통과한 스레드가 획득)
	*           --> NonSignal 변경
	*/
	
	for (int i = 0; i < 100; i++)
	{
		WaitForSingleObject(g_Mutex, INFINITE);
		x = 100;		
		TextOut(hdc, x, 100, TEXT("강아지"), 3);
		ReleaseMutex(g_Mutex);  //소유권 반납(NonSignal 상태 변경)
		Sleep(1);
	}
	

	ReleaseDC((HWND)param, hdc);
	return 0;
}

//200,200 : 고양이
DWORD WINAPI ThreadFun2(LPVOID param)
{
	HDC hdc = GetDC((HWND)param);
	
	for (int i = 0; i < 100; i++)
	{
		WaitForSingleObject(g_Mutex, INFINITE);
		x = 200;		
		TextOut(hdc, x, 200, TEXT("고양이"), 3);
		ReleaseMutex(g_Mutex);  //소유권 반납(NonSignal 상태 변경)
		Sleep(1);
	}	

	ReleaseDC((HWND)param, hdc);
	return 0;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		DWORD ThreadID;
		HANDLE hThread = CreateThread(NULL, 0, ThreadFun1, hwnd, 0, &ThreadID);
		CloseHandle(hThread);

		hThread = CreateThread(NULL, 0, ThreadFun2, hwnd, 0, &ThreadID);
		CloseHandle(hThread);

		return 0;
	}
	case WM_CREATE:	//시작점(초기화 처리, 비큐, )
	{
		//TRUE : PT : 뮤텍스 소유 -> 뮤텍스 상태 NON SIGNAL
		//g_Mutex = CreateMutex(0, TRUE, TEXT("KEY"));

		//FALSE : 소유자가 없다 -> 뮤텍스 상태 SIGNAL
		g_Mutex = CreateMutex(0, FALSE, TEXT("KEY"));

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