//ui_download.cpp
#include "std.h"

static HWND g_hDlg;
HWND hList_Print;
HWND hEdit_Down_FileDir, hEdit_Down_FileName, hEdit_Down_FileSize;

void ui_download_Init(HWND hDlg)
{
	g_hDlg = hDlg;
	ui_download_GetControlHandle(hDlg);
}

void ui_download_GetControlHandle(HWND hDlg)
{
	hList_Print			= GetDlgItem(hDlg, IDC_LIST_FILELIST);
	hEdit_Down_FileDir	= GetDlgItem(hDlg, IDC_EDIT_DOWN_FILEDIR);
	hEdit_Down_FileName = GetDlgItem(hDlg, IDC_EDIT_DOWN_FILENAME);
	hEdit_Down_FileSize = GetDlgItem(hDlg, IDC_EDIT_DOWN_FILESIZE);
}

void ui_download_FileList_Print(pack_FILELIST_ACK* pdata)
{
	SendMessage(hList_Print, LB_RESETCONTENT, 0, 0);

	for (int i = 0; i< pdata->size; i++)
	{
		SendMessage(hList_Print, LB_ADDSTRING, 0, (LPARAM)pdata->filelist[i]);
	}
}

void ui_download_List_SelChange(HWND hDlg)
{
	//1.º±≈√«— ¿Œµ¶Ω∫ »πµÊ
	int idx = (int)SendMessage(hList_Print, LB_GETCURSEL, 0, 0);

	//2.«ÿ¥Á ¿Œµ¶Ω∫¿« πÆ¿⁄ø≠ »πµÊ
	TCHAR buf[100] = TEXT("");
	SendMessage(hList_Print, LB_GETTEXT, idx, (LPARAM)buf);

	//staticø° √‚∑¬
	SetWindowText(hEdit_Down_FileName, buf);
}

void ui_download_GetFileName(TCHAR* filename, int size)
{
	GetWindowText(hEdit_Down_FileName, filename, size);
}