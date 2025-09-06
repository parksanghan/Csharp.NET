/*

메시지 루프 변경 처리

유휴시간 : 화면에 점을 출력!!!

*/

#pragma comment (linker, "/subsystem:windows") // "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

#define CLASS_NAME TEXT("first")
#define WINDOW_NAME TEXT("0904")

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg)
    {
    case WM_CREATE: // 시작점(초기화 처리, 비큐, )
        return 0;

    case WM_DESTROY: // 끝점(종료 처리, 윈도우가 파괴됨)
        PostQuitMessage(0);
        return 0;
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
        100, 300, 500, 800,
        0, 0, hInst, 0);

    ShowWindow(hwnd, nShowCmd);

    // 2. 메시지 루프
    // Getmessage 의 경우 쓰레드 메시지 큐에서 메시지를 가져오는데 메시지가 없으면 새로운 메시지가 전달될 때까지 리턴하지 않는다.
    // 즉 메시지가 없는 경우 무한 대기이다 !
    // - >  만약 메시지가 큐에 없는 경우 다른 작업이 하고 싶다면(-유휴 시간-) 가능한가 ??
    // 그렇다면 이번에 사용할 PeekMessage 함수를 살표보자 
    // 해당 함수는 메시지 큐에서 메시지를 꺼내거나 검사하되 메시지가 없더라도 즉각 리턴한다.
    // 리턴 값이 TRUE 라면 메시지가 있다는 뜻이고 , FALSE 라면 메시지가 없다는 뜻이다 .
    // wRemoveMSG 는 메시지가 있는 경우 이 메시지를 큐에서 제거할 것인지 아닌지를 지정하는데 
    // PM_REMOVE 이면 제거하고  , PM_NOREMOVE 면 제거하지 않는다.
    // 
    MSG msg; // 
    while (true)
    {
        if (PeekMessage(&msg, 0, 0, 0, PM_REMOVE))  // 가져온 메시지는 제거하겠다.
        {
            if (msg.message == WM_QUIT)
                break;
            DispatchMessage(&msg);
        }
        else
        {
            // 화면에 점을 출력하겠다.
            // 1. 그림을 그릴 수 있는 객체 생성 
            HDC hdc = GetDC(0); // 윈도우에 그릴 수 있는 dc를 획득!
            SetPixel(hdc, rand() % 300, rand() % 300, RGB(rand() % 256, rand() % 256, rand() % 256));

            // 3. 객체 소멸처리
            ReleaseDC(hwnd, hdc);
        }
    }

    return 0;
}
