//18_컨트롤.cpp
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

#define CLASS_NAME	TEXT("first")
#define WINDOW_NAME	TEXT("0904")

//컨트롤 생성에 필요한 #define.. 전역변수(핸들저장용)
// 즉 , 컨트롤 ID
#define IDC_BUTTON		1
#define IDC_EDIT		2
#define IDC_LISTBOX		3
#define IDC_COMBOBOX	4
/*
컨트롤도 하나의 윈도우이다. 윈도우를 만들 때는 WNDCLASS 형의 구조체를 정의하고 R
egisterClass 함수를 사용하여 등록한 후 CreateWindow 함수를 호출한다.
*/
HWND hBtn, hEdit, hListBox, hComboBox;
// 기능 추가 1. 문자열이 공백이라면 받지 않는다.
// 기능 추가 2. PUSH 버튼 작동 시 마다 COMBOBOX를 HIDE SHOW 반복한다.
LRESULT OnCreate(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	//1라인
	/*
	컨트롤도 하나의 윈도우이다. 윈도우를 만들 때는 WNDCLASS 형의 구조체를 정의하고
	RegisterClass 함수를 사용하여 등록한 후 CreateWindow 함수를 호출한다.

	그러나 컨트롤은 윈도우즈가 운영체제 차원에서 제공해 주기 때문에
	윈도우 클래스를 만들 필요 없이 윈도우즈에 미리 정의되어 있는 윈도우 클래스를 사용하기만 하면 된다.



	컨트롤을 생성하기 위해서는 2가지 정보가 필요하다.
	첫 번째는 컨트롤의 ID 이고, 두 번째는 컨트롤의 핸들이다
	*/



	hEdit = CreateWindow(TEXT("edit"), TEXT(""), WS_CHILD | WS_VISIBLE | WS_BORDER,
		10, 10, 90, 20, hwnd, (HMENU)IDC_EDIT, 0, 0);

	hBtn = CreateWindow(TEXT("button"), TEXT("PUSH"), WS_CHILD | WS_VISIBLE | WS_BORDER,
		10, 40, 90, 20, hwnd, (HMENU)IDC_BUTTON, 0, 0);

	hListBox = CreateWindow(TEXT("listbox"), TEXT(""),
		WS_CHILD | WS_VISIBLE | WS_BORDER | LBS_NOTIFY,
		150, 10, 90, 90, hwnd, (HMENU)IDC_LISTBOX, 0, 0);

	hComboBox = CreateWindow(TEXT("combobox"), TEXT(""),
		WS_CHILD | WS_VISIBLE | WS_BORDER | CBS_DROPDOWNLIST,
		250, 10, 150, 200, hwnd, (HMENU)IDC_COMBOBOX, 0, 0);

	return 0;
}

LRESULT OnCommand(HWND hwnd, WPARAM wParam, LPARAM lParam) // 이벤트 발생
{	// 해당 함수에서는 문자열에 대한 처리만 한다.
	switch (LOWORD(wParam)) //컨트롤의 ID 
		//LOWORD는 윈도우 메시지의 wParam 매개변수에서 낮은 워드(16비트)를 추출하는 매크로

	{
	case IDC_BUTTON:
	{
		TCHAR buf[100];
		int textLength_test = GetWindowTextLength(hEdit); // 가져온 텍스트 길이 검사
		// 문자의 길이가 없는경우 즉 아무것도 입력하지 않고 누른경우 처리하지 않음
		if (textLength_test > 0) { // 문자열의 길이가 0 이상인 경우만 처리
			GetWindowText(hEdit, buf, _countof(buf));  //?????

			SendMessage(hListBox, LB_ADDSTRING, 0, (LPARAM)buf); // Str 을 추가한다는 메시지 전달 buf 에 있는 배열을 통해 
			SendMessage(hComboBox, CB_ADDSTRING, 0, (LPARAM)buf); // Str 을 추가한다는 메시지 전달 buf 에 있는 배열을 통해 

			SetWindowText(hEdit, TEXT(""));
			static bool isComboBoxVisible = TRUE; // 콤보 박스의 가시성 상태를 추적
			isComboBoxVisible = !isComboBoxVisible; // 가시성 상태를 반전

			if (isComboBoxVisible) {
				ShowWindow(hComboBox, SW_SHOW);
			}
			else {
				ShowWindow(hComboBox, SW_HIDE);
			}

		}
		break;
	}

	case IDC_COMBOBOX:
	{

		if (HIWORD(wParam) == CBN_SELCHANGE)  //왜?? -> CBS_SELCHANGE 컨트롤 wParam 값과 HIWORD(wParam) 값이 같다면
			// HIWORD 는 y좌표 값이다.
			// 즉 , wParam 에는 메시지가 들어있으며 그 메시지가 CBN_SELCHANGE  바뀌었는지 검사한다.
			// 바뀌었다면 , 선택한 인덱스의 문자열을 ComboBox에서  획득하고 그 문자열을 상단의 제목 바에 출력한다.
		{
			//1.선택한 인덱스 획득
			int idx = (int)SendMessage(hComboBox, CB_GETCURSEL, 0, 0);

			//2.해당 인덱스의 문자열 획득
			TCHAR buf[100] = TEXT("");
			SendMessage(hComboBox, CB_GETLBTEXT, idx, (LPARAM)buf);

			//3. 타이틀바에 출력
			SetWindowText(hwnd, buf);

		}
		break;
	}

	case IDC_LISTBOX: // 리스트 박스 
	{
		if (HIWORD(wParam) == LBN_SELCHANGE)  //왜??
			// HIWORD(wParam) 는 y좌표 값에 대한 메시지 값이다. 
			// 즉 , 메시지가 LBN_SELCHANGE 바뀌었다면
		{	// LB_GETCURSEL을 통해 커서가 선택한 인덱스를 반환하고
			// 인덱스의 문자열을 인덱스에 맞게 보여준다.

			//1.선택한 인덱스 획득
			int idx = (int)SendMessage(hListBox, LB_GETCURSEL, 0, 0);

			//2.해당 인덱스의 문자열 획득
			TCHAR buf[100] = TEXT("");
			SendMessage(hListBox, LB_GETTEXT, idx, (LPARAM)buf);

			SetWindowText(hwnd, buf);
		}
		break;
	}
	}
	return 0;
};


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_CREATE:		return OnCreate(hwnd, wParam, lParam);
	case WM_COMMAND:	return OnCommand(hwnd, wParam, lParam);

	case WM_DESTROY: //끝점(종료 처리, 윈도우가 파괴됨)
	{
		PostQuitMessage(0);
		return 0;
	}
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//1. 윈도우 생성, 출력
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //펜, 브러쉬, 폰트
	wc.hCursor = LoadCursor(0, IDC_ARROW);//시스템
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //미리 만들어서 제공되는 프로시저(윈도우 공통 기능)
	wc.lpszClassName = CLASS_NAME;
	wc.lpszMenuName = 0;		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		CLASS_NAME, WINDOW_NAME, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 800, 800,
		//CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,  //시스템이 알아서 출력!
		0, 0, hInst, 0);

	ShowWindow(hwnd, nShowCmd);
	UpdateWindow(hwnd);

	//2. 메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}