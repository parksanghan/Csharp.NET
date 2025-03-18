//01_공유메모리1

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

typedef struct _LINE 
{
	POINTS ptFrom; 
	POINTS ptTo;
} LINE;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static HANDLE hEvent, hMap;		//커널 핸들
	static LINE* pData;
	static POINTS ptFrom;		//시작점.

	switch (msg)
	{
	case WM_LBUTTONDOWN: 
		ptFrom = MAKEPOINTS(lParam); 
		return 0;

	case WM_MOUSEMOVE:
		if (wParam & MK_LBUTTON) //LButton누른 상태에서 MouseMove
		{
			POINTS pt = MAKEPOINTS(lParam); 
			HDC hdc = GetDC(hwnd);

			MoveToEx(hdc, ptFrom.x, ptFrom.y, 0); 
			LineTo(hdc, pt.x, pt.y); 

			ReleaseDC(hwnd, hdc);
			
			// MMF 에   넣는다.
			pData->ptFrom = ptFrom; 
			pData->ptTo = pt; 
			SetEvent(hEvent); 
			ptFrom = pt;
		} 
		return 0;
	case WM_CREATE:
	{
		hEvent = CreateEvent(0, TRUE, 0, TEXT("DRAW_SIGNAL"));

		//MMF
		hMap = CreateFileMapping((HANDLE)-1, // Paging 화일을   사용해서   매핑
			0, PAGE_READWRITE, 0, sizeof(LINE), TEXT("map"));

		pData = (LINE*)MapViewOfFile(hMap, FILE_MAP_WRITE, 0, 0, 0); 
		
		if (pData == 0)
			MessageBox(0, TEXT("Fail"), TEXT(""), MB_OK);
		return 0;
	}
	case WM_DESTROY:
	{
		UnmapViewOfFile(pData); 
		CloseHandle(hMap);
		CloseHandle(hEvent);

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
		TEXT("FIRST"), TEXT("1번"), WS_OVERLAPPEDWINDOW,
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