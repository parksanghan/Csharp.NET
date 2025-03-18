#include <windows.h>

LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);
HINSTANCE g_hInst;
LPSTR lpszClass="InfoDlg";
struct tag_Str {
	int x;
	int y;
	TCHAR str[128];
};
tag_Str Str[2];

int APIENTRY WinMain(HINSTANCE hInstance,HINSTANCE hPrevInstance
		  ,LPSTR lpszCmdParam,int nCmdShow)
{
	HWND hWnd;
	MSG Message;
	WNDCLASS WndClass;
	g_hInst=hInstance;
	
	WndClass.cbClsExtra=0;
	WndClass.cbWndExtra=0;
	WndClass.hbrBackground=(HBRUSH)GetStockObject(WHITE_BRUSH);
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
	return Message.wParam;
}

#include "resource.h"
BOOL CALLBACK InfoDlgProc(HWND hDlg,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	static tag_Str *Str;
	switch (iMessage) {
	case WM_INITDIALOG:
		Str=(tag_Str *)lParam;
		SetDlgItemText(hDlg,IDC_STR,Str->str);
		SetDlgItemInt(hDlg,IDC_X,Str->x,FALSE);
		SetDlgItemInt(hDlg,IDC_Y,Str->y,FALSE);
		return TRUE;
	case WM_COMMAND:
		switch (wParam) {
		case IDOK:
			GetDlgItemText(hDlg,IDC_STR, Str->str,128);
			Str->x=GetDlgItemInt(hDlg,IDC_X,NULL,FALSE);
			Str->y=GetDlgItemInt(hDlg,IDC_Y,NULL,FALSE);
			EndDialog(hDlg,IDOK);
			return TRUE;
		case IDCANCEL:
			EndDialog(hDlg,IDCANCEL);
			return TRUE;
		}
		break;
	}
	return FALSE;
}

LRESULT CALLBACK WndProc(HWND hWnd,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	HDC hdc;
	PAINTSTRUCT ps;
	int i;

	switch (iMessage) {
	case WM_CREATE:
		Str[0].x=100;
		Str[0].y=100;
		lstrcpy(Str[0].str,TEXT("String"));
		Str[1].x=300;
		Str[1].y=200;
		lstrcpy(Str[1].str,TEXT("¹®ÀÚ¿­"));
		return 0;
	case WM_PAINT:
		hdc=BeginPaint(hWnd, &ps);
		for (i=0;i<sizeof(Str)/sizeof(Str[0]);i++) {
			TextOut(hdc,Str[i].x,Str[i].y,Str[i].str,lstrlen(Str[i].str));
		}
		EndPaint(hWnd, &ps);
		return 0;
	case WM_LBUTTONDOWN:
		if (DialogBoxParam(g_hInst,MAKEINTRESOURCE(IDD_DIALOG1),
			hWnd,InfoDlgProc,(LPARAM)&Str[0])==IDOK) 
			InvalidateRect(hWnd, NULL, TRUE);
		return 0;
	case WM_RBUTTONDOWN:
		if (DialogBoxParam(g_hInst,MAKEINTRESOURCE(IDD_DIALOG1),
			hWnd,InfoDlgProc,(LPARAM)&Str[1])==IDOK) 
			InvalidateRect(hWnd, NULL, TRUE);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd,iMessage,wParam,lParam));
}

