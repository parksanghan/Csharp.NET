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
	case IDCANCEL:	EndDialog(hDlg, IDCANCEL); return TRUE;
	//버튼 이벤트
	case IDC_BTN_GROUP_JOIN: con_Group_Join(hDlg);					return TRUE;
	case IDC_BTN_GROUP_DROP: con_Group_Drop(hDlg);					return TRUE;
	case IDC_BTN_GROUP_SEND: con_Group_Send(hDlg);					return TRUE;
	case IDC_BTN_BROAD_SEND: con_Broad_Send(hDlg);					return TRUE;
	//콤보 박스 이벤트
	case IDC_CB_GROUP:		OnComBo_Notify(hDlg, wParam, lParam);	return TRUE;
	}
	return FALSE;
}

void OnComBo_Notify(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	if (HIWORD(wParam) == CBN_SELCHANGE)  
	{
		con_Group_SelChange(hDlg);		
	}
}
