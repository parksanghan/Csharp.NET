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

	printf("���¹�ȣ : ");		scanf_s("%d", &accid);
	char dummy = getchar();
	printf("�̸� : ");		gets_s(name, sizeof(name));
	printf("�Աݾ� : ");		scanf_s("%d", &money);

	////2. ������ Ÿ�� ���� ���� �� �ʱ�ȭ
	//Account* acc = new Account(accid, name, money);

	////3. ���� ��û �� ��� ���
	//if (arr.push_back(acc) == true)
	//	cout << "����Ǿ����ϴ�" << endl;
	//else
	//	cout << "��������� �����մϴ�" << endl;

}

void con_account_select()
{
	int accid;
	printf("���¹�ȣ : ");		scanf_s("%d", &accid);
	
	/*int idx = accid_to_idx(accid);
	if (idx == -1)
	{
		cout << "���� ���¹�ȣ�Դϴ�" << endl;
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

	printf("���¹�ȣ : ");		scanf_s("%d", &accid);
	printf("�Աݾ� : ");		scanf_s("%d", &money);
	/*if (idx == -1)
	{
		cout << "���� ���¹�ȣ�Դϴ�" << endl;
	}
	else
	{
		int money = MyLib::input_integer("�Աݾ�");
		Account* acc = (Account*)arr.get(idx);
		if (acc->input_money(money) == true)
			cout << "�Աݼ���" << endl;
		else
			cout << "�Աݽ���" << endl;
	}*/

}

void con_account_output()
{
	int accid;
	int money;

	printf("���¹�ȣ : ");		scanf_s("%d", &accid);
	printf("��ݾ� : ");		scanf_s("%d", &money);

	/*int idx = accid_to_idx(accid);
	if (idx == -1)
	{
		cout << "���� ���¹�ȣ�Դϴ�" << endl;
	}
	else
	{
		int money = MyLib::input_integer("��ݾ�");
		Account* acc = (Account*)arr.get(idx);
		if (acc->output_money(money) == true)
			cout << "��ݼ���" << endl;
		else
			cout << "��ݽ���" << endl;
	}*/

}

void con_account_delete()
{
	int accid;
	printf("���¹�ȣ : ");		scanf_s("%d", &accid);

	/*int idx = accid_to_idx(accid);
	if (idx == -1)
	{
		cout << "���� ���¹�ȣ�Դϴ�" << endl;
	}
	else
	{
		arr.erase(idx);
		cout << "�����Ǿ����ϴ�" << endl;
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