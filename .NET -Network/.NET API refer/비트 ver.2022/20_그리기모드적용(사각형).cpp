//20_�׸���������(�簢��)

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static POINTS start, end;

	switch (msg)
	{
	case WM_LBUTTONDOWN:  //������
	{
		start = end = MAKEPOINTS(lParam);
		SetCapture(hwnd);

		return 0;
	}
	case WM_LBUTTONUP:  //����
	{
		ReleaseCapture();

		HDC hdc = GetDC(hwnd);
		HBRUSH hbrush = (HBRUSH)SelectObject(hdc, GetStockObject(NULL_BRUSH));
		Rectangle(hdc, start.x, start.y, end.x, end.y);	

		SelectObject(hdc, hbrush);
		ReleaseDC(hwnd, hdc);
		return 0;
	}
	case WM_MOUSEMOVE: //�׸��׸���
	{
		if (GetCapture() == hwnd) //���� ���콺ĸ�ĸ� �� ���¶��
		{
			HDC hdc = GetDC(hwnd);

			//R2_NOTXORPEN(����), R2_NOT(������..)
			SetROP2(hdc, R2_NOTXORPEN);//<=========================���� �׸� ����
			//�������� - ������ ĥ�ϸ� => ����
			
			HPEN pen = CreatePen(PS_SOLID, 3, RGB(255, 0, 0));
			
			HBRUSH hbrush = (HBRUSH)SelectObject(hdc, GetStockObject(NULL_BRUSH));
			HBRUSH oldpen = (HBRUSH)SelectObject(hdc, pen);
			
			//����� ����
			Rectangle(hdc, start.x, start.y, end.x, end.y);

			end = MAKEPOINTS(lParam);

			//�׸��� ����
			Rectangle(hdc, start.x, start.y, end.x, end.y);

			SelectObject(hdc, hbrush);
			DeleteObject(SelectObject(hdc, oldpen));

			ReleaseDC(hwnd, hdc);
		}
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