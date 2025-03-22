//ui_download.h
#pragma once

void ui_download_Init(HWND hDlg);
void ui_download_GetControlHandle(HWND hDlg);

//------ 상단부
void ui_download_FileList_Print(pack_FILELIST_ACK * pdata);
void ui_download_List_SelChange(HWND hDlg);

//-------하단부
void ui_download_GetFileName(TCHAR* filename, int size);
