#pragma once
#include "std.h"






void ui_PrintMessage(HWND hDlg, const TCHAR* msg);





void ui_GetControlHandle(HWND hDlg);
void ui_MsgPrint(HWND hDlg, void* p);
void ui_MsgPrint(HWND hDlg, vector<PACKET> packetList);


