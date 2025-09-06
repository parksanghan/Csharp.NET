//ui.h
#pragma once

void ui_GetControlHandle(HWND hDlg);
void ui_SetStateButton(BOOL b1, BOOL b2, BOOL b3);

void ui_GetHandleNickName(HWND hDlg, TCHAR* s_handle, TCHAR* nickname);

void ui_PrintMessage(HWND hDlg, const TCHAR* msg);

void ui_GetMessage(HWND hDlg, TCHAR* msg, int size);

void ui_SetSendMessageInit(HWND hDlg);

void ui_MsgPrint(HWND hDlg, void* p);

