//Control.cpp
#include "std.h"

//�ʱ�ȭ
void con_Init(HWND hDlg)
{
	ui_GetControlHandle(hDlg);
	ui_SetStateButton(TRUE, FALSE, FALSE);
}


//OnCommand ó�� ���
void con_Connect(HWND hDlg)//���� �õ�
{
	//1. [ui]���� ������ �ڵ�, �г��� ���� ȹ��
	TCHAR strServer[20];
	TCHAR strNickname[20];
	ui_GetHandleNickname(strServer, strNickname);

	BOOL isConnected = mynet_Connect(hDlg, strServer, strNickname);
		//2. ���� ã��(���� ������ �ڵ��)
		//3. ���� ������ ��Ŷ ����ü ����
		//4. COPYATASTUCT ����
		//5. COPYDATA�� ����
	
	//6. [ui]ȭ�鿡 ���
	if (isConnected == TRUE)
	{
		ui_SetStateButton(FALSE, TRUE, TRUE);
		ui_PrintMessage(hDlg, TEXT("���� ����........."));
	}
	else
	{
		ui_PrintMessage(hDlg, TEXT("���� ����........."));
	}
}

void con_DisConnect(HWND hDlg)//���� ����
{
	ui_PrintMessage(hDlg, TEXT("���� ����........."));
	ui_SetStateButton(TRUE, FALSE, FALSE);
}

void con_Send(HWND hDlg)//�޼��� ����
{
	TCHAR msg[100];
	ui_GetMessage(hDlg, msg, _countof(msg));
	ui_PrintMessage(hDlg, msg);
}