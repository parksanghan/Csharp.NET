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
	//ui_header
	case IDC_BTN_CONNECT:	 OnBtnConnect(hDlg);		return TRUE;
	case IDC_BTN_DISCONNECT: OnBtnDisConnect(hDlg);		return TRUE;
	//ui_insert
	case IDC_BTN_INSERT:	OnBtnInsert(hDlg);			return TRUE;
	//ui_delete
	case IDC_BTN_DELETE:	OnBtnDelete(hDlg);			return TRUE;
	//ui_select
	case IDC_BTN_SELECT:	OnBtnSelect(hDlg);			return TRUE;
	//ui_update
	case IDC_BTN_UPDATE:	OnBtnUpdate(hDlg);			return TRUE;
	//ui_print
	case IDC_BTN_PRINT:		OnBtnPrint(hDlg);			return TRUE;
	}
	return FALSE;
}

void OnBtnConnect(HWND hDlg)
{
	con_header_Connect(hDlg);
}

void OnBtnDisConnect(HWND hDlg)
{
	con_header_DisConnect(hDlg);
}

void OnBtnInsert(HWND hDlg)
{
	con_insert_Insert(hDlg);
}

void OnBtnDelete(HWND hDlg)
{
	con_delete_Delete(hDlg);
}

void OnBtnSelect(HWND hDlg)
{
	con_select_Select(hDlg);
}

void OnBtnUpdate(HWND hDlg)
{
	con_update_Update(hDlg);
}

void OnBtnPrint(HWND hDlg)
{
	con_print_Print(hDlg);
}