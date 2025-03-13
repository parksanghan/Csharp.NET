//Control.cpp
#include "std.h"

void con_Init(HWND hDlg)
{
	//소켓 초기화 처리
	net_send_CreateSocket();
	net_recv_CreateSocket();
	net_recv_Run();

	//UI 초기화 처리
	ui_group_Init(hDlg);
	ui_print_Init(hDlg);
	ui_msg_Init(hDlg);	
}

void con_Exit(HWND hDlg)
{
	//소켓 종료처리
	net_send_CloseSocket();
	net_recv_CloseSocket();
}

//가입
//콤보박스에서 선택한 IP획득
//수신용 소켓에 IP가입처리!
void con_Group_Join(HWND hDlg)
{
	char ip[50];
	ui_group_GetIp(hDlg, ip);

	if (net_recv_GroupJoin(ip) == 1)
		MessageBox(hDlg, TEXT("가입성공"), TEXT("알림"), MB_OK);
	else
		MessageBox(hDlg, TEXT("가입실패"), TEXT("알림"), MB_OK);
}

void con_Group_Drop(HWND hDlg)
{
	char ip[50];
	ui_group_GetIp(hDlg, ip);

	if (net_recv_GroupDrop(ip) == 1)
		MessageBox(hDlg, TEXT("탈퇴성공"), TEXT("알림"), MB_OK);
	else
		MessageBox(hDlg, TEXT("탈퇴실패"), TEXT("알림"), MB_OK);
}

void con_Group_Send(HWND hDlg)
{
	TCHAR msg[100];
	ui_msg_GetData(hDlg, msg, _countof(msg));

	char ip[50];
	ui_group_GetIp(hDlg, ip);
	net_send_SendData(ip, (char*)msg, _countof(msg));
}

void con_Broad_Send(HWND hDlg)
{
	TCHAR msg[100];
	ui_msg_GetData(hDlg, msg, _countof(msg));

	char ip[8][50];
	ui_group_GetAllIp(hDlg, ip);

	for (int i = 0; i < 8; i++)
	{
		net_send_SendData(ip[i], (char*)msg, _countof(msg));
	}
}

void con_RecvData(TCHAR* buf)
{
	ui_print_Print(buf);
}

void con_Group_SelChange(HWND hDlg)
{

}
