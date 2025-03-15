#include <windows.h>

LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass=TEXT("FontDial");

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

LOGFONT lf;
COLORREF Col;
TCHAR *str=TEXT("폰트 대화상자 Test 1234");
LRESULT CALLBACK WndProc(HWND hWnd,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	HDC hdc;
	PAINTSTRUCT ps;
	CHOOSEFONT CFT;
	HFONT MyFont, OldFont;
	switch (iMessage) {
	case WM_CREATE:
		lf.lfHeight=20;
		lf.lfCharSet=HANGUL_CHARSET;
		lf.lfPitchAndFamily=VARIABLE_PITCH | FF_ROMAN;
		lstrcpy(lf.lfFaceName,TEXT("바탕"));
		return 0;
	case WM_LBUTTONDOWN:
		memset(&CFT, 0, sizeof(CHOOSEFONT));
		CFT.lStructSize = sizeof(CHOOSEFONT);
		CFT.hwndOwner=hWnd;
		CFT.lpLogFont=&lf;
		CFT.Flags=CF_EFFECTS | CF_SCREENFONTS;
		if (ChooseFont(&CFT)) {
			Col=CFT.rgbColors;
			InvalidateRect(hWnd, NULL, TRUE);
		}
		return 0;
	case WM_PAINT:
		hdc=BeginPaint(hWnd,&ps);
		MyFont=CreateFontIndirect(&lf);
		OldFont=(HFONT)SelectObject(hdc,MyFont);
		SetTextColor(hdc,Col);
		TextOut(hdc,100,100,str,lstrlen(str));
		SelectObject(hdc,OldFont);
		DeleteObject(MyFont);
		EndPaint(hWnd,&ps);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd,iMessage,wParam,lParam));
}

