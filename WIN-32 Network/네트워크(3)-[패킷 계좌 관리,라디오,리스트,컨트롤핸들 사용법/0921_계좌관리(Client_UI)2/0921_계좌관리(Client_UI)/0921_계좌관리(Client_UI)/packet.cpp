//packet.cpp
#include "std.h"

pack_ACCOUNT_INSERT pack_Account_Insert(int accid, const char* name, int money)
{
	pack_ACCOUNT_INSERT pack;

	pack.flag = PACK_ACCOUNT_INSERT;
	pack.accid = accid;
	strcpy_s(pack.name, sizeof(pack.name), name);
	pack.money = money;

	return pack;
}

pack_ACCOUNT_DELETE pack_Account_Delete(int accid)
{
	pack_ACCOUNT_DELETE pack;

	pack.flag = PACK_ACCOUNT_DELETE;
	pack.accid = accid;

	return pack;
}

pack_ACCOUNT_SELECT pack_Account_Select(int accid)
{
	pack_ACCOUNT_SELECT pack;

	pack.flag = PACK_ACCOUNT_SELECT;
	pack.accid = accid;

	return pack;
}

pack_ACCOUNT_INPUT pack_Account_Input(int accid, int money)
{
	pack_ACCOUNT_INPUT pack;

	pack.flag = PACK_ACCOUNT_INPUT;
	pack.accid = accid;
	pack.money = money;

	return pack;
}

pack_ACCOUNT_OUTPUT pack_Account_Output(int accid, int money)
{
	pack_ACCOUNT_OUTPUT pack;

	pack.flag = PACK_ACCOUNT_OUTPUT;
	pack.accid = accid;
	pack.money = money;

	return pack;
}

pack_ACCOUNT_PRINTALL pack_Account_PrintAll()
{
	pack_ACCOUNT_PRINTALL pack;

	pack.flag = PACK_ACCOUNT_PRINTALL;

	return pack;
}