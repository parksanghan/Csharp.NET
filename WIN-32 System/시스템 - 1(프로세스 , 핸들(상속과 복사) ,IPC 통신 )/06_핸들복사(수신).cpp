//06_핸들복사(수신).cpp
/*
* "B" 라는 이름의 타이틀바!
*/
#pragma comment (linker, "/subsystem:windows")	
#include <windows.h> 
#include <tchar.h>

LRESULT CALLBACK WndProc( HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	 switch( msg ) 
	 { 
	 case WM_USER + 100: 
	 { 
		 HANDLE hFile = (HANDLE)lParam;

		 char s[256] = "hello"; 
		 DWORD len; 
		 BOOL b = WriteFile( hFile, s, 256, &len, 0); 
		 if ( b == FALSE ) 
		 { 
			 TCHAR buf[256]; 
			 wsprintf(buf, TEXT("전달된 핸들 : %x \n실패 : %d"), hFile, GetLastError() ); 
			 MessageBox( 0, buf, TEXT(""), MB_OK); 
		 }
		 else 
		 { 
			 MessageBox( 0, TEXT("성공"), TEXT(""), MB_OK); 
			 CloseHandle( hFile ); 
		 } 
		 return 0;
	 }

	 case WM_DESTROY: 
		 PostQuitMessage(0); 
		 return 0; 
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
	wc.lpszClassName = TEXT("CLASSNAME");
	wc.lpszMenuName = 0;		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("CLASSNAME"), TEXT("A"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 800, 800,
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