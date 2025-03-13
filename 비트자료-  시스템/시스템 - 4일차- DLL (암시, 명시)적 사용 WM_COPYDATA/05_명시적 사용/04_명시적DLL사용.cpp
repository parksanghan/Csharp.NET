//04_�����DLL���.cpp
/*
[���� : ���� �ҽ� ������ ��ġ]
.dll  : �Լ��� ���Ǻ�
*/
#include <Windows.h>
#include <iostream>
using namespace std; 

//DLL �� �Լ��� �ּҸ� ȹ���ϱ� ���� �Լ� ������ Ÿ�� ����
typedef float (*FUNC)(int, int);

int main()
{
	//dll�� �̸����� DLL�� ã�� �� ���μ��� �޸𸮿� �ε�!
	//hDll : �ε��� �޸� �ּ�(����� �ڵ�)
	HMODULE hDll = LoadLibrary(TEXT("0915_SampleDll.dll"));
	if (hDll == 0)
	{
		cout << "�ε尡 �ȵ�" << endl;	//�̸�����, dll�� ��ġ�� �ٸ��ų�
		return 0;
	}

	//DLL�� �Լ� ȹ��
	FUNC myadd = (FUNC)GetProcAddress(hDll, "myadd");
	FUNC mysub = (FUNC)GetProcAddress(hDll, "mysub");
	FUNC mymul = (FUNC)GetProcAddress(hDll, "mymul");
	FUNC mydiv = (FUNC)GetProcAddress(hDll, "mydiv");

	int num1 = 10;
	int num2 = 20;

	float fresult = myadd(num1, num2);
	printf("��  �� ��� : %.0f\n", fresult);

	fresult = mysub(num1, num2);
	printf("��  �� ��� : %.0f\n", fresult);

	fresult = mymul(num1, num2);
	printf("��  �� ��� : %.0f\n", fresult);

	fresult = mydiv(num1, num2);
	printf("������ ��� : %.1f\n", fresult);


	//�ش� dll�� ���μ��� �޸𸮿��� ����!
	FreeLibrary(hDll);

	return 0;
}