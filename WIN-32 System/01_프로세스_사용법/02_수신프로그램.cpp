//02_수신프로그램
//속성 >> 구성속성 >> 디버깅  [ 111 ㅁㅇㄹ 1.23 adf ]
#include <stdio.h> 
#include <windows.h>

// 이   실행파일의   이름을   child.exe 변경하세요.. 

//명령행 인자
//int argc : 갯수
//char** argv : 문자열 여러개
/*
int main(int argc, char** argv)
{
	printf("인자의 개수 : %d\n", argc);
	for (int i = 0; i < argc; i++)
	{
		printf("%s\n", argv[i]);
	}

	return 0;
}
*/

int main(int argc, char** argv)  //argv[0], argv[1]
{
	if (argc != 2) //명령행 인자를 2개 받겠다.
	{
		printf("이프로그램은 직접 실행하면 안됩니다. 부모를 실행해 주세요\n");
		return 1;
	}

	// 부모가 보내준 pipe 핸들을 꺼낸다. 
	HANDLE hPipe = (HANDLE)atoi(argv[1]);  //문자열을 숫자로 변경!
	
	char buf[4096];
	while (1) 
	{
		memset(buf, 0, 4096); //buf의 시작주소부터 4096의 크기까지 모든 바이트를 0으로
	
		DWORD len;
		BOOL b = ReadFile(hPipe, buf, 4096, &len, 0); 
		if (b == FALSE) 
			break;
		printf("%s\n", buf);
	}
	CloseHandle(hPipe);

	return 0;
}

	