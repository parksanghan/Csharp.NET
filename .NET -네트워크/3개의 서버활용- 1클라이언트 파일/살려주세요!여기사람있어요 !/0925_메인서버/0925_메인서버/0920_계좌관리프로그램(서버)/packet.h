//packet.h
//App 프로토콜 정의!
#pragma once

//C -> S
#define PACK_LONGMESSAGE		1
#define PACK_FILELIST			2

//S -> C
#define ACK_LONGMESSAGE			20
#define ACK_FILELIST			21


//장문의 메시지
typedef struct tagpack_LONGMESSAGE
{
	int flag;
	TCHAR msg[2000];
}pack_LONGMESSAGE;

//장문의 메시지
typedef struct tagpack_FILELIST
{
	int flag;
}pack_FILELIST;

//파일리스트 응답메시지
typedef struct tagpack_FILELIST_ACK
{
	int flag;
	int size;					//배열에 저장된 파일명 개수
	TCHAR filelist[50][40];		//50개 파일명을 저장, 파일이름 40byte
}pack_FILELIST_ACK;


pack_LONGMESSAGE pack_LongMessage(TCHAR* msg);

pack_FILELIST_ACK pack_FileList(TCHAR(*files)[40], int size);