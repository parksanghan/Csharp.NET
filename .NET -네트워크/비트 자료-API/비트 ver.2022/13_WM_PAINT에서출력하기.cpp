//13_WM_PAINT���� ����ϱ�
/*
* ���콺LButton�� Ŭ������ �� �ش� ��ġ�� 50*50 ũ���� �簢�� ��� 
* [��� : WM_LBUTTONUP]
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static POINTS g_pt = { -100,-100 };  //������������(������������)

	switch (msg)
	{
		//LButtonUp���� ���ó��!
	/*case WM_LBUTTONUP:
	{
		POINTS pt = MAKEPOINTS(lParam);

		HDC hdc = GetDC(hwnd);
		Rectangle(hdc, pt.x-25, pt.y-25, pt.x + 25, pt.y + 25);
		ReleaseDC(hwnd, hdc);
		return 0;
	}*/
	case WM_LBUTTONUP:
	{
		g_pt = MAKEPOINTS(lParam);
		//hwnd�� Ŭ���̾�Ʈ ��ü ������ ��ȿȭ
		//FALSE : ��ȿȭ�� ������ ������ �ʰڴ�.
		//TRUE : ��ȿȭ�� ������ ������ �������� ĥ�ϰڴ�.
		InvalidateRect(hwnd, 0, TRUE);
		return 0;
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);	//��ȿȭó�� �� ��

		Rectangle(hdc, g_pt.x - 25, g_pt.y - 25, g_pt.x + 25, g_pt.y + 25);

		EndPaint(hwnd, &ps);
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