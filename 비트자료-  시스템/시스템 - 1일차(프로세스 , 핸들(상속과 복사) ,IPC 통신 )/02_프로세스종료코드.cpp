//02_프로세스종료.cpp
/*
1) 윈도우 핸들을 갖고 있다면 -> 정상적인 종료를 유도
   SendMessage(h, WM_CLOSE, 0, 0);

2) 프로세스의 핸들을 갖고 있다면 -> 강제적 종료 방법
   1)의 정상적 종료가 불가능할때 수행하는 방법

   2.1) ExitProcess(종료코드값);  //자신을 죽임

   2.2) TerminateProcess(프로세스핸들, 종료코드값);  //자신, 다른 프로세스 종료 가능
   
*/
#include <windows.h> 
#include <stdio.h>

int main()
{
	TCHAR name[] = TEXT("notepad.exe");
	PROCESS_INFORMATION pi;
	STARTUPINFO si = { sizeof(si) };

	BOOL b = CreateProcess(0, name, 0, 0, 0, 0, 0, 0, &si, &pi);

	printf("프로세스 ID : %d\n", (int)pi.dwProcessId);
	printf("쓰레드   ID : %d\n", (int)pi.dwThreadId);
	printf("프로세스 핸들 : %d\n", (int)pi.hProcess);
	printf("쓰레드   핸들 : %d\n", (int)pi.hThread);

	//멈춘 후 메모장을 종료 한 후 실행 or 종료하지 않고 실행
	char dummy = getchar();

	DWORD code;
	GetExitCodeProcess(pi.hProcess, &code);
	if (code == STILL_ACTIVE)
	{
		printf("종료코드 : %d (%d)\n", code, STILL_ACTIVE);
		printf("살아있다\n");
	}
	else
	{
		printf("죽었다\n");
		//더 이상 핸들이 필요없다.
		CloseHandle(pi.hProcess);	//왜 해야 할까?
		CloseHandle(pi.hThread);	//??
	}


	return 0;
}