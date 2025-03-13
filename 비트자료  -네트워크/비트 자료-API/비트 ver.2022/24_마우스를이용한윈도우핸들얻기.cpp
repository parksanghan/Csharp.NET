//24_���콺���̿��� �������ڵ���
/*
* FindWindow()       : Ŭ�������̳� ����������� �����츦 �˻�!
* WindowFromPoint()  : ��ǥ(��ũ����ǥ)�� ���� �����츦 ã�� ���ڴ�
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

void PrintWindowInfo(HWND hwnd)
{
	TCHAR Info[1024] = { 0 };
	TCHAR temp[256];
	RECT rcWin;
	GetClassName(hwnd, temp, 256);
	wsprintf(Info, TEXT("Window Class : %s\n"), temp);

	GetWindowText(hwnd, temp, 256);
	wsprintf(Info + wcslen(Info), TEXT("Window Caption : %s\n"), temp);

	GetWindowRect(hwnd, &rcWin);
	wsprintf(Info + wcslen(Info), TEXT("Window Position : (%d,%d)-(%d,%d)"),
		rcWin.left, rcWin.top, rcWin.right, rcWin.bottom);

	MessageBox(0, Info, TEXT("Window Info"), MB_OK);
}


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		SetCapture(hwnd);
		return 0;
	}
	case WM_LBUTTONUP:
	{
		if (GetCapture() == hwnd)
		{
			ReleaseCapture();

			POINT pt = { LOWORD(lParam), HIWORD(lParam) };	//Ŭ���̾�Ʈ��ǥ
			ClientToScreen(hwnd, &pt);
			HWND hwndDest = WindowFromPoint(pt);

			//POINT pt;
			//GetCursorPos(&pt);	//���� ��ġ�� ���콺 ������ ȹ��(��ũ����ǥ)
			//HWND hwndDest = WindowFromPoint(pt);	//���޵Ǿ�� �� ��ǥ�� ��ũ����ǥ
			PrintWindowInfo(hwndDest);

		}
		return 0;
	}
	//�׽�Ʈ(ĸ�İ� �ǰ� �ִ��� Ȯ��)
	case WM_MOUSEMOVE:
	{
		TCHAR temp[256];
		POINT pt = { LOWORD(lParam),  HIWORD(lParam) };
		wsprintf(temp, TEXT("Cursor Position : (%d, %d)"), pt.x, pt.y);
		SetWindowText(hwnd, temp);
		return 0;
	}
	case WM_CREATE:
	{
		return 0;
	}

	case WM_DESTROY:
	{
		PostQuitMessage(0);
		return 0;
	}
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
	UpdateWindow(hwnd);				//<-----------------(0)

	//�޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);		//<--------------------(0)
		DispatchMessage(&msg);
	}
	return 0;
}