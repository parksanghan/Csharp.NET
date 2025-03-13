//12_MMF기본코드.cpp

#include <stdio.h> 
#include <windows.h> 
#include <string.h>

int main()
{ 
	//1. 파일 생성 
	HANDLE hFile = CreateFile( TEXT("a.txt"), 
		GENERIC_READ | GENERIC_WRITE, 
		FILE_SHARE_READ | FILE_SHARE_WRITE, 0, 
		CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);

	//2. 화일 매핑 KO 생성 
	HANDLE hMap = CreateFileMapping( hFile, 0, // 매핑할 화일, KO 보안 
		PAGE_READWRITE, // 접근 권한 
		0, 100, // 매핑 객체 크기 
		TEXT("map")); // 매핑 객체 이름

	//3. 매핑 객체를 사용해서 가상 주소와 파일 연결 
	char* p = (char*)MapViewOfFileEx( hMap, FILE_MAP_WRITE, 0, 0, // file offset 
				0, // 크기.(0 매핑 객체 크기) 
				0); // 원하는 주소. 

	if ( p == NULL ) 
		printf("error"); 
	else
	{ 
		printf("매핑된 주소 : %p\n", p); 
		strcpy_s( p, 100, "hello"); 
		
		p[50] = 'a'; 
		p[99] = 'b'; 
	} 
	
	//종료처리
	UnmapViewOfFile( p );
	CloseHandle( hMap ); 
	CloseHandle( hFile ); 
}