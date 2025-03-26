//Control.cpp
#include "std.h"

void con_Init(HWND hDlg)
{
	ipc_GetControlHandle(hDlg);
	ipc_SetStateButton(FALSE);
}

void con_Send(HWND hDlg)
{
	TCHAR msg[100];
	ipc_GetMessage(hDlg, msg, _countof(msg));
	ipc_Send(hDlg, msg, (int)_tcslen(msg));
	ipc_PrintMessage(hDlg, msg);
}

void con_Accept(HWND hDlg, LPARAM lParam)
{
	ipc_Accept(hDlg, (HANDLE)lParam);
	ipc_SetStateButton(TRUE);
	ipc_PrintMessage(hDlg, TEXT("핸들 수신...."));
}