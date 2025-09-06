//04_상속가능한 핸들.cpp
/*
CreateXXX 커널 객체를 만들면 자신의 핸들테이블에 핸들이 등록된다.
상속이 불가능하도록 등록된다.

1) 보안속성을 설정하여 상속 가능한 커널 객체를 생성!
   핸들테이블에 등록시 상속이 가능!

2) 동적으로 핸들테이블의 핸들을 상속 가능하게 변경!
*/
#include <windows.h> 

int main()
{ 
	SECURITY_ATTRIBUTES sa; 
	sa.nLength = sizeof( sa ); 
	sa.bInheritHandle = TRUE; // 상속가능하게 
	sa.lpSecurityDescriptor = 0; // 실제 보안정보. 

	HANDLE hEvent = CreateEvent(&sa, 0, 0, TEXT("e"));

	// 상속가능하게 바꾸기.. 
	SetHandleInformation( hEvent, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT); 

	CloseHandle( hEvent );

	return 0;
 }
