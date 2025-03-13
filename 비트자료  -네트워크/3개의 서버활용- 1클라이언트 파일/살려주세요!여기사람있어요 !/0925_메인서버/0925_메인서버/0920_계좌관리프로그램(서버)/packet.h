//packet.h
//App �������� ����!
#pragma once

//C -> S
#define PACK_LONGMESSAGE		1
#define PACK_FILELIST			2

//S -> C
#define ACK_LONGMESSAGE			20
#define ACK_FILELIST			21


//�幮�� �޽���
typedef struct tagpack_LONGMESSAGE
{
	int flag;
	TCHAR msg[2000];
}pack_LONGMESSAGE;

//�幮�� �޽���
typedef struct tagpack_FILELIST
{
	int flag;
}pack_FILELIST;

//���ϸ���Ʈ ����޽���
typedef struct tagpack_FILELIST_ACK
{
	int flag;
	int size;					//�迭�� ����� ���ϸ� ����
	TCHAR filelist[50][40];		//50�� ���ϸ��� ����, �����̸� 40byte
}pack_FILELIST_ACK;


pack_LONGMESSAGE pack_LongMessage(TCHAR* msg);

pack_FILELIST_ACK pack_FileList(TCHAR(*files)[40], int size);