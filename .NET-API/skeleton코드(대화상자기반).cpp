#include <Windows.h>
#include <tchar.h>
#include "resource.h"

BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg)
    {
    case WM_INITDIALOG:
    {
        // 대화 상자 초기화 코드를 여기에 추가하세요.
        return TRUE;
    }
    
    case WM_COMMAND:
    {
        switch (LOWORD(wParam))
        {
        case IDCANCEL:
            EndDialog(hDlg, IDCANCEL);
            return TRUE;
        }
    }
    }
    return FALSE;
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
    UINT ret = DialogBox(hInst,       // instance
        MAKEINTRESOURCE(IDD_DIALOG1),  // 다이얼로그 리소스 ID
        0,             // 부모 윈도우 핸들 (없음)
        DlgProc);      // 대화 상자 프로시저

    return 0;
}
