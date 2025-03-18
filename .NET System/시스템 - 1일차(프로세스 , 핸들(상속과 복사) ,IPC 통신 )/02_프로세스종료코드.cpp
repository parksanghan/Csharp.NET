//02_���μ�������.cpp
/*
1) ������ �ڵ��� ���� �ִٸ� -> �������� ���Ḧ ����
   SendMessage(h, WM_CLOSE, 0, 0);

2) ���μ����� �ڵ��� ���� �ִٸ� -> ������ ���� ���
   1)�� ������ ���ᰡ �Ұ����Ҷ� �����ϴ� ���

   2.1) ExitProcess(�����ڵ尪);  //�ڽ��� ����

   2.2) TerminateProcess(���μ����ڵ�, �����ڵ尪);  //�ڽ�, �ٸ� ���μ��� ���� ����
   
*/
#include <windows.h> 
#include <stdio.h>

int main()
{
	TCHAR name[] = TEXT("notepad.exe");
	PROCESS_INFORMATION pi;
	STARTUPINFO si = { sizeof(si) };

	BOOL b = CreateProcess(0, name, 0, 0, 0, 0, 0, 0, &si, &pi);

	printf("���μ��� ID : %d\n", (int)pi.dwProcessId);
	printf("������   ID : %d\n", (int)pi.dwThreadId);
	printf("���μ��� �ڵ� : %d\n", (int)pi.hProcess);
	printf("������   �ڵ� : %d\n", (int)pi.hThread);

	//���� �� �޸����� ���� �� �� ���� or �������� �ʰ� ����
	char dummy = getchar();

	DWORD code;
	GetExitCodeProcess(pi.hProcess, &code);
	if (code == STILL_ACTIVE)
	{
		printf("�����ڵ� : %d (%d)\n", code, STILL_ACTIVE);
		printf("����ִ�\n");
	}
	else
	{
		printf("�׾���\n");
		//�� �̻� �ڵ��� �ʿ����.
		CloseHandle(pi.hProcess);	//�� �ؾ� �ұ�?
		CloseHandle(pi.hThread);	//??
	}


	return 0;
}