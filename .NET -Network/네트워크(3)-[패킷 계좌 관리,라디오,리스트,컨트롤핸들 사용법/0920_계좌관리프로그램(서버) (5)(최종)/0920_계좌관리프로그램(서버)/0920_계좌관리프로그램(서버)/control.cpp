//control.cpp
#include "std.h"

#define SERVER_PORT     9000

vector<Account> accounts;

int con_Start()
{
    if (net_init(SERVER_PORT) == -1)
    {
        printf("초기화 오류\n");
        return 0;
    }

    printf("서버 구동...........\n");
    net_run();

    return 1;
}

void con_Stop()
{
    net_exit();
}

//데이터 처리 시작점(데이터 판별)
void con_RecvData(SOCKET sock, char* msg)
{
    int* flag = (int*)msg; 

    if (*flag == PACK_ACCOUNT_INSERT)
    {
        pack_ACCOUNT_INSERT* pdata = (pack_ACCOUNT_INSERT*)msg;
		con_account_insert(sock, pdata);
     
    }
    else if (*flag == PACK_ACCOUNT_DELETE)
    {
        pack_ACCOUNT_DELETE* pdata = (pack_ACCOUNT_DELETE*)msg;
		con_account_delete(sock, pdata);
    }
    else if (*flag == PACK_ACCOUNT_SELECT)
    {
        pack_ACCOUNT_SELECT* pdata = (pack_ACCOUNT_SELECT*)msg;
		con_account_select(sock, pdata);
    }
    else if (*flag == PACK_ACCOUNT_INPUT)
    {
        pack_ACCOUNT_INPUT* pdata = (pack_ACCOUNT_INPUT*)msg;
		con_account_input(sock, pdata);
    }
    else if (*flag == PACK_ACCOUNT_OUTPUT)
    {
        pack_ACCOUNT_OUTPUT* pdata = (pack_ACCOUNT_OUTPUT*)msg;
		con_account_output(sock, pdata);
    }
    else if (*flag == PACK_ACCOUNT_PRINTALL)
    {
        pack_ACCOUNT_PRINTALL* pdata = (pack_ACCOUNT_PRINTALL*)msg;
		con_account_printall(sock, pdata);
    }
}


//데이터별 처리 함수
void con_account_insert(SOCKET sock, pack_ACCOUNT_INSERT* pdata)
{	
	//1. Account 객체 생성
	Account acc = acc_MakeAccount(pdata->accid, pdata->name, pdata->money);

	//2. 저장
	accounts.push_back(acc);

	//3. 전체출력
	account_print();	
	//------------------------------------------------------------------------
	//[응답처리]
	//a) 패킷 생성 -> b)직접 net_SendData 호출()
	pack_ACCOUNT_INSERT pack =  pack_Account_Insert(1, acc.accid, acc.name, acc.balance);
	net_SendData(sock, (char*)&pack, sizeof(pack));
	
	//WorkThread에 있는 원본의 값을 변경!
	//WorkThread는 그 이후 net_SendData를 호출하고 있다.
	//*pdata = pack_Account_Insert(1, acc.accid, acc.name, acc.balance);	
}

void con_account_delete(SOCKET sock, pack_ACCOUNT_DELETE* pdata)
{
	//데이터 처리!
	int result;
	int idx = accid_to_idx(pdata->accid);
	if (idx == -1)
	{
		result = 0;
	}
	else
	{		
		accounts.erase(accounts.begin() + idx);
		result = 1;
		account_print();
	}

	//[응답 처리]
	pack_ACCOUNT_DELETE pack = pack_Account_Delete(result, pdata->accid);
	
	net_SendData(sock, (char*)&pack, sizeof(pack));
}

void con_account_select(SOCKET sock, pack_ACCOUNT_SELECT* pdata)
{
	pack_ACCOUNT_SELECT_ACK pack;

	int idx = accid_to_idx(pdata->accid);
	if (idx == -1)
	{
		pack = pack_Account_Select(0, pdata->accid, "", 0);
	}
	else
	{
		Account acc = accounts[idx];	
		pack = pack_Account_Select(1, acc.accid, acc.name, acc.balance);
	}

	//[응답 처리]
	net_SendData(sock, (char*)&pack, sizeof(pack));
}

void con_account_input(SOCKET sock, pack_ACCOUNT_INPUT* pdata)
{	
	pack_ACCOUNT_INPUT pack;

	int idx = accid_to_idx(pdata->accid);
	if (idx == -1)
	{
		//없는 계좌번호
		pack = pack_Account_Input(0, pdata->accid, -1);	//없는 계좌번호
	}	
	else
	{
		if (acc_input_money(&accounts[idx], pdata->money) == 1)
		{
			//성공
			pack = pack_Account_Input(1, pdata->accid, 1);
			account_print();
		}
		else
		{
			//실패
			pack = pack_Account_Input(0, pdata->accid, -2);	//잘못된금액
		}
	}

	//[응답 처리]
	net_SendData(sock, (char*)&pack, sizeof(pack));
}

void con_account_output(SOCKET sock, pack_ACCOUNT_OUTPUT* pdata)
{
	pack_ACCOUNT_OUTPUT pack;

	int idx = accid_to_idx(pdata->accid);
	if (idx == -1)
	{
		//없는 계좌번호
		pack = pack_Account_Output(0, pdata->accid, -1);	//없는 계좌번호
	}
	else
	{
		if (acc_output_money(&accounts[idx], pdata->money) == 1)
		{
			//성공
			pack = pack_Account_Output(0, pdata->accid, 1);
			account_print();
		}
		else
		{
			//실패
			pack = pack_Account_Output(0, pdata->accid, -2);	//잘못된금액
		}
	}
}

void con_account_printall(SOCKET sock, pack_ACCOUNT_PRINTALL* pdata)
{
	//[응답 처리]
	pack_ACCOUNT_PRINTALL_ACK pack = pack_Account_PrintAll(accounts);

	net_SendData(sock, (char*)&pack, sizeof(pack));
}

int accid_to_idx(int accid)
{
	for (int i = 0; i < accounts.size(); i++)
	{
		Account acc = accounts[i];
		if (acc.accid == accid)
			return i;
	}
	return -1;
}


void account_print()
{
	system("cls");
	printf("저장개수 : %d\n", (int)accounts.size());
	for (int i = 0; i < accounts.size(); i++)
	{
		Account acc = accounts[i];
		acc_print(&acc);
	}
}