//05_스레드죽이기
/*
* 쓰레드커널오브젝트
* SuspendCount 0보다 크면 멈춤. (0일 때 실행)
* - 최초 스레드 생성시 CREATE_SUSPENDED  --> SC : 1부터 시작.
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <commctrl.h>  //공통컨트롤(ProgressBar)

//스레드 시작 함수
DWORD WINAPI foo(void* p)
{
	HWND hPrg = (HWND)p;

	for (int i = 0; i < 1000; ++i)
	{
		SendMessage(hPrg, PBM_SETPOS, i, 0); // 프로그래스 전진
		for (int k = 0; k < 5000000; ++k); // 0 6개 - some work.!!
	}
	return 0;
}



LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static HWND hPrg;
	static HANDLE hThread;
	switch (msg)
	{
	case WM_CREATE:
		hPrg = CreateWindow(PROGRESS_CLASS, TEXT(""),
			WS_CHILD | WS_VISIBLE | WS_BORDER | PBS_SMOOTH,
			10, 10, 500, 30, hwnd, (HMENU)1, 0, 0);
		//범위:0 ~1000 초기위치:0으로 초기화.
		SendMessage(hPrg, PBM_SETRANGE32, 0, 1000);
		SendMessage(hPrg, PBM_SETPOS, 0, 0);
		return 0;
	case WM_LBUTTONDOWN:
	{
		// 새로운 스레드를 만들어서 작업을 시키고 주스레드는 최대한 빨리
		// 메세지 루프로 돌아 가서 다음 메세지를 처리한다.
		DWORD tid;
		hThread = CreateThread(0, 0, // TKO 보안, Stack 크기
			foo, (void*)hPrg, // 스레드로 실행할 함수,인자
			0,//바로 실행
			&tid);	  // 생성된 스레드 ID를 담을 변수
		//CloseHandle( hThread ); // TKO의 참조개수를 초기에 2이다.
		// 스레드 종료와 함께 즉시 파괴되도록 1 줄인다.
	}
	return 0;
	case WM_RBUTTONDOWN:
	{
		DWORD code;
		GetExitCodeThread(hThread, &code);
		if (code == STILL_ACTIVE)
		{
			TerminateThread(hThread, 100);
			CloseHandle(hThread);
		}

	}
	return 0;

	case WM_DESTROY:
		CloseHandle(hThread);
		PostQuitMessage(0);
		return 0;
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
	UpdateWindow(hwnd);

	//메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}