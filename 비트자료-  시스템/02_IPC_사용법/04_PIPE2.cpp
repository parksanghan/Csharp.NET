//04_PIPE2.cpp(����������)

#include <iostream> 
using namespace std; 
#include <windows.h> 

void main()
{
	// named pipe �����
	HANDLE hPipe = CreateNamedPipe(TEXT("\\\\.\\pipe\\TimeServer"), // pipe�̸�
			PIPE_ACCESS_OUTBOUND, // ��� ����.
			PIPE_TYPE_BYTE,
			1, // ����    �̸���   ��������   �����   �ִ�   �ִ�   ����. 
			1024,1024, // �����   ����    ũ��
			1000, // WaitNamedPipe�Լ���   ����Ҽ�   �ִ�    �ð� 
			0); // KO ����
	if (hPipe == INVALID_HANDLE_VALUE) 
	{
		cout << "Pipe��   �������   �����ϴ�." << endl; return;
	}

	while (1) 
	{
		// Ŭ���̾�Ʈ�� ������ ����Ѵ�. 
		BOOL b = ConnectNamedPipe( hPipe, 0);
		if (b == FALSE && GetLastError() == ERROR_PIPE_CONNECTED)
			b = TRUE;

		if (b == TRUE) 
		{
			// pipe��   ���ؼ�   ����   �ð���   �˷��ش�. 
			SYSTEMTIME st;
			GetSystemTime(&st); 
			DWORD len;

			//2��° ���ڰ� ���� �����ּ� ~ 3��° ���ڰ� byteũ��
			//4��° ���� : ���� ����ũ�� 
			//��¹���
			WriteFile(hPipe, &st, sizeof(st), &len, 0); 
			//����
			FlushFileBuffers(hPipe);

			// ������   ���´�.
			DisconnectNamedPipe(hPipe); 
			cout << "Work... Done" << endl;
		}
	}
}