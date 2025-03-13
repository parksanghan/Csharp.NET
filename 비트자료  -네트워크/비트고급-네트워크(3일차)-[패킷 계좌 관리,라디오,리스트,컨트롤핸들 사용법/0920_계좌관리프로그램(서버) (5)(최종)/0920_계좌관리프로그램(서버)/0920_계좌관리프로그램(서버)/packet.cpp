//packet.cpp
#include "std.h"

pack_ACCOUNT_INSERT pack_Account_Insert(int result, int accid, const char* name, int money)
{
	pack_ACCOUNT_INSERT pack;

	if( result == 1)
		pack.flag = ACK_ACCOUNT_INSERT_S;
	else
		pack.flag = ACK_ACCOUNT_INSERT_F;

	pack.accid = accid;
	strcpy_s(pack.name, sizeof(pack.name), name);
	pack.money = money;

	return pack;
}

pack_ACCOUNT_DELETE pack_Account_Delete(int result, int accid)
{
	pack_ACCOUNT_DELETE pack;

	if (result == 1)
		pack.flag = ACK_ACCOUNT_DELETE_S;
	else
		pack.flag = ACK_ACCOUNT_DELETE_F;

	pack.accid = accid;

	return pack;
}

pack_ACCOUNT_SELECT_ACK pack_Account_Select(int result, int accid, const char* name, int money)
{
	pack_ACCOUNT_SELECT_ACK pack;

	if (result == 1)
	{
		pack.flag = ACK_ACCOUNT_SELECT_S;
	}
	else
	{
		pack.flag = ACK_ACCOUNT_SELECT_F;
	}

	pack.accid = accid;
	strcpy_s(pack.name, sizeof(pack.name), name);
	pack.money = money;

	return pack;
}

pack_ACCOUNT_INPUT pack_Account_Input(int result, int accid, int money)
{
	pack_ACCOUNT_INPUT pack;

	if (result == 1)
	{
		pack.flag = ACK_ACCOUNT_INPUT_S;
	}
	else
	{
		pack.flag = ACK_ACCOUNT_INPUT_F;
		
	}	
	pack.accid = accid;
	pack.money = money;	//에러 원인!!

	return pack;
}

pack_ACCOUNT_OUTPUT pack_Account_Output(int result, int accid, int money)
{
	pack_ACCOUNT_OUTPUT pack;

	if (result == 1)
	{
		pack.flag = ACK_ACCOUNT_OUTPUT_S;
	}
	else
	{
		pack.flag = ACK_ACCOUNT_OUTPUT_F;
		
	}
	pack.accid = accid;
	pack.money = money;	//에러 원인!!

	return pack;
}

pack_ACCOUNT_PRINTALL_ACK pack_Account_PrintAll(const vector<Account>& accounts)
{
	pack_ACCOUNT_PRINTALL_ACK pack;

	pack.flag = ACK_ACCOUNT_PRINTALL_S;
	pack.size = (int)accounts.size();
	for (int i = 0; i < accounts.size(); i++)
	{
		pack.arr[i] = accounts[i];
	}

	return pack;
}