/*
//함수 : 호출규약(함수가 호출될 때 어떻게 동작할 것인가?)
//       __cdecl 방식(C/C++함수), __stdcall 방식(Win32api에서 제공되는 시스템함수)
//      Visual Studio 기본 호출규약 : __cdecl방식
//      시스템함수 : 시스템에 의해서 호출되는 함수
#include <stdio.h>

int __cdecl main(char** argv, int argc)			//console 시작함수
{
	printf("Hello World!\n");

	return 0;
}
*/

#include <Windows.h> //Win32API 
#include <tchar.h>   //문자열을 범용적으로 사용하기 위한 h(멀티바이트 or 유니코드)

//#define WINAPI __stdcall  // 호출 규약

//_tWinMain은 현재 환경에 따라 유니코드를 사용하는 wWinMain으로 치환되거나
//                            멀티바이트를 사용하는 WinMain으로 치환된다.
//#define _tWinMain wWinMain // 유니코드(문자열 표현)
//#define _tWinMain WinMain  // 멀티바이트(문자열 표현)

//type : Win32API에서 사용되는 타입은 대문자타입..
//       기존타입의 이름을 변경한 것임
//       typedef unsigned int uint;		//단순하게 사용하기 위한 목적
//       typedef unsigned int size_t;	//타입에 의미를 부여 목적(.....)

//       typedef void* HINSTANCE 
// 
//       typedef wchar* LPWSTR    //유니코드
//       typedef char*  LPSTR    //멀티바이트
//#define LPTSTR  LPWSTR    //유니코드(문자열 표현)
//#define LPTSTR  LPSTR     //멀티바이트(문자열 표현)
int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR cmd, int show) //gui 시작함수
{
	return 0;
}