//net_download.h
#pragma once

struct FILE_INFO
{
	TCHAR FileName[260];
	int  size;
};

void net_download_Start(HWND hDlg, const TCHAR* filename, int size);