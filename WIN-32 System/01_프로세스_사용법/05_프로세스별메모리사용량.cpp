//https://docs.microsoft.com/ko-kr/windows/win32/psapi/collecting-memory-usage-information-for-a-process
/*
cb - 구조체의 크기를 입력합니다.

PageFaultCount - 페이지 폴트 수입니다.

PeakWorkingSetSize - 워킹셋의 최고치 크기입니다.

WorkingSetSize - 워킹셋 크기입니다.

QuotaPeakPagedPoolUsage - 페이징된 풀의 최고치 사용량입니다.

QuotaPagedPoolUsage - 페이징된 풀의 사용량입니다.

QuotaPeakNonPagedPoolUsage - 페이징되지 않은 풀의 최고치 사용량입니다.

QuotaNonPagedPoolUsage - 페이징되지 않은 풀의 사용량입니다.

PagefileUsage - 페이지 파일의 사용량입니다.

PeakPagefileUsage - 페이지 파일의 최고치 사용량입니다.



출처: https://slaner.tistory.com/111 [꿈꾸는 프로그래머]
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