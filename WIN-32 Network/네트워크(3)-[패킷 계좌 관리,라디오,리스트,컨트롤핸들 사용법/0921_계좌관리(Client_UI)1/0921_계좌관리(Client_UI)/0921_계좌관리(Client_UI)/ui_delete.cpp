//ui_delete.cpp
#include "std.h"

static HWND g_hDlg;
HWND hEdit_Delete_Num;

//컨트롤 핸들 획득
void ui_delete_Init(HWND hDlg)
{
	g_hDlg = hDlg;
	ui_delete_GetControlHandle(hDlg);
}

void ui_delete_GetControlHandle(HWND hDlg)
{
	hEdit_Delete_Num = GetDlgItem(hDlg, IDC_EDIT_DELETE_NUM);
}

int ui_delete_GetDeleteData(HWND hDlg, int* num)
{
	*num = GetDlgItemInt(hDlg, IDC_EDIT_DELETE_NUM, 0, 0);

	if (*num != 0)
		return 1;
	else
		return 0;
}

void ui_delete_SetClearData(HWND hDlg)
{
	SetWindowText(hEdit_Delete_Num, TEXT(""));
}

void ui_delete_Result(int result, pack_ACCOUNT_DELETE* pdata)
{
	if (result == 1)
	{
		ui_delete_SetClearData(g_hDlg);
		MessageBox(0, TEXT("삭제 성공"), TEXT("알림"), MB_OK);
	}
	else
	{
		MessageBox(0, TEXT("삭제 실패"), TEXT("알림"), MB_OK);
		ui_delete_SetClearData(g_hDlg);
	}
}