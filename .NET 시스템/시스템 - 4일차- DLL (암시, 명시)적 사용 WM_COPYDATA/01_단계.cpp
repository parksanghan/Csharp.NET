//01_�ܰ�.cpp

#include <iostream>
using namespace std;

float myadd(int n1, int n2);
float mysub(int n1, int n2);
float mymul(int n1, int n2);
float mydiv(int n1, int n2);

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

float myadd(int n1, int n2) { return (float)n1 + n2; }
float mysub(int n1, int n2) { return (float)n1 - n2; }
float mymul(int n1, int n2) { return (float)n1 * n2; }
float mydiv(int n1, int n2) { return (float)n1 / n2; }