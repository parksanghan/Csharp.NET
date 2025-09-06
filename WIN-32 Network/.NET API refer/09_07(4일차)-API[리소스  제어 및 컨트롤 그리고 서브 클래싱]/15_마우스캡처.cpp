//15_마우스캡처.cpp
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>
#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")\
POINTS start, end;

/*
일반적으로 마우스 메시지는 발생할 당시 커서 아래 있는 윈도우에 전달됩니다. SetCapture()의 경우 모든 마우스의 메시지는 캡쳐한 마우스를 캡쳐한 윈도우에게 전달됩니다.

Q :   if(GetCapture() == hwnd)   ->  if(GetCapture() != hwnd)
경우 어떻게 작동이 되나?
가장먼저 좌측 클릭 시에 SetCapture()를 한다.
이후 WM_MOUSEMOVE 이벤트 발생 시
기존 코드에서는
  if(GetCapture() != hwnd)
=> 현재 커서에 있는 윈도우와 실행한 윈도우와 같지 않는 경우 리턴한다.
그렇기에 커서에 있는 윈도우와 실행한 윈도우가 같은 경우에만 좌표를 통한 라인을 출력한다.

만약 ,  if(GetCapture() == hwnd) 로 바꾸게 된다면?
좌클릭은 동일하게 가장먼저 클릭 시에 SetCapture()를 한다.
이후 MOUSEMOVE 에서  if(GetCapture() == hwnd) 를 통해
마우스와 실행한 윈도우가 같게 되니 그냥 리턴한다.

그렇기에 이전과 같이 선이 그려지지 않는다.
왼쪽 클릭이 없는 경우에만 선을 나타나며
마우스 캡쳐가 현재 윈도우와 같을 경우만 코드가 실행되며 다른 윈도우에서 발생한 마우스 메시지는 무시된다.
*/


/*
일반적으로 마우스 메시지는, 메시지가 발생할 당시 커서의 아래 있는 윈도우에게 전달된다.
하지만 SetCapture()함수를 사용 하므로서 이런 행동을 변경할 수 있다.
특정윈도우가 SetCapture()함수를 사용해서 마우스를 캡쳐할 경우, 모든 마우스 메시지는
마우스를 캡쳐한 윈도우에게로 전달된다.
*/


/*
마우스 캡쳐는 커서가 윈도우를 벗어나더라도 계속 메시지를 받고자 할때 유용하게 사용
된다.
주소 BUTTONDOWN에서 캡쳐를 BUTTONUP에서 Release를 해준다.
현재 어떤 윈도우가 캡쳐를 했는지를 알아내려면 GetCapture() 함수를 사용한다.
*/
LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		SetCapture(hwnd);					//<----캡쳐
		start = end = MAKEPOINTS(lParam);		
		return 0;
	}
	case WM_MOUSEMOVE:
	{
		if (GetCapture() != hwnd)			//<-- 누가 켭쳐중인가?
			return 0;		

		HDC hdc = GetDC(hwnd);
		SetROP2(hdc, R2_NOT);		//>>>> 반전모드 (0은 1로 , 1은 0으로)

		//이전에 그린 그림을 지우기
		MoveToEx(hdc, start.x, start.y, 0);
		LineTo(hdc, end.x, end.y);

		//자표 이동
		end = MAKEPOINTS(lParam);

		//이동된 좌표로 그리기
		MoveToEx(hdc, start.x, start.y, 0);
		LineTo(hdc, end.x, end.y);

		ReleaseDC(hwnd, hdc);		

		return 0;
	}
	case WM_LBUTTONUP:
	{
		ReleaseCapture();			//<===캡쳐 해제

		return 0;
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