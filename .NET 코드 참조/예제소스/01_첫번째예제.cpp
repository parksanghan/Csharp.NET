/*
//�Լ� : ȣ��Ծ�(�Լ��� ȣ��� �� ��� ������ ���ΰ�?)
//       __cdecl ���(C/C++�Լ�), __stdcall ���(Win32api���� �����Ǵ� �ý����Լ�)
//      Visual Studio �⺻ ȣ��Ծ� : __cdecl���
//      �ý����Լ� : �ý��ۿ� ���ؼ� ȣ��Ǵ� �Լ�
#include <stdio.h>

int __cdecl main(char** argv, int argc)			//console �����Լ�
{
	printf("Hello World!\n");

	return 0;
}
*/

#include <Windows.h> //Win32API 
#include <tchar.h>   //���ڿ��� ���������� ����ϱ� ���� h(��Ƽ����Ʈ or �����ڵ�)

//#define WINAPI __stdcall  // ȣ�� �Ծ�

//_tWinMain�� ���� ȯ�濡 ���� �����ڵ带 ����ϴ� wWinMain���� ġȯ�ǰų�
//                            ��Ƽ����Ʈ�� ����ϴ� WinMain���� ġȯ�ȴ�.
//#define _tWinMain wWinMain // �����ڵ�(���ڿ� ǥ��)
//#define _tWinMain WinMain  // ��Ƽ����Ʈ(���ڿ� ǥ��)

//type : Win32API���� ���Ǵ� Ÿ���� �빮��Ÿ��..
//       ����Ÿ���� �̸��� ������ ����
//       typedef unsigned int uint;		//�ܼ��ϰ� ����ϱ� ���� ����
//       typedef unsigned int size_t;	//Ÿ�Կ� �ǹ̸� �ο� ����(.....)

//       typedef void* HINSTANCE 
// 
//       typedef wchar* LPWSTR    //�����ڵ�
//       typedef char*  LPSTR    //��Ƽ����Ʈ
//#define LPTSTR  LPWSTR    //�����ڵ�(���ڿ� ǥ��)
//#define LPTSTR  LPSTR     //��Ƽ����Ʈ(���ڿ� ǥ��)
int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR cmd, int show) //gui �����Լ�
{
	return 0;
}