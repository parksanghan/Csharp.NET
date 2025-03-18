//17_WaitMultiple
#include <windows.h>
#include <stdio.h>

void Delay() { for (int i = 0; i < 5000000; ++i); }  // �ð� ����
BOOL bWait = TRUE;
CRITICAL_SECTION cs; // �Ӱ迵�� cs �ȿ��� 1���� �����常 ��� �ü� �ִ�.

DWORD WINAPI foo(void* p)
{
	char* name = (char*)p;
	static int x = 0;

	for (int i = 0; i < 20; ++i)
	{
		EnterCriticalSection(&cs); // cs�� ��� ����.
		//---------------------------------------------
		x = 100; Delay();
		x = x + 1; Delay();
		printf("%s : %d\n", name, x); Delay();
		//---------------------------------------------
		LeaveCriticalSection(&cs); // cs���� ���´�.
	}
	printf("Finish...%s\n", name);
	return 0;
}

int main()
{
	InitializeCriticalSection(&cs); // main ���ϾƷ���

	HANDLE h1 = CreateThread(0, 0, foo, (LPVOID)TEXT("A"), 0, 0);
	HANDLE h2 = CreateThread(0, 0, foo, (LPVOID)TEXT("\tB"), 0, 0);

	// 64 �� ������ KO �� ����Ҽ� �ִ�.
	HANDLE h[2] = { h1, h2 };
	WaitForMultipleObjects(2, h, TRUE, // 2�� ��� signal �ɶ� ���� ���
		INFINITE);

	CloseHandle(h1);
	CloseHandle(h2);

	DeleteCriticalSection(&cs);
	return 0;
}
