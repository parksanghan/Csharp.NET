//handler.h
#pragma once

INT_PTR OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
INT_PTR OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);

void OnBtnMessage(HWND hDlg);
void OnBtnFileList(HWND hDlg);
void OnBtnFileDown(HWND hDlg);

void OnBtnUploadFileFind(HWND hDlg);

void OnListNotify(HWND hDlg, WPARAM wParam, LPARAM lParam);
void OnBtnFileUpload(HWND hDlg);