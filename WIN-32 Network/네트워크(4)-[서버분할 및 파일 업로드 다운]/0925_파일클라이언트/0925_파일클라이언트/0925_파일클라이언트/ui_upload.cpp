//ui_upload.cpp
#include "std.h"

//ui_upload.cpp
#include "std.h"




HWND g_edit_froot, g_edit_fname;

void ui_upload_init(HWND hDlg)
{
	g_edit_froot = GetDlgItem(hDlg, IDC_EDIT_UP_FILE_FULLNAME);
	g_edit_fname = GetDlgItem(hDlg, IDC_EDIT_UP_FILE_NAME);
}

void ui_upload_getDirectory(HWND hDlg, TCHAR* directory)
{
	TCHAR rootBuf[50], rootName[50];

	GetWindowText(g_edit_froot, rootBuf, 50);
	GetWindowText(g_edit_fname, rootName, 50);

	wsprintf(directory, rootBuf);
}












void ui_uplaod_FileFind(HWND hDlg)
{	
	TCHAR lpstrFile[MAX_PATH] = TEXT("");

	OPENFILENAME OFN;					//#include <commdlg.h>
	memset(&OFN, 0, sizeof(OPENFILENAME));
	OFN.lStructSize = sizeof(OPENFILENAME);
	OFN.hwndOwner = hDlg;
	OFN.lpstrFilter = TEXT("모든 파일(*.*)\0*.*\0");
	OFN.lpstrFile = lpstrFile;
	OFN.nMaxFile = MAX_PATH;
	if (GetOpenFileName(&OFN) != 0) {
		//SetWindowText(hDlg, OFN.lpstrFile);
		TCHAR drive[100], dir[100], filename[100], ext[20];

		_tsplitpath_s(OFN.lpstrFile,
			drive, _countof(drive),
			dir, _countof(dir),
			filename, _countof(filename),
			ext, _countof(ext));

		wsprintf(filename, TEXT("%s%s"), filename, ext);

		SetDlgItemText(hDlg, IDC_EDIT_UP_FILE_FULLNAME, OFN.lpstrFile);
		SetDlgItemText(hDlg, IDC_EDIT_UP_FILE_NAME, filename);
	}
}

//
//
//static HWND g_hDlg;
//HWND hEdit_Select_Num;
//HWND hEdit_Select_Name;
//HWND hEdit_Select_Money;
//
////컨트롤 핸들 획득
//void ui_select_Init(HWND hDlg)
//{
//	g_hDlg = hDlg;
//	ui_select_GetControlHandle(hDlg);
//}
//
//void ui_select_GetControlHandle(HWND hDlg)
//{
//	hEdit_Select_Num	= GetDlgItem(hDlg, IDC_EDIT_SELECT_NUM);
//	hEdit_Select_Name	= GetDlgItem(hDlg, IDC_EDIT_SELECT_NAME);
//	hEdit_Select_Money	= GetDlgItem(hDlg, IDC_EDIT_SELECT_MONEY);
//}
//
//int ui_select_GetSelectData(HWND hDlg, int* num)
//{
//	*num = GetDlgItemInt(hDlg, IDC_EDIT_SELECT_NUM, 0, 0);
//
//	if (*num != 0)
//		return 1;
//	else
//		return 0;
//}
//
//void ui_select_SetClearData(HWND hDlg)
//{
//	SetWindowText(hEdit_Select_Num, TEXT(""));
//	SetWindowText(hEdit_Select_Name, TEXT(""));
//	SetWindowText(hEdit_Select_Money, TEXT(""));
//}
//
//void ui_select_Result(int result, pack_ACCOUNT_SELECT_ACK* pdata)
//{
//	if (result == 1)
//	{
//		SetDlgItemInt(g_hDlg, IDC_EDIT_SELECT_NUM, pdata->accid, 0);
//		SetDlgItemText(g_hDlg, IDC_EDIT_SELECT_NAME, pdata->name);
//		SetDlgItemInt(g_hDlg, IDC_EDIT_SELECT_MONEY, pdata->money,0);
//	}
//	else
//	{
//		MessageBox(0, TEXT("검색 실패"), TEXT("알림"), MB_OK);
//		ui_select_SetClearData(g_hDlg);
//	}
//}