#include <windows.h>
#include "resource.h"

BOOL CALLBACK MainDlgProc(HWND hDlg,UINT iMessage,WPARAM wParam,LPARAM lParam);
HWND hDlgMain;

int APIENTRY WinMain(HINSTANCE hInstance,HINSTANCE hPrevInstance
	,LPSTR lpszCmdParam,int nCmdShow)
{
	DialogBox(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), HWND_DESKTOP, MainDlgProc);
	
	return 0;
}

BOOL CALLBACK MainDlgProc(HWND hDlg,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	int nHit;

	switch (iMessage) {
	case WM_INITDIALOG:
		hDlgMain = hDlg;
		return TRUE;
	case WM_NCHITTEST:
		nHit=DefWindowProc(hDlg,iMessage,wParam,lParam);
		if (nHit==HTCLIENT) {
			nHit=HTCAPTION;
		}
		// return nHit;			// 이렇게 리턴해서는 안됨
		SetWindowLongPtr(hDlg,DWLP_MSGRESULT,nHit);
		return TRUE;			// return FALSE해도 안됨
//	case WM_LBUTTONDOWN:
//		return DefWindowProc(hDlg, WM_NCLBUTTONDOWN, HTCAPTION, lParam);
	case WM_COMMAND:
		switch (LOWORD(wParam)) {
		case IDOK:
			EndDialog(hDlg,IDOK);
			return TRUE;
		case IDCANCEL:
			EndDialog(hDlg,IDCANCEL);
			return TRUE;
		}
		return FALSE;
	}
	return FALSE;
}
