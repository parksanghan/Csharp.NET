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
	case 1:	//연결
	{
		//MyNet 정보 전달 -> 백터에 저장
		vector<PACKET> packetList = mynet_Connect(ps->lpData);

		//Ui정보 전달-> 화면 출력
		ui_MsgPrint(hDlg, packetList);

	}break;
	case 2:	//연결 해제
	{
		//MyNet 정보 전달 -> 백터에 저장
		vector<PACKET> packetList = mynet_Disconnect(ps->lpData);

		//Ui정보 전달-> 화면 출력
		ui_MsgPrint(hDlg, packetList);
	}break;
	case 3:	//메세지 전송
	{
		//MyNet 정보 전달 -> echo [클라이언트 전송]
		mynet_RecvData(ps->lpData);

		//Ui정보 전달-> 화면 출력
		ui_MsgPrint(hDlg, ps->lpData);
	}break;
	default:
		break;
	}
	ui_PrintMessage(hDlg, TEXT("핸들 수신...."));
}
