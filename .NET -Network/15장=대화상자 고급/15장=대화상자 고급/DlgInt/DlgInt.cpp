#include <windows.h>

LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass=TEXT("DlgInt");

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
enum tag_Method { EDIT, INCDEC };
struct tag_Param {
	int value;
	tag_Method method;
};
BOOL CALLBACK IntDlgProc(HWND hDlg,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	static tag_Param *pParam;
	int temp;

	switch (iMessage) {
	case WM_INITDIALOG:
		pParam=(tag_Param *)lParam;
		SetDlgItemInt(hDlg,IDC_EDINT,pParam->value,TRUE);
		switch (pParam->method) {
		case EDIT:
			ShowWindow(GetDlgItem(hDlg,IDC_BTNINC),SW_HIDE);
			ShowWindow(GetDlgItem(hDlg,IDC_BTNDEC),SW_HIDE);
			break;
		case INCDEC:
			EnableWindow(GetDlgItem(hDlg,IDC_EDINT),FALSE);
			break;
		}
		return TRUE;
	case WM_COMMAND:
		switch (wParam) {
		case IDC_BTNINC:
			temp=GetDlgItemInt(hDlg,IDC_EDINT,NULL,TRUE);
			SetDlgItemInt(hDlg,IDC_EDINT,temp+1,TRUE);
			return TRUE;
		case IDC_BTNDEC:
			temp=GetDlgItemInt(hDlg,IDC_EDINT,NULL,TRUE);
			SetDlgItemInt(hDlg,IDC_EDINT,temp-1,TRUE);
			return TRUE;
		case IDOK:
			pParam->value=GetDlgItemInt(hDlg,IDC_EDINT,NULL,TRUE);
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
	static int value=10;
	TCHAR str[128];
	tag_Param Param;

	switch (iMessage) {
	case WM_CREATE:
		hWndMain=hWnd;
		return 0;
	case WM_LBUTTONDOWN:
	case WM_RBUTTONDOWN:
		Param.value=value;
		if (iMessage == WM_LBUTTONDOWN) {
			Param.method=EDIT;
		} else {
			Param.method=INCDEC;
		}
		if (DialogBoxParam(g_hInst,MAKEINTRESOURCE(IDD_DIALOG1),
			hWnd,IntDlgProc,(LPARAM)&Param)==IDOK) {
			value=Param.value;
			InvalidateRect(hWnd, NULL, TRUE);
		}
		return 0;
	case WM_PAINT:
		hdc=BeginPaint(hWnd, &ps);
		wsprintf(str,TEXT("정수값은 %d입니다."),value);
		TextOut(hdc,10,10,str,lstrlen(str));
		EndPaint(hWnd, &ps);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd,iMessage,wParam,lParam));
}
