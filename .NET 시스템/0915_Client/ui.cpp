//ipc.cpp
#include "std.h"

HWND hEdit_Server, hEdit_Name, hBtn_Connect, hBtn_DisConnect;
HWND hEdit_Print;
HWND hEdit_Send, hBtn_Send;

HANDLE hWrite;

// 초기화
void ui_SetStateButton(BOOL b1, BOOL b2, BOOL b3)
{
	EnableWindow(hBtn_Connect, b1);
	EnableWindow(hBtn_DisConnect, b2);
	EnableWindow(hBtn_Send, b3);
}

void ui_GetControlHandle(HWND hDlg)
{
	//1.핸들획득
	hEdit_Server = GetDlgItem(hDlg, IDC_EDIT_CONNECT);
	hEdit_Name = GetDlgItem(hDlg, IDC_EDIT_NAME);
	hBtn_Connect = GetDlgItem(hDlg, IDC_BTN_CONNECT);
	hBtn_DisConnect = GetDlgItem(hDlg, IDC_BTN_DISCONNECT);
	hEdit_Print = GetDlgItem(hDlg, IDC_EDIT_PRINT);
	hEdit_Send = GetDlgItem(hDlg, IDC_EDIT_SEND);
	hBtn_Send = GetDlgItem(hDlg, IDC_BTN_SEND);
}

void ui_GetHandleNickname(TCHAR* strServer, TCHAR* strNickname) 
{
	GetWindowText(hEdit_Server, strServer, 40);
	GetWindowText(hEdit_Name, strNickname, 40);
}

void ui_PrintMessage(HWND hDlg, const TCHAR* msg)
{
	SYSTEMTIME time;
	GetLocalTime(&time);

	TCHAR buf[100];
	wsprintf(buf, TEXT("%s (%02d:%02d:%02d)"), 
		msg, time.wHour, time.wMinute, time.wSecond);

	SendMessage(hEdit_Print, LB_ADDSTRING, 0, (LPARAM)buf);
	SendMessage(hEdit_Print, LB_ADDSTRING, 0, (LPARAM)"\r\n");
}

void ui_GetMessage(HWND hDlg, TCHAR* msg, int size)
{
	GetWindowText(hEdit_Send, msg, size);
}
