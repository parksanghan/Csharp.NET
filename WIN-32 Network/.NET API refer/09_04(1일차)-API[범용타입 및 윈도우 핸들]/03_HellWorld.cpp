//03_Hello World!
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>
//Win32API를 학습한다?
//내가 어떤일을 하고 싶다.(선을 그린다)
//1) 그 일을 할 수 있는 객체를 확인(펜(HPEN) -> 펜을 생성 --> 펜의 핸들)
//2) 그 객체를 통해서 어떤 함수를 호출해야 하는지 확인( MoveTo(펜의 핸들, ....), LineTo() )

//typedef void* HINSTANCE
//H          : handle  
//INSTANCE   : 메모리 생성.
//HINSTANCE hInst  : 해당 프로그램을 실행하면 메모리가 생성된다. 
//                   그 메모리의 주소가 첫번째 인자로 전달이 된다.
//                 :[활용] 자신의 인스턴스의 핸들이다.
int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR cmd, int show)
{
	MessageBox(0, _TEXT("Hello, World!"), _TEXT("알림"), MB_OKCANCEL| MB_ICONQUESTION);

	return 0;
}