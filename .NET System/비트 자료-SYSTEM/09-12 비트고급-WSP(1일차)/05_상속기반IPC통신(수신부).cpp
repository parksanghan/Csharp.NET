//05_상속기반IPC통신(수신부).cpp

#include <stdio.h> 
#include <windows.h> 

// 이 실행파일의 이름을 child.exe 변경하세요.. 
int main(int argc, char** argv) // argv 에서는 argv [0] 경로 값  [1] 핸들값받아옴 argc는 받은 인자의 개수를 말함
	// 즉 받은 인자의 개수가 2개가 아닌경우 실행을 하지 않는다.
{ 
	if ( argc != 2 ) //0)경로  1)부모로 부터 상속받은 핸들
	{ 
		printf("이프로그램은 직접 실행하면 안됩니다. 부모를 실행해 주세요\n"); 
		return 0; 
	}

	// 부모가 보내준 pipe 핸들을 꺼낸다. 
	// 해당 핸들은 자신의 핸들테이블에 등록이 되어있다.
	HANDLE hPipe = (HANDLE)atoi(argv[1]);  // argv  : 상속받은 핸들값이 문자열로 저장됨  atoi 는 문자열을 핸들값은 정수이기에 정수 
	// 로 변환 
	
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