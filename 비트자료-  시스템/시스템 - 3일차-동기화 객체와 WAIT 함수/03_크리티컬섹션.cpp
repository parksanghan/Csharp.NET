//03_ũ��Ƽ�ü���_01�ڵ� ����.cpp
/*
�Ӱ迵��
1. ũ��Ƽ�ü��� ��ü�� ���� - �ʱ�ȭ / ����ó��
2. �Ӱ迵�� ����
   **** EnterXXXX
    
   **** LeavXXXX

*/

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

//1) ũ��Ƽ�� ��ü ���� ****************************************************
CRITICAL_SECTION g_cs; 

//��������[������ ���� ����]
//��Ƽ������ ȯ��-���� �ٸ� �����嵵 ���� ����
int x;

//100,100 : ������
DWORD WINAPI ThreadFun1(LPVOID param)
{
	HDC hdc = GetDC((HWND)param);
	
	for (int i = 0; i < 100; i++)
	{
		EnterCriticalSection(&g_cs);	//**********************************
		x = 100;
		//Sleep(1);
		TextOut(hdc, x, 100, TEXT("������"), 3);
		LeaveCriticalSection(&g_cs);   //***********************************
	}	

	ReleaseDC((HWND)param, hdc);	
	return 0;
}

//200,200 : �����
DWORD WINAPI ThreadFun2(LPVOID param)
{
	HDC hdc = GetDC((HWND)param);

	
	for (int i = 0; i < 100; i++)
	{
		EnterCriticalSection(&g_cs);	//**********************************
		x = 200;
		//Sleep(1);
		TextOut(hdc, x, 200, TEXT("�����"), 3);
		LeaveCriticalSection(&g_cs);   //***********************************
	}	

	ReleaseDC((HWND)param, hdc);
	return 0;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		DWORD ThreadID;
		HANDLE hThread = CreateThread(NULL, 0, ThreadFun1, hwnd, 0, &ThreadID);
		CloseHandle(hThread);

		hThread = CreateThread(NULL, 0, ThreadFun2, hwnd, 0, &ThreadID);
		CloseHandle(hThread);

		return 0;
	}
	case WM_CREATE:	//������(�ʱ�ȭ ó��, ��ť, )
	{
		InitializeCriticalSection(&g_cs); //1) �Ӱ迵�� ���� �ʱ�ȭ***************
		return 0;
	}
	case WM_DESTROY: //����(���� ó��, �����찡 �ı���)
	{
		DeleteCriticalSection(&g_cs); // 2) �ı� ********************************
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
		CW_USEDEFAULT, 0, 600, 600,
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