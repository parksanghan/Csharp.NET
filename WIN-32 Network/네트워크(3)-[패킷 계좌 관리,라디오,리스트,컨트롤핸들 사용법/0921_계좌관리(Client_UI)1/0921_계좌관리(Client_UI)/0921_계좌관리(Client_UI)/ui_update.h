//ui_update_h
#pragma once

void ui_update_Init(HWND hDlg);
void ui_update_GetControlHandle(HWND hDlg);

int ui_update_GetUpdateData(HWND hDlg, int* num, bool*isIn, int*money);
void ui_update_SetClearData(HWND hDlg);

void ui_update_Input_Result(int result, pack_ACCOUNT_INPUT* pdata);
void ui_update_Output_Result(int result, pack_ACCOUNT_OUTPUT* pdata);