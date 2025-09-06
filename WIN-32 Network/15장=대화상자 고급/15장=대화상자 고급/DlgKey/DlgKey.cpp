#include <windows.h>

LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);
BOOL CALLBACK KeyDlgProc(HWND hDlg,UINT iMessage,WPARAM wParam,LPARAM lParam);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass=TEXT("DlgKey");

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

#include "resource.h"

//* 키입력을 받을 수 없는 방법
BOOL CALLBACK KeyDlgProc(HWND hDlg,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	switch (iMessage) {
	case WM_INITDIALOG:
		return TRUE;
	case WM_KEYDOWN:
		if (wParam == 'A') {
			MessageBox(hDlg,"A키 입력을 받았습니다","알림",MB_OK);
		}
		return TRUE;
	case WM_COMMAND:
		switch (LOWORD(wParam)) {
		case IDOK:
		case IDCANCEL:
			EndDialog(hDlg,LOWORD(wParam));
			return TRUE;
		}
		break;
	}
	return FALSE;
}
//*/

/* 서브클래싱하여 포커스를 강제로 뺏어오는 방법
WNDPROC OldProc;
LRESULT CALLBACK NewProc(HWND hWnd,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	switch (iMessage) {
	case WM_SETFOCUS:
		return 0;
	}
	return CallWindowProc(OldProc,hWnd,iMessage,wParam,lParam);
}

BOOL CALLBACK KeyDlgProc(HWND hDlg,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	switch (iMessage) {
	case WM_INITDIALOG:
		OldProc=(WNDPROC)SetWindowLong(hDlg,GWL_WNDPROC,(LONG)NewProc);
		return TRUE;
	case WM_LBUTTONDOWN:
		SetFocus(hDlg);
		return TRUE;
	case WM_KEYDOWN:
		if (wParam == 'A') {
			MessageBox(hDlg,"A키 입력을 받았습니다","알림",MB_OK);
		}
		return TRUE;
	case WM_COMMAND:
		switch (LOWORD(wParam)) {
		case IDOK:
		case IDCANCEL:
			SetWindowLong(hDlg,GWL_WNDPROC,(LONG)OldProc);
			EndDialog(hDlg,LOWORD(wParam));
			return TRUE;
		}
		break;
	}
	return FALSE;
}
//*/

LRESULT CALLBACK WndProc(HWND hWnd,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	HDC hdc;
	PAINTSTRUCT ps;
	TCHAR *Mes="마우스 왼쪽 버튼을 누르면 키 입력을 받는 대화상자를 보여줍니다";

	switch (iMessage) {
	case WM_CREATE:
		hWndMain=hWnd;
		return 0;
	case WM_LBUTTONDOWN:
		DialogBox(g_hInst,MAKEINTRESOURCE(IDD_DIALOG1),hWnd,KeyDlgProc);
		return 0;
	case WM_PAINT:
		hdc=BeginPaint(hWnd, &ps);
		TextOut(hdc,10,10,Mes,lstrlen(Mes));
		EndPaint(hWnd, &ps);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd,iMessage,wParam,lParam));
}

