#include <windows.h>
#include "resource.h"

BOOL CALLBACK MainDlgProc(HWND hDlg,UINT iMessage,WPARAM wParam,LPARAM lParam);
HINSTANCE g_hInst;
HWND hDlgMain;

int APIENTRY WinMain(HINSTANCE hInstance,HINSTANCE hPrevInstance
	,LPSTR lpszCmdParam,int nCmdShow)
{
	g_hInst=hInstance;
	
	DialogBox(g_hInst, MAKEINTRESOURCE(IDD_DIALOG1), HWND_DESKTOP, MainDlgProc);
	
	return 0;
}

BOOL CALLBACK MainDlgProc(HWND hDlg,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	LONG Unit;
	RECT brt;
	TCHAR str[128];
	int BaseX, BaseY;

	switch (iMessage) {
	case WM_INITDIALOG:
		hDlgMain = hDlg;
//* �� ��ư �߾ӿ� ��ư ��ġ�ϱ�. �ý��� ��Ʈ ����
		Unit=GetDialogBaseUnits();
		BaseX=LOWORD(Unit);
		BaseY=HIWORD(Unit);
		CreateWindow(TEXT("button"),TEXT("Third"),WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
			50*BaseX/4,35*BaseY/8,50*BaseX/4,14*BaseY/8,
			hDlg,(HMENU)0,g_hInst,NULL);
//*/

/* �� ��ư �߾ӿ� ��ư ��ġ�ϱ�. ���� ������ ��Ʈ ����
		SetRect(&brt,50,35,50+50,35+14);
		MapDialogRect(hDlg,&brt);
		CreateWindow(TEXT("button"),TEXT("Third"),WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
			brt.left, brt.top, brt.right-brt.left, brt.bottom-brt.top,
			hDlg,(HMENU)0,g_hInst,NULL);
//*/
		return TRUE;
	case WM_COMMAND:
		switch (LOWORD(wParam)) {
		case IDOK:
		case IDCANCEL:
			EndDialog(hDlgMain,IDOK);
			return TRUE;
		case IDC_BUTTON1:
			Unit=GetDialogBaseUnits();
			wsprintf(str,TEXT("���� ����=%d, ���� ����=%d"),LOWORD(Unit),HIWORD(Unit));
			MessageBox(hDlg,str,TEXT("�˸�"),MB_OK);
			return TRUE;
		}
		return FALSE;
	}
	return FALSE;
}