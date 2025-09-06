//10_강제무효화.cpp

/*

1) LButton 클릭시 2개의 사각형 출력

   - (100,10)~(100+100,10+100)

   - (210,10)~(210+100,10+100)

2) RButton 클릭시 강제 무효화 처리 -> 첫번째 그려진 사각형을 지우고자 함
   InvalidateRect

*/
// 클라이언트 전체 무효화
// WM_ERASEBKGND (무효화 영역을 배경 색상으로 칠하는 기능을 수행하는 메시지)
// TRUE : WM_ERASEBKGND 메시지를 호출 O
// FALSE : WM_ERASEBKGND 메시지 호출 X
// InvalidateRect(hwnd, 0, FALSE);

#pragma comment (linker, "/subsystem:windows") // "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

#define CLASS_NAME TEXT("first")
#define WINDOW_NAME TEXT("0904")

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg)
    {
    case WM_RBUTTONDOWN:
    {
        // 클라이언트 전체 무효화
        // WM_ERASEBKGND (무효화 영역을 배경 색상으로 칠하는 기능을 수행하는 메시지)
        // TRUE : WM_ERASEBKGND 메시지를 호출 O
        // FALSE : WM_ERASEBKGND 메시지 호출 X
        // InvalidateRect(hwnd, 0, FALSE);

        // 왼쪽 사각형만 지우고 싶다
        RECT rt = { 100,10, 200, 110 };
        InvalidateRect(hwnd, &rt, TRUE); // 무효화 함수 
        // InvalidteRect(hwd,0 ,TRUE ) 는  전체를 지워주나 더 효율적으로 하기위해 칠한부분만 무효화함

        return 0;
    }
    case WM_LBUTTONDOWN:
    {
        HDC hdc = GetDC(hwnd);

        Rectangle(hdc, 100, 10, 200, 110);
        Rectangle(hdc, 210, 10, 310, 110);

        ReleaseDC(hwnd, hdc); // 핸들

        return 0;
    }
    case WM_CREATE: // 시작점(초기화 처리, 비큐, )
    {
        return 0;
    }
    case WM_DESTROY: // 끝점(종료 처리, 윈도우가 파괴됨)
    {
        PostQuitMessage(0);
        return 0;
    }
    }

    return DefWindowProc(hwnd, msg, wParam, lParam);
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
    // 1. 윈도우 생성, 출력
    WNDCLASS wc;
    wc.cbClsExtra = 0;
    wc.cbWndExtra = 0;
    wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); // 펜, 브러쉬, 폰트
    wc.hCursor = LoadCursor(0, IDC_ARROW); // 시스템
    wc.hIcon = LoadIcon(0, IDI_APPLICATION);
    wc.hInstance = hInst;
    wc.lpfnWndProc = WndProc; // 미리 만들어서 제공되는 프로시저(윈도우 공통 기능)
    wc.lpszClassName = CLASS_NAME;
    wc.lpszMenuName = 0; // 메뉴 등록
    wc.style = 0; // 윈도우 스타일

    RegisterClass(&wc);

    HWND hwnd = CreateWindowEx(0,
        CLASS_NAME, WINDOW_NAME, WS_OVERLAPPEDWINDOW,
        CW_USEDEFAULT, 0, 800, 800,
        // CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,  // 시스템이 알아서 출력!
        0, 0, hInst, 0);

    ShowWindow(hwnd, nShowCmd);

    // 2. 메시지 루프
    MSG msg;
    while (GetMessage(&msg, 0, 0, 0))
    {
        // TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return 0;
}
