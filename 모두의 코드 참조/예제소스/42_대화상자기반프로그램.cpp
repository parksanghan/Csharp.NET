//42_대화상자기반프로그램.cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"

BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		//최초 호출 시점.
	case WM_INITDIALOG:
	{
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
	UINT ret = DialogBox(hInst,// instance
		MAKEINTRESOURCE(IDD_DIALOG1), // 다이얼로그 선택
		0, // 부모 윈도우
		DlgProc); // Proc..

	return 0;
}