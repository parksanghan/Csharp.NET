//05_��ӱ��IPC���(���ź�).cpp

#include <stdio.h> 
#include <windows.h> 

// �� ���������� �̸��� child.exe �����ϼ���.. 
int main(int argc, char** argv)
{ 
	if ( argc != 2 ) //0)���  1)�θ�� ���� ��ӹ��� �ڵ�
	{ 
		printf("�����α׷��� ���� �����ϸ� �ȵ˴ϴ�. �θ� ������ �ּ���\n"); 
		return 0; 
	}

	// �θ� ������ pipe �ڵ��� ������. 
	// �ش� �ڵ��� �ڽ��� �ڵ����̺� ����� �Ǿ��ִ�.
	HANDLE hPipe = (HANDLE)atoi(argv[1]); 
	
	char buf[4096];

	while ( 1 ) 
	{
		//buf : �����ּҷκ���
		//0   : ��� ����Ʈ�� 0����
		//sizeof(buf) : �̸�ŭ��
		memset( buf, 0, sizeof(buf) ); 

		DWORD len; 
		BOOL b = ReadFile( hPipe, buf, sizeof(buf), &len, 0);
		if ( b == FALSE )
			break; 
		printf("[%dbyte] %s\n", len, buf ); 		
	}
	CloseHandle( hPipe );

	return 0;
}