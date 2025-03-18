//handler.h
#pragma once

INT_PTR OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
INT_PTR OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);

void OnBtnConnect(HWND hDlg);
void OnBtnDisConnect(HWND hDlg);
void OnBtnInsert(HWND hDlg);
void OnBtnDelete(HWND hDlg);
void OnBtnSelect(HWND hDlg);
void OnBtnUpdate(HWND hDlg);
void OnBtnPrint(HWND hDlg);