//ui_print.h
#pragma once

void ui_print_Init(HWND hDlg);
void ui_print_GetControlHandle(HWND hDlg);
void ui_print_ColumeHeader(HWND hDlg);

void ui_print_PrintAll(pack_ACCOUNT_PRINTALL_ACK* pdata);