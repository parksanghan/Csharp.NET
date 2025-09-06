//Control.cpp
#include "std.h"

#define SERVER_IP	"127.0.0.1"
#define SERVER_PORT	9000

//초기화
void con_Init(HWND hDlg)
{
	con_Connect(hDlg);
	ui_download_Init(hDlg);
	ui_message_Init(hDlg);
	ui_upload_init(hDlg);
}

//서버 연결
void con_Connect(HWND hDlg)
{
	if (net_Start(SERVER_IP, SERVER_PORT) == 0) //성공
	{

	}
	else
	{
		MessageBox(hDlg, TEXT("서버 연결 실패"), TEXT("오류"), MB_OK);
		EndDialog(hDlg, 0);
	}
}

//서버 연결 종료
void con_DisConnect(HWND hDlg)
{
	net_Stop();
}

//데이터 처리 시작점(데이터 판별)
void con_RecvData(char* msg)
{
	int* flag = (int*)msg;

	if (*flag == ACK_LONGMESSAGE)
	{
		pack_LONGMESSAGE* pdata = (pack_LONGMESSAGE*)msg;
		con_Message_Ack(pdata);
	}
	else if (*flag == ACK_FILELIST)
	{
		pack_FILELIST_ACK* pdata = (pack_FILELIST_ACK*)msg;
		con_DownLoad_FileList_Ack(pdata);
	}
}

//long메시지 전송
void con_Message_Message(HWND hDlg)
{
	TCHAR msg[2000];

	if (ui_message_GetData(hDlg, msg, _countof(msg)) == 1)
	{
		pack_LONGMESSAGE pack = pack_LongMessage(msg);
		net_SendData((char*)&pack, sizeof(pack));
	}
	else
	{
		MessageBox(hDlg, TEXT("입력이 필요함"), TEXT("알림"), MB_OK);
	}

}

void con_Message_Ack(pack_LONGMESSAGE* pdata)
{
	ui_message_Print(pdata);
}

//file리스트 요청
void con_DownLoad_FileList(HWND hDlg)
{
	pack_FILELIST pack = pack_DownLoad_FileList();
	net_SendData((char*)&pack, sizeof(pack));
}

void con_DownLoad_FileList_Ack(pack_FILELIST_ACK* pdata)
{
	ui_download_FileList_Print(pdata);
}

void con_List_SelChange(HWND hDlg)
{
	ui_download_List_SelChange(hDlg);
}

void con_DownLoad_FileDown(HWND hDlg)
{
	TCHAR filename[20];
	ui_download_GetFileName(filename, _countof(filename));
	net_download_Start(hDlg, filename, _countof(filename));
}



//다이얼로그 실행 -> 업로드 할 파일정보 획득
void con_Upload_FileFind(HWND hDlg)
{
	ui_uplaod_FileFind(hDlg);
}

void con_Upload_Fileload(HWND hDlg) {
	TCHAR fileDir[100];
	ui_upload_getDirectory(hDlg, fileDir);
	net_upload_start(hDlg, fileDir);
	 
}