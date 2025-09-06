//mynet.h
#pragma once

BOOL mynet_Connect(HWND hDlg, const TCHAR* s_handle, const TCHAR* nickname);
BOOL mynet_DisConnect(HWND hDlg, const TCHAR* s_handle, const TCHAR* nickname);
BOOL mynet_SendData(HWND hDlg, const TCHAR* s_handle, const TCHAR* nickname,
	const TCHAR* msg);