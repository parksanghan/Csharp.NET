// 09_송신부(콘솔).cpp

#include <stdio.h>
#include <Windows.h> // 콘솔과 gui 솔루션의 차이밖에 없음.

#define WM_MSG1 WM_USER + 1

#define WM_MSG_RECTANGLE WM_USER + 2
#define WM_MSG_ELLIPSE WM_USER + 3

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

// 07_수신부(GUI).cpp

#pragma comment(linker, "/subsystem:windows") // "/subsystem:console"

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

    return Def
