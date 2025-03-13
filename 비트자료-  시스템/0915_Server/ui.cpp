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
	//에디트 박스에 새로운 메세지 추가
}

void ui_MsgPrint(HWND hDlg, vector<PACKET> packetList) 
{
	SendMessage(hListPrint, LB_RESETCONTENT, 0, 0);
	//리스트 박스를 지운다

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

		wsprintf(buf, TEXT("[%s] 핸들 : %d"), packet.nickName, (int)packet.hWnd);

		SendMessage(hListPrint, LB_ADDSTRING, 0, (LPARAM)buf);
		//저장된 패킷 속에서 모든 이름과 핸들값들을 리스트 박스에 저장... 중복은 처리 안하나?
	}
}