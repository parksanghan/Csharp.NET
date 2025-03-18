// 07_송신부(콘솔).cpp
// 사용자 정의 메시지 
// API를 통한 데이터 송수신(교환) 처리 
#include <stdio.h>
#include <Windows.h> // 콘솔과 GUI 솔루션의 차이: subsystem 차이밖에 없음.

// 사용자 정의 메시지
// 나만의 메시지 사용하기 위한 절차.
// 1. 나만의 메시지를 정의한다.
// 2. 메시지 프로시저에서 해당 메시지를 필터링한다.
// 3. 원하는 시점에서 SendMessage를 사용하여 사용자 정의 메시지를 호춣한다.
// 송신한 #define WM_MYMESSAGE WM_USER+100를 통해 SendMEssage를 통해 
// 수신하는 프로그램에서 프로시저 즉 메시지 처리함수에서 사용자가 정의한 메시지를 case문을 통해
// do_something 을 하면 된다. 
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
    // SendMessage 의 경우 상대방이 리턴하기 전까지 리턴하지 못한다 .
    // 따라서 호출 한 프로글매은 상대방이 메시지 박스를 닫기 전까지는 
    // 프로그램이 진행되지 못한다.
    //  -> 메시지 박스가 종료되면 리턴 값이 호출한 프로그램 쪽으로 전달되게 된다.
    // int value = SendMessage(hwnd, WM_MSG1, 100, 200); 
    // printf("결과 : %d\n", value);

    // 큐메시지
    // PostMessage 는 상대방 큐에 메시지를 넣는다. 
    // 따라서 큐에 넣고 바로 리턴하기 때문에 성공과 실패 여부를 BOOL 형 값을 갖는다.

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

#pragma comment (linker, "/subsystem:windows") // "/subsystem:console"

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
