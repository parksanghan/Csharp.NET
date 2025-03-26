//18_��Ʈ��.cpp
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

//��Ʈ�� ������ �ʿ��� #define.. ��������(�ڵ������)
// �� , ��Ʈ�� ID
#define IDC_BUTTON		1
#define IDC_EDIT		2
#define IDC_LISTBOX		3
#define IDC_COMBOBOX	4
/*
��Ʈ�ѵ� �ϳ��� �������̴�. �����츦 ���� ���� WNDCLASS ���� ����ü�� �����ϰ� R
egisterClass �Լ��� ����Ͽ� ����� �� CreateWindow �Լ��� ȣ���Ѵ�.
*/
HWND hBtn, hEdit, hListBox, hComboBox;
// ��� �߰� 1. ���ڿ��� �����̶�� ���� �ʴ´�.
// ��� �߰� 2. PUSH ��ư �۵� �� ���� COMBOBOX�� HIDE SHOW �ݺ��Ѵ�.
LRESULT OnCreate(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	//1����
	/*
	��Ʈ�ѵ� �ϳ��� �������̴�. �����츦 ���� ���� WNDCLASS ���� ����ü�� �����ϰ�
	RegisterClass �Լ��� ����Ͽ� ����� �� CreateWindow �Լ��� ȣ���Ѵ�.

	�׷��� ��Ʈ���� ������� �ü�� �������� ������ �ֱ� ������
	������ Ŭ������ ���� �ʿ� ���� ������� �̸� ���ǵǾ� �ִ� ������ Ŭ������ ����ϱ⸸ �ϸ� �ȴ�.



	��Ʈ���� �����ϱ� ���ؼ��� 2���� ������ �ʿ��ϴ�.
	ù ��°�� ��Ʈ���� ID �̰�, �� ��°�� ��Ʈ���� �ڵ��̴�
	*/



	hEdit = CreateWindow(TEXT("edit"), TEXT(""), WS_CHILD | WS_VISIBLE | WS_BORDER,
		10, 10, 90, 20, hwnd, (HMENU)IDC_EDIT, 0, 0);

	hBtn = CreateWindow(TEXT("button"), TEXT("PUSH"), WS_CHILD | WS_VISIBLE | WS_BORDER,
		10, 40, 90, 20, hwnd, (HMENU)IDC_BUTTON, 0, 0);

	hListBox = CreateWindow(TEXT("listbox"), TEXT(""),
		WS_CHILD | WS_VISIBLE | WS_BORDER | LBS_NOTIFY,
		150, 10, 90, 90, hwnd, (HMENU)IDC_LISTBOX, 0, 0);

	hComboBox = CreateWindow(TEXT("combobox"), TEXT(""),
		WS_CHILD | WS_VISIBLE | WS_BORDER | CBS_DROPDOWNLIST,
		250, 10, 150, 200, hwnd, (HMENU)IDC_COMBOBOX, 0, 0);

	return 0;
}

LRESULT OnCommand(HWND hwnd, WPARAM wParam, LPARAM lParam) // �̺�Ʈ �߻�
{	// �ش� �Լ������� ���ڿ��� ���� ó���� �Ѵ�.
	switch (LOWORD(wParam)) //��Ʈ���� ID 
		//LOWORD�� ������ �޽����� wParam �Ű��������� ���� ����(16��Ʈ)�� �����ϴ� ��ũ��

	{
	case IDC_BUTTON:
	{
		TCHAR buf[100];
		int textLength_test = GetWindowTextLength(hEdit); // ������ �ؽ�Ʈ ���� �˻�
		// ������ ���̰� ���°�� �� �ƹ��͵� �Է����� �ʰ� ������� ó������ ����
		if (textLength_test > 0) { // ���ڿ��� ���̰� 0 �̻��� ��츸 ó��
			GetWindowText(hEdit, buf, _countof(buf));  //?????

			SendMessage(hListBox, LB_ADDSTRING, 0, (LPARAM)buf); // Str �� �߰��Ѵٴ� �޽��� ���� buf �� �ִ� �迭�� ���� 
			SendMessage(hComboBox, CB_ADDSTRING, 0, (LPARAM)buf); // Str �� �߰��Ѵٴ� �޽��� ���� buf �� �ִ� �迭�� ���� 

			SetWindowText(hEdit, TEXT(""));
			static bool isComboBoxVisible = TRUE; // �޺� �ڽ��� ���ü� ���¸� ����
			isComboBoxVisible = !isComboBoxVisible; // ���ü� ���¸� ����

			if (isComboBoxVisible) {
				ShowWindow(hComboBox, SW_SHOW);
			}
			else {
				ShowWindow(hComboBox, SW_HIDE);
			}

		}
		break;
	}

	case IDC_COMBOBOX:
	{

		if (HIWORD(wParam) == CBN_SELCHANGE)  //��?? -> CBS_SELCHANGE ��Ʈ�� wParam ���� HIWORD(wParam) ���� ���ٸ�
			// HIWORD �� y��ǥ ���̴�.
			// �� , wParam ���� �޽����� ��������� �� �޽����� CBN_SELCHANGE  �ٲ������ �˻��Ѵ�.
			// �ٲ���ٸ� , ������ �ε����� ���ڿ��� ComboBox����  ȹ���ϰ� �� ���ڿ��� ����� ���� �ٿ� ����Ѵ�.
		{
			//1.������ �ε��� ȹ��
			int idx = (int)SendMessage(hComboBox, CB_GETCURSEL, 0, 0);

			//2.�ش� �ε����� ���ڿ� ȹ��
			TCHAR buf[100] = TEXT("");
			SendMessage(hComboBox, CB_GETLBTEXT, idx, (LPARAM)buf);

			//3. Ÿ��Ʋ�ٿ� ���
			SetWindowText(hwnd, buf);

		}
		break;
	}

	case IDC_LISTBOX: // ����Ʈ �ڽ� 
	{
		if (HIWORD(wParam) == LBN_SELCHANGE)  //��??
			// HIWORD(wParam) �� y��ǥ ���� ���� �޽��� ���̴�. 
			// �� , �޽����� LBN_SELCHANGE �ٲ���ٸ�
		{	// LB_GETCURSEL�� ���� Ŀ���� ������ �ε����� ��ȯ�ϰ�
			// �ε����� ���ڿ��� �ε����� �°� �����ش�.

			//1.������ �ε��� ȹ��
			int idx = (int)SendMessage(hListBox, LB_GETCURSEL, 0, 0);

			//2.�ش� �ε����� ���ڿ� ȹ��
			TCHAR buf[100] = TEXT("");
			SendMessage(hListBox, LB_GETTEXT, idx, (LPARAM)buf);

			SetWindowText(hwnd, buf);
		}
		break;
	}
	}
	return 0;
};


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_CREATE:		return OnCreate(hwnd, wParam, lParam);
	case WM_COMMAND:	return OnCommand(hwnd, wParam, lParam);

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
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}