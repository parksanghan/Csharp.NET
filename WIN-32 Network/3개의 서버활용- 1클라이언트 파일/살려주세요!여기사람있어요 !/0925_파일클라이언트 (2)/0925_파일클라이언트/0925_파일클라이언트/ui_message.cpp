//ui_update_cpp

#include "std.h"

static HWND g_hDlg;
HWND hEdit_Message;

void ui_message_Init(HWND hDlg)
{
	g_hDlg = hDlg;
	ui_message_GetControlHandle(hDlg);
}

void ui_message_GetControlHandle(HWND hDlg)
{
	hEdit_Message = GetDlgItem(hDlg, IDC_EDIT_MESSAGE);
}

int ui_message_GetData(HWND hDlg, TCHAR* msg, int size)
{
	GetWindowText(hEdit_Message, msg, size);	

	//비어있을 때
	if (_tcslen(msg) == 0)
		return 0;
	else
		return 1;
}

void ui_message_Print(pack_LONGMESSAGE* pdata)
{
	SetWindowText(hEdit_Message, pdata->msg);
}
