//03_���μ��� ����.cpp

#include <windows.h> 
#include <stdio.h>

int main()
{
	TCHAR name[] = TEXT("0911_ȸ���������α׷�.exe"); // notepad.exe
	PROCESS_INFORMATION pi;
	STARTUPINFO si = { sizeof(si) };

	BOOL b = CreateProcess(0, name, 0, 0, 0, 0, 0, 0, &si, &pi);

	printf("���μ��� ID : %d\n", (int)pi.dwProcessId);
	printf("������   ID : %d\n", (int)pi.dwThreadId);
	printf("���μ��� �ڵ� : %d\n", (int)pi.hProcess);
	printf("������   �ڵ� : %d\n", (int)pi.hThread);

	char dummy = getchar();

	ExitProcess(0);
	//TerminateProcess(pi.hProcess, 0);

	dummy = getchar();
	printf("����....\n");

	return 0;
}