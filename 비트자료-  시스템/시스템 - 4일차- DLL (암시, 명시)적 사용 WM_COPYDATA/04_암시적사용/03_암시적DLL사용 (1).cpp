//03_�Ͻ���DLL���.cpp
/*
[���� : ���� �ҽ� ������ ��ġ]
.h	  : �Լ��� �����
.lib  : dll ��Ÿ������
.dll  : �Լ��� ���Ǻ�
*/
#include <iostream>
using namespace std;
//DLL�� ����ϱ� ���ؼ� ------------------------
#include "mycal.h"

//������Ʈ >> �Ӽ� >> [�����Ӽ� >> ��Ŀ >> �Է�, �߰����Ӽ�]
#pragma comment(lib, "0915_SampleDll.lib")
//----------------------------------------------

int main()
{
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

	return 0;
}
