//15_���콺ĸó.cpp
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>
#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")\
POINTS start, end;

/*
�Ϲ������� ���콺 �޽����� �߻��� ��� Ŀ�� �Ʒ� �ִ� �����쿡 ���޵˴ϴ�. SetCapture()�� ��� ��� ���콺�� �޽����� ĸ���� ���콺�� ĸ���� �����쿡�� ���޵˴ϴ�.

Q :   if(GetCapture() == hwnd)   ->  if(GetCapture() != hwnd)
��� ��� �۵��� �ǳ�?
������� ���� Ŭ�� �ÿ� SetCapture()�� �Ѵ�.
���� WM_MOUSEMOVE �̺�Ʈ �߻� ��
���� �ڵ忡����
  if(GetCapture() != hwnd)
=> ���� Ŀ���� �ִ� ������� ������ ������� ���� �ʴ� ��� �����Ѵ�.
�׷��⿡ Ŀ���� �ִ� ������� ������ �����찡 ���� ��쿡�� ��ǥ�� ���� ������ ����Ѵ�.

���� ,  if(GetCapture() == hwnd) �� �ٲٰ� �ȴٸ�?
��Ŭ���� �����ϰ� ������� Ŭ�� �ÿ� SetCapture()�� �Ѵ�.
���� MOUSEMOVE ����  if(GetCapture() == hwnd) �� ����
���콺�� ������ �����찡 ���� �Ǵ� �׳� �����Ѵ�.

�׷��⿡ ������ ���� ���� �׷����� �ʴ´�.
���� Ŭ���� ���� ��쿡�� ���� ��Ÿ����
���콺 ĸ�İ� ���� ������� ���� ��츸 �ڵ尡 ����Ǹ� �ٸ� �����쿡�� �߻��� ���콺 �޽����� ���õȴ�.
*/


/*
�Ϲ������� ���콺 �޽�����, �޽����� �߻��� ��� Ŀ���� �Ʒ� �ִ� �����쿡�� ���޵ȴ�.
������ SetCapture()�Լ��� ��� �ϹǷμ� �̷� �ൿ�� ������ �� �ִ�.
Ư�������찡 SetCapture()�Լ��� ����ؼ� ���콺�� ĸ���� ���, ��� ���콺 �޽�����
���콺�� ĸ���� �����쿡�Է� ���޵ȴ�.
*/


/*
���콺 ĸ�Ĵ� Ŀ���� �����츦 ������� ��� �޽����� �ް��� �Ҷ� �����ϰ� ���
�ȴ�.
�ּ� BUTTONDOWN���� ĸ�ĸ� BUTTONUP���� Release�� ���ش�.
���� � �����찡 ĸ�ĸ� �ߴ����� �˾Ƴ����� GetCapture() �Լ��� ����Ѵ�.
*/
LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		SetCapture(hwnd);					//<----ĸ��
		start = end = MAKEPOINTS(lParam);		
		return 0;
	}
	case WM_MOUSEMOVE:
	{
		if (GetCapture() != hwnd)			//<-- ���� �������ΰ�?
			return 0;		

		HDC hdc = GetDC(hwnd);
		SetROP2(hdc, R2_NOT);		//>>>> ������� (0�� 1�� , 1�� 0����)

		//������ �׸� �׸��� �����
		MoveToEx(hdc, start.x, start.y, 0);
		LineTo(hdc, end.x, end.y);

		//��ǥ �̵�
		end = MAKEPOINTS(lParam);

		//�̵��� ��ǥ�� �׸���
		MoveToEx(hdc, start.x, start.y, 0);
		LineTo(hdc, end.x, end.y);

		ReleaseDC(hwnd, hdc);		

		return 0;
	}
	case WM_LBUTTONUP:
	{
		ReleaseCapture();			//<===ĸ�� ����

		return 0;
	}
	case WM_CREATE:	//������(�ʱ�ȭ ó��, ��ť, )
	{
		
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
		CW_USEDEFAULT, 0, 800, 800,
		//CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,  //�ý����� �˾Ƽ� ���!
		0, 0, hInst, 0);

	ShowWindow(hwnd, nShowCmd);
	UpdateWindow(hwnd);

	//2. �޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		//TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}