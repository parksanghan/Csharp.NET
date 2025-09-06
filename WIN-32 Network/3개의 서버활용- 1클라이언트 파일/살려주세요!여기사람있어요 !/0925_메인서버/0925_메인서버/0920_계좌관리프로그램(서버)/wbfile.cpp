//wbfile.cpp
#include "std.h"

int wbfile_GetFileList(const TCHAR *path, TCHAR(*files)[40])
{
	WIN32_FIND_DATA wfd;
	//TCHAR fname[MAX_PATH];
	BOOL bResult = TRUE;

	TCHAR Path[MAX_PATH];
	wsprintf(Path, TEXT("%s"), path);

	HANDLE hSrch = FindFirstFile(Path, &wfd);
	if (hSrch == INVALID_HANDLE_VALUE) return 0;
	
	int i = 0;
	while (bResult) 
	{
		if (wfd.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) {
			//wsprintf(fname, "[ %s ]", wfd.cFileName);
		}
		else {
			//wsprintf(fname, "%s", wfd.cFileName);
			_tcscpy_s(files[i], _countof(files[i]), wfd.cFileName);
			i++;
		}		
		bResult = FindNextFile(hSrch, &wfd);
	}
	FindClose(hSrch);
	return i;
}