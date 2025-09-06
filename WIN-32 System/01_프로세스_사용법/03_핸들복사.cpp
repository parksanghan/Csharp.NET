//03_핸들복사.cpp

#include <stdio.h>
#include <windows.h> 

void main()
{
	HANDLE hFile = CreateFile(TEXT("a.txt"), 
		GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE,
		0, // 보안
		CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);

	printf("생성된   화일   핸들(Table Index) : %x\n", hFile); 
	
	HWND hwnd = FindWindow(0, TEXT("B"));
	DWORD pid;
	DWORD tid = GetWindowThreadProcessId(hwnd, &pid); 
	HANDLE hProcess = OpenProcess(PROCESS_ALL_ACCESS, 0, pid); 
	
	//핸들복사
	HANDLE h;
	DuplicateHandle(GetCurrentProcess(), hFile, // source
			hProcess, &h,    // target 
		0, 0, DUPLICATE_SAME_ACCESS);

	printf("B에   복사한   핸들(Table index) : %x\n", h); 
	SendMessage(hwnd, WM_USER + 100, 0, (LPARAM)h); 
	CloseHandle(hFile);
}