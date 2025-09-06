//Control.h
#pragma once

void con_Init(HWND hDlg);

//연결 & 종료
void con_Connect(HWND hDlg);
void con_DisConnect(HWND hDlg);

//ui_message
void con_Message_Message(HWND hDlg);
void con_Message_Ack(pack_LONGMESSAGE* pdata);

//ui_download
void con_DownLoad_FileList(HWND hDlg);
void con_DownLoad_FileList_Ack(pack_FILELIST_ACK *pdata);

void con_List_SelChange(HWND hDlg);
void con_DownLoad_FileDown(HWND hDlg);


//ui_uplaod
void con_Upload_FileFind(HWND hDlg);
void con_Upload_Fileload(HWND hDlg);

//수신 처리부----------------------------------------------
void con_RecvData(char* msg);

