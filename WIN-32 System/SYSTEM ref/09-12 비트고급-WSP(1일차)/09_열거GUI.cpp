//07_프로세스열거실습.cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>
#include <commctrl.h>
#include <tlhelp32.h>   // CreateToolhelp32Snapshot() 를 사용하기 위한 해더포함  
#include <vector>
using namespace std;
#include "resource.h"

typedef struct tagPROCESSLIST
{
	TCHAR name[50];
	int   pid;
	int   threads;
	int   priority;
}PROCESSLIST;

vector<PROCESSLIST> plist;
HWND hListView;

//리스트 헤더 초기화
void mainui_ListViewColumeHeader(HWND hDlg)
{
	hListView = GetDlgItem(hDlg, IDC_LIST1);

	LVCOLUMN COL;

	COL.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM;
	COL.fmt = LVCFMT_LEFT;
	COL.cx = 100;
	COL.pszText = (LPTSTR)TEXT("프로세스명");				// 첫 번째 헤더
	COL.iSubItem = 0;
	SendMessage(hListView, LVM_INSERTCOLUMN, 0, (LPARAM)&COL);

	COL.cx = 100;
	COL.pszText = (LPTSTR)TEXT("PID");			// 두 번째 헤더
	COL.iSubItem = 1;
	SendMessage(hListView, LVM_INSERTCOLUMN, 1, (LPARAM)&COL);

	COL.cx = 100;
	COL.pszText = (LPTSTR)TEXT("스레드");				// 세 번째 헤더
	COL.iSubItem = 2;
	SendMessage(hListView, LVM_INSERTCOLUMN, 2, (LPARAM)&COL);

	COL.cx = 100;
	COL.pszText = (LPTSTR)TEXT("우선순위");				// 네 번째 헤더
	COL.iSubItem = 3;
	SendMessage(hListView, LVM_INSERTCOLUMN, 3, (LPARAM)&COL);

	//확장 스타일
	ListView_SetExtendedListViewStyle(hListView, LVS_EX_FULLROWSELECT |
		LVS_EX_GRIDLINES | LVS_EX_CHECKBOXES | LVS_EX_HEADERDRAGDROP);
}

void mainui_ListViewPrintAll(HWND hDlg, vector<PROCESSLIST> prolist)
{
	//기존 리스트뷰에 출력된 정보를 제거 
	SendMessage(hListView, LVM_DELETEALLITEMS, 0, 0);

	LVITEM LI;
	LI.mask = LVIF_TEXT;
	TCHAR temp[100];
	for (int i = 0; i < (int)prolist.size(); i++)
	{
		PROCESSLIST pro = prolist[i];

		LI.iItem = i; //<-------
		LI.iSubItem = 0;
		LI.pszText = (LPTSTR)pro.name;			// 첫 번째 아이템
		SendMessage(hListView, LVM_INSERTITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 1;
		wsprintf(temp, TEXT("%d"), pro.pid);
		LI.pszText = (LPTSTR)temp;
		SendMessage(hListView, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 2;
		wsprintf(temp, TEXT("%d"), pro.threads);
		LI.pszText = (LPTSTR)temp;
		SendMessage(hListView, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 3;
		wsprintf(temp, TEXT("%d"), pro.priority);
		LI.pszText = (LPTSTR)temp;
		SendMessage(hListView, LVM_SETITEM, 0, (LPARAM)&LI);
	}
}

void mainui_PrintProcess(HWND hDlg)
{
	TCHAR buf[100];
	wsprintf(buf, TEXT("실행중인 프로세스 : %d개"), plist.size());
	SetDlgItemText(hDlg, IDC_STATIC1, buf);

	mainui_ListViewPrintAll(hDlg, plist);
}

//프로세스 열거코드
void OnEnumProcess(HWND hDlg)
{
	HANDLE hProcess = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
	PROCESSENTRY32 pe32 = { 0 };
	pe32.dwSize = sizeof(PROCESSENTRY32);
	int count = 0;
	if (Process32First(hProcess, &pe32))
	{
		do
		{
			PROCESSLIST list;
			_tcscpy_s(list.name, _countof(list.name), pe32.szExeFile);
			list.pid = pe32.th32ProcessID;
			list.priority = pe32.pcPriClassBase;
			list.threads = pe32.cntThreads;

			plist.push_back(list);

		} while (Process32Next(hProcess, &pe32));
	}
	CloseHandle(hProcess);

	mainui_PrintProcess(hDlg);
}

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	//리스트뷰의 컬럼 헤더 
	mainui_ListViewColumeHeader(hDlg);

	return TRUE;
}


INT_PTR CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_INITDIALOG:		return OnInitDialog(hDlg, wParam, lParam);
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDC_BUTTON1:	OnEnumProcess(hDlg);		return TRUE;
		case IDCANCEL:		EndDialog(hDlg, IDCANCEL);	return TRUE;
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
