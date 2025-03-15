//15_WM_COPYDATA(����).cpp
//[���ۺ�] char* �Է� �޾�
//         wchar_t* ��ȯ

//[���ź�] TCHAR* (wchar_t*)

#include <windows.h> 
#include <stdio.h>
#include <tchar.h>
#include <locale.h>		//<-- unicode �ѱ� ����� ���� h 1)
int main()
{
	HWND hwnd = FindWindow(0, TEXT("B")); if (hwnd == 0)
	{
		printf("B �����츦 ���� ������ �ּ���\n");
		return 0;
	}
	_wsetlocale(LC_ALL, L"Korean");//<-- unicode �ѱ� ����� ���� �� ���� h 2)
	char buf[256] = { 0 };
	while (1)
	{
		printf("B���� ������ �޼����� �Է��ϼ��� >> ");
		gets_s(buf); //

		//char* -> wchar*t
		wchar_t atow[250];
		MultiByteToWideChar(CP_ACP, 0, buf, -1, atow, 250);
		fwprintf(stdout, L"%s\n", atow);	//<-- unicode �ѱ� ����� ���� �Լ����� h 3)

		 //��������
		COPYDATASTRUCT cds;
		cds.cbData = _countof(atow) * 2; // ������ data ũ��
		cds.dwData = 1; // �ĺ��� 
		cds.lpData = atow; // ������ Pointer

		SendMessage(hwnd, WM_COPYDATA, 0, (LPARAM)&cds);
	}
	return 0;
}