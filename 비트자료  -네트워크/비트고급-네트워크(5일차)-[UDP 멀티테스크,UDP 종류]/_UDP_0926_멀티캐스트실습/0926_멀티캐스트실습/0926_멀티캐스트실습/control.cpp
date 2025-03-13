//Control.cpp
#include "std.h"

void con_Init(HWND hDlg)
{
	//���� �ʱ�ȭ ó��
	net_send_CreateSocket();
	net_recv_CreateSocket();
	net_recv_Run();

	//UI �ʱ�ȭ ó��
	ui_group_Init(hDlg);
	ui_print_Init(hDlg);
	ui_msg_Init(hDlg);	
}

void con_Exit(HWND hDlg)
{
	//���� ����ó��
	net_send_CloseSocket();
	net_recv_CloseSocket();
}

//����
//�޺��ڽ����� ������ IPȹ��
//���ſ� ���Ͽ� IP����ó��!
void con_Group_Join(HWND hDlg)
{
	char ip[50];
	ui_group_GetIp(hDlg, ip);

	if (net_recv_GroupJoin(ip) == 1)
		MessageBox(hDlg, TEXT("���Լ���"), TEXT("�˸�"), MB_OK);
	else
		MessageBox(hDlg, TEXT("���Խ���"), TEXT("�˸�"), MB_OK);
}

void con_Group_Drop(HWND hDlg)
{
	char ip[50];
	ui_group_GetIp(hDlg, ip);

	if (net_recv_GroupDrop(ip) == 1)
		MessageBox(hDlg, TEXT("Ż�𼺰�"), TEXT("�˸�"), MB_OK);
	else
		MessageBox(hDlg, TEXT("Ż�����"), TEXT("�˸�"), MB_OK);
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
