//1_ũ��Ƽ�ü���
#include <windows.h>
#include <iostream>
using namespace std;

void WorkFunc() { for (int i = 0; i < 10000000; i++); }// �ð� ���� �Լ�	
// ���� �ڿ�
int g_x = 0;

CRITICAL_SECTION	g_cs;	// ����  (1)

DWORD WINAPI Func(PVOID p) 
{
	
	for (int i = 0; i < 20; i++)
	{
		EnterCriticalSection(&g_cs);
		g_x = 200;
		WorkFunc();
		g_x++;
		cout << "             Func : " << g_x << endl;		//201
		LeaveCriticalSection(&g_cs);
	}
	
	return 0;
}

int main() 
{
	InitializeCriticalSection(&g_cs);				// �Ӱ迵�� ���� �ʱ�ȭ.(2)

	DWORD tid;
	HANDLE hThread = CreateThread(NULL, 0, Func, 0, NORMAL_PRIORITY_CLASS, &tid);

	//	Sleep(1);
	
	for (int i = 0; i < 20; i++) 
	{
		EnterCriticalSection(&g_cs);
		g_x = 200;
		WorkFunc();
		g_x--;
		cout << "             Main : " << g_x << endl;  //199
		LeaveCriticalSection(&g_cs);
	}


	WaitForSingleObject(hThread, INFINITE);
	CloseHandle(hThread);	
		
	DeleteCriticalSection(&g_cs);	// �ı�				//3)

	return 0;
}
