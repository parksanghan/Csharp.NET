//Control.cpp
#include "std.h"

//초기화
void con_Init(HWND hDlg)
{
	ui_header_Init(hDlg);
	ui_insert_Init(hDlg);
	ui_delete_Init(hDlg);
	ui_select_Init(hDlg);
	ui_update_Init(hDlg);
	ui_print_Init(hDlg);
}

//서버 연결
void con_header_Connect(HWND hDlg)
{
	TCHAR ip[50];
	int port;

	ui_header_GetServerInfo(hDlg, ip, &port);
	if (net_Start(ip, port) == 0) //성공
	{
		ui_header_SetStateButton(FALSE, TRUE);
	}
	else
	{
		MessageBox(hDlg, TEXT("서버 연결 실패"), TEXT("오류"), MB_OK);
	}
}

//서버 연결 종료
void con_header_DisConnect(HWND hDlg)
{
	net_Stop();
	ui_header_SetStateButton(TRUE, FALSE);
}

//계좌 개설(insert)
void con_insert_Insert(HWND hDlg)
{
	int num;
	TCHAR name[20];
	int money;

	if (ui_insert_GetInsertData(hDlg, &num, name, &money) == 1)
	{
		pack_ACCOUNT_INSERT pack = pack_Account_Insert(num, name, money);
		net_SendData((char*)&pack, sizeof(pack));
	}
	else
	{
		MessageBox(hDlg, TEXT("입력이 필요함"), TEXT("알림"), MB_OK);
	}
}

void con_insert_Insert_Ack(int result, pack_ACCOUNT_INSERT* pdata)
{
	ui_insert_Result(result, pdata);
}

//계좌 삭제(delete)
void con_delete_Delete(HWND hDlg)
{
	int num;

	if (ui_delete_GetDeleteData(hDlg, &num) == 1)
	{
		pack_ACCOUNT_DELETE pack = pack_Account_Delete(num);
		net_SendData((char*)&pack, sizeof(pack));
	}
	else
	{
		MessageBox(hDlg, TEXT("입력이 필요함"), TEXT("알림"), MB_OK);
	}
}

void con_delete_Delete_Ack(int result, pack_ACCOUNT_DELETE* pdata)
{
	ui_delete_Result(result, pdata);
}

//계좌 검색(select)
void con_select_Select(HWND hDlg)
{
	int num;

	if (ui_select_GetSelectData(hDlg, &num) == 1)
	{
		pack_ACCOUNT_SELECT pack = pack_Account_Select(num);
		net_SendData((char*)&pack, sizeof(pack));
	}
	else
	{
		MessageBox(hDlg, TEXT("입력이 필요함"), TEXT("알림"), MB_OK);
	}
}

void con_select_Select_Ack(int result, pack_ACCOUNT_SELECT_ACK* pdata)
{
	ui_select_Result(result, pdata);
}

//계좌 수정(update)
void con_update_Update(HWND hDlg)
{
	int num;
	bool isIn;	//true:입금 , false:출금
	int money;

	if (ui_update_GetUpdateData(hDlg, &num, &isIn, &money) == 1)
	{
		if (isIn == true)
		{
			pack_ACCOUNT_INPUT pack = pack_Account_Input(num, money);
			net_SendData((char*)&pack, sizeof(pack));
		}
		else
		{
			pack_ACCOUNT_OUTPUT pack = pack_Account_Output(num, money);
			net_SendData((char*)&pack, sizeof(pack));
		}		
	}
	else
	{
		MessageBox(hDlg, TEXT("입력이 필요함"), TEXT("알림"), MB_OK);
	}
}

void con_update_Input_Ack(int result, pack_ACCOUNT_INPUT* pdata)
{
	ui_update_Input_Result(result, pdata);
}

void con_update_Output_Ack(int result, pack_ACCOUNT_OUTPUT* pdata)
{
	ui_update_Output_Result(result, pdata);
}

//계좌 출력
void con_print_Print(HWND hDlg)
{
	pack_ACCOUNT_PRINTALL pack = pack_Account_PrintAll();
	net_SendData((char*)&pack, sizeof(pack));
}

void con_account_Printall_Ack(pack_ACCOUNT_PRINTALL_ACK* pdata)
{
	ui_print_PrintAll(pdata);
}

//데이터 처리 시작점(데이터 판별)
void con_RecvData(char* msg)
{
	int* flag = (int*)msg;

	if (*flag == ACK_ACCOUNT_INSERT_S)
	{
		pack_ACCOUNT_INSERT* pdata = (pack_ACCOUNT_INSERT*)msg;
		con_insert_Insert_Ack(1, pdata);
	}
	else if (*flag == ACK_ACCOUNT_INSERT_F)
	{
		pack_ACCOUNT_INSERT* pdata = (pack_ACCOUNT_INSERT*)msg;
		con_insert_Insert_Ack(0, pdata);
	}
	else if (*flag == ACK_ACCOUNT_DELETE_S)
	{
		pack_ACCOUNT_DELETE* pdata = (pack_ACCOUNT_DELETE*)msg;
		con_delete_Delete_Ack(1, pdata);
	}
	else if (*flag == ACK_ACCOUNT_DELETE_F)
	{
		pack_ACCOUNT_DELETE* pdata = (pack_ACCOUNT_DELETE*)msg;
		con_delete_Delete_Ack(0, pdata);
	}
	else if (*flag == ACK_ACCOUNT_SELECT_S)
	{
		pack_ACCOUNT_SELECT_ACK* pdata = (pack_ACCOUNT_SELECT_ACK*)msg;
		con_select_Select_Ack(1, pdata);
	}
	else if (*flag == ACK_ACCOUNT_SELECT_F)
	{
		pack_ACCOUNT_SELECT_ACK* pdata = (pack_ACCOUNT_SELECT_ACK*)msg;
		con_select_Select_Ack(0, pdata);
	}
	else if (*flag == ACK_ACCOUNT_INPUT_S)
	{
		pack_ACCOUNT_INPUT* pdata = (pack_ACCOUNT_INPUT*)msg;
		con_update_Input_Ack(1, pdata);
	}
	else if (*flag == ACK_ACCOUNT_INPUT_F)
	{
		pack_ACCOUNT_INPUT* pdata = (pack_ACCOUNT_INPUT*)msg;
		con_update_Input_Ack(0, pdata);
	}
	else if (*flag == ACK_ACCOUNT_OUTPUT_S)
	{
		pack_ACCOUNT_OUTPUT* pdata = (pack_ACCOUNT_OUTPUT*)msg;
		con_update_Output_Ack(1, pdata);
	}
	else if (*flag == ACK_ACCOUNT_OUTPUT_F)
	{
		pack_ACCOUNT_OUTPUT* pdata = (pack_ACCOUNT_OUTPUT*)msg;
		con_update_Output_Ack(0, pdata);
	}
	else if (*flag == ACK_ACCOUNT_PRINTALL_S)
	{
		pack_ACCOUNT_PRINTALL_ACK* pdata = (pack_ACCOUNT_PRINTALL_ACK*)msg;
		con_account_Printall_Ack(pdata);
	}
}