//ipc.cpp
#include "std.h"

HWND hEdit_Connect, hBtn_Connect, hBtn_DisConnect;
HWND hList_Print;
HWND hEdit_Send, hBtn_Send;

HANDLE hWrite;

/*
버튼 컨트롤 활성화여부
*/
void ipc_SetStateButton(BOOL b1, BOOL b2, BOOL b3)
{
	EnableWindow(hBtn_Connect, b1);
	EnableWindow(hBtn_DisConnect, b2);
	EnableWindow(hBtn_Send, b3);
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

/*
모든 컨트롤의 핸들 획득
*/
void ipc_GetControlHandle(HWND hDlg)
{
	//1.핸들획득
	hEdit_Connect	= GetDlgItem(hDlg, IDC_EDIT_CONNECT);
	hBtn_Connect	= GetDlgItem(hDlg, IDC_BTN_CONNECT);
	hBtn_DisConnect = GetDlgItem(hDlg, IDC_BTN_DISCONNECT);
	hList_Print		= GetDlgItem(hDlg, IDC_LIST_PRINT);
	hEdit_Send		= GetDlgItem(hDlg, IDC_EDIT_SEND);
	hBtn_Send		= GetDlgItem(hDlg, IDC_BTN_SEND);
}

/*
1. 파이프 생성
2. 연결할 상대방 H획득
3. Read핸들을 상대방에 HT 복사
4. Read핸들을 상대방에 전송
5. 버튼 상태 변경
*/
BOOL ipc_Connect(HWND hDlg)
{
	//1. 파이프 생성
	HANDLE hRead;
	CreatePipe(&hRead, &hWrite, 0, 4096);

	//2. FindWindow
	//1. 윈도우 핸들 획득
	TCHAR buf[50];
	GetWindowText(hEdit_Connect, buf, _countof(buf));
	HWND hwnd = FindWindow(0, buf);
	if (hwnd == NULL)
		return FALSE;

	//3. 상대방의 핸들테이블에 hRead 복사
	DWORD pid;
	DWORD tid = GetWindowThreadProcessId(hwnd, &pid);
	HANDLE hProcess = OpenProcess(PROCESS_ALL_ACCESS, 0, pid);
	HANDLE h;
	DuplicateHandle(GetCurrentProcess(), hRead, // source 
		hProcess, &h, // target 
		0, 0, DUPLICATE_SAME_ACCESS);	

	//4. hRead핸들을 상대방에 전송
	SendMessage(hwnd, WM_USER + 100, 0, (LPARAM)h);
	CloseHandle(hRead);

	return TRUE;
}

void ipc_Disonnect(HWND hDlg)
{
	CloseHandle(hWrite);
}


void ipc_GetMessage(HWND hDlg, TCHAR* msg, int size)
{
	GetWindowText(hEdit_Send, msg, size);
}

int ipc_Send(HWND hDlg, const TCHAR* msg, int size)
{
	//전송코드[시작주소, byte크기]
	DWORD len;
	WriteFile(hWrite, msg, size, &len, 0);
	return len;
}

void PrintMessage(HWND hDlg)
{

}

void ipc_GetMessage(HWND hDlg)
{

}

