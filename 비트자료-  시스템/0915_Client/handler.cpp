//handler.cpp [ manager ]

#include "std.h"

INT_PTR OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	con_Init(hDlg);

	return TRUE;
}

INT_PTR OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL:			 EndDialog(hDlg, IDCANCEL); return TRUE;	
	//�����ư Ŭ�� 
	case IDC_BTN_CONNECT:		return OnConnect(hDlg);
	//������ư Ŭ��
	case IDC_BTN_DISCONNECT:	return OnDisConnect(hDlg);
	//���۹�ư Ŭ��
	case IDC_BTN_SEND:			return OnSend(hDlg);
	}

	return FALSE;
}


//OnCommand ���� �Լ���
INT_PTR OnConnect(HWND hDlg)
{
	con_Connect(hDlg);

	return TRUE;
}

INT_PTR OnDisConnect(HWND hDlg)
{
	con_DisConnect(hDlg);

	return TRUE;
}

INT_PTR OnSend(HWND hDlg)
{
	con_Send(hDlg);

	return TRUE;
}