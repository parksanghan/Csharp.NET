//06_뮤텍스.cpp
/*
프로세스와 프로세스 간 동기화
2번 실행!
첫번째 실행 : CreateMutex("mutex")이름의 객체 생성
두번째 실행 : CreateMutex("mutex")이름의 객체 생성하려고 하니
              이미 "mutex"라는 이름의 객체가 생성되어 있다.
			  -> OpenMutex로 동작하게 된다.
              
*/

#include <windows.h> 
#include <iostream> 
using namespace std; 

int main()
{ 
	// 뮤텍스 생성 
	HANDLE hMutex = CreateMutex(NULL, // 보안속성
								FALSE, // 생성시 뮤텍스 소유 여부 
								TEXT("mutex")); // 이름 			
	
	cout << "뮤택스를 기다리고 있다." << endl; 
	
	//hMutex : Signal 상태면 통과
	//만약, 내가 Mutex를 소유하고 있다면(hMutex : Non Signal상황임)
	//통과할 수 있다. 
	DWORD d = WaitForSingleObject(hMutex, INFINITE); // 대기 
	if( d == WAIT_TIMEOUT) 
		MessageBox(NULL, TEXT("WAIT_TIMEOUT"), TEXT(""), MB_OK); 
	else if( d ==WAIT_OBJECT_0) 
		MessageBox(NULL, TEXT("WAIT_OBJECT_0"), TEXT(""), MB_OK); 
	else if( d == WAIT_ABANDONED_0) 
		MessageBox(NULL, TEXT("WAIT_ABANDONED_0"), TEXT(""), MB_OK); 
	
	cout << "뮤택스 획득" << endl; 
	
	MessageBox(NULL, TEXT("뮤택스를 놓는다.") , TEXT(""), MB_OK); 
	
	//ReleaseMutex(hMutex); 

	return 0;
}