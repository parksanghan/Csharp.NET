//09_자동이벤트.cpp

#include <iostream> 
#include <windows.h> 
using namespace std;

HANDLE hEvent1, hEvent2; 

BOOL bContinue = TRUE; 
int g_x;  //main thread에서 설정!(1)
int sum;  //2nd thread에서 g_x의 값을 활용하여 설정!(2)
		  //main thread에서 sum의 값 출력(3)

DWORD WINAPI ServerThread(LPVOID p) 
{
	while (bContinue) 
	{
		//최초 멈춤!
		WaitForSingleObject(hEvent1, INFINITE);  //<-SetEvent(hEvent1);
		sum = 0; 
		for (int i = 0; i < g_x; i++) 
			sum += i;

		SetEvent(hEvent2);
	} 
	cout << "Server종료" << endl; 
	return 0;
}

int main() 
{
	//hEvent1 : NS
	//hEvent2 : NS
	hEvent1 = CreateEvent(0, 0, 0, TEXT("e1")); 
	hEvent2 = CreateEvent(0, 0, 0, TEXT("e2")); 
	
	HANDLE hThread = CreateThread(NULL, NULL, ServerThread, 0, 0, 0);
	
	while (1) 
	{
		cin >> g_x; 
		if (g_x == -1) 
			break; 
		
		SetEvent(hEvent1); // Signal 발생... 
		
		// ... 다른 일 수행

		WaitForSingleObject(hEvent2, INFINITE); //2nd T가 연산이 끝나는걸 대기
		cout << "결과 : " << sum << endl;		//출력
	}

	// 먼저 ServerThread 종료 
	bContinue = FALSE; 
	SetEvent(hEvent1); 

	WaitForSingleObject(hThread, INFINITE);
	CloseHandle(hThread); 

	return 0;
}