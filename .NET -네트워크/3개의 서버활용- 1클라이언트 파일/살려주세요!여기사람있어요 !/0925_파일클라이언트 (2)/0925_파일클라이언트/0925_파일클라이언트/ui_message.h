//ui_update_h
#pragma once

void ui_message_Init(HWND hDlg);
void ui_message_GetControlHandle(HWND hDlg);

int ui_message_GetData(HWND hDlg, TCHAR* msg, int size);

void ui_message_Print(pack_LONGMESSAGE* pdata);

