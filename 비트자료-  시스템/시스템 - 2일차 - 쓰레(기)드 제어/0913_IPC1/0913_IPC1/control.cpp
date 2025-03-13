//Control.cpp
#include "std.h"

void con_Init(HWND hDlg)
{
	ipc_GetControlHandle(hDlg);
	ipc_SetStateButton(TRUE, FALSE, FALSE);
}

void con_Connect(HWND hDlg)
{
	if (ipc_Connect(hDlg) == TRUE)
	{
		ipc_SetStateButton(FALSE, TRUE, TRUE);
		ipc_PrintMessage(hDlg, TEXT("���� ����........."));
	}
	else
	{
		MessageBox(hDlg, TEXT("�������"), TEXT("�˸�"), MB_OK);
	}
}

void con_DisConnect(HWND hDlg)
{
	ipc_Disonnect(hDlg);
	ipc_PrintMessage(hDlg, TEXT("���� ����........."));
	ipc_SetStateButton(TRUE, FALSE, FALSE);
}

void con_Send(HWND hDlg)
{
	TCHAR msg[100];
	ipc_GetMessage(hDlg, msg, _countof(msg));
	ipc_Send(hDlg, msg, (int)_countof(msg));
	ipc_PrintMessage(hDlg, msg);
}