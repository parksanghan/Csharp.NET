//Control.cpp
#include "std.h"

void con_Init(HWND hDlg)
{
	ui_GetControlHandle(hDlg);
	ui_SetStateButton(TRUE, FALSE, FALSE);
}

void con_Connect(HWND hDlg)
{
	//1. [ui]서버윈도우핸들, 닉네임 정보 획득
	TCHAR s_handle[20];
	TCHAR nickname[20];

	ui_GetHandleNickName(hDlg, s_handle, nickname);
	
	//2. 서버 찾기(서버윈도우핸들)
	//3. 패킷구조체 생성
	//4. COPYDATASTRUCT 구조체 생성
	//5. SendMessage
	BOOL result = mynet_Connect(hDlg, s_handle, nickname);
	
	//6. [ui]화면에 출력
	if (result == TRUE)
	{
		ui_SetStateButton(FALSE, TRUE, TRUE);
		ui_PrintMessage(hDlg, TEXT("연결 성공........."));
	}
	else
	{
		ui_PrintMessage(hDlg, TEXT("연결 실패........."));
	}
}

void con_DisConnect(HWND hDlg)
{
	//1. [ui]서버윈도우핸들, 닉네임 정보 획득
	TCHAR s_handle[20];
	TCHAR nickname[20];

	ui_GetHandleNickName(hDlg, s_handle, nickname);

	//2. 서버 찾기(서버윈도우핸들)
	//3. 패킷구조체 생성
	//4. COPYDATASTRUCT 구조체 생성
	//5. SendMessage
	BOOL result = mynet_DisConnect(hDlg, s_handle, nickname);

	//6. [ui]화면에 출력
	if (result == TRUE)
	{
		ui_SetStateButton(TRUE, FALSE, FALSE);
		ui_PrintMessage(hDlg, TEXT("해제 성공........."));
	}
	else
	{
		ui_PrintMessage(hDlg, TEXT("해제 실패........."));
	}
}

void con_Send(HWND hDlg)
{
	//1. [ui]서버윈도우핸들, 닉네임 정보 *메시지* 획득
	TCHAR s_handle[20];
	TCHAR nickname[20];
	TCHAR msg[50];

	ui_GetHandleNickName(hDlg, s_handle, nickname);
	ui_GetMessage(hDlg, msg, _countof(msg));

	//2. 서버 찾기(서버윈도우핸들)
	//3. 패킷구조체 생성
	//4. COPYDATASTRUCT 구조체 생성
	//5. SendMessage
	BOOL result = mynet_SendData(hDlg, s_handle, nickname, msg);

	ui_SetSendMessageInit(hDlg);
}

void con_CopyData(HWND hDlg, COPYDATASTRUCT* p)
{	
	if (p->dwData == 3) //메시지전송
	{
		//ui 정보 전달->화면출력
		ui_MsgPrint(hDlg, p->lpData);
	}
}