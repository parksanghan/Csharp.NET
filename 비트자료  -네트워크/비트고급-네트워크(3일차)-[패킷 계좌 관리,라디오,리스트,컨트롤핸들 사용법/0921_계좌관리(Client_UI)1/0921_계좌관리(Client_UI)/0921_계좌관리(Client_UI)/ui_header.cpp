//ui_header.cpp
#include "std.h"

HWND hIPAddress_IP;
HWND hEdit_Port;
HWND hBtn_Start, hBtn_Stop;

//*****************************************************************
//[Control] �ʱ�ȭ �ܰ迡�� ȣ��
// - ��Ʈ�� �ڵ� ȹ��
// - ��ư�� Ȱ��ȭ ���� ����
// - �����Ʈ�ѿ� �ʱ�ȭ �� ����
//*****************************************************************
void ui_header_Init(HWND hDlg)
{
	ui_header_GetControlHandle(hDlg);
	ui_header_SetStateButton(TRUE, FALSE);

	SetWindowText(hIPAddress_IP, TEXT("220.90.179.75"));
	SetDlgItemInt(hDlg, IDC_EDIT_SERVER_PORT, 9000,0);
}

void ui_header_GetControlHandle(HWND hDlg)
{
	hIPAddress_IP	= GetDlgItem(hDlg, IDC_IPADDRESS_SERVER_IP);
	hEdit_Port		= GetDlgItem(hDlg, IDC_EDIT_SERVER_PORT);
	hBtn_Start		= GetDlgItem(hDlg, IDC_BTN_CONNECT);
	hBtn_Stop		= GetDlgItem(hDlg, IDC_BTN_DISCONNECT);
}

void ui_header_SetStateButton(BOOL b1, BOOL b2)
{
	EnableWindow(hBtn_Start, b1);
	EnableWindow(hBtn_Stop, b2);
}

void ui_header_GetServerInfo(HWND hDlg, TCHAR* ip, int* port)
{
	GetWindowText(hIPAddress_IP, ip, 40);
	*port = GetDlgItemInt(hDlg, IDC_EDIT_SERVER_PORT, 0, 0);
}