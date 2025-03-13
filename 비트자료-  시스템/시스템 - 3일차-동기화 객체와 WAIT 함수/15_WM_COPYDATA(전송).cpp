//15_WM_COPYDATA(전송).cpp
//[전송부] char* 입력 받아
//         wchar_t* 변환

//[수신부] TCHAR* (wchar_t*)

#include <windows.h> 
#include <stdio.h>
#include <tchar.h>
#include <locale.h>		//<-- unicode 한글 출력을 위한 h 1)
int main()
{
	HWND hwnd = FindWindow(0, TEXT("B")); if (hwnd == 0)
	{
		printf("B 윈도우를 먼저 실행해 주세요\n");
		return 0;
	}
	_wsetlocale(LC_ALL, L"Korean");//<-- unicode 한글 출력을 위한 셋 설정 h 2)
	char buf[256] = { 0 };
	while (1)
	{
		printf("B에게 전달한 메세지를 입력하세요 >> ");
		gets_s(buf); //

		//char* -> wchar*t
		wchar_t atow[250];
		MultiByteToWideChar(CP_ACP, 0, buf, -1, atow, 250);
		fwprintf(stdout, L"%s\n", atow);	//<-- unicode 한글 출력을 위한 함수설정 h 3)

		 //전달정보
		COPYDATASTRUCT cds;
		cds.cbData = _countof(atow) * 2; // 전달한 data 크기
		cds.dwData = 1; // 식별자 
		cds.lpData = atow; // 전달할 Pointer

		SendMessage(hwnd, WM_COPYDATA, 0, (LPARAM)&cds);
	}
	return 0;
}