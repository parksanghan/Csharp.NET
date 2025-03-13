//06_메시지처리(04_윈도우 생성코드에 추가)
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>

//메시지 처리함수(프로시저, 시스템함수)
//Callback : 미리 등록해서 필요할 때 호출되는 함수
LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN: //마우스L버튼 클릭
	{
		POINTS pt = MAKEPOINTS(lParam);
		TCHAR msg[10];
		wsprintf(msg, _TEXT("%d, %d"), pt.x, pt.y);

		SetWindowText(hwnd, msg);

		return 0;
	}

	//초기화 시점
	case WM_CREATE:    //비큐메시지(윈도우 생성, CreateWindow함수 반환이전)
	{
		return 0;
	}

	//종료처리 시점
	case WM_DESTROY:	//윈도우가 파괴되었을 때 호출되는 메시지
		//App 큐에 WM_QUIT 메시지를 넣어야 한다.
		PostQuitMessage(0);
		return 0;
	}

	return DefWindowProc(hwnd, msg, wParam, lParam);
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR cmd, int show)
{
	//윈도우 생성
	//1. 윈도우 클래스 정의
	WNDCLASS	wc;
	wc.cbClsExtra = 0;  //나중에 혹시 (임시 맴버)
	wc.cbWndExtra = 0;
	//--------------------------------------------------------------------
	wc.hbrBackground = (HBRUSH)GetStockObject(LTGRAY_BRUSH);  //배경
	wc.hCursor = LoadCursor(0, IDC_ARROW);
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;  //자신의 인스턴스(이 윈도우 클래스를 누가 만들었느냐?)
	wc.lpfnWndProc = WndProc;		//내가 만든 프로시저 등록
	//-----------------------------------------------------------------------
	wc.lpszClassName = _TEXT("First");  //KEY 중복되면 안됨
	wc.lpszMenuName = 0;  //메뉴의 리소스아이디
	wc.style = 0;

	//2. 윈도우 클래스를 레지스트리에 등록
	RegisterClass(&wc);

	//3. 윈도우 생성(첫번째 객체) : UI객체(메모리, 데이터) -> 핸들
	HWND hwnd = CreateWindowEx(0, _TEXT("First"), _TEXT("윈도우생성"),
		WS_OVERLAPPEDWINDOW & ~WS_THICKFRAME,
		100, 100, 500, 500,
		0, 0, hInst, 0);

	//4. 화면 출력
	ShowWindow(hwnd, show);

	//5. 메시지루프
	//GetMessage : 자신의 App Queue에서 메시지를 가져온다.
	//만약에 App Queue에 메시지가 없다면? 무한 대기 ex) scanf_s()
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0)) //GetMessage : WM_QUIT 일때만 리턴이 false
	{
		//등록된 프로시저 전달
		//해당 프로시저가 종료되어야만 리턴.
		DispatchMessage(&msg);
	}

	return 0;
}