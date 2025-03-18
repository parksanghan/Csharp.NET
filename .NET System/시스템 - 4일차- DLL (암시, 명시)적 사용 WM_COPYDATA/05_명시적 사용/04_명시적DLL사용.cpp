//04_명시적DLL사용.cpp
/*
[복사 : 현재 소스 파일의 위치]
.dll  : 함수의 정의부
*/
#include <Windows.h>
#include <iostream>
using namespace std; 

//DLL 내 함수의 주소를 획득하기 위한 함수 포인터 타입 정의
typedef float (*FUNC)(int, int);

int main()
{
	//dll의 이름으로 DLL을 찾아 내 프로세스 메모리에 로딩!
	//hDll : 로딩된 메모리 주소(모듈의 핸들)
	HMODULE hDll = LoadLibrary(TEXT("0915_SampleDll.dll"));
	if (hDll == 0)
	{
		cout << "로드가 안됨" << endl;	//이름오류, dll의 위치가 다르거나
		return 0;
	}

	//DLL의 함수 획득
	FUNC myadd = (FUNC)GetProcAddress(hDll, "myadd");
	FUNC mysub = (FUNC)GetProcAddress(hDll, "mysub");
	FUNC mymul = (FUNC)GetProcAddress(hDll, "mymul");
	FUNC mydiv = (FUNC)GetProcAddress(hDll, "mydiv");

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


	//해당 dll을 프로세스 메모리에서 제외!
	FreeLibrary(hDll);

	return 0;
}