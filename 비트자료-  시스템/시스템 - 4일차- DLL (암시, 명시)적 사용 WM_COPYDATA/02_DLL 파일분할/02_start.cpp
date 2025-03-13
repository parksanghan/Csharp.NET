//02_start.cpp

#include <iostream>
using namespace std;
#include "02_mycal.h"

int main()
{
	int num1 = 10;
	int num2 = 20;

	float fresult = myadd(num1, num2);
	printf("µ¡  ¼À °á°ú : %.0f\n", fresult);

	fresult = mysub(num1, num2);
	printf("»¬  ¼À °á°ú : %.0f\n", fresult);

	fresult = mymul(num1, num2);
	printf("°ö  ¼À °á°ú : %.0f\n", fresult);

	fresult = mydiv(num1, num2);
	printf("³ª´°¼À °á°ú : %.1f\n", fresult);

	return 0;
}
