//01_�����޸�1

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

typedef struct _LINE 
{
	POINTS ptFrom; 
	POINTS ptTo;
} LINE;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static HANDLE hEvent, hMap;		//Ŀ�� �ڵ�
	static LINE* pData;
	static POINTS ptFrom;		//������.

	switch (msg)
	{
	case WM_LBUTTONDOWN: 
		ptFrom = MAKEPOINTS(lParam); 
		return 0;

	case WM_MOUSEMOVE:
		if (wParam & MK_LBUTTON) //LButton���� ���¿��� MouseMove
		{
			POINTS pt = MAKEPOINTS(lParam); 
			HDC hdc = GetDC(hwnd);

			MoveToEx(hdc, ptFrom.x, ptFrom.y, 0); 
			LineTo(hdc, pt.x, pt.y); 

			ReleaseDC(hwnd, hdc);
			
			// MMF ��   �ִ´�.
			pData->ptFrom = ptFrom; 
			pData->ptTo = pt; 
			SetEvent(hEvent); 
			ptFrom = pt;
		} 
		return 0;
	case WM_CREATE:
	{
		hEvent = CreateEvent(0, TRUE, 0, TEXT("DRAW_SIGNAL"));

		//MMF
		hMap = CreateFileMapping((HANDLE)-1, // Paging ȭ����   ����ؼ�   ����
			0, PAGE_READWRITE, 0, sizeof(LINE), TEXT("map"));

		pData = (LINE*)MapViewOfFile(hMap, FILE_MAP_WRITE, 0, 0, 0); 
		
		if (pData == 0)
			MessageBox(0, TEXT("Fail"), TEXT(""), MB_OK);
		return 0;
	}
	case WM_DESTROY:
	{
		UnmapViewOfFile(pData); 
		CloseHandle(hMap);
		CloseHandle(hEvent);

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
		TEXT("FIRST"), TEXT("1��"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 500, 500,
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