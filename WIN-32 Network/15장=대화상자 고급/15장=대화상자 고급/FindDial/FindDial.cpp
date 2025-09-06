#include <windows.h>

LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass=TEXT("FindDial");
UINT FRMsg;
HWND hDlgFR=NULL;
FINDREPLACE FR;
TCHAR szFindWhat[256];

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
		if (!IsWindow(hDlgFR) || !IsDialogMessage(hDlgFR,&Message)) {
			TranslateMessage(&Message);
			DispatchMessage(&Message);
		}
	}
	return (int)Message.wParam;
}

LRESULT CALLBACK WndProc(HWND hWnd,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	HDC hdc;
	PAINTSTRUCT ps;
	TCHAR *Mes=TEXT("찾기 대화상자를 보여줍니다");
	FINDREPLACE *pFR;
	static int Count=1;
	static TCHAR str[10000];
	TCHAR sTmp[256];
	RECT crt;

	if (iMessage == FRMsg) {
		pFR=(FINDREPLACE *)lParam;
		// 대화상자 종료
		if (pFR->Flags & FR_DIALOGTERM) {
			hDlgFR=NULL;
			return 0;
		}

		// 다음 찾기
		if (pFR->Flags & FR_FINDNEXT) {
			wsprintf(sTmp,TEXT("%s 문자열을 %s 방향으로 대소문자 구분%s %s검색합니다\r\n"),
				szFindWhat, (pFR->Flags & FR_DOWN ? TEXT("아래쪽"):TEXT("위쪽")),
				(pFR->Flags & FR_MATCHCASE ? TEXT("하여"):TEXT("없이")),
				(pFR->Flags & FR_WHOLEWORD ? TEXT("단어단위로 "):TEXT("")));
			lstrcat(str,sTmp);
			InvalidateRect(hWnd,NULL,TRUE);
		}
		return 0;
	}

	switch (iMessage) {
	case WM_CREATE:
		// 메시지 등록
		FRMsg=RegisterWindowMessage(FINDMSGSTRING);
		return 0;
	case WM_LBUTTONDOWN:
		// 대화상자 호출
		if (hDlgFR == NULL) {
			memset(&FR,0,sizeof(FINDREPLACE));
			FR.lStructSize=sizeof(FINDREPLACE);
			FR.hwndOwner=hWnd;
			FR.lpstrFindWhat=szFindWhat;
			FR.wFindWhatLen=256;

			hDlgFR=FindText(&FR);
		}
		return 0;
	case WM_PAINT:
		hdc=BeginPaint(hWnd, &ps);
		GetClientRect(hWnd,&crt);
		crt.left=10;
		crt.top=30;
		TextOut(hdc,10,10,Mes,lstrlen(Mes));
		DrawText(hdc,str,-1,&crt,0);
		EndPaint(hWnd, &ps);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd,iMessage,wParam,lParam));
}

