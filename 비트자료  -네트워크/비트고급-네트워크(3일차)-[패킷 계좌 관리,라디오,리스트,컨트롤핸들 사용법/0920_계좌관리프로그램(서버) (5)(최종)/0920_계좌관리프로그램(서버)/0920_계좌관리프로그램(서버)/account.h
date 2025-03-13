//account.h
#pragma once

struct Account
{
	int accid;
	char name[20];
	int balance;
};

Account acc_MakeAccount(int _accid, const char* _name, int _balance);
bool acc_input_money(Account* acc, int money);
bool acc_output_money(Account* acc, int money);
void acc_print(Account* acc);
void acc_println(Account* acc);

