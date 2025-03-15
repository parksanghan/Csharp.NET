//Control.h
#pragma once

void con_Init(HWND hDlg);

//ui_header
void con_header_Connect(HWND hDlg);
void con_header_DisConnect(HWND hDlg);

//ui_insert
void con_insert_Insert(HWND hDlg);
void con_insert_Insert_Ack(int result, pack_ACCOUNT_INSERT* pdata);

//ui_delete
void con_delete_Delete(HWND hDlg);
void con_delete_Delete_Ack(int result, pack_ACCOUNT_DELETE* pdata);

//ui_select
void con_select_Select(HWND hDlg);
void con_select_Select_Ack(int result, pack_ACCOUNT_SELECT_ACK* pdata);

//ui_update
void con_update_Update(HWND hDlg);
void con_update_Input_Ack(int result, pack_ACCOUNT_INPUT* pdata);
void con_update_Output_Ack(int result, pack_ACCOUNT_OUTPUT* pdata);

//ui_print
void con_print_Print(HWND hDlg);
void con_account_Printall_Ack(pack_ACCOUNT_PRINTALL_ACK* pdata);

//수신 처리부----------------------------------------------
void con_RecvData(char* msg);

