//14_HitTestCode.cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
// 클라이언트 영역 마우스 메시지일 경우 클라이 언트 좌표로, 비클라이언트 영역 메세지일경우는 스크린 좌표로 들어온다.
// 아래의 매크로를 사용하면 lParam으로 부터 쉽게 x, y 좌표를 받을 수 있다.x, y좌표를 얻을 수 있다.
// 또한 비 클라이언트 영역 마우스 메시지의 경우는 lParam의 경우 윈도우 좌표가 들어오고,
// wParam에는 hit - test code가 들어온다.
/*
int x = LOWORD(lParam);
int y = HIWORD(lParam);
*/

// 스크린 좌표는  위의 코드로 얻을 수 있다.
// 하지만 클라이언트 좌표의 경우 ClientToScreen 함수로 좌표를 얻을 수 있다.
/*
POINTS pt = MAKEPOINTS(lParam);
POINT pt1 = { LOWORD(lParam),HIWORD(lParam)};
TCHAR buf[50];														=> 클라이언트 좌표를 얻는다.
wsprintf(buf, TEXT("(%d:%d)\r\n(%d:%d)"), pt.x, pt.y, pt1.x, pt1.y);
MessageBox(NULL, buf, TEXT(""), MB_OK);

*/
// -> HIT TEST code 는 현재 커서가 어느 부분에 있는지를 알려주는 상수 이다.
// => WM_NCHITEST 메시지가 DefWindowPorc()에 전달될 때 이 코드는 리턴한다.

 

세지일경우는 스크린 좌표로 들어온다.아래의 매크로를 사용하면 lParam으로 부터 쉽게
x, y좌표를 얻을 수 있다.
#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_NCHITTEST: // 클라이언트 영역이 아닌 경우 
	{
		int code = DefWindowProc(hwnd, msg, wParam, lParam); // 
		if (code == HTCLIENT && GetKeyState(VK_CONTROL) < 0)//-> ctrl 키 버튼 눌러져 있는지 확인하고 , 마우스 커서가 클라이언트 영역에 있는지 확인
		{	//HTCLIENT는  WM_NCHITEST 메시지에서 반환되는 메시지 중 하나.
			
			code = HTCAPTION; //	  그렇다면 HTCAPTION 으로 값을 변경하고  
			// 타이틀 바를 드래그 하는 것처럼 이동할 수 있다.
		}

		if (code == HTLEFT)  // 만약 DefWindowProc에 반환 된 값이 HTLEFT 인 경우
							// 즉 , 클라이언트 왼쪽에 커서를 둔 경우 , 값을 HTRIGHT 로 변경한다. 
							// -> 이렇게 하여 왼쪽에서 드래그 해도 오른쪽에서 레이아웃이 변동됨.
			code = HTRIGHT;

		return code;
	}
	case WM_CREATE:	//시작점(초기화 처리, 비큐, )
	{
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
		CW_USEDEFAULT, 0, 800, 800,
		//CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,  //시스템이 알아서 출력!
		0, 0, hInst, 0);

	ShowWindow(hwnd, nShowCmd);
	UpdateWindow(hwnd);

	//2. 메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		//TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}