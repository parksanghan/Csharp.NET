//05_상속기반IPC통신.cpp
/*
IPC(InterProcess Commnuication) , 하나의 PC안에서 서로 다른 프로세스 간 통신
  1) SendMessage(핸들, 보낼수 있는 데이터의 최대 크기:4+4)
  2) WM_COPYDATA : SendMessage의 단점을 극복(주소와 크기를 전달)
  3) PIPE        : 가상의 통로 구성[단방향임]
*/

#include <stdio.h> 
#include <windows.h> 

void main()
{ 
	HANDLE hRead, hWrite;
	
	//2개의 핸들이 HT에 등록(상속X)
	//hRead  : 상속 X
	//hWrite : 상속 X
	CreatePipe(&hRead, &hWrite, 0, 4096); 

	//hRead : 상속 O
	SetHandleInformation(hRead, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT); 
	
	// 명령형 전달인자 사용 
	TCHAR cmd[256];
	wsprintf(cmd, TEXT("child.exe %d"), hRead);  //child.exe 1234

	
	PROCESS_INFORMATION pi; 
	STARTUPINFO si = { sizeof(si)}; 
	BOOL b = CreateProcess( 0, cmd, 0, 0, TRUE, CREATE_NEW_CONSOLE, 0,0,&si, &pi); 

	if( b ) 
	{ 
		CloseHandle( pi.hProcess);	// 자식을 닫음
		CloseHandle( pi.hThread);	// 자식을 닫음
		CloseHandle( hRead );		// hRead 닫음
	} 
	//-------------------------------------------- 
	
	char buf[4096]; 
	while(1)
	{ 
		printf("전달할 메세지를 입력하세요 >> "); 
		gets_s(buf, sizeof(buf)); 

		//전송코드[시작주소, byte크기]
		DWORD len; 
		WriteFile(hWrite, buf, strlen(buf) + 1, &len, 0);
		printf("보낸 바이트 크기 : %dbyte\n", len);
	}
}