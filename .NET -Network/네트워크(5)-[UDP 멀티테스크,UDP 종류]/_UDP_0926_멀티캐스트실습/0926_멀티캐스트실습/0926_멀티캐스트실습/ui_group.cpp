//ui_group.cpp
#include "std.h"

static HWND g_hDlg;
HWND hCB_Group;

char g_GroupIP[9][40] = { "",
	"234.5.5.1","234.5.5.2","234.5.5.3","234.5.5.4",
	"234.5.5.5","234.5.5.6","234.5.5.7","234.5.5.8"
};

void ui_group_Init(HWND hDlg)
{
	ui_group_GetHandle(hDlg);
	ui_group_CB_Init(hDlg);
}

void ui_group_GetHandle(HWND hDlg)
{
	g_hDlg = hDlg;
	hCB_Group = GetDlgItem(hDlg, IDC_CB_GROUP);
}

void ui_group_CB_Init(HWND hDlg)
{
	TCHAR buf[50];
	SendMessage(hCB_Group, CB_ADDSTRING, 0, (LPARAM)TEXT("[조별IP선택]"));

	for (int i = 1; i < 9; i++)
	{
		wsprintf(buf, TEXT("%d조(%s)"), i, g_GroupIP[i]);
		SendMessage(hCB_Group, CB_ADDSTRING, 0, (LPARAM)buf);
	}
	SendMessage(hCB_Group, CB_SETCURSEL, 0, 0);
}

void ui_group_GetIp(HWND hDlg, char* ip)
{
	int idx = (int)SendMessage(hCB_Group, CB_GETCURSEL, 0, 0);
	strcpy_s(ip, 50, g_GroupIP[idx]);
}

void ui_group_GetAllIp(HWND hDlg, char(*ip)[50])
{
	for (int i = 0; i < 8; i++)
	{
		strcpy_s(ip[i], 50, g_GroupIP[i + 1]);
	}
}