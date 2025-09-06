#include <windows.h>

LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass=TEXT("FindFile");

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
/// <summary>
///  필요부분
/// </summary>
HWND hList;
#define ID_LISTBOX 100
void MyFindFile()
{
	HANDLE hSrch;
	WIN32_FIND_DATA wfd;
	TCHAR fname[MAX_PATH];
	BOOL bResult=TRUE;
	TCHAR Path[MAX_PATH];

	GetWindowsDirectory(Path,MAX_PATH);
	lstrcat(Path,"\\*.*");
	hSrch=FindFirstFile(Path,&wfd);
	if (hSrch==INVALID_HANDLE_VALUE) return;
	while (bResult) {
		if (wfd.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) {
			wsprintf(fname,"[ %s ]",wfd.cFileName);
		} else {
			wsprintf(fname,"%s",wfd.cFileName);
		}
		SendMessage(hList,LB_ADDSTRING,0,(LPARAM)fname);
		bResult=FindNextFile(hSrch,&wfd);
	}
	FindClose(hSrch);
}
/// <summary>
/// 
/// </summary>
/// <param name="hWnd"></param>
/// <param name="iMessage"></param>
/// <param name="wParam"></param>
/// <param name="lParam"></param>
/// <returns></returns>
LRESULT CALLBACK WndProc(HWND hWnd,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	switch (iMessage) {
	case WM_CREATE:
		hList=CreateWindow("listbox",NULL,WS_CHILD | WS_VISIBLE | 
			WS_BORDER | WS_VSCROLL | LBS_NOTIFY | LBS_SORT,
			0,0,0,0,hWnd,(HMENU)ID_LISTBOX,g_hInst,NULL);
		MyFindFile();
		return 0;
	case WM_SIZE:
		MoveWindow(hList,0,0,LOWORD(lParam),HIWORD(lParam),TRUE);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd,iMessage,wParam,lParam));
}

