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

	// 프로세스의   핸들   얻기	
	HANDLE hProcess = OpenProcess(PROCESS_QUERY_INFORMATION |
		PROCESS_VM_READ, FALSE, processID); // process 이름   가져오기
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
	// process list 가져오기(id값)
	DWORD aProcess[1024], cbNeeded, cProcesses;
	unsigned int i;

	// 배열    수, 리턴되는   바이트   수 // 배열에   id값들이   들어간다. 
	if (!EnumProcesses(aProcess, sizeof(aProcess), &cbNeeded))
		return;

	// 얼마나   많은   프로세스들이   리턴되었나   계산 
	cProcesses = cbNeeded / sizeof(DWORD); // process 이름   출력
	printf("프로세스 개수 : %d\n", cProcesses);
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