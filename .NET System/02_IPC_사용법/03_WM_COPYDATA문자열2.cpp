//03_WM_COPYDATA문자열전송1.cpp
//skeleton 코드
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

HWND hEdit;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	char buf[100];
	TCHAR tbuf[100];
	switch (msg)
	{
	case WM_CREATE:
	{
		hEdit = CreateWindow(TEXT("edit"), TEXT(""), 
			WS_CHILD | WS_VISIBLE | WS_BORDER | ES_MULTILINE, 
			10, 10, 400, 400, hwnd, (HMENU)1, 0, 0);

		return 0;
	}
	case WM_COPYDATA:
	{
		COPYDATASTRUCT* p = (COPYDATASTRUCT*)lParam; 
		if (p->dwData == 1) // 식별자   조사.
		{
			memcpy(buf, p->lpData, sizeof(buf));
			MultiByteToWideChar(CP_ACP, 0, buf, -1, tbuf, _countof(tbuf));
			// Edit Box에 추가  한다.
			//SendMessage(hEdit, EM_REPLACESEL, 0, (LPARAM)(p->lpData)); 
			SendMessage(hEdit, EM_REPLACESEL, 0, (LPARAM)tbuf);
			SendMessage(hEdit, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
		}
	}
	return 0;
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
		TEXT("FIRST"), TEXT("B"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 800, 500,
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