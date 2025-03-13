#include <windows.h>

LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass=TEXT("MultiSel");

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

LRESULT CALLBACK WndProc(HWND hWnd,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	HDC hdc;
	PAINTSTRUCT ps;
	TCHAR *Mes=TEXT("마우스 왼쪽 버튼을 누르십시요");
	OPENFILENAME OFN;
	TCHAR str[32000]=TEXT("");
	TCHAR lpstrFile[10000]=TEXT("");
	TCHAR *p;
	TCHAR Name[MAX_PATH];
	TCHAR szTmp[MAX_PATH];
	int i=1;

	switch (iMessage) {
	case WM_LBUTTONDOWN:
		memset(&OFN, 0, sizeof(OPENFILENAME));
		OFN.lStructSize = sizeof(OPENFILENAME);
		OFN.hwndOwner=hWnd;
		OFN.lpstrFilter=TEXT("모든 파일(*.*)\0*.*\0");
		OFN.lpstrFile=lpstrFile;
		OFN.nMaxFile=10000;
		OFN.Flags=OFN_EXPLORER | OFN_ALLOWMULTISELECT;
		if (GetOpenFileName(&OFN)!=0) {
			p=lpstrFile;
			lstrcpy(Name, p);
			if (*(p+lstrlen(Name)+1)==0) {
				wsprintf(str,TEXT("%s 파일 하나만 선택했습니다"),Name);
			} else {
				wsprintf(str,TEXT("%s 디렉토리에 있는 다음 파일들이 선택되었습니다")
					TEXT("\r\n\r\n"), Name);
				for (;;) {
					p=p+lstrlen(Name)+1;
					if (*p==0)
						break;
					lstrcpy(Name,p);
					wsprintf(szTmp,TEXT("%d번째 파일 = %s \r\n"),i++,Name);
					lstrcat(str,szTmp);
				}
			}
			MessageBox(hWnd,str,TEXT("선택한 파일"),MB_OK);
		} else {
			if (CommDlgExtendedError()==FNERR_BUFFERTOOSMALL) {
				MessageBox(hWnd,TEXT("버퍼 크기가 너무 작습니다"),TEXT("에러"),MB_OK);
			}
		}
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
