//03_Hello World!
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>
//Win32API�� �н��Ѵ�?
//���� ����� �ϰ� �ʹ�.(���� �׸���)
//1) �� ���� �� �� �ִ� ��ü�� Ȯ��(��(HPEN) -> ���� ���� --> ���� �ڵ�)
//2) �� ��ü�� ���ؼ� � �Լ��� ȣ���ؾ� �ϴ��� Ȯ��( MoveTo(���� �ڵ�, ....), LineTo() )

//typedef void* HINSTANCE
//H          : handle  
//INSTANCE   : �޸� ����.
//HINSTANCE hInst  : �ش� ���α׷��� �����ϸ� �޸𸮰� �����ȴ�. 
//                   �� �޸��� �ּҰ� ù��° ���ڷ� ������ �ȴ�.
//                 :[Ȱ��] �ڽ��� �ν��Ͻ��� �ڵ��̴�.
int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR cmd, int show)
{
	MessageBox(0, _TEXT("Hello, World!"), _TEXT("�˸�"), MB_OKCANCEL| MB_ICONQUESTION);

	return 0;
}