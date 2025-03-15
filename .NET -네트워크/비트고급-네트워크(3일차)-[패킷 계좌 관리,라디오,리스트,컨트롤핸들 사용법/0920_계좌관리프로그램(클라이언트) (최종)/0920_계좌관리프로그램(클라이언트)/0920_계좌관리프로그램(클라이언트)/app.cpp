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
	printf("���� ���� ���α׷�\n");
	printf("2023-09-20\n");
	printf("******************************************************\n");
	system("pause");
}

void ending()
{
	system("cls");
	printf("******************************************************\n");
	printf("���α׷��� �����մϴ�\n");
	printf("******************************************************\n");
	system("pause");
}

char menuprint()
{
	printf("******************************************************\n");
	printf("[1] ���� ����\n");
	printf("[2] ���� �˻�\n");
	printf("[3] �Ա�\n");
	printf("[4] ���\n");
	printf("[5] ���� ����\n");
	printf("[6] ���� ��ü ����Ʈ\n");
	printf("[7] ���α׷� ����\n");
	printf("******************************************************\n");
	
	return _getch();
}

