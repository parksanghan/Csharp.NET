//16_WM_COPYDATA(����).cpp

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
		
		if (p->dwData == 1) // �ĺ��� ����.
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
			//�������.
			//EndDialog : ���̾�α׸� �����ϴ� �Լ�
			//hDlg : ������ , IDCANCEL : ����� ��ȯ��
		case IDCANCEL: EndDialog(hDlg, IDCANCEL); return TRUE;
		}
	}
	}
	return FALSE;	//�޽����� ó������ �ʾҴ�.-> �� ������ ��ȭ���� ó���ϴ� default���ν���
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	INT_PTR ret = DialogBox(hInst,// instance
		MAKEINTRESOURCE(IDD_DIALOG1), // ���̾�α� ����
		0, // �θ� ������
		DlgProc); // Proc..

	return 0;
}