//05_상속기반IPC통신(수신부).cpp

#include <stdio.h> 
#include <windows.h> 

// 이 실행파일의 이름을 child.exe 변경하세요.. 
int main(int argc, char** argv)
{ 
	if ( argc != 2 ) //0)경로  1)부모로 부터 상속받은 핸들
	{ 
		printf("이프로그램은 직접 실행하면 안됩니다. 부모를 실행해 주세요\n"); 
		return 0; 
	}

	// 부모가 보내준 pipe 핸들을 꺼낸다. 
	// 해당 핸들은 자신의 핸들테이블에 등록이 되어있다.
	HANDLE hPipe = (HANDLE)atoi(argv[1]); 
	
	char buf[4096];

	while ( 1 ) 
	{
		//buf : 시작주소로부터
		//0   : 모든 바이트를 0으로
		//sizeof(buf) : 이만큼을
		memset( buf, 0, sizeof(buf) ); 

		DWORD len; 
		BOOL b = ReadFile( hPipe, buf, sizeof(buf), &len, 0);
		if ( b == FALSE )
			break; 
		printf("[%dbyte] %s\n", len, buf ); 		
	}
	CloseHandle( hPipe );

	return 0;
}