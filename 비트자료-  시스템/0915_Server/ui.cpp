#include "std.h"

HWND hEditPrint, hListPrint;

void ui_PrintMessage(HWND hDlg, const TCHAR* msg)
{
	SYSTEMTIME time;
	GetLocalTime(&time);

	TCHAR buf[100];
	wsprintf(buf, TEXT("%s (%02d:%02d:%02d)"),
		msg, time.wHour, time.wMinute, time.wSecond);

	SendMessage(hListPrint, LB_ADDSTRING, 0, (LPARAM)buf);
}

void ui_GetControlHandle(HWND hDlg)
{
	hEditPrint = GetDlgItem(hDlg, IDC_EDIT_PRINT);
	hListPrint = GetDlgItem(hDlg, IDC_LIST_PRINT);
}

void ui_MsgPrint(HWND hDlg, void* p) 
{
	PACKET* packet = (PACKET*)p;

	TCHAR buf[100];
	wsprintf(buf, TEXT("[%s] (%02d:%02d:%02d) %s"), packet->nickName, packet->dt.wHour, packet->dt.wMinute, packet->dt.wSecond, packet->msg);

	SendMessage(hEditPrint, EM_REPLACESEL, 0, (LPARAM)buf);
	SendMessage(hEditPrint, EM_REPLACESEL, 0, (LPARAM)"\r\n");
	//����Ʈ �ڽ��� ���ο� �޼��� �߰�
}

void ui_MsgPrint(HWND hDlg, vector<PACKET> packetList) 
{
	SendMessage(hListPrint, LB_RESETCONTENT, 0, 0);
	//����Ʈ �ڽ��� �����

	for (int i = 0; i < packetList.size(); i++) 
	{
		PACKET packet = packetList[i];

		TCHAR buf[50];

		for (int t = 0; t < i; t++)
		{
			PACKET pastPacket = packetList[t];
			if (packet.hWnd != pastPacket.hWnd) 
			{
				continue;
			}
		}

		wsprintf(buf, TEXT("[%s] �ڵ� : %d"), packet.nickName, (int)packet.hWnd);

		SendMessage(hListPrint, LB_ADDSTRING, 0, (LPARAM)buf);
		//����� ��Ŷ �ӿ��� ��� �̸��� �ڵ鰪���� ����Ʈ �ڽ��� ����... �ߺ��� ó�� ���ϳ�?
	}
}