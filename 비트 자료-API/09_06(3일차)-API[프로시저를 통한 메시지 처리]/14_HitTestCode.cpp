//14_HitTestCode.cpp

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
// Ŭ���̾�Ʈ ���� ���콺 �޽����� ��� Ŭ���� ��Ʈ ��ǥ��, ��Ŭ���̾�Ʈ ���� �޼����ϰ��� ��ũ�� ��ǥ�� ���´�.
// �Ʒ��� ��ũ�θ� ����ϸ� lParam���� ���� ���� x, y ��ǥ�� ���� �� �ִ�.x, y��ǥ�� ���� �� �ִ�.
// ���� �� Ŭ���̾�Ʈ ���� ���콺 �޽����� ���� lParam�� ��� ������ ��ǥ�� ������,
// wParam���� hit - test code�� ���´�.
/*
int x = LOWORD(lParam);
int y = HIWORD(lParam);
*/

// ��ũ�� ��ǥ��  ���� �ڵ�� ���� �� �ִ�.
// ������ Ŭ���̾�Ʈ ��ǥ�� ��� ClientToScreen �Լ��� ��ǥ�� ���� �� �ִ�.
/*
POINTS pt = MAKEPOINTS(lParam);
POINT pt1 = { LOWORD(lParam),HIWORD(lParam)};
TCHAR buf[50];														=> Ŭ���̾�Ʈ ��ǥ�� ��´�.
wsprintf(buf, TEXT("(%d:%d)\r\n(%d:%d)"), pt.x, pt.y, pt1.x, pt1.y);
MessageBox(NULL, buf, TEXT(""), MB_OK);

*/
// -> HIT TEST code �� ���� Ŀ���� ��� �κп� �ִ����� �˷��ִ� ��� �̴�.
// => WM_NCHITEST �޽����� DefWindowPorc()�� ���޵� �� �� �ڵ�� �����Ѵ�.

 

�����ϰ��� ��ũ�� ��ǥ�� ���´�.�Ʒ��� ��ũ�θ� ����ϸ� lParam���� ���� ����
x, y��ǥ�� ���� �� �ִ�.
#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_NCHITTEST: // Ŭ���̾�Ʈ ������ �ƴ� ��� 
	{
		int code = DefWindowProc(hwnd, msg, wParam, lParam); // 
		if (code == HTCLIENT && GetKeyState(VK_CONTROL) < 0)//-> ctrl Ű ��ư ������ �ִ��� Ȯ���ϰ� , ���콺 Ŀ���� Ŭ���̾�Ʈ ������ �ִ��� Ȯ��
		{	//HTCLIENT��  WM_NCHITEST �޽������� ��ȯ�Ǵ� �޽��� �� �ϳ�.
			
			code = HTCAPTION; //	  �׷��ٸ� HTCAPTION ���� ���� �����ϰ�  
			// Ÿ��Ʋ �ٸ� �巡�� �ϴ� ��ó�� �̵��� �� �ִ�.
		}

		if (code == HTLEFT)  // ���� DefWindowProc�� ��ȯ �� ���� HTLEFT �� ���
							// �� , Ŭ���̾�Ʈ ���ʿ� Ŀ���� �� ��� , ���� HTRIGHT �� �����Ѵ�. 
							// -> �̷��� �Ͽ� ���ʿ��� �巡�� �ص� �����ʿ��� ���̾ƿ��� ������.
			code = HTRIGHT;

		return code;
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