//07_송신부(콘솔).cpp

#include <stdio.h>
#include <Windows.h>

#define WM_MSG1 WM_USER + 1
#define WM_MSG_RECTANGLE WM_USER + 2
#define WM_MSG_ELLIPSE WM_USER + 3 // 윈도우 메시지 상수 중에서 가장 마지막에 위치한 값 
// ->  사용자 정의 메시지에 값을 난해하여 겹치지 않도록 하여 충돌 방지

// 정수 2개 전달 -> 합의 결과 반환
void exam1()
{
    HWND hwnd = FindWindow(0, TEXT("A"));

    if (hwnd == 0)
    {
        printf("A를 먼저 실행하세요\n");
        return;
    }

    printf("메시지 전송................\n");

    // 비큐메시지
    // int value = SendMessage(hwnd, WM_MSG1, 100, 200);
    // printf("결과 : %d\n", value);

    // 큐메시지
    bool value = PostMessage(hwnd, WM_MSG1, 100, 200);

    if (value == true)
        printf("메시지 송신 성공\n");
    else
        printf("메시지 송신 실패\n");
}

// 좌표 전달
void exam2()
{
    HWND hwnd = FindWindow(0, TEXT("A"));

    if (hwnd == 0)
    {
        printf("A를 먼저 실행하세요\n");
        return;
    }

    int x, y, type;

    while (1)
    {
        printf("x좌표 : ");
        scanf_s("%d", &x);

        printf("y좌표 : ");
        scanf_s("%d", &y);

        printf("[1]사각형 [2]타원 : ");
        scanf_s("%d", &type);

        if (type == 1)
            SendMessage(hwnd, WM_MSG_RECTANGLE, x, y);
        else if (type == 2)
            SendMessage(hwnd, WM_MSG_ELLIPSE, x, y);
    }
}

int main()
{
    exam2();

    return 0;
}

//07_수신부(GUI).cpp

#pragma comment(linker, "/subsystem:windows")

#include <Windows.h>
#include <tchar.h>

#define WM_MSG1 WM_USER + 1
#define WM_MSG_RECTANGLE WM_USER + 2
#define WM_MSG_ELLIPSE WM_USER + 3

#define CLASS_NAME TEXT("first")
#define WINDOW_NAME TEXT("A")

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg)
    {
    case WM_MSG_RECTANGLE:
    {
        HDC hdc = GetDC(hwnd);

        int x = (int)wParam;
        int y = (int)lParam;

        Rectangle(hdc, x, y, x + 50, y + 50);

        ReleaseDC(hwnd, hdc);

        return 0;
    }
    case WM_MSG_ELLIPSE:
    {
        HDC hdc = GetDC(hwnd);

        int x = (int)wParam;
        int y = (int)lParam;

        Ellipse(hdc, x, y, x + 50, y + 50);

        ReleaseDC(hwnd, hdc);

        return 0;
    }
    case WM_MSG1:
    {
        TCHAR buf[20];
        wsprintf(buf, TEXT("%d, %d"), wParam, lParam);
        MessageBox(hwnd, buf, TEXT("수신"), MB_OK);

        return wParam + lParam;
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
    // 1. 윈도우 생성, 출력
    WNDCLASS wc;
    wc.cbClsExtra = 0;
    wc.cbWndExtra = 0;
    wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); // 펜, 브러쉬, 폰트
    wc.hCursor = LoadCursor(0, IDC_ARROW); // 시스템
    wc.hIcon = LoadIcon(0, IDI_APPLICATION);
    wc.hInstance = hInst;
    wc.lpfnWndProc = WndProc;  // 미리 만들어서 제공되는 프로시저(윈도우 공통 기능)
    wc.lpszClassName = CLASS_NAME;
    wc.lpszMenuName = 0; // 메뉴 등록
    wc.style = 0; // 윈도우 스타일

    RegisterClass(&wc);

    HWND hwnd = CreateWindowEx(0,
        CLASS_NAME, WINDOW_NAME, WS_OVERLAPPEDWINDOW,
        100, 300, 500, 500,
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
