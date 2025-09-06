//handler.cpp [ manager ]

#include "std.h"

INT_PTR OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	//네트웤 서버 연결
	con_Init(hDlg);

	return TRUE;
}

INT_PTR OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL:			
		//서버 종료
		con_DisConnect(hDlg);
		EndDialog(hDlg, IDCANCEL); return TRUE;

	//ui_message
	case IDC_BTN_MESSAGE:			OnBtnMessage(hDlg);					return TRUE;

	//ui_download
	case IDC_BTN_FILELIST:			OnBtnFileList(hDlg);				return TRUE;
	case IDC_LIST_FILELIST:			OnListNotify(hDlg,wParam,lParam);	return TRUE;
	case IDC_BTN_FILEDOWN:			OnBtnFileDown(hDlg);				return TRUE;

	//ui_upload
	case IDC_BTN_UPLOAD_FILEFIND:	 OnBtnUploadFileFind(hDlg);		return TRUE;	
	case IDC_BTN_DOWN_FILE:			OnBtnFileUpload(hDlg);
	}
	return FALSE;
}

void OnListNotify(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	if (HIWORD(wParam) == LBN_SELCHANGE)  //왜??
	{
		con_List_SelChange(hDlg);		
	}
}

void OnBtnMessage(HWND hDlg)
{
	con_Message_Message(hDlg);
}

void OnBtnFileList(HWND hDlg)
{
	con_DownLoad_FileList(hDlg);
}

void OnBtnFileDown(HWND hDlg)
{
	con_DownLoad_FileDown(hDlg);
}

void OnBtnUploadFileFind(HWND hDlg)
{
	con_Upload_FileFind(hDlg);
}
void OnBtnFileUpload(HWND hDlg) {
	con_Upload_Fileload(hDlg);
}