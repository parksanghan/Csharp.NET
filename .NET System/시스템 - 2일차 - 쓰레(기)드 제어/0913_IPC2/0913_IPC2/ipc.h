//ipc.h
#pragma once

void ipc_Accept(HWND hDlg, HANDLE h);

DWORD WINAPI RecvThread(LPVOID temp);

void ipc_GetControlHandle(HWND hDlg);
void ipc_SetStateButton(BOOL b1);

void ipc_PrintMessage(HWND hDlg, const TCHAR* msg);

void ipc_GetMessage(HWND hDlg, TCHAR* msg, int size);
int ipc_Send(HWND hDlg, const TCHAR* msg, int size);



