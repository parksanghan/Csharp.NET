//06_����������.cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <commctrl.h>	

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

//���α׷����� ID / HANDLE
#define IDC_PRO  1
HWND hPrg;

//Thread�ڵ�
HANDLE hThread;

//thread �Լ�
//���α׷����ٸ� 0...1000 �̵�!
DWORD WINAPI ThreadFunc1(LPVOID temp)
{
	//���α׷����� ��Ʈ�� �ڵ�
	HWND hPrg = (HWND)temp;

	for (int i = 0; i < 1000; ++i) //0...1000
	{
		SendMessage(hPrg, PBM_SETPOS, i, 0); // ���α׷��� ���� 
		for (int k = 0; k < 5000000; ++k); // 0 6�� - some work.!! 
	}

	return 0;	//ī��Ʈ 0���� 
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		//������ ���� -> Ŀ�� ��ü ����[�ڵ�2�� ����->���μ�������...]
		DWORD ThreadID;
		hThread = CreateThread(0, 0, ThreadFunc1, hPrg, 0, &ThreadID); // ī��Ʈ:2 

		SetWindowText(hwnd, TEXT("������ ����"));

		//������ ���� ���ҷ�!!!
		//CloseHandle(hThread); // ī��Ʈ : 1

		return TRUE;
	}
	case WM_RBUTTONDOWN:
	{
		DWORD code; 
		GetExitCodeThread(hThread, &code); 
		if (code == STILL_ACTIVE) 
		{ 
			SetWindowText(hwnd, TEXT("������ ������"));
			TerminateThread(hThread, 100); 
			CloseHandle(hThread); 
		}
		else
		{
			SetWindowText(hwnd, TEXT("������ ����"));
		}

		return 0;
	}

	case WM_CREATE:	//������(�ʱ�ȭ ó��, ��ť, )
	{
		hPrg = CreateWindow(PROGRESS_CLASS, TEXT(""),
			WS_CHILD | WS_VISIBLE | WS_BORDER | PBS_SMOOTH,
			10, 10, 500, 30, hwnd, (HMENU)IDC_PRO, 0, 0);

		//����:0 ~1000 
		SendMessage(hPrg, PBM_SETRANGE32, 0, 1000);

		//�ʱ���ġ:0���� �ʱ�ȭ.
		SendMessage(hPrg, PBM_SETPOS, 0, 0);

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