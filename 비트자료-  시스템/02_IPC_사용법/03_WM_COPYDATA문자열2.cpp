//03_WM_COPYDATA���ڿ�����1.cpp
//skeleton �ڵ�
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

HWND hEdit;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	char buf[100];
	TCHAR tbuf[100];
	switch (msg)
	{
	case WM_CREATE:
	{
		hEdit = CreateWindow(TEXT("edit"), TEXT(""), 
			WS_CHILD | WS_VISIBLE | WS_BORDER | ES_MULTILINE, 
			10, 10, 400, 400, hwnd, (HMENU)1, 0, 0);

		return 0;
	}
	case WM_COPYDATA:
	{
		COPYDATASTRUCT* p = (COPYDATASTRUCT*)lParam; 
		if (p->dwData == 1) // �ĺ���   ����.
		{
			memcpy(buf, p->lpData, sizeof(buf));
			MultiByteToWideChar(CP_ACP, 0, buf, -1, tbuf, _countof(tbuf));
			// Edit Box�� �߰�  �Ѵ�.
			//SendMessage(hEdit, EM_REPLACESEL, 0, (LPARAM)(p->lpData)); 
			SendMessage(hEdit, EM_REPLACESEL, 0, (LPARAM)tbuf);
			SendMessage(hEdit, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
		}
	}
	return 0;
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
		TEXT("FIRST"), TEXT("B"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 800, 500,
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