//Control.cpp
#include "std.h"

void con_Init(HWND hDlg)
{
	ui_GetControlHandle(hDlg);
	ui_SetStateButton(TRUE, FALSE, FALSE);
}

void con_Connect(HWND hDlg)
{
	//1. [ui]�����������ڵ�, �г��� ���� ȹ��
	TCHAR s_handle[20];
	TCHAR nickname[20];

	ui_GetHandleNickName(hDlg, s_handle, nickname);
	
	//2. ���� ã��(�����������ڵ�)
	//3. ��Ŷ����ü ����
	//4. COPYDATASTRUCT ����ü ����
	//5. SendMessage
	BOOL result = mynet_Connect(hDlg, s_handle, nickname);
	
	//6. [ui]ȭ�鿡 ���
	if (result == TRUE)
	{
		ui_SetStateButton(FALSE, TRUE, TRUE);
		ui_PrintMessage(hDlg, TEXT("���� ����........."));
	}
	else
	{
		ui_PrintMessage(hDlg, TEXT("���� ����........."));
	}
}

void con_DisConnect(HWND hDlg)
{
	//1. [ui]�����������ڵ�, �г��� ���� ȹ��
	TCHAR s_handle[20];
	TCHAR nickname[20];

	ui_GetHandleNickName(hDlg, s_handle, nickname);

	//2. ���� ã��(�����������ڵ�)
	//3. ��Ŷ����ü ����
	//4. COPYDATASTRUCT ����ü ����
	//5. SendMessage
	BOOL result = mynet_DisConnect(hDlg, s_handle, nickname);

	//6. [ui]ȭ�鿡 ���
	if (result == TRUE)
	{
		ui_SetStateButton(TRUE, FALSE, FALSE);
		ui_PrintMessage(hDlg, TEXT("���� ����........."));
	}
	else
	{
		ui_PrintMessage(hDlg, TEXT("���� ����........."));
	}
}

void con_Send(HWND hDlg)
{
	//1. [ui]�����������ڵ�, �г��� ���� *�޽���* ȹ��
	TCHAR s_handle[20];
	TCHAR nickname[20];
	TCHAR msg[50];

	ui_GetHandleNickName(hDlg, s_handle, nickname);
	ui_GetMessage(hDlg, msg, _countof(msg));

	//2. ���� ã��(�����������ڵ�)
	//3. ��Ŷ����ü ����
	//4. COPYDATASTRUCT ����ü ����
	//5. SendMessage
	BOOL result = mynet_SendData(hDlg, s_handle, nickname, msg);

	ui_SetSendMessageInit(hDlg);
}

void con_CopyData(HWND hDlg, COPYDATASTRUCT* p)
{	
	if (p->dwData == 3) //�޽�������
	{
		//ui ���� ����->ȭ�����
		ui_MsgPrint(hDlg, p->lpData);
	}
}