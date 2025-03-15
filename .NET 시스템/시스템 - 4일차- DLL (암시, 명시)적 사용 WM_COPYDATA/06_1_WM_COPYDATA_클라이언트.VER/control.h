//Control.h
#pragma once

void con_Init(HWND hDlg);

void con_Connect(HWND hDlg);
void con_DisConnect(HWND hDlg);
void con_Send(HWND hDlg);

void con_CopyData(HWND hDlg, COPYDATASTRUCT* p);