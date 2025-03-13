//app.cpp
#include "std.h"


void app_init()
{
	logo();
	con_init();
}

void app_run()
{
	while (1)
	{
		system("cls");
		switch (menuprint())
		{
		case '1': con_account_insert();		break;
		case '2': con_account_select();		break;
		case '3': con_account_input();		break;
		case '4': con_account_output();		break;
		case '5': con_account_printall();	break;
		case '6': con_account_insert();		break;
		case '7':	return;
		}
		system("pause");
	}
}

void app_exit()
{
	ending();
	con_exit();
}

void logo()
{
	system("cls");
	printf("******************************************************\n");
	printf("계좌 관리 프로그램\n");
	printf("2023-09-20\n");
	printf("******************************************************\n");
	system("pause");
}

void ending()
{
	system("cls");
	printf("******************************************************\n");
	printf("프로그램을 종료합니다\n");
	printf("******************************************************\n");
	system("pause");
}

char menuprint()
{
	printf("******************************************************\n");
	printf("[1] 계좌 개설\n");
	printf("[2] 계좌 검색\n");
	printf("[3] 입금\n");
	printf("[4] 출금\n");
	printf("[5] 계좌 삭제\n");
	printf("[6] 계좌 전체 리스트\n");
	printf("[7] 프로그램 종료\n");
	printf("******************************************************\n");
	
	return _getch();
}

