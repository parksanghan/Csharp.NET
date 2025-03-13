//control.cpp
#include "std.h"

void con_init()
{

}

void con_exit()
{

}

void con_account_insert()
{
	int accid;
	char name[20];
	int money;

	printf("계좌번호 : ");		scanf_s("%d", &accid);
	char dummy = getchar();
	printf("이름 : ");		gets_s(name, sizeof(name));
	printf("입금액 : ");		scanf_s("%d", &money);

	////2. 저장할 타입 변수 선언 및 초기화
	//Account* acc = new Account(accid, name, money);

	////3. 저장 요청 및 결과 출력
	//if (arr.push_back(acc) == true)
	//	cout << "저장되었습니다" << endl;
	//else
	//	cout << "저장공간이 부족합니다" << endl;

}

void con_account_select()
{
	int accid;
	printf("계좌번호 : ");		scanf_s("%d", &accid);
	
	/*int idx = accid_to_idx(accid);
	if (idx == -1)
	{
		cout << "없는 계좌번호입니다" << endl;
	}
	else
	{
		Account* acc = (Account*)arr.get(idx);
		acc->println();
	}*/

}

void con_account_input()
{
	int accid;
	int money;

	printf("계좌번호 : ");		scanf_s("%d", &accid);
	printf("입금액 : ");		scanf_s("%d", &money);
	/*if (idx == -1)
	{
		cout << "없는 계좌번호입니다" << endl;
	}
	else
	{
		int money = MyLib::input_integer("입금액");
		Account* acc = (Account*)arr.get(idx);
		if (acc->input_money(money) == true)
			cout << "입금성공" << endl;
		else
			cout << "입금실패" << endl;
	}*/

}

void con_account_output()
{
	int accid;
	int money;

	printf("계좌번호 : ");		scanf_s("%d", &accid);
	printf("출금액 : ");		scanf_s("%d", &money);

	/*int idx = accid_to_idx(accid);
	if (idx == -1)
	{
		cout << "없는 계좌번호입니다" << endl;
	}
	else
	{
		int money = MyLib::input_integer("출금액");
		Account* acc = (Account*)arr.get(idx);
		if (acc->output_money(money) == true)
			cout << "출금성공" << endl;
		else
			cout << "출금실패" << endl;
	}*/

}

void con_account_delete()
{
	int accid;
	printf("계좌번호 : ");		scanf_s("%d", &accid);

	/*int idx = accid_to_idx(accid);
	if (idx == -1)
	{
		cout << "없는 계좌번호입니다" << endl;
	}
	else
	{
		arr.erase(idx);
		cout << "삭제되었습니다" << endl;
	}*/


}

void con_account_printall()
{
	/*for (int i = 0; i < arr.get_size(); i++)
	{
		Account* acc = (Account*)arr.get(i);
		acc->print();
	}
	cout << endl;*/

}

int accid_to_idx(int accid)
{
	/*for (int i = 0; i < arr.get_size(); i++)
	{
		Account* acc = (Account*)arr.get(i);
		if (acc->get_accid() == accid)
			return i;
	}*/
	return -1;
}