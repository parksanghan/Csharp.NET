//skeleton �ڵ�
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

typedef struct _LINE
{
	POINTS ptFrom;
	POINTS ptTo;
} LINE;

DWORD WINAPI foo(void* p) 
{	
	HWND hwnd = (HWND)p;

	HANDLE hEvent = OpenEvent(EVENT_ALL_ACCESS, FALSE, TEXT("DRAW_SIGNAL")); 
	HANDLE hMap = OpenFileMapping(FILE_MAP_ALL_ACCESS, FALSE, TEXT("map")); 
	if (hMap == 0)
	{
		MessageBox(0, TEXT("1��   ���α׷���   ����   �����ϼ���"),TEXT(""), MB_OK);
		return 0;
	}

	LINE* pData = (LINE*)MapViewOfFile(hMap, FILE_MAP_READ, 0, 0, 0); 
	
	while (1)
	{
		// Event��   ����Ѵ�.
		WaitForSingleObject(hEvent, INFINITE); // ����    Line��   ������   pData��   �ִ�. 
		
		HDC hdc = GetDC( hwnd );
		MoveToEx(hdc, pData->ptFrom.x, pData->ptFrom.y, 0); 
		LineTo(hdc, pData->ptTo.x, pData->ptTo.y); 
		ReleaseDC(hwnd, hdc);
	}
	UnmapViewOfFile(pData); 
	CloseHandle(hMap); 
	CloseHandle(hEvent); 
	return 0;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		//foo(hwnd);
		//������ ����
		DWORD threadid;
		HANDLE hThread = CreateThread(0, 0, foo, hwnd, 0, &threadid);		
		CloseHandle(hThread);
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
		TEXT("FIRST"), TEXT("2��"), WS_OVERLAPPEDWINDOW,
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