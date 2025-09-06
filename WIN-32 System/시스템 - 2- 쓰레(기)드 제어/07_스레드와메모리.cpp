//07_스레드와메모리.cpp(p84)

#include <windows.h> 
#include <stdio.h>
#include <process.h>

// goo 는 static 지역 변수을 사용해서 원하는 기능을 수행하였다. 
// 싱글스레드 환경에서는 아무 문제 없다. 멀티 스레드 라면. ? 
void goo( char* name) 
{ 
	// TLS 공간에 변수를 생성한다. 
	__declspec(thread) static int c = 0; 
	 
	//static int c = 0;	//모든 스레드가 접근이 가능한 변수
	++c; 
	printf("%s : %d\n", name, c ); 
	// 함수가 몇번 호출되었는지 알고 싶다. 
} 

//DWORD WINAPI foo( void* p ) 
unsigned int __stdcall foo(void* p)
{ 
	char* name = (char*)p; 
	
	goo( name );
	goo( name ); 
	goo( name ); 
	
	return 0; 
} 

//main thread에서 , 2nd, 2rd 스레드 생성
//main thread는 종료! -> 프로세스가 종료된다는 뜻 -> 메모리 사라짐
void exam1()
{
	//thread 2개 생성(시작함수 동일!, 인자가 다름)
	//HANDLE h1 = CreateThread(0, 0, foo, (void*)"A", 0, 0);
	//HANDLE h2 = CreateThread(0, 0, foo, (void*)"\tB", 0, 0);
	uintptr_t h1 = _beginthreadex(0, 0, foo, (void*)"A", 0, 0); //#include <process.h>
	uintptr_t h2 = _beginthreadex(0, 0, foo, (void*)"\tB", 0, 0); //#include <process.h>
}

void exam2()
{
	//thread 2개 생성(시작함수 동일!, 인자가 다름)
	//HANDLE h1 = CreateThread(0, 0, foo, (void*)"A", 0, 0);
	//HANDLE h2 = CreateThread(0, 0, foo, (void*)"\tB", 0, 0);
	uintptr_t h1 = _beginthreadex(0, 0, foo, (void*)"A", 0, 0); //#include <process.h>
	uintptr_t h2 = _beginthreadex(0, 0, foo, (void*)"\tB", 0, 0); //#include <process.h>

	HANDLE h[2] = { (HANDLE)h1, (HANDLE)h2 };

	//TRUE : 기다리는 2명이 다 시그널되면(종료되면) 리턴
	WaitForMultipleObjects(2, h, TRUE, INFINITE);

	CloseHandle((HANDLE)h1);
	CloseHandle((HANDLE)h2);
}

int main() 
{ 	
	exam2();

	return 0;
}