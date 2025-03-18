//ui_print.cpp
#include "std.h"

static HWND g_hDlg;
HWND hList_Print;

void ui_print_Init(HWND hDlg)
{
	ui_print_GetControlHandle(hDlg);
	ui_print_ColumeHeader(hDlg);
}

void ui_print_GetControlHandle(HWND hDlg)
{
	g_hDlg = hDlg;
	hList_Print = GetDlgItem(hDlg, IDC_LIST_PRINT);
}

void ui_print_ColumeHeader(HWND hDlg)
{
	LVCOLUMN COL;

	// 헤더를 추가한다.
	COL.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM;
	COL.fmt = LVCFMT_LEFT;
	COL.cx = 100;
	COL.pszText = (LPTSTR)TEXT("계좌번호");				// 첫 번째 헤더
	COL.iSubItem = 0;
	SendMessage(hList_Print, LVM_INSERTCOLUMN, 0, (LPARAM)&COL);

	COL.cx = 100;
	COL.pszText = (LPTSTR)TEXT("이름");			// 두 번째 헤더
	COL.iSubItem = 1;
	SendMessage(hList_Print, LVM_INSERTCOLUMN, 1, (LPARAM)&COL);

	COL.cx = 130;
	COL.pszText = (LPTSTR)TEXT("잔액");				// 세 번째 헤더
	COL.iSubItem = 2;
	SendMessage(hList_Print, LVM_INSERTCOLUMN, 2, (LPARAM)&COL);


	ListView_SetExtendedListViewStyle(hList_Print,
		LVS_EX_FULLROWSELECT | LVS_EX_GRIDLINES | LVS_EX_HEADERDRAGDROP);

}

void ui_print_PrintAll(pack_ACCOUNT_PRINTALL_ACK* pdata)
{
	ListView_DeleteAllItems(hList_Print);

	for (int i = 0; i < pdata->size; i++)
	{
		Account acc = pdata->arr[i];

		LVITEM LI;

		LI.mask = LVIF_TEXT;
		LI.iSubItem = 0;
		LI.iItem = 0;
		TCHAR buf[20];
		wsprintf(buf, TEXT("%d"), acc.accid);
		LI.pszText = buf;						// 첫 번째 아이템
		SendMessage(hList_Print, LVM_INSERTITEM, i, (LPARAM)&LI);

		LI.iSubItem = 1;
		LI.pszText = acc.name;
		SendMessage(hList_Print, LVM_SETITEM, i, (LPARAM)&LI);

		LI.iSubItem = 2;
		wsprintf(buf, TEXT("%d"), acc.balance);
		LI.pszText = buf;
		SendMessage(hList_Print, LVM_SETITEM, i, (LPARAM)&LI);
	}
}