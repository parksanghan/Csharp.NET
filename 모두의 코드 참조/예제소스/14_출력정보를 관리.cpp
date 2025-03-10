//14_��������� �����ϴ� ���α׷�.

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <vector>
using namespace std;

//��������
vector<POINTS> g_shapes;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONUP:
	{
		POINTS pt = MAKEPOINTS(lParam);
		g_shapes.push_back(pt);

		//��ü ��ȿȭ(ȭ�� �����)
		//InvalidateRect(hwnd, 0, TRUE);

		//��ü ��ȿȭ(ȭ���� ������ ����)
		//InvalidateRect(hwnd, 0, FALSE);

		//�Ϻ� ��ȿȭ(ȭ�� �����)
		RECT rc = { pt.x, pt.y, pt.x + 50, pt.y + 50 };
		//InvalidateRect(hwnd, &rc, TRUE);

		//�Ϻ� ��ȿȭ(ȭ�� ������ ����) --> �ְ��� ȿ����
		InvalidateRect(hwnd, &rc, FALSE);
		
		return 0;
	}
	
	case WM_RBUTTONDOWN:
	{
		//�������� ����� ������ ����
		if (g_shapes.size() <= 0)
			return 0;

		g_shapes.pop_back();
		InvalidateRect(hwnd, 0, TRUE);

		return 0;
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);	//��ȿȭó�� �� ��

		for (int i = 0; i < (int)g_shapes.size(); i++)
		{
			POINTS pt = g_shapes[i];
			Rectangle(hdc, pt.x, pt.y, pt.x + 50, pt.y + 50);
		}		

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