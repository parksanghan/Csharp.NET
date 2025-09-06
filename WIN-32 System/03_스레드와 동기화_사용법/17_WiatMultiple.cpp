//17_WaitMultiple
#include <windows.h>
#include <stdio.h>

void Delay() { for (int i = 0; i < 5000000; ++i); }  // 시간 지연
BOOL bWait = TRUE;
CRITICAL_SECTION cs; // 임계영역 cs 안에는 1개의 스레드만 들어 올수 있다.

DWORD WINAPI foo(void* p)
{
	char* name = (char*)p;
	static int x = 0;

	for (int i = 0; i < 20; ++i)
	{
		EnterCriticalSection(&cs); // cs에 들어 간다.
		//---------------------------------------------
		x = 100; Delay();
		x = x + 1; Delay();
		printf("%s : %d\n", name, x); Delay();
		//---------------------------------------------
		LeaveCriticalSection(&cs); // cs에서 나온다.
	}
	printf("Finish...%s\n", name);
	return 0;
}

int main()
{
	InitializeCriticalSection(&cs); // main 제일아래에

	HANDLE h1 = CreateThread(0, 0, foo, (LPVOID)TEXT("A"), 0, 0);
	HANDLE h2 = CreateThread(0, 0, foo, (LPVOID)TEXT("\tB"), 0, 0);

	// 64 개 까지의 KO 를 대기할수 있다.
	HANDLE h[2] = { h1, h2 };
	WaitForMultipleObjects(2, h, TRUE, // 2개 모두 signal 될때 까지 대기
		INFINITE);

	CloseHandle(h1);
	CloseHandle(h2);

	DeleteCriticalSection(&cs);
	return 0;
}
