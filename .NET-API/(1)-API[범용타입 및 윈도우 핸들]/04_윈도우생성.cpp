//05_�ڵ�����.cpp(page24~27)

/*

GDI Object   : �������� ���·� ���

USER Object  : �ý��ۿ� ������ ���(������ �������� �ڵ��� ������ �� �ڵ鰪�� ����)

               FindWindow : �̸�����(window�� Ȥ�� class��) ������ �ڵ��� ���� �� �ִ�.

   ������ �ڵ鸸 ������ �ش� �����츦 ������ �� �ִ�.

?

[LButton Ŭ����] ->[���⸦ �����Ű�� �ʹ�]

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

        //�ΰ� ����

        POINTS pt = MAKEPOINTS(lParam);

        ?

            TCHAR temp[100];

        wsprintf(temp, TEXT("%d:%d"), pt.x, pt.y);

        SetWindowText(hwnd, temp);

        ?

            //------------------------- FindWindow -----------------------

            //calc

            HWND calc_hwnd = FindWindow(0, TEXT("����"));

        if (calc_hwnd != 0)

        {

            TCHAR temp[20];

            wsprintf(temp, TEXT("���� �ڵ� : %d"), calc_hwnd);

            MessageBox(hwnd, temp, TEXT(""), MB_OK);

            ?

                MoveWindow(calc_hwnd, 0, 0, 200, 200, TRUE);

            ?

                MessageBox(hwnd, TEXT("���� ����"), TEXT(""), MB_OK);

            ?

                //�޽��� ���ν��� �̸����� ȣ�� �Ұ�

                //SendMessge �Լ��� ȣ�� ����!!!!(4���� ����-> WndProc�� 4���� ���ڷ� ����)

                SendMessage(calc_hwnd, WM_CLOSE, 0, 0);  //**********************

            ?

        }

        else

        {

            MessageBox(hwnd, TEXT("���⸦ ���� ������ �ּ���"), TEXT(""), MB_OK);

        }

        ?

            return 0;

    }

    ?

    case WM_CREATE: //������(�ʱ�ȭ ó��, ��ť, )

    {

        ?

            return 0;

    }

    case WM_DESTROY: //����(���� ó��, �����찡 �ı���)

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

    //1. ������ ����, ���

    WNDCLASS wc;

    wc.cbClsExtra = 0;

    wc.cbWndExtra = 0;

    wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //��, �귯��, ��Ʈ

    wc.hCursor = LoadCursor(0, IDC_ARROW);//�ý���

    wc.hIcon = LoadIcon(0, IDI_APPLICATION);

    wc.hInstance = hInst;

    wc.lpfnWndProc = WndProc;  //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)

    wc.lpszClassName = CLASS_NAME;

    wc.lpszMenuName = 0; //�޴� ���

    wc.style = 0; //������ ��Ÿ��

    ?

        RegisterClass(&wc);

    ?

        HWND hwnd = CreateWindowEx(0,

            CLASS_NAME, WINDOW_NAME, WS_OVERLAPPEDWINDOW,

            100, 300, 500, 800,

            //CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,  //�ý����� �˾Ƽ� ���!

            0, 0, hInst, 0);

    ?

        ShowWindow(hwnd, nShowCmd);

    ?

        //2. �޽��� ����

        MSG msg;

    while (GetMessage(&msg, 0, 0, 0))

    {

        //TranslateMessage(&msg);

        DispatchMessage(&msg);

    }

    return 0;

}
 