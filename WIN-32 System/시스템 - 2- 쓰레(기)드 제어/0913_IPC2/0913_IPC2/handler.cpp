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
	//전송버튼 클릭
	case IDC_BTN_SEND:		 return OnSend(hDlg);
	}
	return FALSE;
}

INT_PTR OnAccept(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	con_Accept(hDlg, lParam);

	return TRUE;
}

INT_PTR OnSend(HWND hDlg)
{
	con_Send(hDlg);

	return TRUE;
}