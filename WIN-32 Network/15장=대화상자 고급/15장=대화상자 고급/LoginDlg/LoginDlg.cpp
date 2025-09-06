#include <windows.h>
#include "resource.h"

BOOL CALLBACK MainDlgProc(HWND hDlg,UINT iMessage,WPARAM wParam,LPARAM lParam);
HINSTANCE g_hInst;
HWND hDlgMain;
TCHAR ID[128], Pass[128];

int APIENTRY WinMain(HINSTANCE hInstance,HINSTANCE hPrevInstance
	,LPSTR lpszCmdParam,int nCmdShow)
{
	g_hInst=hInstance;
	
	DialogBox(g_hInst, MAKEINTRESOURCE(IDD_DIALOG1), HWND_DESKTOP, MainDlgProc);
	// todo : ID, Pass 비교 후 인증
	
	return 0;
}

void DrawBitmap(HDC hdc,int x,int y,HBITMAP hBit)
{
	HDC MemDC;
	HBITMAP OldBitmap;
	int bx,by;
	BITMAP bit;

	MemDC=CreateCompatibleDC(hdc);
	OldBitmap=(HBITMAP)SelectObject(MemDC, hBit);

	GetObject(hBit,sizeof(BITMAP),&bit);
	bx=bit.bmWidth;
	by=bit.bmHeight;

	BitBlt(hdc,x,y,bx,by,MemDC,0,0,SRCCOPY);

	SelectObject(MemDC,OldBitmap);
	DeleteDC(MemDC);
}

BOOL CALLBACK MainDlgProc(HWND hDlg,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	HDC hdc;
	PAINTSTRUCT ps;
	static HBITMAP hBit;
	RECT crt;

	switch (iMessage) {
	case WM_INITDIALOG:
		hDlgMain = hDlg;
		hBit=LoadBitmap(g_hInst, MAKEINTRESOURCE(IDB_BITMAP1));
/*		
		SetRect(&crt,0,0,320,200);
		AdjustWindowRect(&crt,WS_POPUPWINDOW | WS_DLGFRAME,FALSE);
		SetWindowPos(hDlg,NULL,0,0,crt.right-crt.left,crt.bottom-crt.top,
			SWP_NOMOVE | SWP_NOZORDER);
		SetWindowPos(GetDlgItem(hDlg,IDC_EDID),NULL,120,125,120,25,SWP_NOZORDER);
		SetWindowPos(GetDlgItem(hDlg,IDC_EDPASS),NULL,120,155,120,25,SWP_NOZORDER);
		SetWindowPos(GetDlgItem(hDlg,IDOK),NULL,250,155,40,25,SWP_NOZORDER);
//*/
		return TRUE;
	case WM_COMMAND:
		switch (LOWORD(wParam)) {
		case IDOK:
		case IDCANCEL:
			// todo : 입력된 ID와 Pass 조사
			DeleteObject(hBit);
			EndDialog(hDlg,0);
			return TRUE;
		}
		return FALSE;
	case WM_PAINT:
		hdc=BeginPaint(hDlg, &ps);
		DrawBitmap(hdc,0,0,hBit);
		EndPaint(hDlg, &ps);
		return TRUE;
	}
	return FALSE;
}
