//control.h
#pragma once

int con_Start();
void con_Stop();

void con_RecvData(SOCKET sock, char* msg);

//�幮 �޽��� ��û
void con_LongMessage(SOCKET sock, pack_LONGMESSAGE* pdata);

//���ε� ���� ����Ʈ ��û
void con_UpLoadFileList(SOCKET sock, pack_FILELIST* pdata);
