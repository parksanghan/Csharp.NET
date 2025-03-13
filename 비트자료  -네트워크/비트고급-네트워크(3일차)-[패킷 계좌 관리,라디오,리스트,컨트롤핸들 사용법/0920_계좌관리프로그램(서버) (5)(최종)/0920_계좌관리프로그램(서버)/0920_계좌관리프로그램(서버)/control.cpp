//control.cpp
#include "std.h"

#define SERVER_PORT     9000

vector<Account> accounts;

int con_Start()
{
    if (net_init(SERVER_PORT) == -1)
    {
        printf("�ʱ�ȭ ����\n");
        return 0;
    }

    printf("���� ����...........\n");
    net_run();

    return 1;
}

void con_Stop()
{
    net_exit();
}

//������ ó�� ������(������ �Ǻ�)
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


//�����ͺ� ó�� �Լ�
void con_account_insert(SOCKET sock, pack_ACCOUNT_INSERT* pdata)
{	
	//1. Account ��ü ����
	Account acc = acc_MakeAccount(pdata->accid, pdata->name, pdata->money);

	//2. ����
	accounts.push_back(acc);

	//3. ��ü���
	account_print();	
	//------------------------------------------------------------------------
	//[����ó��]
	//a) ��Ŷ ���� -> b)���� net_SendData ȣ��()
	pack_ACCOUNT_INSERT pack =  pack_Account_Insert(1, acc.accid, acc.name, acc.balance);
	net_SendData(sock, (char*)&pack, sizeof(pack));
	
	//WorkThread�� �ִ� ������ ���� ����!
	//WorkThread�� �� ���� net_SendData�� ȣ���ϰ� �ִ�.
	//*pdata = pack_Account_Insert(1, acc.accid, acc.name, acc.balance);	
}

void con_account_delete(SOCKET sock, pack_ACCOUNT_DELETE* pdata)
{
	//������ ó��!
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

	//[���� ó��]
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

	//[���� ó��]
	net_SendData(sock, (char*)&pack, sizeof(pack));
}

void con_account_input(SOCKET sock, pack_ACCOUNT_INPUT* pdata)
{	
	pack_ACCOUNT_INPUT pack;

	int idx = accid_to_idx(pdata->accid);
	if (idx == -1)
	{
		//���� ���¹�ȣ
		pack = pack_Account_Input(0, pdata->accid, -1);	//���� ���¹�ȣ
	}	
	else
	{
		if (acc_input_money(&accounts[idx], pdata->money) == 1)
		{
			//����
			pack = pack_Account_Input(1, pdata->accid, 1);
			account_print();
		}
		else
		{
			//����
			pack = pack_Account_Input(0, pdata->accid, -2);	//�߸��ȱݾ�
		}
	}

	//[���� ó��]
	net_SendData(sock, (char*)&pack, sizeof(pack));
}

void con_account_output(SOCKET sock, pack_ACCOUNT_OUTPUT* pdata)
{
	pack_ACCOUNT_OUTPUT pack;

	int idx = accid_to_idx(pdata->accid);
	if (idx == -1)
	{
		//���� ���¹�ȣ
		pack = pack_Account_Output(0, pdata->accid, -1);	//���� ���¹�ȣ
	}
	else
	{
		if (acc_output_money(&accounts[idx], pdata->money) == 1)
		{
			//����
			pack = pack_Account_Output(0, pdata->accid, 1);
			account_print();
		}
		else
		{
			//����
			pack = pack_Account_Output(0, pdata->accid, -2);	//�߸��ȱݾ�
		}
	}
}

void con_account_printall(SOCKET sock, pack_ACCOUNT_PRINTALL* pdata)
{
	//[���� ó��]
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
	printf("���尳�� : %d\n", (int)accounts.size());
	for (int i = 0; i < accounts.size(); i++)
	{
		Account acc = accounts[i];
		acc_print(&acc);
	}
}