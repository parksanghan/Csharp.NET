//07_������͸޸�.cpp(p84)

#include <windows.h> 
#include <stdio.h>
#include <process.h>

// goo �� static ���� ������ ����ؼ� ���ϴ� ����� �����Ͽ���. 
// �̱۽����� ȯ�濡���� �ƹ� ���� ����. ��Ƽ ������ ���. ? 
void goo( char* name) 
{ 
	// TLS ������ ������ �����Ѵ�. 
	__declspec(thread) static int c = 0; 
	 
	//static int c = 0;	//��� �����尡 ������ ������ ����
	++c; 
	printf("%s : %d\n", name, c ); 
	// �Լ��� ��� ȣ��Ǿ����� �˰� �ʹ�. 
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

//main thread���� , 2nd, 2rd ������ ����
//main thread�� ����! -> ���μ����� ����ȴٴ� �� -> �޸� �����
void exam1()
{
	//thread 2�� ����(�����Լ� ����!, ���ڰ� �ٸ�)
	//HANDLE h1 = CreateThread(0, 0, foo, (void*)"A", 0, 0);
	//HANDLE h2 = CreateThread(0, 0, foo, (void*)"\tB", 0, 0);
	uintptr_t h1 = _beginthreadex(0, 0, foo, (void*)"A", 0, 0); //#include <process.h>
	uintptr_t h2 = _beginthreadex(0, 0, foo, (void*)"\tB", 0, 0); //#include <process.h>
}

void exam2()
{
	//thread 2�� ����(�����Լ� ����!, ���ڰ� �ٸ�)
	//HANDLE h1 = CreateThread(0, 0, foo, (void*)"A", 0, 0);
	//HANDLE h2 = CreateThread(0, 0, foo, (void*)"\tB", 0, 0);
	uintptr_t h1 = _beginthreadex(0, 0, foo, (void*)"A", 0, 0); //#include <process.h>
	uintptr_t h2 = _beginthreadex(0, 0, foo, (void*)"\tB", 0, 0); //#include <process.h>

	HANDLE h[2] = { (HANDLE)h1, (HANDLE)h2 };

	//TRUE : ��ٸ��� 2���� �� �ñ׳εǸ�(����Ǹ�) ����
	WaitForMultipleObjects(2, h, TRUE, INFINITE);

	CloseHandle((HANDLE)h1);
	CloseHandle((HANDLE)h2);
}

int main() 
{ 	
	exam2();

	return 0;
}