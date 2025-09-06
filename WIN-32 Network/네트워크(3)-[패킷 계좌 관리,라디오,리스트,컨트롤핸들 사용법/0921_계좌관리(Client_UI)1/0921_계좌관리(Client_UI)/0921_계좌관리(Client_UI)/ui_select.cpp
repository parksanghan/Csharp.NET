//ui_select.cpp
#include "std.h"


static HWND g_hDlg;
HWND hEdit_Select_Num;
HWND hEdit_Select_Name;
HWND hEdit_Select_Money;

//컨트롤 핸들 획득
void ui_select_Init(HWND hDlg)
{
	g_hDlg = hDlg;
	ui_select_GetControlHandle(hDlg);
}

void ui_select_GetControlHandle(HWND hDlg)
{
	hEdit_Select_Num	= GetDlgItem(hDlg, IDC_EDIT_SELECT_NUM);
	hEdit_Select_Name	= GetDlgItem(hDlg, IDC_EDIT_SELECT_NAME);
	hEdit_Select_Money	= GetDlgItem(hDlg, IDC_EDIT_SELECT_MONEY);
}

int ui_select_GetSelectData(HWND hDlg, int* num)
{
	*num = GetDlgItemInt(hDlg, IDC_EDIT_SELECT_NUM, 0, 0);

	if (*num != 0)
		return 1;
	else
		return 0;
}

void ui_select_SetClearData(HWND hDlg)
{
	SetWindowText(hEdit_Select_Num, TEXT(""));
	SetWindowText(hEdit_Select_Name, TEXT(""));
	SetWindowText(hEdit_Select_Money, TEXT(""));
}

void ui_select_Result(int result, pack_ACCOUNT_SELECT_ACK* pdata)
{
	if (result == 1)
	{
		SetDlgItemInt(g_hDlg, IDC_EDIT_SELECT_NUM, pdata->accid, 0);
		SetDlgItemText(g_hDlg, IDC_EDIT_SELECT_NAME, pdata->name);
		SetDlgItemInt(g_hDlg, IDC_EDIT_SELECT_MONEY, pdata->money,0);
	}
	else
	{
		MessageBox(0, TEXT("검색 실패"), TEXT("알림"), MB_OK);
		ui_select_SetClearData(g_hDlg);
	}
}