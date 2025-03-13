//control.h
#pragma once

int con_Start();
void con_Stop();

void con_RecvData(SOCKET sock, char* msg);

//장문 메시지 요청
void con_LongMessage(SOCKET sock, pack_LONGMESSAGE* pdata);

//업로드 파일 리스트 요청
void con_UpLoadFileList(SOCKET sock, pack_FILELIST* pdata);
