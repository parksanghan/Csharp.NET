//03_WM_COPYDATA���ڿ�����1.cpp
#include <windows.h> 
#include <stdio.h>
#include <tchar.h>
/*
void main() 
{
	//���� ������ �ڵ� ���
	HWND hwnd = FindWindow(0, TEXT("B")); 
	if (hwnd == 0)
	{
		printf("B �����츦   ����   ������   �ּ���\n");      
		return;
	}

	//�޽��� ����
	TCHAR buf[256] = { 0 };
	while (1) 
	{
		printf("B����    ������   �޼�����   �Է��ϼ���   >> "); 
		_getts_s(buf, _countof(buf)); // 1���Է�   ?

		//_tprintf(TEXT("%s\n"), buf);
		//_tprintf(TEXT("%d\n"), (_tcslen(buf) + 1)*2);
		
		// ��������   Pointer��   �����ϱ�   ����   �޼���   - WM_COPYDATA 
		COPYDATASTRUCT cds;
		cds.lpData = buf;
		cds.cbData = (_tcslen(buf) + 1) * 2; //_tcslen(buf) + 1;  // ������   data ũ��
		// �ĺ���
		cds.dwData = 1;
		
		// ������
		SendMessage( hwnd, WM_COPYDATA, 0,  (LPARAM)&cds);
	}
}
*/

void main()
{
	//���� ������ �ڵ� ���
	HWND hwnd = FindWindow(0, TEXT("B"));
	if (hwnd == 0)
	{
		printf("B �����츦   ����   ������   �ּ���\n");
		return;
	}

	//�޽��� ����
	char buf[256] = { 0 };
	while (1)
	{
		printf("B����    ������   �޼�����   �Է��ϼ���   >> "); 
		gets_s(buf); // 1���Է�   ?

		// ��������   Pointer��   �����ϱ�   ����   �޼���   - WM_COPYDATA 
		COPYDATASTRUCT cds;
		cds.cbData = strlen(buf) + 1;  // ������   data ũ��
		// �ĺ���
		cds.dwData = 1;
		cds.lpData = buf;          // ������   Pointer 
		SendMessage( hwnd, WM_COPYDATA, 0,  (LPARAM)&cds);
	}
}