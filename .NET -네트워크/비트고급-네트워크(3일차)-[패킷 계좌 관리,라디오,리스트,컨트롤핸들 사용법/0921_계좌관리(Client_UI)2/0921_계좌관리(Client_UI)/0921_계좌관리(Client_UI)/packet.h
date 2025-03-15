//packet.h
//App 프로토콜 정의!
#pragma once

//C -> S
#define PACK_ACCOUNT_INSERT		1
#define PACK_ACCOUNT_DELETE		2
#define PACK_ACCOUNT_SELECT		3
#define PACK_ACCOUNT_INPUT		4
#define PACK_ACCOUNT_OUTPUT		5
#define PACK_ACCOUNT_PRINTALL	6

//S -> C
#define ACK_ACCOUNT_INSERT_S	20
#define ACK_ACCOUNT_INSERT_F	21
#define ACK_ACCOUNT_DELETE_S	22
#define ACK_ACCOUNT_DELETE_F	23
#define ACK_ACCOUNT_SELECT_S	24
#define ACK_ACCOUNT_SELECT_F	25
#define ACK_ACCOUNT_INPUT_S		26
#define ACK_ACCOUNT_INPUT_F		27
#define ACK_ACCOUNT_OUTPUT_S	28
#define ACK_ACCOUNT_OUTPUT_F	29
#define ACK_ACCOUNT_PRINTALL_S	30
#define ACK_ACCOUNT_PRINTALL_F	31

//계좌 개설
typedef struct tagpack_ACCOUNT_INSERT
{
	int flag;
	int accid;
	char name[20];
	int  money;
}pack_ACCOUNT_INSERT;

//계좌 삭제
typedef struct tagpack_ACCOUNT_DELETE
{
	int flag;
	int accid;
}pack_ACCOUNT_DELETE;

//계좌 검색
typedef pack_ACCOUNT_DELETE pack_ACCOUNT_SELECT;

//계좌 입금
typedef struct tagpack_ACCOUNT_INPUT
{
	int flag;
	int accid;
	int money;
}pack_ACCOUNT_INPUT;

//계좌 출금
typedef pack_ACCOUNT_INPUT pack_ACCOUNT_OUTPUT;

//계좌 전체 출력
typedef struct tagpack_ACCOUNT_PRINTALL
{
	int flag;
}pack_ACCOUNT_PRINTALL;

pack_ACCOUNT_INSERT pack_Account_Insert(int accid, const char* name, int money);
pack_ACCOUNT_DELETE pack_Account_Delete(int accid);
pack_ACCOUNT_SELECT pack_Account_Select(int accid);
pack_ACCOUNT_INPUT pack_Account_Input(int accid, int money);
pack_ACCOUNT_OUTPUT pack_Account_Output(int accid, int money);
pack_ACCOUNT_PRINTALL pack_Account_PrintAll();
