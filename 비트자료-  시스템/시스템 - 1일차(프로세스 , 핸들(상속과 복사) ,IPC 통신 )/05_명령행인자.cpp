//05_���������.cpp
#include <stdio.h>
#include <stdlib.h>  //���� = atio("���ڿ�");    10 = atio("10");

//argc(count) : ����
//argv(value) : ���ڿ���
 
//�ܼ� : �������� ��ü ��ΰ� 0��° ���ڿ��� �ڵ� ���޵�

//����� ���� �׽�Ʈ : ������Ʈ>>�Ӽ� >> [�����Ӽ�>>����� : ����μ�]
// 111 aaa 10.2234

//���� �ܼ�â���� exe �������� ����
/*
1) exe �� �ִ� ���� ����
2) ��ܿ��� cmd �Է� �� ����(����)
   �ش� ���� ��η� �ܼ�â�� ����
3) �������ϸ��� �Է�
   0912_���μ���.exe 10 + 20
*/

int main(int argc, char** argv)
{
	/*
	for (int i = 0; i < argc; i++)
	{
		printf("%s\n", argv[i]); 
	}
	*/

	if (argc != 4)
	{
		printf("���� ��� >>  .exe 10 + 3\n");
		return 0;
	}

	int num1 = atoi(argv[1]);
	char oper = argv[2][0];
	int num2 = atoi(argv[3]);

	switch (oper)
	{
	case '+':	printf("%d %c %d = %d\n", num1, oper, num2, num1 + num2); break;
	case '-':	printf("%d %c %d = %d\n", num1, oper, num2, num1 - num2); break;
	}

	return 0;
}