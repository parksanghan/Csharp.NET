//08_수동이벤트.cpp
/*
자고 있던 모든 스레드(프로세스)를 한번에 깨울 수 있다.

WaitForSingleObject(h)
- 통과하는 순간 자동으로 h의 상태를 Non Signal로 변경해준다.

-[수동] WaitForSingleObject를 통과해도 상태변화가 없다.
        따라서 기다리던 모든 스레드가 다 통과된다.
*/
#include <iostream> 
#include <windows.h> 

using namespace std; 
void main() 
{ 
    HANDLE hEvent = CreateEvent(NULL, // 보안속성 
                                TRUE, // 수동리셋(TRUE), 자동리셋(FALSE) 
                                FALSE, // 초기 상태( NON SIGNAL ) 
                                TEXT("e")); // 공유할 이벤트 이름 
    
    /* AUTO RESET : WaitForSingleObjecct를 통과하는 순간 
                    Signal => NonSignal로 변경시켜 줌.
       MANUAL RESET : WaitForSingleObject를 통화하더라도 Signal 상태를 계속 유지
                        = > 기다리던 스레드들을 다 깨워주는 역할.. 
    */ 
    cout << "Event를 기다린다." << endl; 
    WaitForSingleObject(hEvent, INFINITE); 

    cout << "Event 획득 " << endl;
    cout << "Event를 기다린다." << endl;

    WaitForSingleObject(hEvent, INFINITE); 
    cout << "Event 획득" << endl;

    CloseHandle(hEvent); 
}