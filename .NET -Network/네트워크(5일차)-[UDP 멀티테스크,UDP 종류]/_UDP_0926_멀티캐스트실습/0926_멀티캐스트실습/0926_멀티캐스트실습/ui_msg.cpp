//ui_msg.cpp
#include "std.h"

static HWND g_hDlg;
HWND hEdit_Message;

void ui_msg_Init(HWND hDlg)
{
	ui_msg_GetHandle(hDlg);
}

void ui_msg_GetHandle(HWND hDlg)
{
	g_hDlg = hDlg;
	hEdit_Message = GetDlgItem(hDlg, IDC_EDIT_MESSAGE);
}

void ui_msg_GetData(HWND hDlg, TCHAR* msg, int size)
{
	GetWindowText(hEdit_Message, msg, size);
}