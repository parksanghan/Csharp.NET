//02_�������α׷�.cpp

#include <stdio.h>
#include <windows.h>

void main() 
{
	HANDLE hRead, hWrite;
	CreatePipe(&hRead, &hWrite, 0, 4096);

	// �б�    ����   �ڵ���   ���   �����ϰ�   �Ѵ�.
	SetHandleInformation(hRead, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT); 
	//-----------------------------------------------------------------------
	
	// �����   ��������   ��� 
	PROCESS_INFORMATION pi;
	TCHAR cmd[256];
	wsprintf(cmd, TEXT("child.exe %d"), hRead); 
	STARTUPINFO si = { sizeof(si) };
	BOOL b = CreateProcess(0, cmd, 0, 0, TRUE, CREATE_NEW_CONSOLE,0, 0, &si, &pi);
	if (b) 
	{
		//�ڽ����μ��� ������ϰڴ�.
		CloseHandle(pi.hProcess);
		CloseHandle(pi.hThread);
		//hRead�����ϰڴ�.
		CloseHandle(hRead);
	}
	//-------------------------------------------- 
	
	char buf[4096];
	while (1) 
	{
		printf("������   �޼�����   �Է��ϼ���   >> "); 
		gets_s(buf);
		DWORD len;
		WriteFile(hWrite, buf, strlen(buf) + 1, &len, 0);
	}
}