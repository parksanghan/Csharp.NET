//04_��Ӱ����� �ڵ�.cpp
/*
CreateXXX Ŀ�� ��ü�� ����� �ڽ��� �ڵ����̺� �ڵ��� ��ϵȴ�.
����� �Ұ����ϵ��� ��ϵȴ�.

1) ���ȼӼ��� �����Ͽ� ��� ������ Ŀ�� ��ü�� ����!
   �ڵ����̺� ��Ͻ� ����� ����!

2) �������� �ڵ����̺��� �ڵ��� ��� �����ϰ� ����!
*/
#include <windows.h> 

int main()
{ 
	SECURITY_ATTRIBUTES sa; 
	sa.nLength = sizeof( sa ); 
	sa.bInheritHandle = TRUE; // ��Ӱ����ϰ� 
	sa.lpSecurityDescriptor = 0; // ���� ��������. 

	HANDLE hEvent = CreateEvent(&sa, 0, 0, TEXT("e"));

	// ��Ӱ����ϰ� �ٲٱ�.. 
	SetHandleInformation( hEvent, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT); 

	CloseHandle( hEvent );

	return 0;
 }
