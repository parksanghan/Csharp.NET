//04_������ ����
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>

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
	wc.lpfnWndProc = DefWindowProc;		//�̸� ���� �����Ǵ� ���ν���
	//-----------------------------------------------------------------------
	wc.lpszClassName = _TEXT("First");  //KEY �ߺ��Ǹ� �ȵ�
	wc.lpszMenuName = 0;  //�޴��� ���ҽ����̵�
	wc.style = 0;
	
	//2. ������ Ŭ������ ������Ʈ���� ���
	RegisterClass(&wc);

	//3. ������ ����(ù��° ��ü) : UI��ü(�޸�, ������) -> �ڵ�
	HWND hwnd = CreateWindowEx(0, _TEXT("First"), _TEXT("���������"),
		WS_OVERLAPPEDWINDOW &~WS_THICKFRAME,
		100, 100, 500, 500,
		0, 0, hInst, 0);
	
	//4. ȭ�� ���
	ShowWindow(hwnd, show);

	MessageBox(0, _TEXT("Hello, World!"), _TEXT("�˸�"), MB_OKCANCEL | MB_ICONQUESTION);

	return 0;
}