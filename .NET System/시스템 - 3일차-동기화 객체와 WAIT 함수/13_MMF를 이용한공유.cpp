//13_MMF�� �̿��Ѱ���.cpp
/*
MMF ���
1) ��뷮 �޸�(12_����)
2) ���� �ٸ� ���μ��� �� ������ ����(***)
   �����޸� 
*/
//04_skeleton.cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

typedef struct _LINE 
{ 
	POINTS ptFrom; 
	POINTS ptTo; 
}LINE;

POINTS ptFrom;	//������!


//1) �����޸𸮸� ���� ������ ����
HANDLE hEvent;		//�ڵ��̺�Ʈ��ü(����!)
HANDLE hMap;		//MMF
LINE* pData;		//�����޸𸮿� �����ϴ� ������

//2) WM_CREATE : �����޸𸮸� ���� �ʱ�ȭ ó��(��ü ���� �� ������ ��ȯ)
//3) WM_DESTROY: �����޸� ���� ���� ó��
//4) WM_MOUSEMOVE : �����޸𸮿� ��ǥ ����ü�� ���� -> �̺�Ʈ �߻�


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		ptFrom = MAKEPOINTS(lParam); 
		return 0;
	}
	case WM_MOUSEMOVE: 
	{
		if (wParam & MK_LBUTTON)
		{
			POINTS pt = MAKEPOINTS(lParam);			
			HDC hdc = GetDC(hwnd);						
			MoveToEx(hdc, ptFrom.x, ptFrom.y, 0);	//L��ư ������!
			LineTo(hdc, pt.x, pt.y);				//MouseMove
			ReleaseDC(hwnd, hdc);

			// MMF �� �ִ´�. 
			pData->ptFrom = ptFrom; 
			pData->ptTo = pt; 
			//SetEvent( hEvent );
			PulseEvent(hEvent); //Set + ReSet
			
			ptFrom = pt;		//��ǥ�̵�			
		}
	}
	case WM_CREATE:	//������(�ʱ�ȭ ó��, ��ť, )
	{
		//�Ѹ�....
		//hEvent = CreateEvent(0, 0, 0, TEXT("DRAW_SIGNAL")); 
		//�ټ�����...
		hEvent = CreateEvent(0, TRUE, 0, TEXT("DRAW_SIGNAL"));

		hMap = CreateFileMapping((HANDLE)-1, // Paging ȭ���� ����ؼ� ���� 
			0, PAGE_READWRITE, 0, sizeof(LINE), TEXT("map"));

		pData = (LINE*)MapViewOfFile( hMap, FILE_MAP_WRITE, 0, 0,0); 
		if ( pData == 0 ) 
			MessageBox( 0, TEXT("Fail"), TEXT(""), MB_OK);

		return 0;
	}
	case WM_DESTROY: //����(���� ó��, �����찡 �ı���)
	{
		UnmapViewOfFile(pData); 
		CloseHandle(hMap); 
		CloseHandle(hEvent);

		PostQuitMessage(0);
		return 0;
	}
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
	wc.lpszClassName = CLASS_NAME;
	wc.lpszMenuName = 0;		//�޴� ���
	wc.style = 0;				//������ ��Ÿ��

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		CLASS_NAME, WINDOW_NAME, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 500, 500,
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

