//05_���ؽ�.cpp
//���� �ڵ� ���� -> ���� ���μ��� �� �����尣 ����ȭ ó��!
/*
������

1) ���� �� ���� CreateMutex / OpenMutext
2) ������ �ݳ�  ReleaseMutex
   - ������ �ִ� �����常 �ݳ�
*/

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

//��������[������ ���� ����]
//��Ƽ������ ȯ��-���� �ٸ� �����嵵 ���� ����
int x;

HANDLE g_Mutex;

//100,100 : ������
DWORD WINAPI ThreadFun1(LPVOID param)
{
	HDC hdc = GetDC((HWND)param);

	//��ȣ��!
	/*
	* hMutex : Signal����(�ƹ��� �������� �ʰ� �ִ�)
	*          ����ϰ� �ǰ�(�ش� �������� ����� �����尡 ȹ��)
	*           --> NonSignal ����
	*/
	
	for (int i = 0; i < 100; i++)
	{
		WaitForSingleObject(g_Mutex, INFINITE);
		x = 100;		
		TextOut(hdc, x, 100, TEXT("������"), 3);
		ReleaseMutex(g_Mutex);  //������ �ݳ�(NonSignal ���� ����)
		Sleep(1);
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
		WaitForSingleObject(g_Mutex, INFINITE);
		x = 200;		
		TextOut(hdc, x, 200, TEXT("�����"), 3);
		ReleaseMutex(g_Mutex);  //������ �ݳ�(NonSignal ���� ����)
		Sleep(1);
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
		//TRUE : PT : ���ؽ� ���� -> ���ؽ� ���� NON SIGNAL
		//g_Mutex = CreateMutex(0, TRUE, TEXT("KEY"));

		//FALSE : �����ڰ� ���� -> ���ؽ� ���� SIGNAL
		g_Mutex = CreateMutex(0, FALSE, TEXT("KEY"));

		return 0;
	}
	case WM_DESTROY: //����(���� ó��, �����찡 �ı���)
	{
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