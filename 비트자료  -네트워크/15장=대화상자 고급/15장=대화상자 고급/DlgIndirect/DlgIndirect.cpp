#include <windows.h>

LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass=TEXT("DlgIndirect");

int APIENTRY WinMain(HINSTANCE hInstance,HINSTANCE hPrevInstance
	  ,LPSTR lpszCmdParam,int nCmdShow)
{
	HWND hWnd;
	MSG Message;
	WNDCLASS WndClass;
	g_hInst=hInstance;

	WndClass.cbClsExtra=0;
	WndClass.cbWndExtra=0;
	WndClass.hbrBackground=(HBRUSH)(COLOR_WINDOW+1);
	WndClass.hCursor=LoadCursor(NULL,IDC_ARROW);
	WndClass.hIcon=LoadIcon(NULL,IDI_APPLICATION);
	WndClass.hInstance=hInstance;
	WndClass.lpfnWndProc=WndProc;
	WndClass.lpszClassName=lpszClass;
	WndClass.lpszMenuName=NULL;
	WndClass.style=CS_HREDRAW | CS_VREDRAW;
	RegisterClass(&WndClass);

	hWnd=CreateWindow(lpszClass,lpszClass,WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT,CW_USEDEFAULT,CW_USEDEFAULT,CW_USEDEFAULT,
		NULL,(HMENU)NULL,hInstance,NULL);
	ShowWindow(hWnd,nCmdShow);

	while (GetMessage(&Message,NULL,0,0)) {
		TranslateMessage(&Message);
		DispatchMessage(&Message);
	}
	return (int)Message.wParam;
}

BOOL CALLBACK DialogProc(HWND hDlg,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	switch (iMessage) {
	case WM_INITDIALOG:
		return TRUE;
	case WM_COMMAND:
		switch (LOWORD(wParam)) {
		case IDOK:
		case IDCANCEL:
			EndDialog(hDlg,LOWORD(wParam));
			return TRUE;
		}
		break;
	}
	return FALSE;
}

LRESULT DisplayMyMessage(HINSTANCE hinst, HWND hwndOwner)
{
    HGLOBAL hgbl;
    LPDLGTEMPLATE pdt;
    LPDLGITEMTEMPLATE pdit;
    LPWORD pw;
    LPWSTR pwsz;
    int nchar;

    hgbl = GlobalAlloc(GMEM_ZEROINIT, 1024);
    pdt = (LPDLGTEMPLATE)GlobalLock(hgbl);
 
	// ��ȭ���� ���ø� ����
    pdt->style = WS_POPUP | WS_BORDER | WS_SYSMENU | WS_CAPTION;
    pdt->cdit = 1;
    pdt->x  = 10;
	pdt->y  = 10;
    pdt->cx = 200;
	pdt->cy = 100;

	// �޴��� ���� Ŭ������ ����Ʈ�� ���Ѵ�.
    pw = (LPWORD) (pdt + 1);
    *pw++ = 0;
    *pw++ = 0;
    pwsz = (LPWSTR) pw;
    nchar=MultiByteToWideChar(CP_ACP,0,"Run Time Dialog",-1,pwsz,50)+1;
    pw+=nchar;

	// Ȯ�� ��ư�� �����Ѵ�.
	pw=(LPWORD)((ULONG)pw+3 & 0xfffffffc);
    pdit = (LPDLGITEMTEMPLATE) pw;
    pdit->x  = 50; 
	pdit->y  = 40;
    pdit->cx = 100; 
	pdit->cy = 20;
    pdit->id = IDOK;
    pdit->style = WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON;

    pw = (LPWORD) (pdit + 1);
    *pw++ = 0xFFFF;
    *pw++ = 0x0080;

    pwsz = (LPWSTR) pw;
    nchar = MultiByteToWideChar (CP_ACP, 0, "Ȯ��", -1, pwsz, 50)+1;
    pw   += nchar;
	pw=(LPWORD)((ULONG)pw+3 & 0xfffffffc);
    *pw++ = 0;

	LRESULT Result;
    GlobalUnlock(hgbl); 
    Result=DialogBoxIndirect(hinst, (LPDLGTEMPLATE) hgbl, 
        hwndOwner, (DLGPROC) DialogProc); 
    GlobalFree(hgbl); 
    return Result; 
}

LRESULT CALLBACK WndProc(HWND hWnd,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	HDC hdc;
	PAINTSTRUCT ps;
	TCHAR *Mes=TEXT("���콺 ���� ��ư�� ������ �����߿� ��ȭ���ڸ� ����� �����ݴϴ�");
	switch (iMessage) {
	case WM_LBUTTONDOWN:
		DisplayMyMessage(g_hInst,hWnd);
		return 0;
	case WM_PAINT:
		hdc=BeginPaint(hWnd, &ps);
		TextOut(hdc,10,10,Mes,lstrlen(Mes));
		EndPaint(hWnd, &ps);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd,iMessage,wParam,lParam));
}

