//02_start.cpp

#include <iostream>
using namespace std;
#include "02_mycal.h"

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
