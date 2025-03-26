//acount.cpp
#include "std.h"

Account acc_MakeAccount(int _accid, const char* _name, int _balance)
{
	Account acc;

	acc.accid = _accid;
	strcpy_s(acc.name, sizeof(acc.name), _name);
	acc.balance = _balance;

	return acc;
}

bool acc_input_money(Account* acc, int money)
{
	if (money <= 0)
		return false;

	acc->balance = acc->balance + money;
	return true;
}

bool acc_output_money(Account* acc, int money)
{
	if (money <= 0)
		return false;

	if (acc->balance < money)
		return false;

	acc->balance = acc->balance - money;
	return true;
}

void acc_print(Account* acc)
{
	printf("%d\t %s\t %d��\n", acc->accid, acc->name, acc->balance);

}

void acc_println(Account* acc)
{
	printf("���¹�ȣ : %d\n", acc->accid);
	printf("��    �� : %s\n", acc->name);
	printf("��    �� : %d��\n", acc->balance);
}