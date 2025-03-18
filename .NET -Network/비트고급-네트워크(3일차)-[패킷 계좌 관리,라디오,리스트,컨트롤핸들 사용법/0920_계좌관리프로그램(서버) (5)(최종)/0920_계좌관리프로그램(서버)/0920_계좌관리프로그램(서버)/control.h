//control.h
#pragma once

int con_Start();
void con_Stop();

void con_RecvData(SOCKET sock, char* msg);

void con_account_insert(SOCKET sock, pack_ACCOUNT_INSERT* pdata);
void con_account_delete(SOCKET sock, pack_ACCOUNT_DELETE* pdata);
void con_account_select(SOCKET sock, pack_ACCOUNT_SELECT* pdata);
void con_account_input(SOCKET sock, pack_ACCOUNT_INPUT* pdata);
void con_account_output(SOCKET sock, pack_ACCOUNT_OUTPUT* pdata);
void con_account_printall(SOCKET sock, pack_ACCOUNT_PRINTALL* pdata);

int accid_to_idx(int accid);

void account_print();