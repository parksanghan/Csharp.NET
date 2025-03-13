//01_��ȭ���ڱ��.cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>
#include "resource.h"
#include "wbprocess.h"

BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_INITDIALOG:
	{
		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDC_BUTTON1:	wb_CreateProcess(hDlg);			return TRUE;
		case IDC_BUTTON2:	wb_ExitProcess(hDlg);			return TRUE;
		case IDC_BUTTON3:	wb_GetExitCodeProcess(hDlg);	return TRUE;
		case IDC_BUTTON4:	wb_HwndToProcessHandle(hDlg);	return TRUE;
		case IDCANCEL:		EndDialog(hDlg, IDCANCEL);		return TRUE;
		}
	}
	}
	return FALSE;	//�޽����� ó������ �ʾҴ�.-> �� ������ ��ȭ���� ó���ϴ� default���ν���
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	UINT ret = DialogBox(hInst,// instance
		MAKEINTRESOURCE(IDD_DIALOG1), // ���̾�α� ����
		0, // �θ� ������
		DlgProc); // Proc..


	return 0;
}