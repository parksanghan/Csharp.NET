#include <windows.h> 
#include <tchar.h> 
#include <stdio.h> 
#include "psapi.h"
#include <vector>
using namespace std;

struct WBPROCESS
{
	TCHAR name[100];
	int pid;
};

vector<WBPROCESS> g_processs;

void PrintProcessNameAndID(DWORD processID)
{
	TCHAR szProcessName[MAX_PATH] = TEXT("unknown");

	// ���μ�����   �ڵ�   ���	
	HANDLE hProcess = OpenProcess(PROCESS_QUERY_INFORMATION |
		PROCESS_VM_READ, FALSE, processID); // process �̸�   ��������
	if (NULL != hProcess)
	{
		HMODULE hMod;
		DWORD    cbNeeded;
		if (EnumProcessModules(hProcess, &hMod, sizeof(hMod), &cbNeeded))
		{
			GetModuleBaseName(hProcess, hMod, szProcessName, sizeof(szProcessName)/sizeof(TCHAR));
		}
		else
			return;
	}
	else
		return;

	//print
	WBPROCESS pinfo;
	_tcscpy_s(pinfo.name, _countof(pinfo.name), szProcessName);
	pinfo.pid = processID;

	g_processs.push_back(pinfo);

	//_tprintf(TEXT("%s ( PROCESS ID : %u )\n"), szProcessName, processID); 
	CloseHandle(hProcess);
}

void WbEnumProcess()
{
	// process list ��������(id��)
	DWORD aProcess[1024], cbNeeded, cProcesses;
	unsigned int i;

	// �迭    ��, ���ϵǴ�   ����Ʈ   �� // �迭��   id������   ����. 
	if (!EnumProcesses(aProcess, sizeof(aProcess), &cbNeeded))
		return;

	// �󸶳�   ����   ���μ�������   ���ϵǾ���   ��� 
	cProcesses = cbNeeded / sizeof(DWORD); // process �̸�   ���
	printf("���μ��� ���� : %d\n", cProcesses);
	for (i = 0; i < cProcesses; i++)
		PrintProcessNameAndID(aProcess[i]);
}

void WbPrintProcess()
{
	for (int i = 0; i < (int)g_processs.size(); i++)
	{
		WBPROCESS p = g_processs[i];
		printf("%ls, %d\n", p.name, p.pid);
	}
}

void main() 
{
	WbEnumProcess();

	WbPrintProcess();	
}


/*
#include <windows.h> 
#include <TLHelp32.h> 
#include <stdio.h> 

int main()
{
	HANDLE   hSnap = CreateToolhelp32Snapshot(
	TH32CS_SNAPPROCESS, 0);      // process ID
	if (hSnap == 0)
	return 1;

PROCESSENTRY32     ppe;
BOOL     b = Process32First(hSnap, &ppe);
while (b) {
	printf("%04d %04d %s\n", ppe.th32ProcessID,
		ppe.th32ParentProcessID, ppe.szExeFile);
	b = Process32Next(hSnap, &ppe);
}
CloseHandle(hSnap);
return 0;
}
*/