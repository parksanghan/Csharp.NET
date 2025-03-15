//ui_group.h
#pragma once

void ui_group_Init(HWND hDlg);
void ui_group_GetHandle(HWND hDlg);
void ui_group_CB_Init(HWND hDlg);

void ui_group_GetIp(HWND hDlg, char* ip);
void ui_group_GetAllIp(HWND hDlg, char (*ip)[50]);
