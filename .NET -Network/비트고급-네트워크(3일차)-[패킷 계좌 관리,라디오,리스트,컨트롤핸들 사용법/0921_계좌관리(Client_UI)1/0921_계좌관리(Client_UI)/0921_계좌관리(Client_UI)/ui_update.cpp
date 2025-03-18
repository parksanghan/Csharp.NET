//ui_update_cpp

#include "std.h"

static HWND g_hDlg;
HWND hEdit_Update_Num;
HWND hRadio_Update_Input, hRadio_Update_Output;
HWND hEdit_Update_Money;

void ui_update_Init(HWND hDlg)
{
	g_hDlg = hDlg;
	ui_update_GetControlHandle(hDlg);
}

void ui_update_GetControlHandle(HWND hDlg)
{
	hEdit_Update_Num		= GetDlgItem(hDlg, IDC_EDIT_UPDATE_NUM);
	hRadio_Update_Input		= GetDlgItem(hDlg, IDC_RADIO_UPDATE_IN);
	hRadio_Update_Output	= GetDlgItem(hDlg, IDC_RADIO_UPDATE_OUT);
	hEdit_Update_Money		= GetDlgItem(hDlg, IDC_EDIT_UPDATE_MONEY);
}

int ui_update_GetUpdateData(HWND hDlg, int* num, bool* isIn, int* money)
{
	*num = GetDlgItemInt(hDlg, IDC_EDIT_UPDATE_NUM, 0, 0);

	//라디오버튼 채크 여부 확인
	if (IsDlgButtonChecked(hDlg, IDC_RADIO_UPDATE_IN) == TRUE)
		*isIn = true;
	else
		*isIn = false;

	*money = GetDlgItemInt(hDlg, IDC_EDIT_UPDATE_MONEY, 0, 0);

	if (*num != 0 && *money != 0)
		return 1;
	else
		return 0;
}

void ui_update_SetClearData(HWND hDlg)
{
	SetWindowText(hEdit_Update_Num, TEXT(""));
	SetWindowText(hEdit_Update_Money, TEXT(""));

	CheckRadioButton(hDlg, IDC_RADIO_UPDATE_IN, IDC_RADIO_UPDATE_OUT, -1);
}

void ui_update_Input_Result(int result, pack_ACCOUNT_INPUT* pdata)
{
	if (result == 1)
	{
		//ui_update_SetClearData(g_hDlg);
	}
	else
	{
		MessageBox(0, TEXT("입금 실패"), TEXT("알림"), MB_OK);
	}
}

void ui_update_Output_Result(int result, pack_ACCOUNT_OUTPUT* pdata)
{
	if (result == 1)
	{
		//ui_update_SetClearData(g_hDlg);
	}
	else
	{
		MessageBox(0, TEXT("출금 실패"), TEXT("알림"), MB_OK);
	}
}