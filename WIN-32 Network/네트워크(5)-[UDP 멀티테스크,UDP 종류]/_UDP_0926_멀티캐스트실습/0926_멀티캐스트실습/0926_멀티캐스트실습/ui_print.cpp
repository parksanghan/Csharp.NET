//ui_update_cpp

#include "std.h"

static HWND g_hDlg;
HWND hEdit_Print;

void ui_print_Init(HWND hDlg)
{
	ui_print_GetHandle(hDlg);
}

void ui_print_GetHandle(HWND hDlg)
{
	g_hDlg = hDlg;
	hEdit_Print = GetDlgItem(hDlg, IDC_EDIT_PRINT);
}

void ui_print_Print(TCHAR* buf)
{
	SendMessage(hEdit_Print, EM_REPLACESEL, 0, (LPARAM)buf);
	SendMessage(hEdit_Print, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
}