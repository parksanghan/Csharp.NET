//07_beginthread.cpp
/*
* c/c++라이브러리를 사용한다면 _beginthread를 사용하자..
*/

#include <iostream>
#include <process.h>  // _beginthreadex() 를 사용하기 위해..
#include <windows.h>
using namespace std;

unsigned int __stdcall foo(void* p)  // 결국 DWORD WINAPI foo() 이다 ~!!
{
	cout << "foo" << endl;
	Sleep(1000);
	cout << "foo finish" << endl;
	return 0;
}

int main()
{
	unsigned long h = _beginthreadex(0, 0, foo, 0, 0, 0);

	//생성된 스레드가 종료할때까지 대기.
	// h가 결국 핸들이다.
	WaitForSingleObject((HANDLE)h, INFINITE);
	CloseHandle((HANDLE)h);

	cout << "2nd thread 종료됨..." << endl; 
	return 0;
}
