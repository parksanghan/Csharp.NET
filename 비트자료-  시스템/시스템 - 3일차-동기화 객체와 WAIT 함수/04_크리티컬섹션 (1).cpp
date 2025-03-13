//04_크리티컬섹션.cpp
/*
공유자원 체크?
- 전역변수
- 콘솔화면
*/

#include <windows.h> 
#include <iostream> 
using namespace std; 

// 시간 지연 함수
void WorkFunc() 
{ 
	for( int i=0; i <10000000; i++);
}


// 공유 자원 
int g_x = 0;

CRITICAL_SECTION g_cs;  

//스레드 함수
DWORD WINAPI Func( PVOID p)
{ 
	for( int i=0; i< 100; i++)
	{ 
		//EnterCriticalSection(&g_cs);	//*************************************
		g_x = 200; 
		WorkFunc(); 
		g_x++;
		cout << "Func : ................" << g_x << endl;	//Func : 201
		//LeaveCriticalSection(&g_cs);	//*************************************
		Sleep(1);
	} 	
	return 0; 
} 

void main() 
{
	// 임계영역 변수 초기화
	InitializeCriticalSection(&g_cs); 
	
	DWORD tid; 
	HANDLE hThread = CreateThread(NULL, 0, Func, 0, NORMAL_PRIORITY_CLASS, &tid); 
	
	Sleep(1); 

	for( int i=0; i< 100; i++)
	{ 
		//EnterCriticalSection(&g_cs); //*************************************
		g_x = 200; 
		WorkFunc(); 
		g_x--;

		cout << " .............Main : " << g_x << endl;	//Main : 199
		//LeaveCriticalSection(&g_cs); //*************************************
		Sleep(1);
	} 
	
	
	WaitForSingleObject(hThread, INFINITE); 
	CloseHandle(hThread);

	// 파괴
	DeleteCriticalSection(&g_cs);  
}