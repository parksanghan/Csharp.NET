//Control.h
#pragma once

void con_Init(HWND hDlg);
void con_Exit(HWND hDlg);

void con_Group_Join(HWND hDlg);
void con_Group_Drop(HWND hDlg);
void con_Group_Send(HWND hDlg);
void con_Broad_Send(HWND hDlg);
void con_Group_SelChange(HWND hDlg);

void con_RecvData(TCHAR* buf);
