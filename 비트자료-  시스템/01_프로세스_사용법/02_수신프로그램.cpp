//02_�������α׷�
//�Ӽ� >> �����Ӽ� >> �����  [ 111 ������ 1.23 adf ]
#include <stdio.h> 
#include <windows.h>

// ��   ����������   �̸���   child.exe �����ϼ���.. 

//����� ����
//int argc : ����
//char** argv : ���ڿ� ������
/*
int main(int argc, char** argv)
{
	printf("������ ���� : %d\n", argc);
	for (int i = 0; i < argc; i++)
	{
		printf("%s\n", argv[i]);
	}

	return 0;
}
*/

int main(int argc, char** argv)  //argv[0], argv[1]
{
	if (argc != 2) //����� ���ڸ� 2�� �ްڴ�.
	{
		printf("�����α׷��� ���� �����ϸ� �ȵ˴ϴ�. �θ� ������ �ּ���\n");
		return 1;
	}

	// �θ� ������ pipe �ڵ��� ������. 
	HANDLE hPipe = (HANDLE)atoi(argv[1]);  //���ڿ��� ���ڷ� ����!
	
	char buf[4096];
	while (1) 
	{
		memset(buf, 0, 4096); //buf�� �����ּҺ��� 4096�� ũ����� ��� ����Ʈ�� 0����
	
		DWORD len;
		BOOL b = ReadFile(hPipe, buf, 4096, &len, 0); 
		if (b == FALSE) 
			break;
		printf("%s\n", buf);
	}
	CloseHandle(hPipe);

	return 0;
}

	