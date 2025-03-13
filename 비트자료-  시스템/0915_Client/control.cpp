//Control.cpp
#include "std.h"

//초기화
void con_Init(HWND hDlg)
{
	ui_GetControlHandle(hDlg);
	ui_SetStateButton(TRUE, FALSE, FALSE);
}


//OnCommand 처리 기능
void con_Connect(HWND hDlg)//연결 시도
{
	//1. [ui]서버 윈도우 핸들, 닉네임 정보 획득
	TCHAR strServer[20];
	TCHAR strNickname[20];
	ui_GetHandleNickname(strServer, strNickname);

	BOOL isConnected = mynet_Connect(hDlg, strServer, strNickname);
		//2. 서버 찾기(서버 윈도우 핸들로)
		//3. 전송 가능한 패킷 구조체 생성
		//4. COPYATASTUCT 생성
		//5. COPYDATA로 전송
	
	//6. [ui]화면에 출력
	if (isConnected == TRUE)
	{
		ui_SetStateButton(FALSE, TRUE, TRUE);
		ui_PrintMessage(hDlg, TEXT("연결 성공........."));
	}
	else
	{
		ui_PrintMessage(hDlg, TEXT("연결 실패........."));
	}
}

void con_DisConnect(HWND hDlg)//연결 끊기
{
	ui_PrintMessage(hDlg, TEXT("연결 해제........."));
	ui_SetStateButton(TRUE, FALSE, FALSE);
}

void con_Send(HWND hDlg)//메세지 전송
{
	TCHAR msg[100];
	ui_GetMessage(hDlg, msg, _countof(msg));
	ui_PrintMessage(hDlg, msg);
}