//11_WM_PAINT에서 그림을 그리자.cpp

/*

LButtonDown 클릭시 해당 위치에 50*50 사각형을 출력하고자 한다.

단, 해당 그림은 무효화 영역이 발생해도 지워지지 않아야 한다.

*/

// VECTOR 를 활용한 예제 .
// 1. 왼쪽 클릭 이벤트를 받아 좌표 값을 VECTOR 배열에 저장 
// 2. InvalidateRect 를 통한 무효화로 좌표 값에 대한 영역을 무효화하여 
#pragma comment (linker, "/subsystem:windows") // "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <vector>

using namespace std;

// 전역 변수
vector<POINTS> points;

#define CLASS_NAME TEXT("first")
#define WINDOW_NAME TEXT("0904")

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg)
    {
    case WM_PAINT:
    {
        PAINTSTRUCT ps;
        HDC hdc = BeginPaint(hwnd, &ps);
        HPEN pen = CreatePen(PS_SOLID, 5, RGB(255, 0, 0));
        HPEN old = (HPEN)SelectObject(hdc, pen);
        Rectangle(hdc, 10, 10, 100, 100);
        DeleteObject(SelectObject(hdc, old));
        EndPaint(hwnd, &ps);
    }
    return 0;
    ;
    case WM_DESTROY:
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
    wc.style = 0;        // 윈도우 스타일

    RegisterClass(&wc);

    HWND hwnd = CreateWindowEx(0,
        CLASS_NAME, WINDOW_NAME, WS_OVERLAPPEDWINDOW,
        CW_USEDEFAULT, 0, 800, 800,
        0, 0, hInst, 0);

    ShowWindow(hwnd, nShowCmd);

    // 2. 메시지 루프
    MSG msg;
    while (GetMessage(&msg, 0, 0, 0))
    {
        DispatchMessage(&msg);
    }

    return 0;
}
