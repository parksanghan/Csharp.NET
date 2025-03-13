//Control.cpp
#include "std.h"

void con_Init(HWND hDlg)
{
	ui_GetControlHandle(hDlg);
}

void con_CopyData(HWND hDlg, COPYDATASTRUCT* ps)
{
	switch (ps->dwData)
	{
	case 1:	//����
	{
		//MyNet ���� ���� -> ���Ϳ� ����
		vector<PACKET> packetList = mynet_Connect(ps->lpData);

		//Ui���� ����-> ȭ�� ���
		ui_MsgPrint(hDlg, packetList);

	}break;
	case 2:	//���� ����
	{
		//MyNet ���� ���� -> ���Ϳ� ����
		vector<PACKET> packetList = mynet_Disconnect(ps->lpData);

		//Ui���� ����-> ȭ�� ���
		ui_MsgPrint(hDlg, packetList);
	}break;
	case 3:	//�޼��� ����
	{
		//MyNet ���� ���� -> echo [Ŭ���̾�Ʈ ����]
		mynet_RecvData(ps->lpData);

		//Ui���� ����-> ȭ�� ���
		ui_MsgPrint(hDlg, ps->lpData);
	}break;
	default:
		break;
	}
	ui_PrintMessage(hDlg, TEXT("�ڵ� ����...."));
}
