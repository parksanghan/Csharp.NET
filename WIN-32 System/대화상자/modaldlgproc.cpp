//modaldlgproc.cpp

#include <Windows.h>
#include <tchar.h>
#include "resource.h"
#include "LineData.h"

static LINEDATA* pData = NULL;
static HWND hCombobox;

BOOL modal_OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	pData = (LINEDATA*)lParam;

	//콤보박스는 미리 0, 1, 2, 3, 4
	hCombobox = GetDlgItem(hDlg, IDC_COMBO1);
	TCHAR temp[10];
	for (int i = 0; i <= 4; i++)
	{
		wsprintf(temp, TEXT("- %d -"), i);
		SendMessage(hCombobox, CB_ADDSTRING, 0, (LPARAM)temp);
	}

	//컨트롤 초기화
	SetDlgItemInt(hDlg, IDC_EDIT1, pData->xline, 0);
	SetDlgItemInt(hDlg, IDC_EDIT2, pData->yline, 0);
	SendMessage(hCombobox, CB_SETCURSEL, pData->penColor, 0);

	return TRUE;
}

BOOL modal_OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL: EndDialog(hDlg, IDCANCEL); return TRUE;
	case IDOK:		
	{
		//자신의 컨틀롤 정보 획득--> pData를 이용해 원본 데이터 변경!
		pData->xline = GetDlgItemInt(hDlg, IDC_EDIT1, 0, 0);
		pData->yline = GetDlgItemInt(hDlg, IDC_EDIT2, 0, 0);
		pData->penColor = SendMessage(hCombobox, CB_GETCURSEL, 0, 0);

		EndDialog(hDlg, IDOK);
		return TRUE;
	}
	}
	return FALSE;
}

BOOL CALLBACK ModalDlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_INITDIALOG:		return modal_OnInitDialog(hDlg, wParam, lParam);
	case WM_COMMAND:		return modal_OnCommand(hDlg, wParam, lParam);
	}
	return FALSE;	
}