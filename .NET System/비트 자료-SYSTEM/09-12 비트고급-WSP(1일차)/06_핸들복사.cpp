//06_�ڵ麹��.cpp

#include <windows.h> 
#include <stdio.h>

void main()
{ 
	HANDLE hFile = CreateFile( TEXT("a.txt"), 
		GENERIC_READ | GENERIC_WRITE, 
		FILE_SHARE_READ | FILE_SHARE_WRITE, 0, // ���� 
		CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);

	printf("������ ȭ�� �ڵ�(Table Index) : %d\n", hFile); 

	//1. ������ �ڵ� ȹ��
	HWND hwnd = FindWindow(0, TEXT("B"));
	
	//2. ���μ��� ID ȹ��
	DWORD pid; 
	DWORD tid = GetWindowThreadProcessId( hwnd, &pid );

	//3. ���μ����� �ڵ�
	HANDLE hProcess = OpenProcess( PROCESS_ALL_ACCESS, 0, pid ); 

	//"B" ���μ����� �ڵ��� ����!
	HANDLE h;
	DuplicateHandle(
		GetCurrentProcess(), hFile, // source 
		hProcess, &h,				// target 
		0, 0, DUPLICATE_SAME_ACCESS); 
	
	printf("B�� ������ �ڵ�(Table index) : %d\n", h ); 
	
	SendMessage( hwnd, WM_USER+100, 0, (LPARAM) h ); 
	
	CloseHandle( hFile );
}