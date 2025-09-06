//skeleton 코드
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

typedef struct _LINE
{
	POINTS ptFrom;
	POINTS ptTo;
} LINE;

DWORD WINAPI foo(void* p) 
{	
	HWND hwnd = (HWND)p;

	HANDLE hEvent = OpenEvent(EVENT_ALL_ACCESS, FALSE, TEXT("DRAW_SIGNAL")); 
	HANDLE hMap = OpenFileMapping(FILE_MAP_ALL_ACCESS, FALSE, TEXT("map")); 
	if (hMap == 0)
	{
		MessageBox(0, TEXT("1번   프로그램을   먼저   실행하세요"),TEXT(""), MB_OK);
		return 0;
	}

	LINE* pData = (LINE*)MapViewOfFile(hMap, FILE_MAP_READ, 0, 0, 0); 
	
	while (1)
	{
		// Event를   대기한다.
		WaitForSingleObject(hEvent, INFINITE); // 이제    Line의   정보가   pData에   있다. 
		
		HDC hdc = GetDC( hwnd );
		MoveToEx(hdc, pData->ptFrom.x, pData->ptFrom.y, 0); 
		LineTo(hdc, pData->ptTo.x, pData->ptTo.y); 
		ReleaseDC(hwnd, hdc);
	}
	UnmapViewOfFile(pData); 
	CloseHandle(hMap); 
	CloseHandle(hEvent); 
	return 0;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		//foo(hwnd);
		//쓰레드 생성
		DWORD threadid;
		HANDLE hThread = CreateThread(0, 0, foo, hwnd, 0, &threadid);		
		CloseHandle(hThread);
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
		TEXT("FIRST"), TEXT("2번"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 500, 500,
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