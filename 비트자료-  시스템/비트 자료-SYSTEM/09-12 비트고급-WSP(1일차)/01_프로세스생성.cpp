//01_���μ�������.cpp
#include <windows.h> 
#include <stdio.h>

int main()
{
	TCHAR name[] = TEXT("notepad.exe");
	PROCESS_INFORMATION pi;
	STARTUPINFO si = { sizeof(si) };

	BOOL b = CreateProcess(0, name, 0, 0, 0, 0, 0, 0, &si, &pi);

	printf("���μ��� ID : %d\n", (int)pi.dwProcessId);
	printf("������   ID : %d\n", (int)pi.dwThreadId);
	printf("���μ��� �ڵ� : %d\n", (int)pi.hProcess);
	printf("������   �ڵ� : %d\n", (int)pi.hThread);

	return 0;
}