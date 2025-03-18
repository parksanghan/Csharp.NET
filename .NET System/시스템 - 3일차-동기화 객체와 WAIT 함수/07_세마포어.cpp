//07_세마포어.cpp
/*
*개수 관리
*/
#include <windows.h> 
#include <stdio.h> 

int main() 
{ 
	HANDLE hSemaphore = CreateSemaphore( 0, // 보안 
										 3, // count 초기값 
										 3, // 최대 count
										TEXT("s")); // 이름 
	
	printf("세마포어를 대기합니다.\n");
	WaitForSingleObject(hSemaphore, INFINITE); //--count

	printf("세마 포어를 획득\n"); 
	
	MessageBox(0, TEXT("Release??"), TEXT(""), MB_OK);

	LONG old; 
	ReleaseSemaphore(hSemaphore, 1, &old); //++count
	
	CloseHandle( hSemaphore ); 
}