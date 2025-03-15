//15_����ü�� Ȱ���� �����������
/*
* ������ ����(������ Ÿ��, ��ǥ����)�� �����ϴ� ���α׷�
*/

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <vector>
using namespace std;

struct Shape
{
	int type;		//1(�簢��), 2(Ÿ��)		//Ű���带 ������ R ->1, E -> 2
	POINTS pt;		//��ǥ����
};

//��������
vector<Shape>	g_shapes;			//��������
Shape			g_curshape;			//���� ��������

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_KEYDOWN:
	{
		if (wParam == 'R')
			g_curshape.type = 1;
		else if (wParam == 'E')
			g_curshape.type = 2;

		return 0;
	}
	case WM_LBUTTONUP:
	{
		g_curshape.pt = MAKEPOINTS(lParam);
		g_shapes.push_back(g_curshape);

		InvalidateRect(hwnd, 0, TRUE);
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
			Shape sh = g_shapes[i];
			if(sh.type == 1)
				Rectangle(hdc, sh.pt.x, sh.pt.y, sh.pt.x + 50, sh.pt.y + 50);
			else if(sh.type == 2)
				Ellipse(hdc, sh.pt.x, sh.pt.y, sh.pt.x + 50, sh.pt.y + 50);
		}

		EndPaint(hwnd, &ps);
		return 0;
	}

	case WM_CREATE: //�ʱ�ȭ
	{
		g_curshape.type = 1;
		g_curshape.pt.x = 0;
		g_curshape.pt.y = 0;

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