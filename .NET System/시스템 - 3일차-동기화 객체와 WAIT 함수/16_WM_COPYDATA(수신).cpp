//16_WM_COPYDATA(수신).cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"

HWND hEdit;

INT_PTR CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_COPYDATA:
	{
		COPYDATASTRUCT* p = (COPYDATASTRUCT*)lParam; 
		
		if (p->dwData == 1) // 식별자 조사.
		{  
			SendMessage( hEdit, EM_REPLACESEL, 0, (LPARAM)(p->lpData)); 
			SendMessage( hEdit, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
		}
		return 0;
	}
	case WM_INITDIALOG:
	{
		hEdit = GetDlgItem(hDlg, IDC_EDIT1);
		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
			//종료시점.
			//EndDialog : 다이얼로그를 종료하는 함수
			//hDlg : 종료대상 , IDCANCEL : 종료시 반환값
		case IDCANCEL: EndDialog(hDlg, IDCANCEL); return TRUE;
		}
	}
	}
	return FALSE;	//메시지를 처리하지 않았다.-> 이 다음에 대화상자 처리하는 default프로시저
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	INT_PTR ret = DialogBox(hInst,// instance
		MAKEINTRESOURCE(IDD_DIALOG1), // 다이얼로그 선택
		0, // 부모 윈도우
		DlgProc); // Proc..

	return 0;
}