//04_����������
/*
* ������Ŀ�ο�����Ʈ
* SuspendCount 0���� ũ�� ����. (0�� �� ����)
* - ���� ������ ������ CREATE_SUSPENDED  --> SC : 1���� ����.
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <commctrl.h>  //������Ʈ��(ProgressBar)

//������ ���� �Լ�
DWORD WINAPI foo(void* p) 
{
	HWND hPrg = (HWND)p;

	for (int i = 0; i < 1000; ++i)
	{
		SendMessage(hPrg, PBM_SETPOS, i, 0); // ���α׷��� ����
		for (int k = 0; k < 5000000; ++k); // 0 6�� - some work.!!
	}
	return 0;
}



LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static HWND hPrg;
	static HANDLE hThread;
	switch (msg)
	{
	case WM_CREATE:
		hPrg = CreateWindow(PROGRESS_CLASS, TEXT(""),
			WS_CHILD | WS_VISIBLE | WS_BORDER | PBS_SMOOTH,
			10, 10, 500, 30, hwnd, (HMENU)1, 0, 0);
		//����:0 ~1000 �ʱ���ġ:0���� �ʱ�ȭ.
		SendMessage(hPrg, PBM_SETRANGE32, 0, 1000);
		SendMessage(hPrg, PBM_SETPOS, 0, 0);
		return 0;
	case WM_LBUTTONDOWN:
	{
		// ���ο� �����带 ���� �۾��� ��Ű�� �ֽ������ �ִ��� ����
		// �޼��� ������ ���� ���� ���� �޼����� ó���Ѵ�.
		DWORD tid;
		hThread = CreateThread(0, 0, // TKO ����, Stack ũ��
			foo, (void*)hPrg, // ������� ������ �Լ�,����
			CREATE_SUSPENDED,// ���������� ������ ���� �ʴ´�.
			&tid);	  // ������ ������ ID�� ���� ����
		//CloseHandle( hThread ); // TKO�� ���������� �ʱ⿡ 2�̴�.
		// ������ ����� �Բ� ��� �ı��ǵ��� 1 ���δ�.
	}
	return 0;
	case WM_RBUTTONDOWN:
	{
		static BOOL bRun = FALSE;
		bRun = !bRun; // Toggle
		if (bRun)
			ResumeThread(hThread);	// SC 1 ���ҽ�Ű�� ����
		else
			SuspendThread(hThread);  // SC 1 ������Ű�� ����
	}
	return 0;

	case WM_DESTROY:
		CloseHandle(hThread);
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