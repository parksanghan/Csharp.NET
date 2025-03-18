//39_��޴��̾�α�.cpp
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"

struct DATA
{
	TCHAR name[20];
	int age;
};

//�ڽ������� ���ν���
BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static DATA* pdata = NULL;

	switch (msg)
	{
	case WM_INITDIALOG:
	{
		pdata = (DATA*)lParam;

		SetDlgItemText(hDlg, IDC_EDIT1, pdata->name);
		SetDlgItemInt(hDlg, IDC_EDIT2, pdata->age,0);

		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{			
		case IDOK:
		{
			//��Ʈ�ѿ� ��µ� ������ ���޵� �ּҷ� ����...
			GetDlgItemText(hDlg, IDC_EDIT1, pdata->name, _countof(pdata->name));
			pdata->age = GetDlgItemInt(hDlg, IDC_EDIT2, 0, 0);

			EndDialog(hDlg, IDOK); 
			return TRUE;
		}

		case IDCANCEL: 
			EndDialog(hDlg, IDCANCEL); 
			return TRUE;
		}
	}
	}
	return FALSE;
}

//-----------�������� ----------
DATA g_data = { TEXT("ȫ�浿"), 10 };

//�θ������� ���ν��� 
LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		TextOut(hdc, 10, 10, g_data.name, _tcslen(g_data.name));
		TCHAR temp[20];
		wsprintf(temp, TEXT("%d"), g_data.age);
		TextOut(hdc, 10, 30, temp, _tcslen(temp));

		EndPaint(hwnd, &ps);
		return 0;
	}
	case WM_LBUTTONDOWN:	//��� ����
	{
		DATA temp;
		_tcscpy_s(temp.name, _countof(temp.name), g_data.name);
		temp.age = g_data.age;

		UINT ret = DialogBoxParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG1),
			hwnd, DlgProc, (LPARAM)&temp);
		if (ret == IDOK)
		{
			//�ڽ��� ����� ������ ������ ��..
			//���⼭ ó��!!!!
			_tcscpy_s(g_data.name, _countof(g_data.name), temp.name);
			g_data.age = temp.age;
			InvalidateRect(hwnd, 0, TRUE);
		}

		return 0;
	}
	case WM_CREATE:
	{
		return 0;
	}

	case WM_DESTROY:
	{
		PostQuitMessage(0);
		return 0;
	}
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//������ Ŭ���� ����
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //��, �귯��, ��Ʈ
	wc.hCursor = LoadCursor(0, IDC_ARROW);//�ý���
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = 0;		//�޴� ���
	wc.style = 0;				//������ ��Ÿ��

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("0830"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);				//<-----------------(0)

	//�޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);		//<--------------------(0)
		DispatchMessage(&msg);
	}
	return 0;
}