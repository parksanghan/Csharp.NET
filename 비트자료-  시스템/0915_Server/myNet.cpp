//mynet.cpp
#include "std.h"

vector<PACKET> packetList;

vector<PACKET> mynet_Connect(void* pData)
{
	PACKET* p = (PACKET*)pData;

	//PACKET pk = *p;
	PACKET pk = packet_CreatePacket(p->hWnd, p->nickName, p->msg, p->dt);
	//���� ���� ��Ŷ�� ����

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
	//�׵��� ����� ��� ��Ŷ�� ����� ���� < �̰� �³�?

	return packetList;
}

//������ �޼��� ��ü ���� ( 1��� ���� )
void mynet_RecvData(void* pData)
{
	PACKET* p = (PACKET*)pData;
	PACKET packet = *p;
	//�Էµ� ��Ŷ�� ���������� ����

	COPYDATASTRUCT cds;
	cds.cbData = 3;
	cds.dwData = sizeof(PACKET);
	cds.lpData = &packet;
	//ī�ǵ����Ϳ� �����

	for (int i = 0; i < packetList.size(); i++)
	{
		PACKET pk = packetList[i];
		SendMessage(pk.hWnd, WM_COPYDATA, 0, (LPARAM)&cds);
		//��Ŷ����Ʈ�� ��� �޼����� hwnd�� ���� ����
	}
}
