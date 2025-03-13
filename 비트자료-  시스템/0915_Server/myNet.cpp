//mynet.cpp
#include "std.h"

vector<PACKET> packetList;

vector<PACKET> mynet_Connect(void* pData)
{
	PACKET* p = (PACKET*)pData;

	//PACKET pk = *p;
	PACKET pk = packet_CreatePacket(p->hWnd, p->nickName, p->msg, p->dt);
	//입장 정보 패킷을 저장

	packetList.push_back(pk);
	return packetList;
}

vector<PACKET> mynet_Disconnect(void* pData)
{
	PACKET* p = (PACKET*)pData;

	for (int i = 0; i < packetList.size(); i++) 
	{
		if (packetList[i].hWnd == p->hWnd)
		{
			packetList.erase(packetList.begin() + i);
			break;
		}
	}
	//그동안 저장된 모든 패킷을 지우고 떠남 < 이게 맞나?

	return packetList;
}

//수신한 메세지 전체 전송 ( 1대다 전송 )
void mynet_RecvData(void* pData)
{
	PACKET* p = (PACKET*)pData;
	PACKET packet = *p;
	//입력된 패킷을 지역변수로 복사

	COPYDATASTRUCT cds;
	cds.cbData = 3;
	cds.dwData = sizeof(PACKET);
	cds.lpData = &packet;
	//카피데이터에 담아줌

	for (int i = 0; i < packetList.size(); i++)
	{
		PACKET pk = packetList[i];
		SendMessage(pk.hWnd, WM_COPYDATA, 0, (LPARAM)&cds);
		//패킷리스트의 모든 메세지의 hwnd를 향해 전송
	}
}
