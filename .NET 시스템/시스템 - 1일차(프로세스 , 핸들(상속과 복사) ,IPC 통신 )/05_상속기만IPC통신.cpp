//05_��ӱ��IPC���.cpp
/*
IPC(InterProcess Commnuication) , �ϳ��� PC�ȿ��� ���� �ٸ� ���μ��� �� ���
  1) SendMessage(�ڵ�, ������ �ִ� �������� �ִ� ũ��:4+4)
  2) WM_COPYDATA : SendMessage�� ������ �غ�(�ּҿ� ũ�⸦ ����)
  3) PIPE        : ������ ��� ����[�ܹ�����]
*/

#include <stdio.h> 
#include <windows.h> 

void main()
{ 
	HANDLE hRead, hWrite;
	
	//2���� �ڵ��� HT�� ���(���X)
	//hRead  : ��� X
	//hWrite : ��� X
	CreatePipe(&hRead, &hWrite, 0, 4096); 

	//hRead : ��� O
	SetHandleInformation(hRead, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT); 
	
	// ����� �������� ��� 
	TCHAR cmd[256];
	wsprintf(cmd, TEXT("child.exe %d"), hRead);  //child.exe 1234

	
	PROCESS_INFORMATION pi; 
	STARTUPINFO si = { sizeof(si)}; 
	BOOL b = CreateProcess( 0, cmd, 0, 0, TRUE, CREATE_NEW_CONSOLE, 0,0,&si, &pi); 

	if( b ) 
	{ 
		CloseHandle( pi.hProcess);	// �ڽ��� ����
		CloseHandle( pi.hThread);	// �ڽ��� ����
		CloseHandle( hRead );		// hRead ����
	} 
	//-------------------------------------------- 
	
	char buf[4096]; 
	while(1)
	{ 
		printf("������ �޼����� �Է��ϼ��� >> "); 
		gets_s(buf, sizeof(buf)); 

		//�����ڵ�[�����ּ�, byteũ��]
		DWORD len; 
		WriteFile(hWrite, buf, strlen(buf) + 1, &len, 0);
		printf("���� ����Ʈ ũ�� : %dbyte\n", len);
	}
}