//03_프로세스 종료.cpp

#include <windows.h> 
#include <stdio.h>

int main()
{
	TCHAR name[] = TEXT("0911_회원관리프로그램.exe"); // notepad.exe
	PROCESS_INFORMATION pi;
	STARTUPINFO si = { sizeof(si) };

	BOOL b = CreateProcess(0, name, 0, 0, 0, 0, 0, 0, &si, &pi);

	printf("프로세스 ID : %d\n", (int)pi.dwProcessId);
	printf("쓰레드   ID : %d\n", (int)pi.dwThreadId);
	printf("프로세스 핸들 : %d\n", (int)pi.hProcess);
	printf("쓰레드   핸들 : %d\n", (int)pi.hThread);

	char dummy = getchar();

	ExitProcess(0);
	//TerminateProcess(pi.hProcess, 0);

	dummy = getchar();
	printf("종료....\n");

	return 0;
}