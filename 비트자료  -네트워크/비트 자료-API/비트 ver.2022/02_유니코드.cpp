//02_유니코드
#include <stdio.h>
#include <tchar.h>	//범용 타입
#include <string.h>	//문자열 함수

//문자열은 기본 멀티바이트 문자열이다.
//문자열 앞에 L을 붙히면 유니코드 문자열...
/*
* [결론] 범용타입 사용하자
* TCHAR 타입사용하자
* _TEXT("문자열") : 상수 문자열 사용시
* _tcsXXX() :       문자열 함수도 범용함수로.
*/
int main()
{
	char str[10] = "12한글";					//char : 멀티바이트 1byte

	wchar_t str1[10] = L"12한글";			//wchar : 유니코드 : 2byte

	TCHAR str2[10] = _TEXT("12한글");		//TCHAR : 범용타입

	printf("%dbyte, %dbyte\n", sizeof(char), sizeof(wchar_t)); //1byte, 2byte
	printf("%dbyte, %dbyte\n", sizeof(str), sizeof(str1)); //10byte, 20byte

	//문자열 함수를 사용
	//문자의 개수를 반환
	printf("%dbyte\n", strlen(str));	//6byte
	printf("%d개\n", wcslen(str1));		//4개(문자의 개수)
	printf("%d\n", _tcslen(str2));		//4

	return 0;
}