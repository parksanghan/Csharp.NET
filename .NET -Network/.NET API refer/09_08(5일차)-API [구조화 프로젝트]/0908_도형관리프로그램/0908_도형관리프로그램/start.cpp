//start.cpp
#include "std.h"

INT_PTR CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_INITDIALOG:		return OnInitDialog(hDlg, wParam, lParam);
	case WM_COMMAND:		return OnCommand(hDlg, wParam, lParam);
	}
	return FALSE;	//메시지를 처리하지 않았다.-> 이 다음에 대화상자 처리하는 default프로시저
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	INT_PTR ret = DialogBox(hInst,// instance
		MAKEINTRESOURCE(IDD_DIALOG_MAIN), // 다이얼로그 선택
		0, // 부모 윈도우
		DlgProc); // Proc..

	return 0;
}