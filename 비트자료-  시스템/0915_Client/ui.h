//ipc.h
#pragma once

void ui_SetStateButton(BOOL b1, BOOL b2, BOOL b3);
void ui_GetControlHandle(HWND hDlg);
void ui_GetHandleNickname(TCHAR* strServer, TCHAR* strNickname);
void ui_PrintMessage(HWND hDlg, const TCHAR* msg);
void ui_GetMessage(HWND hDlg, TCHAR* msg, int size);


