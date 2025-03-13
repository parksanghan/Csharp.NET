//https://docs.microsoft.com/ko-kr/windows/win32/psapi/collecting-memory-usage-information-for-a-process
/*
cb - ����ü�� ũ�⸦ �Է��մϴ�.

PageFaultCount - ������ ��Ʈ ���Դϴ�.

PeakWorkingSetSize - ��ŷ���� �ְ�ġ ũ���Դϴ�.

WorkingSetSize - ��ŷ�� ũ���Դϴ�.

QuotaPeakPagedPoolUsage - ����¡�� Ǯ�� �ְ�ġ ��뷮�Դϴ�.

QuotaPagedPoolUsage - ����¡�� Ǯ�� ��뷮�Դϴ�.

QuotaPeakNonPagedPoolUsage - ����¡���� ���� Ǯ�� �ְ�ġ ��뷮�Դϴ�.

QuotaNonPagedPoolUsage - ����¡���� ���� Ǯ�� ��뷮�Դϴ�.

PagefileUsage - ������ ������ ��뷮�Դϴ�.

PeakPagefileUsage - ������ ������ �ְ�ġ ��뷮�Դϴ�.



��ó: https://slaner.tistory.com/111 [�޲ٴ� ���α׷���]
*/
#include <windows.h>
#include <stdio.h>
#include <psapi.h>

// To ensure correct resolution of symbols, add Psapi.lib to TARGETLIBS
// and compile with -DPSAPI_VERSION=1

void PrintMemoryInfo(DWORD processID)
{
    HANDLE hProcess;
    PROCESS_MEMORY_COUNTERS pmc;

    // Print the process identifier.

    printf("\nProcess ID: %u\n", processID);

    // Print information about the memory usage of the process.

    hProcess = OpenProcess(PROCESS_QUERY_INFORMATION |
        PROCESS_VM_READ,
        FALSE, processID);
    if (NULL == hProcess)
        return;

    if (GetProcessMemoryInfo(hProcess, &pmc, sizeof(pmc)))
    {
        printf("\tPageFaultCount: 0x%08X\n", pmc.PageFaultCount);
        printf("\tPeakWorkingSetSize: 0x%08X\n",
            pmc.PeakWorkingSetSize);
        printf("\tWorkingSetSize: 0x%08X\n", pmc.WorkingSetSize);
        printf("\tQuotaPeakPagedPoolUsage: 0x%08X\n",
            pmc.QuotaPeakPagedPoolUsage);
        printf("\tQuotaPagedPoolUsage: 0x%08X\n",
            pmc.QuotaPagedPoolUsage);
        printf("\tQuotaPeakNonPagedPoolUsage: 0x%08X\n",
            pmc.QuotaPeakNonPagedPoolUsage);
        printf("\tQuotaNonPagedPoolUsage: 0x%08X\n",
            pmc.QuotaNonPagedPoolUsage);
        printf("\tPagefileUsage: 0x%08X\n", pmc.PagefileUsage);
        printf("\tPeakPagefileUsage: 0x%08X\n",
            pmc.PeakPagefileUsage);
    }

    CloseHandle(hProcess);
}

int main(void)
{
    // Get the list of process identifiers.

    DWORD aProcesses[1024], cbNeeded, cProcesses;
    unsigned int i;

    if (!EnumProcesses(aProcesses, sizeof(aProcesses), &cbNeeded))
    {
        return 1;
    }

    // Calculate how many process identifiers were returned.

    cProcesses = cbNeeded / sizeof(DWORD);

    // Print the memory usage for each process

    for (i = 0; i < cProcesses; i++)
    {
        PrintMemoryInfo(aProcesses[i]);
    }

    return 0;
}