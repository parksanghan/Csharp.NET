//03_암시적DLL사용.cpp
/*
[복사 : 현재 소스 파일의 위치]
.h	  : 함수의 선언부
.lib  : dll 메타데이터
.dll  : 함수의 정의부
*/
#include <iostream>
using namespace std;
//DLL을 사용하기 위해서 ------------------------
#include "mycal.h"

//프로젝트 >> 속성 >> [구성속성 >> 링커 >> 입력, 추가종속성]
#pragma comment(lib, "0915_SampleDll.lib")
//----------------------------------------------

int main()
{
	int num1 = 10;
	int num2 = 20;

	float fresult = myadd(num1, num2);
	printf("덧  셈 결과 : %.0f\n", fresult);

	fresult = mysub(num1, num2);
	printf("뺄  셈 결과 : %.0f\n", fresult);

	fresult = mymul(num1, num2);
	printf("곱  셈 결과 : %.0f\n", fresult);

	fresult = mydiv(num1, num2);
	printf("나눗셈 결과 : %.1f\n", fresult);

	return 0;
}
