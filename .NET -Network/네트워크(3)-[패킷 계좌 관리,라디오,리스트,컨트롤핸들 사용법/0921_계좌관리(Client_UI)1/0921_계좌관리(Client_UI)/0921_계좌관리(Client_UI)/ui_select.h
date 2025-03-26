//ui_select.h
#pragma once

void ui_select_Init(HWND hDlg);
void ui_select_GetControlHandle(HWND hDlg);

int ui_select_GetSelectData(HWND hDlg, int* num);
void ui_select_SetClearData(HWND hDlg);

void ui_select_Result(int result, pack_ACCOUNT_SELECT_ACK* pdata);