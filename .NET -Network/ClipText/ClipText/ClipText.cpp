#include <windows.h>

LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass=TEXT("ClipText");

int APIENTRY WinMain(HINSTANCE hInstance,HINSTANCE hPrevInstance
	  ,LPSTR lpszCmdParam,int nCmdShow)
{
	HWND hWnd;
	MSG Message;
	WNDCLASS WndClass;
	g_hInst=hInstance;

	WndClass.cbClsExtra=0;
	WndClass.cbWndExtra=0;
	WndClass.hbrBackground=(HBRUSH)(COLOR_WINDOW+1);
	WndClass.hCursor=LoadCursor(NULL,IDC_ARROW);
	WndClass.hIcon=LoadIcon(NULL,IDI_APPLICATION);
	WndClass.hInstance=hInstance;
	WndClass.lpfnWndProc=WndProc;
	WndClass.lpszClassName=lpszClass;
	WndClass.lpszMenuName=NULL;
	WndClass.style=CS_HREDRAW | CS_VREDRAW;
	RegisterClass(&WndClass);

	hWnd=CreateWindow(lpszClass,lpszClass,WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT,CW_USEDEFAULT,CW_USEDEFAULT,CW_USEDEFAULT,
		NULL,(HMENU)NULL,hInstance,NULL);
	ShowWindow(hWnd,nCmdShow);

	while (GetMessage(&Message,NULL,0,0)) {
		TranslateMessage(&Message);
		DispatchMessage(&Message);
	}
	return (int)Message.wParam;
}

#define ID_BTN_COPY 100
#define ID_BTN_PASTE 101
HWND Edit1;
TCHAR *str="Clipboard Test String";
LRESULT CALLBACK WndProc(HWND hWnd,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	HDC hdc;
	PAINTSTRUCT ps;
	HGLOBAL hmem;
	TCHAR *ptr;
	switch (iMessage) {
	case WM_CREATE:
		CreateWindow("button","Copy",WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
			10,50,100,25,hWnd,(HMENU)ID_BTN_COPY,g_hInst,NULL);
		CreateWindow("button","Paste",WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
			10,90,100,25,hWnd,(HMENU)ID_BTN_PASTE,g_hInst,NULL);
		Edit1=CreateWindow("edit","",WS_CHILD | WS_VISIBLE | WS_BORDER | 
			ES_MULTILINE,
			10,130,200,200,hWnd,(HMENU)3,g_hInst,NULL);
		return 0;
	case WM_COMMAND:
		switch (LOWORD(wParam)) {
		case ID_BTN_COPY:				// Copy
			hmem=GlobalAlloc(GHND, lstrlen(str)+1);
			ptr=(TCHAR *)GlobalLock(hmem);
			memcpy(ptr,str,lstrlen(str)+1);
			GlobalUnlock(hmem);
			if (OpenClipboard(hWnd)) {
				EmptyClipboard();
				SetClipboardData(CF_TEXT,hmem);
				CloseClipboard();
			} else {
				GlobalFree(hmem);
			}
			break;
		case ID_BTN_PASTE:				// Paste
			if (IsClipboardFormatAvailable(CF_TEXT)) { // 클립보드에 텍스트가 있는가 
				OpenClipboard(hWnd);
				hmem=GetClipboardData(CF_TEXT);// 핸들가져옴 
				ptr=(TCHAR *)GlobalLock(hmem);
				SetWindowText(Edit1, ptr);
				GlobalUnlock(hmem);
				CloseClipboard();
			}
			break;
		}
		return 0;
	case WM_PAINT:
		hdc=BeginPaint(hWnd,&ps);
		TextOut(hdc, 10, 10, str, lstrlen(str));
		EndPaint(hWnd,&ps);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd,iMessage,wParam,lParam));
}
