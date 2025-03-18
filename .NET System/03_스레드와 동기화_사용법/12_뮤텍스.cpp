//12_뮤텍스
#include <windows.h>
#include <iostream>
using namespace std;

int main() 
{
	// 뮤텍스 생성
	HANDLE	hMutex = CreateMutex(NULL,				// 보안속성
								FALSE,				// 아무도 뮤텍스 소유하지 않은상태->SIGNAL
								TEXT("mutex"));		// 이름(KEY)

	// 소유가 ture일때 -> Signal 을 nonsignal로 바꾼다.
	cout << "뮤택스를 기다리고 있다." << endl;
	//첫번째 실행한 프로세스는 통과
	//why? 현재 상태가 SIGNAL상태이기 때문에
	//WaitForSingleObject를 통과하는 순간
	//1) hMutex의 소유권을 통과한 스레드(프로세스)가 갖게 된다.
	//2) hMutex는 NONSIGNAL 상태로 변경.
	DWORD d = WaitForSingleObject(hMutex, INFINITE);		// 대기
	if (d == WAIT_TIMEOUT)
		MessageBox(NULL, TEXT("WAIT_TIMEOUT"), TEXT(""), MB_OK);
	else if (d == WAIT_OBJECT_0)
		MessageBox(NULL, TEXT("WAIT_OBJECT_0"), TEXT(""), MB_OK);
	else if (d == WAIT_ABANDONED_0)
		MessageBox(NULL, TEXT("WAIT_ABANDONED_0"), TEXT(""), MB_OK);
	cout << "뮤택스 획득" << endl;


	MessageBox(NULL, TEXT("뮤택스를 놓는다."), TEXT(""), MB_OK);
	//뮤텍스 소유권을 반납
	//hMenux는 SIGNAL
	//ReleaseMutex(hMutex);	
	
	return 0;
}
