#include <iostream>
#include <windows.h>
using namespace std;
/*
* 테스트 순서 : 수동이벤트.cpp 먼저 실행(Create) --> 이벤트 발생 테스트코드 나중에 실행(OPen)
*/

int main()
{
	HANDLE	hEvent = CreateEvent(NULL,		//  보안속성
								TRUE,		// 수동리셋(TRUE), 자동리셋(FALSE)
								FALSE,		// 초기 상태( NON SIGNAL )
								TEXT("e"));		// 공유할 이벤트 이름
	/*
	AUTO RESET : WaitForSingleObjecct를 통과하는 순간 Signal => NonSignal로 변경시켜 줌.

	MANUAL RESET : WaitForSingleObject를 통화하더라도 Signal 상태를 계속 유지함..
	=> 기다리던 스레드들을 다 깨워주는 역할..
	*/
	cout << "Event를 기다린다." << endl;
	WaitForSingleObject(hEvent, INFINITE);
	cout << "Event 획득 " << endl;

	cout << "Event를 기다린다." << endl;
	WaitForSingleObject(hEvent, INFINITE);
	cout << "Event 획득" << endl;

	CloseHandle(hEvent);
}
