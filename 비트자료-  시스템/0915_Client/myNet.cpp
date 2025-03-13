#include "std.h"


HWND hServer;

BOOL mynet_Connect(HWND hDlg, const TCHAR* strServer, const TCHAR* nickName) 
{
	hServer = FindWindow(0, strServer);

	if (hServer == NULL) 
	{
		//���� : �Է��� �̸��� �����찡 ����
		return false;
	}

	PACKET packet = packet_CreatePacket(hDlg, nickName, TEXT(""));

	COPYDATASTRUCT cds;

	cds.dwData = 1;
	cds.cbData = sizeof(PACKET);
	cds.lpData = &packet;

	SendMessage(hServer, WM_COPYDATA, 0, (LPARAM)&cds);
	return true;
}
