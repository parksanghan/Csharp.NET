//ui.insert.h
#pragma once

void ui_insert_Init(HWND hDlg);
void ui_insert_Result(int result, pack_ACCOUNT_INSERT* pdata);

void ui_insert_GetControlHandle(HWND hDlg);
int ui_insert_GetInsertData(HWND hDlg, int* num, TCHAR* name, int* money);
void ui_insert_SetClearData(HWND hDlg);


