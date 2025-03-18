//04_PIPE1.cpp

#include <windows.h> 
#include <stdio.h> 
#include <tchar.h> 

void main()
{
	HANDLE hPipe = CreateFile(TEXT("\\\\.\\pipe\\TimeServer"), // UNC
		GENERIC_READ, FILE_SHARE_WRITE, 0,
		OPEN_EXISTING,
		FILE_ATTRIBUTE_NORMAL, 0);

	if (hPipe == INVALID_HANDLE_VALUE)
	{
		_tprintf(TEXT("Pipe 서버에   연결할수   없습니다\n"));
		return;
	}

	DWORD len; 
	SYSTEMTIME st;
	ReadFile(hPipe, &st, sizeof(st), &len, 0); // UTC 시간을   ->지역   시간으로
	::SystemTimeToTzSpecificLocalTime(0, &st, &st); 
	SetLocalTime(&st);
	printf("%d시   %d분\n", st.wHour, st.wMinute); 
	
	TCHAR date[256];
	TCHAR time[256];
	GetDateFormat(LOCALE_USER_DEFAULT, 0, &st, 0, date, 256); 
	GetTimeFormat(LOCALE_USER_DEFAULT, 0, &st, 0, time, 256); 
	_tprintf(TEXT("%s\n"), date);
	_tprintf(TEXT("%s\n"), time); 
	CloseHandle(hPipe);
}