//ui_insert.cpp
#include "std.h"

static HWND g_hDlg;		
HWND hEdit_Insert_Num;
HWND hEdit_Insert_Name;
HWND hEdit_Insert_Money;

//��Ʈ�� �ڵ� ȹ��
void ui_insert_Init(HWND hDlg)
{
	g_hDlg = hDlg;
	ui_insert_GetControlHandle(hDlg);
}

void ui_insert_GetControlHandle(HWND hDlg)
{
	hEdit_Insert_Num	= GetDlgItem(hDlg, IDC_EDIT_INSERT_NUM);
	hEdit_Insert_Name	= GetDlgItem(hDlg, IDC_EDIT_INSERT_NAME);
	hEdit_Insert_Money = GetDlgItem(hDlg, IDC_EDIT_INSERT_MONEY);
}

int ui_insert_GetInsertData(HWND hDlg, int*num, TCHAR* name, int*money)
{
	*num = GetDlgItemInt(hDlg, IDC_EDIT_INSERT_NUM, 0, 0);
	GetWindowText(hEdit_Insert_Name, name, 40);
	*money = GetDlgItemInt(hDlg, IDC_EDIT_INSERT_MONEY, 0, 0);

	if (*num != 0 && _tcslen(name) != 0 && *money != 0)
		return 1;
	else
		return 0;
}

void ui_insert_SetClearData(HWND hDlg)
{
	//SetDlgItemInt(hDlg, IDC_EDIT_INSERT_NUM, 0, 0);
	SetWindowText(hEdit_Insert_Num, TEXT(""));
	SetWindowText(hEdit_Insert_Name, TEXT(""));
	SetWindowText(hEdit_Insert_Money, TEXT(""));
	//SetDlgItemInt(hDlg, IDC_EDIT_INSERT_MONEY, 0, 0);
}

//��� ó��
void ui_insert_Result(int result, pack_ACCOUNT_INSERT* pdata)
{
	if (result == 1)
	{
		ui_insert_SetClearData(g_hDlg);
	}
	else
	{
		MessageBox(0, TEXT("���� ����"), TEXT("�˸�"), MB_OK);
	}
}

