//02_�����ڵ�
#include <stdio.h>
#include <tchar.h>	//���� Ÿ��
#include <string.h>	//���ڿ� �Լ�

//���ڿ��� �⺻ ��Ƽ����Ʈ ���ڿ��̴�.
//���ڿ� �տ� L�� ������ �����ڵ� ���ڿ�...
/*
* [���] ����Ÿ�� �������
* TCHAR Ÿ�Ի������
* _TEXT("���ڿ�") : ��� ���ڿ� ����
* _tcsXXX() :       ���ڿ� �Լ��� �����Լ���.
*/
int main()
{
	char str[10] = "12�ѱ�";					//char : ��Ƽ����Ʈ 1byte

	wchar_t str1[10] = L"12�ѱ�";			//wchar : �����ڵ� : 2byte

	TCHAR str2[10] = _TEXT("12�ѱ�");		//TCHAR : ����Ÿ��

	printf("%dbyte, %dbyte\n", sizeof(char), sizeof(wchar_t)); //1byte, 2byte
	printf("%dbyte, %dbyte\n", sizeof(str), sizeof(str1)); //10byte, 20byte

	//���ڿ� �Լ��� ���
	//������ ������ ��ȯ
	printf("%dbyte\n", strlen(str));	//6byte
	printf("%d��\n", wcslen(str1));		//4��(������ ����)
	printf("%d\n", _tcslen(str2));		//4

	return 0;
}