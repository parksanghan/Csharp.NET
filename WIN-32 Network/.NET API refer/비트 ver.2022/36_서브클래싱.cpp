//36_서브클래싱.cpp
//숫자만 입력 가능한 에디트 컨트롤 만들기

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//----자식 ID로 사용할 정보를 정의
#define IDC_EDIT1		1
#define IDC_STATIC1		2

HWND g_hedit1;
HWND g_static1;

WNDPROC old; // 원래의 EditBox의 메세지 처리함수의 주소를 담을 변수

LRESULT CALLBACK foo(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_CHAR:
		if ((wParam >= '0' && wParam <= '9') || wParam == 8)			
			return CallWindowProc(old, hwnd, msg, wParam, lParam);
		return 0; // 숫자 이외의 경우는 무시한다.
	}
	// 나머지 모든 메세지는 원래의 함수로 전달한다.
	return CallWindowProc(old, hwnd, msg, wParam, lParam);
}


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		//자식 컨트를 생성
	case WM_CREATE:
	{
		//자식 컨트롤 생성
		g_hedit1 = CreateWindow(TEXT("edit"), TEXT("숫자만입력가능"),
			WS_CHILD | WS_BORDER | WS_VISIBLE,
			20, 20, 200, 25, hwnd, (HMENU)IDC_EDIT1, GetModuleHandle(0), 0);

		g_static1 = CreateWindow(TEXT("static"), NULL,
			WS_CHILD | WS_BORDER | WS_VISIBLE,
			20, 50, 200, 25, hwnd, (HMENU)IDC_STATIC1, GetModuleHandle(0), 0);

		//에디트컨트롤의 프로시저를 변경!
		old = (WNDPROC)SetWindowLong(g_hedit1, GWL_WNDPROC, (LONG)foo);
		SetFocus(g_hedit1);
		return 0;
	}

	//통지 메시지 수신
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))  //메뉴아이템ID, 자식컨트롤ID
		{
		case IDC_EDIT1:  //누가 통지했는가?
		{
			if (HIWORD(wParam) == EN_CHANGE)  //왜??
			{
				TCHAR buf[50];
				//GetWindowText(g_hedit1, buf, _countof(buf));
				//SetWindowText(hwnd, buf);

				//Dlg(Main윈도우)의 Item(자식 컨트롤)의 글자를 get!
				GetDlgItemText(hwnd, IDC_EDIT1, buf, _countof(buf));
				SetDlgItemText(hwnd, IDC_STATIC1, buf);
			}
			break;
		}
		}
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