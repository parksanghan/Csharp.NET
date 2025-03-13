//13_MMF를 이용한공유.cpp
/*
MMF 사용
1) 대용량 메모리(12_예시)
2) 서로 다른 프로세스 간 데이터 공유(***)
   공유메모리 
*/
//04_skeleton.cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

typedef struct _LINE 
{ 
	POINTS ptFrom; 
	POINTS ptTo; 
}LINE;

POINTS ptFrom;	//시작점!


//1) 공유메모리를 위한 데이터 선언
HANDLE hEvent;		//자동이벤트객체(순서!)
HANDLE hMap;		//MMF
LINE* pData;		//공유메모리에 접근하는 포인터

//2) WM_CREATE : 공유메모리를 위한 초기화 처리(객체 생성 및 포인터 반환)
//3) WM_DESTROY: 공유메모리 관련 해제 처리
//4) WM_MOUSEMOVE : 공유메모리에 좌표 구조체를 저장 -> 이벤트 발생


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		ptFrom = MAKEPOINTS(lParam); 
		return 0;
	}
	case WM_MOUSEMOVE: 
	{
		if (wParam & MK_LBUTTON)
		{
			POINTS pt = MAKEPOINTS(lParam);			
			HDC hdc = GetDC(hwnd);						
			MoveToEx(hdc, ptFrom.x, ptFrom.y, 0);	//L버튼 시작점!
			LineTo(hdc, pt.x, pt.y);				//MouseMove
			ReleaseDC(hwnd, hdc);

			// MMF 에 넣는다. 
			pData->ptFrom = ptFrom; 
			pData->ptTo = pt; 
			//SetEvent( hEvent );
			PulseEvent(hEvent); //Set + ReSet
			
			ptFrom = pt;		//좌표이동			
		}
	}
	case WM_CREATE:	//시작점(초기화 처리, 비큐, )
	{
		//한명만....
		//hEvent = CreateEvent(0, 0, 0, TEXT("DRAW_SIGNAL")); 
		//다수에게...
		hEvent = CreateEvent(0, TRUE, 0, TEXT("DRAW_SIGNAL"));

		hMap = CreateFileMapping((HANDLE)-1, // Paging 화일을 사용해서 매핑 
			0, PAGE_READWRITE, 0, sizeof(LINE), TEXT("map"));

		pData = (LINE*)MapViewOfFile( hMap, FILE_MAP_WRITE, 0, 0,0); 
		if ( pData == 0 ) 
			MessageBox( 0, TEXT("Fail"), TEXT(""), MB_OK);

		return 0;
	}
	case WM_DESTROY: //끝점(종료 처리, 윈도우가 파괴됨)
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
		CW_USEDEFAULT, 0, 500, 500,
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

