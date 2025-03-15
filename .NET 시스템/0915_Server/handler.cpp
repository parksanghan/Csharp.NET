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
	}
	return FALSE;
}

INT_PTR OnCopyData(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	COPYDATASTRUCT* ps = (COPYDATASTRUCT*)lParam;
	con_CopyData(hDlg, ps);
	return TRUE;
}