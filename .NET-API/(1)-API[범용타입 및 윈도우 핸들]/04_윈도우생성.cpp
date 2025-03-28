//05_핸들제어.cpp(page24~27)

/*

GDI Object   : 지역변수 형태로 사용

USER Object  : 시스템에 전역로 사용(동일한 윈도우의 핸들을 얻으면 그 핸들값은 같다)

               FindWindow : 이름으로(window명 혹은 class명) 윈도우 핸들을 얻을 수 있다.

   윈도우 핸들만 있으면 해당 윈도우를 제어할 수 있다.

?

[LButton 클릭시] ->[계산기를 종료시키고 싶다]

*/

?

#pragma comment (linker, "/subsystem:windows") // "/subsystem:console"

?

#include <Windows.h>

#include <tchar.h>

?

#define CLASS_NAME TEXT("first")

#define WINDOW_NAME TEXT("0904")

?

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)

{

    switch (msg)

    {

    case WM_LBUTTONDOWN:

    {

        //부가 정보

        POINTS pt = MAKEPOINTS(lParam);

        ?

            TCHAR temp[100];

        wsprintf(temp, TEXT("%d:%d"), pt.x, pt.y);

        SetWindowText(hwnd, temp);

        ?

            //------------------------- FindWindow -----------------------

            //calc

            HWND calc_hwnd = FindWindow(0, TEXT("계산기"));

        if (calc_hwnd != 0)

        {

            TCHAR temp[20];

            wsprintf(temp, TEXT("계산기 핸들 : %d"), calc_hwnd);

            MessageBox(hwnd, temp, TEXT(""), MB_OK);

            ?

                MoveWindow(calc_hwnd, 0, 0, 200, 200, TRUE);

            ?

                MessageBox(hwnd, TEXT("계산기 종료"), TEXT(""), MB_OK);

            ?

                //메시지 프로시저 이름으로 호출 불가

                //SendMessge 함수로 호출 가능!!!!(4개의 인자-> WndProc의 4개의 인자로 전달)

                SendMessage(calc_hwnd, WM_CLOSE, 0, 0);  //**********************

            ?

        }

        else

        {

            MessageBox(hwnd, TEXT("계산기를 먼저 실행해 주세요"), TEXT(""), MB_OK);

        }

        ?

            return 0;

    }

    ?

    case WM_CREATE: //시작점(초기화 처리, 비큐, )

    {

        ?

            return 0;

    }

    case WM_DESTROY: //끝점(종료 처리, 윈도우가 파괴됨)

    {

        PostQuitMessage(0);

        ?

            return 0;

    }

    }

    ?

        return DefWindowProc(hwnd, msg, wParam, lParam);

}

?

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)

{

    //1. 윈도우 생성, 출력

    WNDCLASS wc;

    wc.cbClsExtra = 0;

    wc.cbWndExtra = 0;

    wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //펜, 브러쉬, 폰트

    wc.hCursor = LoadCursor(0, IDC_ARROW);//시스템

    wc.hIcon = LoadIcon(0, IDI_APPLICATION);

    wc.hInstance = hInst;

    wc.lpfnWndProc = WndProc;  //미리 만들어서 제공되는 프로시저(윈도우 공통 기능)

    wc.lpszClassName = CLASS_NAME;

    wc.lpszMenuName = 0; //메뉴 등록

    wc.style = 0; //윈도우 스타일

    ?

        RegisterClass(&wc);

    ?

        HWND hwnd = CreateWindowEx(0,

            CLASS_NAME, WINDOW_NAME, WS_OVERLAPPEDWINDOW,

            100, 300, 500, 800,

            //CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,  //시스템이 알아서 출력!

            0, 0, hInst, 0);

    ?

        ShowWindow(hwnd, nShowCmd);

    ?

        //2. 메시지 루프

        MSG msg;

    while (GetMessage(&msg, 0, 0, 0))

    {

        //TranslateMessage(&msg);

        DispatchMessage(&msg);

    }

    return 0;

}
 