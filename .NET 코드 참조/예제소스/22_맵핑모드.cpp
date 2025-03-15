//22_맵핑모드
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

void Print1(HDC hdc)
{
	//100,100,200,200 --> 상수(단위가  pixel)
	Rectangle(hdc, 100, 100, 200, 200); 

	// 논리 1= 0.1mm(물리), y축 증가 위 )
	SetMapMode(hdc, MM_LOMETRIC);
	Rectangle(hdc, 0, 0, 100, -100);
}

//10*10mm의 사각형 출력(마우스 클릭 위치에)
void Print2(HDC hdc, POINT pt)  //물리좌표
{
	SetMapMode(hdc, MM_LOMETRIC);

	//물리좌표 --> 논리좌표 
	DPtoLP(hdc, &pt, 1);

	Rectangle(hdc, pt.x, pt.y, pt.x + 50, pt.y + 50); //논리좌표(mm)
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		HDC hdc = GetDC(hwnd);
		POINT pt = { LOWORD(lParam), HIWORD(lParam) };
		Print2(hdc, pt);

		ReleaseDC(hwnd, hdc);
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