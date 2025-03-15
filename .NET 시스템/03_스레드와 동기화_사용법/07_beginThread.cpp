//07_beginthread.cpp
/*
* c/c++���̺귯���� ����Ѵٸ� _beginthread�� �������..
*/

#include <iostream>
#include <process.h>  // _beginthreadex() �� ����ϱ� ����..
#include <windows.h>
using namespace std;

unsigned int __stdcall foo(void* p)  // �ᱹ DWORD WINAPI foo() �̴� ~!!
{
	cout << "foo" << endl;
	Sleep(1000);
	cout << "foo finish" << endl;
	return 0;
}

int main()
{
	unsigned long h = _beginthreadex(0, 0, foo, 0, 0, 0);

	//������ �����尡 �����Ҷ����� ���.
	// h�� �ᱹ �ڵ��̴�.
	WaitForSingleObject((HANDLE)h, INFINITE);
	CloseHandle((HANDLE)h);

	cout << "2nd thread �����..." << endl; 
	return 0;
}
