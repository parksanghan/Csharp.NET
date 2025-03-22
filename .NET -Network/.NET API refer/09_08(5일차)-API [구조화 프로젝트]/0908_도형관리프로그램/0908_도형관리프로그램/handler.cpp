//handler.cpp [ manager ]

#include "std.h"

SHAPE g_setShape;

INT_PTR OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	ShapeSet_GetControlHandle(hDlg);
	ShapeSet_Init(&g_setShape);
	ShapeSet_Print(hDlg, g_setShape);

	ShapeCur_GetControlHandle(hDlg);
	ShapeCur_Init();
	ShapeCur_Print(hDlg);

	return TRUE;
}

INT_PTR OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL:			 EndDialog(hDlg, IDCANCEL); return TRUE;
	case IDC_SET_COMBO_TYPE:
	{
		if (HIWORD(wParam) == CBN_SELCHANGE)
			return OnComboTypeSelChange(hDlg, wParam, lParam);
	}
	case IDC_SET_COMBO_SIZE:
	{
		if (HIWORD(wParam) == CBN_SELCHANGE)
			return OnComboSizeSelChange(hDlg, wParam, lParam);
	}
	case IDC_SET_EDIT_R:	
	{
		if (HIWORD(wParam) == EN_CHANGE)
			return OnEditColorChange(hDlg, wParam, lParam);
	}
	case IDC_SET_EDIT_G:
	{
		if (HIWORD(wParam) == EN_CHANGE)
			return OnEditColorChange(hDlg, wParam, lParam);
	}
	case IDC_SET_EDIT_B:
	{
		if (HIWORD(wParam) == EN_CHANGE)
			return OnEditColorChange(hDlg, wParam, lParam);
	}

	}

	return FALSE;
}

INT_PTR OnComboTypeSelChange(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	ShapeSet_UpdateType(&g_setShape);
	return TRUE;
}

INT_PTR OnComboSizeSelChange(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	ShapeSet_UpdateSize(&g_setShape);
	return TRUE;
}

INT_PTR OnEditColorChange(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	ShapeSet_UpdateColor(hDlg, &g_setShape);
	return TRUE;
}