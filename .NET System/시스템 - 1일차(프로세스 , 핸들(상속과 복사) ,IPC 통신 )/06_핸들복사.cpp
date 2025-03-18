//06_핸들복사.cpp

#include <windows.h> 
#include <stdio.h>

void main()
{ 
	HANDLE hFile = CreateFile( TEXT("a.txt"), 
		GENERIC_READ | GENERIC_WRITE, 
		FILE_SHARE_READ | FILE_SHARE_WRITE, 0, // 보안 
		CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);

	printf("생성된 화일 핸들(Table Index) : %d\n", hFile); 

	//1. 윈도우 핸들 획득
	HWND hwnd = FindWindow(0, TEXT("B"));
	
	//2. 프로세스 ID 획득
	DWORD pid; 
	DWORD tid = GetWindowThreadProcessId( hwnd, &pid );

	//3. 프로세스의 핸들
	HANDLE hProcess = OpenProcess( PROCESS_ALL_ACCESS, 0, pid ); 

	//"B" 프로세스에 핸들을 복사!
	HANDLE h;
	DuplicateHandle(
		GetCurrentProcess(), hFile, // source 
		hProcess, &h,				// target 
		0, 0, DUPLICATE_SAME_ACCESS); 
	
	printf("B에 복사한 핸들(Table index) : %d\n", h ); 
	
	SendMessage( hwnd, WM_USER+100, 0, (LPARAM) h ); 
	
	CloseHandle( hFile );
}