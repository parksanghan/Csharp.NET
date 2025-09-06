//handler.h
#pragma once

INT_PTR OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
INT_PTR OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);
INT_PTR OnCopyData(HWND hDlg, WPARAM wParam, LPARAM lParam);

INT_PTR OnConnect(HWND hDlg);
INT_PTR OnDisConnect(HWND hDlg);
INT_PTR OnSend(HWND hDlg);
