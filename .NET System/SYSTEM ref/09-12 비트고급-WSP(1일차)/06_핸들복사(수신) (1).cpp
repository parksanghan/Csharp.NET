//06_�ڵ麹��(����).cpp
/*
* "B" ��� �̸��� Ÿ��Ʋ��!
*/
#pragma comment (linker, "/subsystem:windows")	
#include <windows.h> 
#include <tchar.h>

LRESULT CALLBACK WndProc( HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	 switch( msg ) 
	 { 
	 case WM_USER + 100: 
	 { 
		 HANDLE hFile = (HANDLE)lParam;

		 char s[256] = "hello"; 
		 DWORD len; 
		 BOOL b = WriteFile( hFile, s, 256, &len, 0); 
		 if ( b == FALSE ) 
		 { 
			 TCHAR buf[256]; 
			 wsprintf(buf, TEXT("���޵� �ڵ� : %x \n���� : %d"), hFile, GetLastError() ); 
			 MessageBox( 0, buf, TEXT(""), MB_OK); 
		 }
		 else 
		 { 
			 MessageBox( 0, TEXT("����"), TEXT(""), MB_OK); 
			 CloseHandle( hFile ); 
		 } 
		 return 0;
	 }

	 case WM_DESTROY: 
		 PostQuitMessage(0); 
		 return 0; 
	 } 
	 return DefWindowProc(hwnd, msg, wParam, lParam); 
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//1. ������ ����, ���
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //��, �귯��, ��Ʈ
	wc.hCursor = LoadCursor(0, IDC_ARROW);//�ý���
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)
	wc.lpszClassName = TEXT("CLASSNAME");
	wc.lpszMenuName = 0;		//�޴� ���
	wc.style = 0;				//������ ��Ÿ��

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("CLASSNAME"), TEXT("A"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 800, 800,
		//CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,  //�ý����� �˾Ƽ� ���!
		0, 0, hInst, 0);

	ShowWindow(hwnd, nShowCmd);
	UpdateWindow(hwnd);

	//2. �޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}