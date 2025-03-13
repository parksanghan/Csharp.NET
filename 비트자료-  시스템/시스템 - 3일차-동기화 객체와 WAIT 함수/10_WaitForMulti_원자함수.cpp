//10_WaitForMulti_원자함수.cpp
#include <windows.h> 
#include <stdio.h>

long value = 0; 


DWORD WINAPI ThreadFunc( void* p) 
{ 
	for (int i = 0; i < 100000; ++i ) 
		++value; 
	return 0; 
} 

CRITICAL_SECTION g_cs;

DWORD WINAPI ThreadFunc1(void* p)
{
	for (int i = 0; i < 100000; ++i)
	{
		EnterCriticalSection(&g_cs);
		++value;
		LeaveCriticalSection(&g_cs);
	}
	return 0;
}

DWORD WINAPI ThreadFunc2(void* p)
{
	for (int i = 0; i < 100000; ++i)
	{
		//++value;
		InterlockedIncrement(&value); //내부적으로 동기화 처리가 되어 있다.
	}
	return 0;
}

int main() 
{ 
	InitializeCriticalSection(&g_cs);

	int i = 0; HANDLE hThread[5]; 
	for ( i = 0; i < 5; ++i )
		hThread[i] = CreateThread( 0, 0, ThreadFunc2, 0, 0,0);
	
	WaitForMultipleObjects( 5, hThread, TRUE, INFINITE ); 
	for ( i = 0; i < 5; ++i ) 
		CloseHandle( hThread[i]);

	printf("value = %d\n", value); 

	DeleteCriticalSection(&g_cs);
	return 0;
}