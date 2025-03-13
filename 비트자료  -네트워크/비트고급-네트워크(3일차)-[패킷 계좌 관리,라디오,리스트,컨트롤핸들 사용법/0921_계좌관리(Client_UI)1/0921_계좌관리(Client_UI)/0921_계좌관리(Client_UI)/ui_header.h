//ui.header.h
#pragma once

void ui_header_Init(HWND hDlg);

void ui_header_GetControlHandle(HWND hDlg);
void ui_header_SetStateButton(BOOL b1, BOOL b2);

void ui_header_GetServerInfo(HWND hDlg, TCHAR* ip, int *port);