//07_��������.cpp
/*
*���� ����
*/
#include <windows.h> 
#include <stdio.h> 

int main() 
{ 
	HANDLE hSemaphore = CreateSemaphore( 0, // ���� 
										 3, // count �ʱⰪ 
										 3, // �ִ� count
										TEXT("s")); // �̸� 
	
	printf("������� ����մϴ�.\n");
	WaitForSingleObject(hSemaphore, INFINITE); //--count

	printf("���� ��� ȹ��\n"); 
	
	MessageBox(0, TEXT("Release??"), TEXT(""), MB_OK);

	LONG old; 
	ReleaseSemaphore(hSemaphore, 1, &old); //++count
	
	CloseHandle( hSemaphore ); 
}