//06_�޽���ó��(04_������ �����ڵ忡 �߰�)
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>

//�޽��� ó���Լ�(���ν���, �ý����Լ�)
//Callback : �̸� ����ؼ� �ʿ��� �� ȣ��Ǵ� �Լ�
LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN: //���콺L��ư Ŭ��
	{
		POINTS pt = MAKEPOINTS(lParam);
		TCHAR msg[10];
		wsprintf(msg, _TEXT("%d, %d"), pt.x, pt.y);

		SetWindowText(hwnd, msg);

		return 0;
	}

	//�ʱ�ȭ ����
	case WM_CREATE:    //��ť�޽���(������ ����, CreateWindow�Լ� ��ȯ����)
	{
		return 0;
	}

	//����ó�� ����
	case WM_DESTROY:	//�����찡 �ı��Ǿ��� �� ȣ��Ǵ� �޽���
		//App ť�� WM_QUIT �޽����� �־�� �Ѵ�.
		PostQuitMessage(0);
		return 0;
	}

	return DefWindowProc(hwnd, msg, wParam, lParam);
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR cmd, int show)
{
	//������ ����
	//1. ������ Ŭ���� ����
	WNDCLASS	wc;
	wc.cbClsExtra = 0;  //���߿� Ȥ�� (�ӽ� �ɹ�)
	wc.cbWndExtra = 0;
	//--------------------------------------------------------------------
	wc.hbrBackground = (HBRUSH)GetStockObject(LTGRAY_BRUSH);  //���
	wc.hCursor = LoadCursor(0, IDC_ARROW);
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;  //�ڽ��� �ν��Ͻ�(�� ������ Ŭ������ ���� ���������?)
	wc.lpfnWndProc = WndProc;		//���� ���� ���ν��� ���
	//-----------------------------------------------------------------------
	wc.lpszClassName = _TEXT("First");  //KEY �ߺ��Ǹ� �ȵ�
	wc.lpszMenuName = 0;  //�޴��� ���ҽ����̵�
	wc.style = 0;

	//2. ������ Ŭ������ ������Ʈ���� ���
	RegisterClass(&wc);

	//3. ������ ����(ù��° ��ü) : UI��ü(�޸�, ������) -> �ڵ�
	HWND hwnd = CreateWindowEx(0, _TEXT("First"), _TEXT("���������"),
		WS_OVERLAPPEDWINDOW & ~WS_THICKFRAME,
		100, 100, 500, 500,
		0, 0, hInst, 0);

	//4. ȭ�� ���
	ShowWindow(hwnd, show);

	//5. �޽�������
	//GetMessage : �ڽ��� App Queue���� �޽����� �����´�.
	//���࿡ App Queue�� �޽����� ���ٸ�? ���� ��� ex) scanf_s()
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0)) //GetMessage : WM_QUIT �϶��� ������ false
	{
		//��ϵ� ���ν��� ����
		//�ش� ���ν����� ����Ǿ�߸� ����.
		DispatchMessage(&msg);
	}

	return 0;
}