//04_ũ��Ƽ�ü���.cpp
/*
�����ڿ� üũ?
- ��������
- �ܼ�ȭ��
*/

#include <windows.h> 
#include <iostream> 
using namespace std; 

// �ð� ���� �Լ�
void WorkFunc() 
{ 
	for( int i=0; i <10000000; i++);
}


// ���� �ڿ� 
int g_x = 0;

CRITICAL_SECTION g_cs;  

//������ �Լ�
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
	// �Ӱ迵�� ���� �ʱ�ȭ
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

	// �ı�
	DeleteCriticalSection(&g_cs);  
}