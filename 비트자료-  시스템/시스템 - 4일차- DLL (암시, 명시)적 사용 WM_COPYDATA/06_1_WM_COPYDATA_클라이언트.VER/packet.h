//packet.h
#pragma once

//���� �ְ�޴� ������(packet) ����
typedef struct tagPACKET
{
	HWND	hwnd;			//Ŭ���̾�Ʈ �ڵ�
	TCHAR	nickname[20];	//��ȭ��
	TCHAR   msg[100];		//�޽���
	SYSTEMTIME dt;			//�ð�
}PACKET;

PACKET packet_CreatePacket(HWND h, const TCHAR* nn, const TCHAR* m);
PACKET packet_CreatePacket(HWND h, const TCHAR* nn, const TCHAR* m, SYSTEMTIME t);
