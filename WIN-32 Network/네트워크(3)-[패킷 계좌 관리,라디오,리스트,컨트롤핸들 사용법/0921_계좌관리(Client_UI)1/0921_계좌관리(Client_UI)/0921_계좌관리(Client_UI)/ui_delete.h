//ui_delete.h
#pragma once

void ui_delete_Init(HWND hDlg);
void ui_delete_GetControlHandle(HWND hDlg);

int ui_delete_GetDeleteData(HWND hDlg, int* num);
void ui_delete_SetClearData(HWND hDlg);

void ui_delete_Result(int result, pack_ACCOUNT_DELETE* pdata);