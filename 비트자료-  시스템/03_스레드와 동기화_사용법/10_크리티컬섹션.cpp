//10_ũ��Ƽ�ü���(�Ӱ迵��)

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

int x;
CRITICAL_SECTION	g_cs;		//ũ��Ƽ�ü��� ���� ����  -----------------1)

DWORD WINAPI ThreadFun1(LPVOID param)
{
	HDC hdc = GetDC((HWND)param);
	for (int i = 0; i < 100; i++)
	{
		EnterCriticalSection(&g_cs);
		x = 100;
		TextOut(hdc, x, 100, TEXT("������"), 3);
		LeaveCriticalSection(&g_cs);
	}
	ReleaseDC((HWND)param, hdc);
	return 0;
}

DWORD WINAPI ThreadFun2(LPVOID param)
{
	HDC hdc = GetDC((HWND)param);
	for (int i = 0; i < 100; i++)
	{
		EnterCriticalSection(&g_cs);			//--�Ӱ迵���� ���� (4)
		x = 200;
		TextOut(hdc, x, 200, TEXT("�����"), 3);
		LeaveCriticalSection(&g_cs);
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
		HANDLE hThread1 = CreateThread(NULL, 0, ThreadFun1, hwnd, 0, &ThreadID);
		HANDLE hThread2 = CreateThread(NULL, 0, ThreadFun2, hwnd, 0, &ThreadID);

		CloseHandle(hThread1);
		CloseHandle(hThread2);
	}
	return 0;
	case WM_CREATE: 
	{
		InitializeCriticalSection(&g_cs);	//				�Ӱ迵�� ���� �ʱ�ȭ	 2)
		return 0;
	}
	case WM_DESTROY:
		DeleteCriticalSection(&g_cs);	//					 �ı�		3)	
		PostQuitMessage(0);
		return 0;
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}



int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//������ Ŭ���� ����
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //��, �귯��, ��Ʈ
	wc.hCursor = LoadCursor(0, IDC_ARROW);//�ý���
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = 0;		//�޴� ���
	wc.style = 0;				//������ ��Ÿ��

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("0830"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);

	//�޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}