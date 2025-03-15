//05_명령행인자.cpp
#include <stdio.h>
#include <stdlib.h>  //정수 = atio("문자열");    10 = atio("10");

//argc(count) : 개수
//argv(value) : 문자열들
 
//콘솔 : 실행파일 전체 경로가 0번째 문자열로 자동 전달됨

//명령행 인자 테스트 : 프로젝트>>속성 >> [구성속성>>디버깅 : 명령인수]
// 111 aaa 10.2234

//직접 콘솔창에서 exe 실행파일 실행
/*
1) exe 가 있는 폴더 열기
2) 상단에서 cmd 입력 후 엔터(실행)
   해당 폴더 경로로 콘솔창이 열림
3) 실행파일명을 입력
   0912_프로세스.exe 10 + 20
*/

int main(int argc, char** argv)
{
	/*
	for (int i = 0; i < argc; i++)
	{
		printf("%s\n", argv[i]); 
	}
	*/

	if (argc != 4)
	{
		printf("실행 방법 >>  .exe 10 + 3\n");
		return 0;
	}

	int num1 = atoi(argv[1]);
	char oper = argv[2][0];
	int num2 = atoi(argv[3]);

	switch (oper)
	{
	case '+':	printf("%d %c %d = %d\n", num1, oper, num2, num1 + num2); break;
	case '-':	printf("%d %c %d = %d\n", num1, oper, num2, num1 - num2); break;
	}

	return 0;
}