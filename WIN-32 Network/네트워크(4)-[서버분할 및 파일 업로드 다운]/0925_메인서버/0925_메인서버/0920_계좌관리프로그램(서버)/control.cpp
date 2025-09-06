//control.cpp
#include "std.h"

#define SERVER_PORT     9000

int con_Start()
{
    if (net_init(SERVER_PORT) == -1)
    {
        printf("초기화 오류\n");
        return 0;
    }

    printf("서버 구동...........\n");
    net_run();

    return 1;
}

void con_Stop()
{
    net_exit();
}

//데이터 처리 시작점(데이터 판별)
void con_RecvData(SOCKET sock, char* msg)
{
    int* flag = (int*)msg; 

    if (*flag == PACK_LONGMESSAGE)
    {
		pack_LONGMESSAGE* pdata = (pack_LONGMESSAGE*)msg;
		con_LongMessage(sock, pdata);     
    }
    else if (*flag == PACK_FILELIST)
    {
        pack_FILELIST* pdata = (pack_FILELIST*)msg;
        con_UpLoadFileList(sock, pdata);
    }
}

//LONG 메세지 처리 함수
void con_LongMessage(SOCKET sock, pack_LONGMESSAGE* pdata)
{    
    printf("LONG메시지 수신.............\n");
    pack_LONGMESSAGE pack = pack_LongMessage(pdata->msg);

    net_SendAllData(sock, (char*)&pack, sizeof(pack));
}

//UpLoad 파일리스트 요청
void con_UpLoadFileList(SOCKET sock, pack_FILELIST* pdata)
{
    TCHAR files[50][40] = { 0 };
    int  count;

    count = wbfile_GetFileList(TEXT("C:\\wb38\\*.*"), files);

    pack_FILELIST_ACK pack = pack_FileList(files, count);  
    net_SendData(sock, (char*)&pack, sizeof(pack));
}