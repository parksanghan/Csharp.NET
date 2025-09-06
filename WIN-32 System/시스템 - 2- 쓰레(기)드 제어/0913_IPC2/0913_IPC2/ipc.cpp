//ipc.cpp
#include "std.h"

HWND hList_Print;
HWND hEdit_Send, hBtn_Send;

HANDLE hRead;

void ipc_Accept(HWND hDlg, HANDLE h)
{
	hRead = h;

	//****
	HANDLE hThread = CreateThread(0, 0, RecvThread, hDlg, 0, 0);
	CloseHandle(hThread);
}

DWORD WINAPI RecvThread(LPVOID temp)
{
	HWND hDlg = (HWND)temp;
	
	while (true)
	{
		TCHAR buf[100] = { 0 };
		DWORD len;
		BOOL b = ReadFile(hRead, buf, sizeof(buf), &len, 0);
		if (b == FALSE)
			break;

		ipc_PrintMessage(hDlg, buf);
	}
	CloseHandle(hRead);
	return 0;
}

/*
모든 컨트롤의 핸들 획득
*/
void ipc_GetControlHandle(HWND hDlg)
{
	//1.핸들획득
	hList_Print = GetDlgItem(hDlg, IDC_LIST_PRINT);
	hEdit_Send = GetDlgItem(hDlg, IDC_EDIT_SEND);
	hBtn_Send = GetDlgItem(hDlg, IDC_BTN_SEND);
}

/*
버튼 컨트롤 활성화여부
*/
void ipc_SetStateButton(BOOL b1)
{
	EnableWindow(hBtn_Send, b1);
}

void ipc_PrintMessage(HWND hDlg, const TCHAR* msg)
{
	SYSTEMTIME time;
	GetLocalTime(&time);

	TCHAR buf[100];
	wsprintf(buf, TEXT("%s (%02d:%02d:%02d)"), 
		msg, time.wHour, time.wMinute, time.wSecond);

	SendMessage(hList_Print, LB_ADDSTRING, 0, (LPARAM)buf);
}

void ipc_GetMessage(HWND hDlg, TCHAR* msg, int size)
{
	GetWindowText(hEdit_Send, msg, size);
}

int ipc_Send(HWND hDlg, const TCHAR* msg, int size)
{
	//전송코드[시작주소, byte크기]
	//DWORD len;
	//WriteFile(hWrite, msg, size, &len, 0);
	//return len;
	return -1;
}
