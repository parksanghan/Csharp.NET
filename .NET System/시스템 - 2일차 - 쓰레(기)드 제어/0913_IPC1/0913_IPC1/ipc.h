//ipc.h
#pragma once

void ipc_SetStateButton(BOOL b1, BOOL b2, BOOL b3);
void ipc_PrintMessage(HWND hDlg, const TCHAR* msg);

void ipc_GetControlHandle(HWND hDlg);
BOOL ipc_Connect(HWND hDlg);

void ipc_Disonnect(HWND hDlg);

void ipc_GetMessage(HWND hDlg, TCHAR* msg, int size);
int ipc_Send(HWND hDlg, const TCHAR* msg, int size);

void PrintMessage(HWND hDlg);
void ipc_GetMessage(HWND hDlg);




