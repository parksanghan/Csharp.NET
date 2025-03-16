# .NET SYSTE

**HANDLE OpenProcess(DWORD dwDesiredAccess, BOOL bInheritHandle,
DWORD dwProcessId);**

 ****

# **WindowObject**

**API 에서 사용했던것과 같이 CreateWindow 를 통해 윈도우를 만들 수 있었다.**

**그렇다면, 여기서 잠깐 !** 

**의문이 들지 않는가 ? 윈도우를 제어를 하여 minimize 하거나 maxmize 를 하여도 우리는 결국** 

**윈도우가  다시 복구되는 모습을 볼 수 있었다.**

**결론은 말해줘야하는 것이 인지상정 !**

**→ 윈도우는 결국 구조체 형식의 객체인 것이다.**

**CreateWindow() → 시스템:  구조체 형식의 변수 하나 생성 → createWindow()인자로 초기화 한다.**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled.png)

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%201.png)

# **Window Object Handler VS Kernel Object Handler**

![            나   윈도우요               나    커널이요](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%202.png)

            나   윈도우요               나    커널이요

- **WindowObjectHandler**

**이전에 사용하였듯이,  만약 A 라는 프로세스가 Window를 생성하였을 떄 ,  WindowHandle을 0x00014 라고 가정하자.**  

**이때  , A가  B 라는 프로세스에게 SendMessage 를 통해 윈도우 핸들을 전달하고 B 프로세스에서는 MoveWindow 를 통해 윈도우 핸들을 제어하는 것을 확인할 수 있다.**

**→ 즉, WindowObjectHandler 의 특징인 윈도우의 모든 핸들은 모든 프로세스에 대해 전역적이다.**

**[모든 프레서스들이 특정 윈도우의 핸들 값을 획득하면 동일한 핸들 값을 얻게 된다는 것이다.]**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%203.png)

- **Kernel Object Hanler**

**커널 객체의 하위 개념 중 하나인 파일의 예시로** 

**비슷한 작업을 수행하였다.** 

**A 프로세스에서 파일을 생성하였고 파일의 핸들은 00x007F0 이라고 가정하였을 떄** 

**A 프로세스가  B 프로세스에게 Sendmessage를 사용하여 파일의 핸들을 전달하여도** 

**B 프로세스에서는 전달 받은 핸들을 사용해서 WriteFile 로 임으의 데이터를 파일에 쓸 수 없다.**

**→ 커널은 모든 프로세스에 대해 상대적인 핸들을 사용한다.**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%204.png)

# **WindowObjectHandler . Kernel Object Hanler 차이**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%205.png)

**User객체와 GDI 객체는 프로세스가 소유한다**

**User 객체는 전역적 핸들값으로** 

**GDI 객체는 지역적 핸들값으로**

**Kernel 객체는 Kernel에 의해 소유된다**

**즉 , 프로세스가 종료되더라도 해당 프로세스에서 생성된 커널 객체들은 종료되지 않는다.**

# **커널 객체와 Handle Table**

**모든 프로세스는 커널 객체의 정보를 담는 Handle Table 을 가지고 있다.** 

**A 라는 프로세스가 Create 함수를 호출하여 B 라는 커널 객체의 생성을 요구한다면** 

**B 객체는 커널 메모리 영역에 생성된다.  이때 공통적으로 가지는 정보는 다음과 같다.**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%206.png)

**Usage Count : 참조 개수이다 .  방금과 같은 경우 A 라는 프로세스에서 생성되어 Usage Count가 + 1 하게 된다. (A 프로세스의 핸들 테이블에는 B 오브젝트를 접근할 수 있는 핸들의 정보가 저장된다.)**

**→ Process 와 Thread는 + 2 증가된다.  (생성한 프로세스와 자신의 프로세스의 핸들테이블에 모두**

 **등록된다.)  + 2 경우 [핸들 복제 , 프로세스 또는 쓰레드 간 공유 , 공용 커널 객체]**

    **< 흐름 >**

- **A라는 프로세스가 CloseHandle(B)을 호출하게 되면 자신의 핸들테이블에 해당 핸들
값이 존재하는지 확인한다.**
- **만약, 존재한다면 B의 커널오브젝트의 정보 중 Usage Count 값을 -1 감소시킨다. 이
때 Usage Count 값이 0이 되면 해당 커널 객체는 소멸되게 된다.**
- **그 후 자신의 핸들테이블에서 해당 객체의 핸들값을 제거한다.**

**→ 하지만 비정상적인 종료나 예외로 인해 Usage Count가 감소가 되지 않아 소멸하지 않게 된다.** 

**이 경우 ,  CloseHandle 이나 Terminate 와 같은 함수로 종료해야 한다.**

# **Process (프로세스)**

**프로세스(Process)란 실행중인 프로그램을 말한다.** 

**구분되는 키값은 프로세스의 ID이다. (이 후 프로세스의 ID 를 통해 값을 얻어와 제어를 할 것이다.)**

**기본적으로 운영체제는 프로그램을 프로세스 단위로 관리한다.**

**이때 프로세스란 무엇인가 ?** 

**Process :  실행중인 프로그램이지만 실제로 작업을 하는 주체는 아니다**

**→ 즉 , 프로세스는 메모리이다.**

**Thread :  작업  (실행) 은 쓰레드가 한다 .** 

**프로세스는 최소 한 개 이상의 쓰레드를 가져야 한다. → 이때 생성되는 쓰레드가 주 쓰레드가 된다.**

         **←프로세스를 생성하면 쓰레드도 총 생성 커널 오브젝트 2개 생성→**

# **Process 생성**

 **프로세스를 생성하는 가장 쉽고도 간단한 함수는 WinExec 함수이다**

**UINT WinExec(LPCSTR lpCmdLine, UINT uCmdShow);**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%207.png)

**방법 1**

```cpp
#include <windows.h>
void main(){
// Process 생성 함수
WinExec("calc.exe", SW_SHOW);
ShellExecute(0, TEXT("open"), TEXT("calc.exe"), 0, 0, SW_SHOW);
}//프로그램을 실행하거나 원하는파일을 탐색기로 열 때
```

**방법 2**

```cpp
#include <windows.h> 
#include <stdio.h>
int main()
{
TCHAR name[] = TEXT("notepad.exe"); // 문자열 배열로 넘겨야 함
PROCESS_INFORMATION pi; // 프로세스 관련 정보 저장하기 위한 구조체
STARTUPINFO si = { sizeof(si) }; // 새로운 프로세스를 시작하기 위한 정보를 제공하는 
// 구조체 선언 및 초기화 
BOOL b = CreateProcess(0, name, 0, 0, 0, 0, 0, 0, &si, &pi); // 프로세스 생성
printf("프로세스 ID : %d\n", (int)pi.dwProcessId);
printf("쓰레드   ID : %d\n", (int)pi.dwThreadId);
printf("프로세스 핸들 : %d\n", (int)pi.hProcess);
printf("쓰레드   핸들 : %d\n", (int)pi.hThread);
return 0;
}
```

```cpp
#include <windows.h>
void main(){
TCHAR name[] = TEXT("calc.exe");
PROCESS_INFORMATION pi;
STARTUPINFO si = { sizeof(si) };
BOOL b = CreateProcess(0, name, 0, 0,
FALSE, NORMAL_PRIORITY_CLASS, 0, 0,
&si, &pi);
if( b ) {
WaitForInputIdle(pi.hProcess, INFINITE);
CloseHandle(pi.hProcess);
CloseHandle(pi.hThread);
}
HWND hwnd = FindWindow(0, TEXT("계산기"));
if( hwnd != 0)
MessageBox(NULL, TEXT("계산기를 생성했습니다."), TEXT("알림"), MB_OK);
}
```

**CreateProcess 함수는 프로세스를 생성하고 바로 리턴하므로 이 함수가 리턴된 직후에는 메모장이 아직도 초기화 중이며 메인 윈도우가 만들어져 있지 않는 상태이다.**

**따라서 해당 함수 호출 즉시 FindWindow 함수로 계산기 윈도우를 찾을 수 없다.**

**그렇기에 WaitForInputIdle 함수를 통해 인자로 전달된 프로세스가 사용자 입력을 받을 수 있을 떄까지 → 초기화가 될 때까지 두번째 인자의 시간까지 대기한다.**

# **Process 종료**

**윈도우를 사용할 때는 일반적으로 WM_CLOSE 또는 DestoryWindow 로 메인 윈도우를 파괴하였다.**

### **void ExitProcess(UINT nExitCode);→ 해당 함수 호출 시 정리 작업 들어가 종료**

      **과정**

1. **프로세스와 연결된 모든 DLL을 종료시키기 위해 각 DLL의 DllMain 함수가 호출되며
DLL 들은 스스로 정리작업을 한다.**
2. **모든 열려진 핸들을 닫는다.**
3. **실행중인 모든 스레드를 종료한다.**
4. **프로세스 커널 객체와 스레드 객체는 신호상태가 되며 이 객체를 기다리는 다른 프로
세스는 대기상태를 해제할 수 있다.**
5. **프로세스의 종료코드는 STILL_ACTIVE 에서 ExitProcess 가 지정하는 종료 값이 된다.**

```cpp
LRESULT CALLBACK WndProc( HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam){
switch( msg ) {
case WM_LBUTTONDOWN:
SendMessage(hwnd, WM_CLOSE, 0, 0); // 비큐 메시지 
return 0;
case WM_RBUTTONDOWN:
ExitProcess(0);
return 0;
 
case WM_DESTROY:
PostQuitMessage(0);
return 0;
}
return DefWindowProc(hwnd, msg, wParam, lParam);
}
[
```

### **BOOL TerminateProcess(HANDLE hProcess, UINT uExitCode);**

**다른 방법으로는 TerminateProcess 가 있다.**

**이전의 종료코드는 자기 자신을 종료 시키지만 TerminateProcess 는 자신이 아닌 다른 프로세스를**

 **강제로 종료시킬 수 있다 .**

**→ 해당 함수는 매우 위험 !  : TerminateProcess가 호출되면 ExitProcess 와 동일한 정리 작업이**

 **수행되나 연결된 DLL 에게 종료 사실이 통지 되지 않아서 DLL 에 있는 메모리가 사라질 수 있다.**

### **WaitForSignleObject**

 ****

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%208.png)

## **Process 종료에 대한 설명**

**프로세스는 다음의 4가지 방법으로 종료 할 수 있다.**

**가장 적합한 방법 : 주 스레드가 엔트리 포인트 함수에서 리턴한다.** 

```cpp
#include <windows.h>

int main() {
    // 프로세스 초기화 및 작업 수행
    // ExitProcess 함수를 호출하여 프로세스 종료
    ExitProcess(0);
    // 아래 코드는 실행되지 않음
    // (주 스레드가 이미 종료되었으므로)
    
    return 0;
}
```

**적합하지 아니한 방법: 프로세스내의 하나의 스레드에서 ExitProcess 함수를 호출(좋지 않은 방법)**

```cpp
#include <windows.h>

DWORD WINAPI ThreadFunc(LPVOID lpParam) {
    // 스레드 작업 수행

    // 작업이 완료되면 프로세스 종료
    ExitProcess(0);

    // 아래 코드는 실행되지 않음
    // (스레드가 이미 종료되었으므로)
    return 0;
}

int main() {
    // 스레드 생성
    HANDLE hThread = CreateThread(NULL, 0, ThreadFunc, NULL, 0, NULL);

    // 스레드가 종료될 때까지 대기
    WaitForSingleObject(hThread, INFINITE);

    // 스레드 핸들 닫기
    CloseHandle(hThread);

    return 0;
}
```

**좆되는 법: 다른 프로세스 안에 있는 스레드에서 TerminateProcess 함수 호출(좋지 않은 방법)**

```cpp
#include <windows.h>

int main() {
    // 종료할 대상 프로세스의 핸들을 얻어옵니다.
    DWORD targetProcessId = 12345;  // 대상 프로세스의 ID로 변경해야 합니다.
    HANDLE hTargetProcess = OpenProcess(PROCESS_TERMINATE, FALSE, targetProcessId);

    if (hTargetProcess != NULL) {
        // TerminateProcess 함수를 호출하여 대상 프로세스를 종료합니다.
        if (TerminateProcess(hTargetProcess, 0)) {
            printf("대상 프로세스를 종료했습니다.\n");
        } else {
            printf("대상 프로세스를 종료하지 못했습니다. 오류 코드: %d\n", GetLastError());
        }

        // 프로세스 핸들을 닫습니다.
        CloseHandle(hTargetProcess);
    } else {
        printf("대상 프로세스 핸들을 열 수 없습니다. 오류 코드: %d\n", GetLastError());
    }
    return 0;
}
```

- **프로세스내의 모든 스레드가 스스로 죽음**

**함수의 끝에 도달하면 프로세스 내의 모든 쓰레드는 종료되고 프로세스가 종료된다.**

**하지만 ExitThread함수를 사용하여 종료가 된다면 다른쓰레드는 다른 쓰레드는 게속 실행될 수 있다. → 이때 다른 쓰레드가 종료되면 프로세스는 종료된다.** 

# **프로세스 ID**

**모든 프로세스는 자신만의 ID를 갖는다.**

**→ 내가 2개의 메모장을 실행한다고 해도 각자 다른 프로세스 ID 를 갖게 된다.**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%209.png)

**ID 는 같지만 프로세스 Handler 의 값은 다르다** 

**A 에 접근하기 위해 Object Table 인덱스 값을 사용한다.**

**→ 프로세스 x 에 있는 핸들값을 y 에 전달한다고 사용할 수 없다**

**→ 그 이유는  , 해당 핸들 값이 자신의 Object Table 에 존재 하지 않기 때문** 

**(프로세스에서 커널 오브젝트를 제어하기 위해서는 제어하고자 하는 핸들이 테이블에 등록이 되있어야 한다.)**

### **DWORD GetWindowThreadProcessId(HWND hWnd, LPDWORD lpdwProcessId);**

**→ 윈도우 핸들을 안다면 프로세스 ID 를 얻어올 수 있다.**

### **HANDLE OpenProcess(DWORD dwDesiredAccess, BOOL bInheritHandle,
DWORD dwProcessId);**

**→ 이미 생성된 객체의 핸들을 획득할 목적** 

**그렇다면 , 여기서 잠깐 ?  커널 객체를 open 하면**

**[해당 커널 객체 생성 → 사용 카운트  증가  → 내 핸들 테이블에 등록 → 핸들값 반환]**

**create : [해당 커널 객체 생성 → 사용 카운트 증가  → 내 핸들 테이블에 등록 → 핸들값반환]**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2010.png)

**즉 , 그렇다면 여기서 본론은 무엇이냐 …** 

**두 create 와 open은 차이는 객체를 생성하느냐 이미 생성된 객체를 참조하냐 이 차이다 ~ 이말이야**

# **Handle 상속**

**Handle 상속 :  프로세서가 자식 프로세서를 생성하면서 자신의 ObjectTable 에 있던 핸들을 자식 프로세서에게 HandTable 에 전달할 수 있다**  

- **Handle 을 상속하기 위한 2가지 조건**

1. **가장 먼저 object Table 에 등록된 커널 핸드 오브젝트의 핸들이 상속 가능하도록 등록상태**
2. **자식 프로세서 생성 시 핸들 상속 여부에 핸들 값을  TRUE 로 전달**

```cpp
#include <windows.h>
void main(){
// 상속 가능한 KO 만들기
SECURITY_ATTRIBUTES sa; // 부모 커널
sa.nLength = sizeof( sa );
sa.bInheritHandle = TRUE; // 상속가능하게
sa.lpSecurityDescriptor = 0; // 실제 보안정보.
HANDLE hEvent = CreateEvent( &sa, 0, 0, TEXT("e")); // 자식핸들
// 상속가능하게 바꾸기..
SetHandleInformation( hEvent, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);
CloseHandle( hEvent );
}
```

**그렇다면 이번엔 자식 프로세스에게 옛다 ! 여기 핸들 테이블 ! 하고 주었다.  상속된 핸들로** 

**해당 커널에 접근해보자**

**송신부 (부모)**

```cpp
#include <stdio.h>
#include <windows.h>
void main(){
HANDLE hRead, hWrite;
CreatePipe( &hRead, &hWrite, 0, 4096);
// 읽기 위한 핸들을 상속 가능하게 한다.
 
SetHandleInformation( hRead, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);
TCHAR cmd[256];
wsprintf( cmd, TEXT("child.exe %d"), hRead); // 명령형 전달인자 사용
PROCESS_INFORMATION pi;
STARTUPINFO si = { sizeof(si)};
BOOL b = CreateProcess( 0, cmd, 0, 0, TRUE, CREATE_NEW_CONSOLE,
0,0,&si, &pi);
if ( b ) {
CloseHandle( pi.hProcess);
CloseHandle( pi.hThread);
CloseHandle( hRead );
}
//--------------------------------------------
char buf[4096];
while ( 1 ) {
printf("전달할 메세지를 입력하세요 >> ");
gets( buf );
DWORD len;
WriteFile( hWrite, buf, strlen(buf)+1, &len, 0);
}
}
```

**수신부 (자식 )**

```cpp
#include <stdio.h>
#include <windows.h>
// 이 실행파일의 이름을 child.exe 변경하세요..
void main(int argc, char** argv){
if ( argc != 2 ) {
printf("이프로그램은 직접 실행하면 안됩니다. 부모를 실행해 주세요\n");
return;
}

 
// 부모가 보내준 pipe 핸들을 꺼낸다.
HANDLE hPipe = (HANDLE)atoi(argv[1]);
char buf[4096];
while ( 1 ) {
memset( buf, 0, 4096 );
DWORD len;
BOOL b = ReadFile( hPipe, buf, 4096, &len, 0);
if ( b == FALSE ) break;
printf("%s\n", buf );
}
CloseHandle( hPipe );
}
```

**해당 프로그램은 송신부는 부모 , 수신부는 자식 프로세스이다 .**

**부모에서 커널 오브젝트를 생성하는데 이때 상속된 핸들 값을**

**(SetHandleInformation( hRead, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);)**

 **명령형 인자로 전달한다.**

**→ 핸들 자식은 명령형 인자로 전달된 핸들을 가지고 이름없는 파이프에 접근할 수 있게h된다.**

# **Handler 복사**

**이전에도 보았듯이 특정 프로세스가 파일의 핸들 값을 가지고 있다고 해도 파일의 정보가 object table에 존재하지 않는다면 해당 파일에 read, write 할 수 없다.**

**→ 즉 , A 라는 프로세스에서 B 라는 프로세스에게 핸들값을 주어도 B 는 제어할 수없다.**

**→ B 에게도 접근하게 하고 싶다면 B 프로세스의 object table 에 커널 오브젝트의 핸들 값을 복사하면 그만이다.**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2011.png)

## **DuplicateHandle**

**DuplicateHandle을 이용하면 핸들 테이블에 있는 핸들 값을 얻을 수 있다는 것이다. 이말이야~**

**1,2 번째 인자는 핸들을 가지고 있는 복제할 프로세스 지정, 복제할 핸들의 종류** 

**3,4 번째 인자는 복제된 핸들 값을 소유할 프로세스 지정 , 복제된 핸들값을 받을 프로세스 리턴** 

**핸들을 복사**

```cpp
LRESULT CALLBACK WndProc( HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam){
static HANDLE hProcess1;
static HANDLE hProcess2;
switch( msg ) {
case WM_LBUTTONDOWN:
{
TCHAR name[] = TEXT("calc.exe");
STARTUPINFO si = { sizeof(si) };
PROCESS_INFORMATION pi;
BOOL b = CreateProcess(NULL, name, NULL, NULL, FALSE, NORMAL_PRIORITY_CLASS,
 NULL, NULL, &si, &pi);
// 가짜 복사
hProcess1 = pi.hProcess;
// 진짜 복사
DuplicateHandle( GetCurrentProcess(), // 이 프로세스안의
 pi.hProcess, // 이 핸들을
 GetCurrentProcess(), // 이 프로세스에 복사하라.
 &hProcess2, // hProcess에 핸들값 저장
 0, FALSE, // 접근 권한, 상속
 DUPLICATE_SAME_ACCESS);
if( b ) {
CloseHandle(pi.hProcess);
CloseHandle(pi.hThread);
 }
 }
return 0;
case WM_RBUTTONDOWN:
TerminateProcess(hProcess2, 0);
return 0;
case WM_DESTROY:
PostQuitMessage(0);
return 0;
}
return DefWindowProc(hwnd, msg, wParam, lParam);
}
```

# ****

# **진짜 복사 vs 가짜 복사**

![**프로세스야… 날 속인거니 ?**](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2012.png)

**프로세스야… 날 속인거니 ?**

## **가짜 복사 hProcess1 = pi.hProcess;**

**가짜 복사란 ?**

- **가짜 복사는 단순하게 핸들의 값을 복사하는 것이다.**

**→ 그렇기에 , 복사 전의 원본이 closeHandle을 하게 되면 복사된 핸들 값도 당연하게도 사용하지 못한다.**

## **진짜 복사  DuplicateHandle**

```cpp
DuplicateHandle( GetCurrentProcess(), // 이 프로세스안의
 pi.hProcess, // 이 핸들을
 GetCurrentProcess(), // 이 프로세스에 복사하라.
 &hProcess2, // hProcess에 핸들값 저장
 0, FALSE, // 접근 권한, 상속
 DUPLICATE_SAME_ACCESS);
```

**진짜 복사란 ?**  

- **실제 커널 오브젝트의 사용카운트를 증가 시키고 object Table 에 해당 핸들이 복사가 되는 것이다.**

**→ 진짜 복사이기에 원본이 closeHandle을 해도 복사된 핸들값을 사용할 수 있다.**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2013.png)

**프로세스간의 핸들 복사 .1** 

```cpp
#include <windows.h>
void main(){
HANDLE hFile = CreateFile( TEXT("a.txt"), GENERIC_READ | GENERIC_WRITE,
FILE_SHARE_READ | FILE_SHARE_WRITE,
0, // 보안
CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);
printf( "생성된 화일 핸들(Table Index) : %x\n", hFile );
HWND hwnd = FindWindow( 0, TEXT("B"));
// hwnd을 만든 프로세스의 ID를 구한다.
DWORD pid;
DWORD tid = GetWindowThreadProcessId( hwnd, &pid );
// 프로세스 ID를 가지고 PROCESS 핸들을 얻는다.
HANDLE h;
HANDLE hProcess = OpenProcess( PROCESS_ALL_ACCESS, 0, pid );
// A의 Table의 내용을 B의 Table 에 복사 해준다.
38
DuplicateHandle( GetCurrentProcess(), hFile, // source
hProcess, &h, // target
0, 0, DUPLICATE_SAME_ACCESS);
printf("B에 복사한 핸들(Table index) : %x\n", h );
SendMessage( hwnd, WM_USER+100, 0, (LPARAM) h );
CloseHandle( hFile );
}
```

**프로세스간의 핸들 복사 .2** 

```cpp
#include <windows.h>
LRESULT CALLBACK WndProc( HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam){
switch( msg ) {
case WM_USER + 100:
{
HANDLE hFile = (HANDLE)lParam;
char s[256] = "hello";
DWORD len;
BOOL b = WriteFile( hFile, s, 256, &len, 0);
if ( b == FALSE )
{
TCHAR buf[256];
wsprintf(buf, TEXT("전달된 핸들 : %x \n실패 : %d"), hFile,
 GetLastError() );
MessageBox( 0, buf, TEXT(""), MB_OK);
}
else
{
MessageBox( 0, TEXT("성공"), TEXT(""), MB_OK);
CloseHandle( hFile );
}
}

  
return 0;
case WM_DESTROY:
PostQuitMessage(0);
return 0;
}
return DefWindowProc( hwnd, msg, wParam, lParam);
}
```

**해당 코드는 2개의 프로그램이 구동되는 소스로서 프로세스간 핸들 복사2 프로그램을 먼저
실행시키고, 프로세스 간 핸들 복사1 프로그램을 실행시키면 된다.**

# **Process 열거**

**당신은 대전의 맛집 작업관리자를 가본 적이 있는가?** 

**이 작업을 하기 위해서는 TOOL HELP 라이브러리의 스냅샷을 사용하는 방법과** 

**Enum.cpp** 

```cpp
#include <windows.h>
#include <TLHelp32.h>
#include <stdio.h>
void main(){
HANDLE hSnap = CreateToolhelp32Snapshot(
TH32CS_SNAPPROCESS, 0); // process ID
if( hSnap == 0 )
return ;
PROCESSENTRY32 ppe;
BOOL b = Process32First(hSnap, &ppe);
 
while ( b )
{
printf("%04d %04d %s\n", ppe.th32ProcessID,
ppe.th32ParentProcessID,
ppe.szExeFile);
b = Process32Next( hSnap, &ppe );
}
CloseHandle(hSnap);
}
```

**Enumcon.cpp**

```cpp
#include <windows.h>
#include <tchar.h>
#include <stdio.h>
#include "psapi.h"
void PrintProcessNameAndID( DWORD processID){
TCHAR szProcessName[ MAX_PATH ] = TEXT("unknown");
// 프로세스의 핸들 얻기
HANDLE hProcess = OpenProcess( PROCESS_QUERY_INFORMATION |
 PROCESS_VM_READ, FALSE, processID);
// process 이름 가져오기
if( NULL != hProcess) {
HMODULE hMod;
DWORD cbNeeded;
if( EnumProcessModules( hProcess, &hMod, sizeof( hMod), &cbNeeded))
{
GetModuleBaseName( hProcess, hMod, szProcessName, sizeof( szProcessName));
}
else
return;
}
else return;
//print
_tprintf(TEXT("%s ( PROCESS ID : %u )\n"), szProcessName, processID);
CloseHandle(hProcess);
}
void main()
{
// process list 가져오기(id값)
DWORD aProcess[1024], cbNeeded, cProcesses;
unsigned int i;
if( !EnumProcesses(aProcess, sizeof( aProcess), &cbNeeded) )
// 배열 수, 리턴되는 바이트 수
// 배열에 id값들이 들어간다.
return;
// 얼마나 많은 프로세스들이 리턴되었나 계산
cProcesses = cbNeeded / sizeof( DWORD);
// process 이름 출력
for( i = 0; i < cbNeeded; i++)
PrintProcessNameAndID( aProcess[i] );
}
```

**EnumModule.cpp**

```cpp
#include <windows.h>
#include <TLHelp32.h>
#include <iostream>
using namespace std;
void main(){
// 계산기 ProcessID얻기
HWND hCalc = FindWindow(0, TEXT("계산기"));
DWORD pid;
GetWindowThreadProcessId(hCalc, &pid);
42
HANDLE hSnap = CreateToolhelp32Snapshot( TH32CS_SNAPMODULE, pid);
MODULEENTRY32 mme;
BOOL b = Module32First( hSnap, &mme);
while( b )
{
cout << (void*)mme.modBaseAddr << " : "
<< mme.szModule << " PATH : " << mme.szExePath << endl;
b = Module32Next( hSnap, &mme);
}
CloseHandle(hSnap);
}
```

**커널 자체 메모리 커널 객체 관리 메모리** 

**명령형인자 - 프로그램 실행 시 외부에서 전달되는 값.** 

**char * 문자열 한개 받을 수 잇는 타입 char [10]**

**char* * 문자열 을 받을 수 잇는 타입 char [10][10]** 

# **쓰레드 Thread**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2014.png)

**쓰레드는 프로세스가 생성되면 자동적으로 같이 생성된다.**

**멀티 태스킹 : 여러 개  작업(프로세스가) 동시에 수행  !**

**→ 사실 동시에 실행하는 건 있을 수가 없는일 너무나도 빨라서 동시에 실행하는것처럼 보임**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2015.png)

```cpp
#include<Thread.h>
int A; 
void do_something(); //<- //여기에서 a++ 한다고 가정하자 
// 그럼 저기서 A의 값은 최종적으로 어떻게 나올까
// 만약그 샴푸같은 TLS / _beginThread 를 사용하면 ? 
int main(){
	thread t1(do_something);
  thread t2(do_something);
  thread t3(do_something);ab 

  t1.join();
  t2.join(); 
  t3.join();
-> 3 case CreatThread
-> 1 case TLS / _beginThread 
}
```

**멀티 쓰레드 :  여러 개의  작업을 여러 개의 쓰레드가 수행 ! (동시에 수행하는것처럼 보이는 거지 동시에 하는 건 아님)**

**→ 둘다 거의 같은 말 (나랑 말장난하니?)**

**→  CPU 1개 → 동시에 돌릴수 있는 프로세스 개수 1개 (CPU 코어 겠쥐..?)**

**→ 그렇다면 만약 ?  같은 메모리를 다루는 쓰레드에서 동시에 작업을 수행한다고** 

**반드시 primary thread는 메시지 기본 흐름이 원활하게 동작할 수 있도록 처리** 

```cpp
#include<Thread.h>
void func1();
void func2();
void func3();
int tmain(){
	thread t1(func1);
  thread t2(func2);
  thread t3(func3);

  t1.join();
  t2.join();
  t3.join();

}
```

# **CreateThread**

**앞에서도 설명하였듯이 쓰레드는 프로세스가 생성되면 자동적으로 쓰레도 또한 생성된다.**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2016.png)

**→createthread 5번째인자 시작함수 생성 flag = 우선순위** 

**그렇다면 쓰레드마다 다른것을 준비해야 하나 ?** 

**→ Nope! Nine!  비슷한  처리를 하는 쓰레드를 많이 생성하는 경우 동일한 프로시저를 사용해서 CreateThread API 를 여러 번 호출하면 그만이다.**

- **값을 넘기고 싶은데 인자가 많다면 구조체로 보내면 된다.**
- **→프로세스 내의 쓰레드는 메모리 공간을 공유하기 때문에 포인터로 서로의 데이터를 참조할 수 있다.**
- **포인터가 가르키는 데이터의 유효성을 판단할 때에는 주의해야한다.  그이유는 CreateThread는 쓰레드를 생성을 하고 나면 프로시저의 종료를 기다리지 않고 호출로 부터 복귀하기 때문이다.**
- 

**그러나 포인터가 가리키는 데이터의 유효성을 판단할 때는 주의해야 한다. CreateThread
API는 스레드 생성에 성공하면 스레드 프로시저의 종료를 기다리지 않고 즉시 호출로부
터 복귀한다. 즉 호출한 곳은 스레드 프로시저의 처리 상태에 관계없이 계속해서 프로그
램을 실행한다. 만약 호출한 곳의 함수가 종료되어 변수가 유효 영역을 벗어나거나 동적
으로 할당된 메모리를 해제하였다면 다른 스레드에서 참조하려고 해도 이미 데이터는 무
효가 되어 있을 것이다. 이런 일을 막기 위해서는 전역변수, 정적변수를 사용하거나 동적
으로 할당한 메모리 포인터를 넘겨서 호출된 스레드에서 해제하는 방법을 사용할 수 있
다.
CreateThread()의 5번째 인자**

### **dwCreationFlags**

**마지막 인자인 dwCreationFlags의 경우 Created_suspended 를 주면 실행을 중지한다 .**

**→ 이다 Suspended Count는 1인 상태이다 .** 

# **쓰레드 제어**

**countof  -  멀티바이트 환경과 유니코드 환경을 둘다 제공한다. → 요소의 개수** 

**그렇다면 여기서 같은 작업을 하는데 ? 같은 메모리 , 즉 , 자원을 같이 사용한다면 꼬여버리지 않을까?**

**→ 그건 이제 다음에서 부터 다루도록 하겠다.**

**스택메모리를 다루는 주체는 쓰레드** 

**createthread 3번째인자 시작함수** 

**생성 flag = 우선순위** 

**쓰레드는 커널객체를 서스펜드 가 0 일때 작동하고 0보다 크면멈춤**

**→ 프로세서에서의 참조개수와 같은역할이다.**

**쓰레드 생성- > 커널객체생성 [핸들 객체 2개 생성] 하나는 부모 하나는 자신이 →프로세스내에 생성**

**해제안하면 쓰레드가 죽어도 커널 객체가 남아있으**

**Resume Thread 는 count 1감소**

**suspendThread는 count 1증가**

**if ( bRun )
ResumeThread( hThread ); // 스레드 재개 서스팬드 -1   
else  
SuspendThread( hThread ); // 스레드 일시 중지 서스팬드 + 1** 

# **그렇다면 쓰레드가 왜 필요한가?  ? ?**

```cpp
void fun1( LPVOID temp)
{
HDC hdc;
BYTE Blue=0;
HBRUSH hBrush, hOldBrush;
 
HWND h = (HWND)temp;
hdc = GetDC(h);
while(1)
{
Blue++;
Sleep(1);
hBrush = CreateSolidBrush(RGB(0, 0, Blue));
hOldBrush = (HBRUSH)SelectObject(hdc, hBrush);
Rectangle(hdc, 100, 100, 300, 400);
SelectObject(hdc, hOldBrush);
DeleteObject(hBrush);
}
ReleaseDC(h, hdc);
}
LRESULT CALLBACK WndProc( HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
static HANDLE hThread;
static DWORD ThreadID;
switch( msg )
{
case WM_LBUTTONDOWN:
{
fun1(hwnd);
}
return 0;
case WM_RBUTTONDOWN:
{
HDC hdc = GetDC(hwnd);
Ellipse(hdc, LOWORD(lParam), HIWORD(lParam), LOWORD(lParam)+20,
HIWORD(lParam)+20);

 
 ReleaseDC(hwnd, hdc);
}
return 0;
case WM_DESTROY:
PostQuitMessage(0);
return 0;
}
return DefWindowProc(hwnd, msg, wParam, lParam);
}
```

**해당 코드는 L 버튼을 누르면 While 문을 빠져나오지 못해 R 버튼을 눌러도 반응하지 못한다.**

**이렇게 다른작업을 할때에는 쓰레드를 사용하여야 하는 경우가 언젠가 찾아온다.** 

**다음은 쓰레드를 사용한 코드이다.**

```cpp
DWORD WINAPI ThreadFunc1( LPVOID temp)
{
HDC hdc;
BYTE Blue=0;
HBRUSH hBrush, hOldBrush;
HWND h = (HWND)temp;
hdc = GetDC(h);
while(1)
{
Blue++;
Sleep(1);
hBrush = CreateSolidBrush(RGB(0, 0, Blue));
hOldBrush = (HBRUSH)SelectObject(hdc, hBrush);
Rectangle(hdc, 100, 100, 300, 400);
SelectObject(hdc, hOldBrush);
74
DeleteObject(hBrush);
}
ReleaseDC(h, hdc);
}
LRESULT CALLBACK WndProc( HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
static HANDLE hThread;
static DWORD ThreadID;
switch( msg )
{
case WM_LBUTTONDOWN:
{
hThread = CreateThread(NULL, 0, ThreadFunc1, hwnd, 0, &ThreadID); // 2
CloseHandle(hThread); // use count : 1
}
return 0;
case WM_RBUTTONDOWN:
{
HDC hdc = GetDC(hwnd);
Ellipse(hdc, LOWORD(lParam), HIWORD(lParam), LOWORD(lParam)+20,
HIWORD(lParam)+20);
ReleaseDC(hwnd, hdc);
}
return 0;
case WM_DESTROY:
PostQuitMessage(0);
return 0;
}
return DefWindowProc(hwnd, msg, wParam, lParam);
```

# **쓰레드 제어**

**쓰레드의 구동방식은 Suspended 를 통해 프로세서와 비슷한 방식으로 구동하는 것을** 

**우리는 확인했다. 아래의 함수를 통해 쓰레드를 제어할 수 이**

**DWORD SuspendThread(HWND hThread); → Suspended 값을 1증가시킨다. → 중지
DWORD ResumeThread(HWND hThread); → Suspended 값을 1감소 시킨다. → 실행**

**물론 , 감소 시켰을 경우 Suspended 값이 0이 되어야 실행을 하게 된다.**

**다음의 예제를 통해 Suspend와 Resume 을 살표보자 .** 

```cpp
#include <windows.h>
#include <commctrl.h>

DWORD WINAPI foo( void* p){
    HWND hPrg = (HWND)p;
    for ( int i = 0; i < 1000; ++i )
    {
        SendMessage( hPrg, PBM_SETPOS, i, 0); // 프로그래스 전진
        for ( int k = 0; k < 5000000; ++k ); // 0 6개 - some work.!!
    }
    return 0;
}

LRESULT CALLBACK WndProc( HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    static HWND hPrg;
    static HANDLE hThread;
    switch( msg )
    {
        case WM_CREATE:
            hPrg = CreateWindow( PROGRESS_CLASS, TEXT(""),
                WS_CHILD | WS_VISIBLE | WS_BORDER | PBS_SMOOTH,
                10, 10, 500,30, hwnd, (HMENU)1, 0, 0);
            //범위:0 ~1000 초기위치:0으로 초기화.
            SendMessage( hPrg, PBM_SETRANGE32, 0, 1000);
            SendMessage( hPrg, PBM_SETPOS, 0, 0);
            return 0;
        case WM_LBUTTONDOWN:
        {
            // 새로운 스레드를 만들어서 작업을 시키고 주스레드는 최대한 빨리
            // 메세지 루프로 돌아 가서 다음 메세지를 처리한다.
            DWORD tid;
            hThread = CreateThread( 0, 0, // TKO 보안, Stack 크기
                foo, (void*)hPrg, // 스레드로 실행할 함수,인자
                CREATE_SUSPENDED,// 생성하지만 실행은 하지 않는다.
                &tid); // 생성된 스레드 ID를 담을 변수
            //CloseHandle( hThread ); // TKO의 참조개수를 초기에 2이다.
            // 스레드 종료와 함께 즉시 파괴되도록 1 줄인다.
        }
        return 0;
        case WM_RBUTTONDOWN:
        {
            static BOOL bRun = FALSE;

            bRun = !bRun; // Toggle
            if ( bRun )
                ResumeThread( hThread ); // 스레드 재개
            else
                SuspendThread( hThread ); // 스레드 일시 중지
        }
        return 0;
        case WM_DESTROY:
            CloseHandle( hThread);
            PostQuitMessage(0);
            return 0;
    }
    return DefWindowProc( hwnd, msg, wParam, lParam);
}
```

**다음의 예제는 왼쪽 버튼을 쓰레드를 생성하여 아까 While 문을 빠지지 못하였던 함수를 할당하여 왼쪽 클릭 후 우클릭도 작동하는 것을 확인할 수 있다 .**

# **쓰레드 종료**

**자 ! 쓰레드를 생성과 제어도 하였으니 착한 어린이라면 종료도 할 줄 하어야 한다.** 

**일반적으로 쓰레드는 일정한 백그라운드 작업을 맡아 처리하고 해당 작업이 끝난 후 , 종료되는 것이  보통이다.**

**→ 이런 경우, 프로세스에서 생성된 주 쓰레드에서 작업을 위한 쓰레드를 생성하여 독립적으로 실행한다.**  

**[그렇기에 , 우리는 작업 쓰레드가 종료되었는지 주기적으로 확인 할 수 있어야 한다. ]**

```cpp
#include <windows.h>
#include <commctrl.h>
DWORD WINAPI foo( void* p)
{
HWND hPrg = (HWND)p;
for ( int i = 0; i < 1000; ++i )
{
SendMessage( hPrg, PBM_SETPOS, i, 0); // 프로그래스 전진
for ( int k = 0; k < 5000000; ++k ); // 0 6개 - some work.!!
}
return 0;
}
LRESULT CALLBACK WndProc( HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
static HWND hPrg;
static HANDLE hThread;
switch( msg )
{
case WM_CREATE:
hPrg = CreateWindow( PROGRESS_CLASS, TEXT(""),
WS_CHILD | WS_VISIBLE | WS_BORDER | PBS_SMOOTH,
10, 10, 500,30, hwnd, (HMENU)1, 0, 0);
//범위:0 ~1000 초기위치:0으로 초기화.
SendMessage( hPrg, PBM_SETRANGE32, 0, 1000);
SendMessage( hPrg, PBM_SETPOS, 0, 0);
return 0;
case WM_LBUTTONDOWN:
{
DWORD tid;
hThread = CreateThread( 0, 0, // TKO 보안, Stack 크기

foo, (void*)hPrg, // 스레드로 실행할 함수,인자
0,
&tid);
}
return 0;
case WM_RBUTTONDOWN:
{
DWORD code;
GetExitCodeThread(hThread, &code);
if( code == STILL_ACTIVE)
{
TerminateThread(hThread, 100);
CloseHandle(hThread);
}
}
return 0;
case WM_DESTROY:
CloseHandle( hThread);
PostQuitMessage(0);
return 0;
}
return DefWindowProc( hwnd, msg, wParam, lParam);
```

**위의 코드를 보면 종료라고 하나알려준게 바로 좆되는 Terminate 를 활용하여 종료하는 것이다…**

# **_beginThread & TLS**

# **CreateThread   VS  _beginThread**

**CreateThread API로 스레드를생성하는 경우는 드물다.**

**CreateThread를 이용해서는 안 되는 이유는 다음과 같다.**

**각각의 스레드는 고유한 스택을 갖기 때문에 스택 변수( 지역 변수)는 스레드 별로 고유
하다.**

- **프로세스 내의 스레드는 메모리 공간을 공유하기 때문에 정적변수를 사용할 때 문제가
생길 수 있다.**

 ****

**C 런타임 라이브러리 함수는 싱글스레드용과 멀티스레드용으로 다른 라이브러리 파일을
제공한다.**

**→ 하지만 이 TLS 를 사용하려면 쓰레드를 생성할 때 초기화 등의 준비가필요하다 .**

**createThread 를 사용해 쓰레드를 생성하면 이런 처리를 사용할 수 없다.**

- **쓰레드 생성용으로 전용 함수를 준비해서 그 안에서 초기화 처리를 한 뒤**
    
    **쓰레드를 생성한다.**
    

```jsx
// 컴파일러에서 정의 안했다면 정의
#ifndef _MT
#define _MT // MSDN이나 다른 소스에서는 반드시 정의하라고 되어 있는 심볼...
// 결국 _MT심볼은 컴파일러 옵션에서 /MT 를 지정하는 것과 동일한 효과 ..
#endif
#include <iostream>
#include <process.h> // _beginthreadex() 를 사용하기 위해..
#include <windows.h>
using namespace std;
unsigned int __stdcall foo(void *p) // 결국 DWORD WINAPI foo() 이다 ~!!
{
cout << "foo" << endl;
Sleep(1000);
cout << "foo finish" << endl;
return 0;
}
void main()
{
unsigned long h = _beginthreadex(0, 0, foo, 0, 0, 0);
// h가 결국 핸들이다.
WaitForSingleObject((HANDLE)h, INFINITE);
CloseHandle((HANDLE)h);
}
```

## **TLS(Thread Local Storage)**

**스레드 별로 고유한 저장공간을 가질 수 있는 방법이다.**

  **각각의 스레드는 고유한 스택을 갖기 때문에 스택 변수( 지역 변수)는 스레드 별로 고유
하다.**

**→ 예를 들어, 각각의 쓰레드가 같은 함수를 실행한다고 해도 해당 함수에 정의 된 지역변수는 실제로 서로 다른 메모리 공간에 위치한다는 의미이다. 하지만 전역 변수나 정적 변수의 경우  프로세스 내의 모든 쓰레드에 의해서 공유된다.** 

**TLS 는 즉, 정적 or 전역 변수를 각각의 쓰레드에게 독립적으로 만들고 싶을 때 사용한다.**

```jsx
#include <windows.h>
#include <stdio.h>
// goo 는 static 지역 변수을 사용해서 원하는 기능을 수행하였다.
// 싱글스레드 환경에서는 아무 문제 없다. 멀티 스레드 라면. ?
void goo( char* name)
{
// TLS 공간에 변수를 생성한다.
__declspec(thread) static int c = 0;
// static int c = 0;
++c;
printf("%s : %d\n", name, c ); // 함수가 몇번 호출되었는지 알고 싶다.
}
DWORD WINAPI foo( void* p )
{
char* name = (char*)p;
goo( name );
goo( name );
goo( name );
return 0;
}
void main()
{
HANDLE h1 = CreateThread( 0, 0, foo, "A", 0, 0);
HANDLE h2 = CreateThread( 0, 0, foo, "\tB", 0, 0);
HANDLE h[2] = { h1, h2};
 
WaitForMultipleObjects( 2, h, TRUE, INFINITE );
CloseHandle( h1 );
CloseHandle( h2 );
}
```

# **쓰레드 생명 주기**

**쓰레드는 생성되고 소멸될 때 까지 여러 행태의 생명주기를 가진다.**

**→ 상태는 즉 , Alived 와 Dead  두 가지로 나누어진다.** 

- **Dead :  쓰레드가 자신의 run() 메소드를 완전히 수행하여 더 수행할 코드가 남아 있지 않거나 stop() 메소드에 의해 종류되는 경우이다.**
- **Alived : 나머지 모든 상태이며 , 실행 가능 상태, 실행 상태 , 대기 상태로 나눌 수 있다 .**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2017.png)

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2018.png)

**int c; → 2개의 쓰레드에서 접근하면 6번호출한다할때 6이된다.**

**하지만 TLS 쓰면  3, 3이된다.**

**begin_thread   vs createThread** 

**createThread 사용하면 프로세스 내의 쓰레드는 메모리 공간을 공유하기에 정적변수를 사용할 떄 문제가 생길 수 있다.**

# **쓰레드 우선 순위**

**멀티 쓰레드란 복수 개의 쓰레드가 동시에 실행되는 시스템이다.**

**→ 만약 모든 쓰레드가 동시에 실행되는 것이 가능할까?**

- **cpu가 하나라면 멀티 쓰레드는 동시에 실행되는 것처럼 흉내내는 것 !**

**0.02초 씩 쪼개어 쓰레드를 조금씩 순서대로 실행함으로써 실행되는 것이다.**

**→ 로빈 방식 (Round Robin)**

**시스템을 더욱 효율적으로 사용하기 위해서는 우선 순위를 정하여 사용하여야 한다.**

- **우선 순위 레벨은 프로세스 내에서 쓰레드의 우선 순위를 지정하여 일단 쓰레드를 생성한 후 아래와 같은 함수로 설정하거나 읽을 수 있습니다.**

**BOOL SetPriorityClass( HANDLE hProcess, DWORD dwPriorityClass );
DWORD GetPriorityClass( HANDLE hProcess );
BOOL SetThreadPriority( HANDLE hThread, int nPriority );
int GetThreadPriority( HANDLE hThread );**

**지정할 수 있는 순위레벨은 7개 중하나이며 디폴트는 THREAD_PRIORITY_NORMAL 입니다.** 

**THEREAD_PRIORITY_IDLE
THEREAD_PRIORITY_LOWEST
THEREAD_PRIORITY_BELOW_NORMAL
THEREAD_PRIORITY_NORMAL
THEREAD_PRIORITY_ABOVE_NORMAL
THEREAD_PRIORITY_HIGHEST
THEREAD_PRIORITY_TIME_CRITICAL**

# **스레드 간 동기화 - 동기화가 필요한 이유**

**동기화 란  , 멀티 태스킹 환경에서 여러 개의 처리를 서로의 진행 상태에 맞추어 진행하는 것.**

**→ 관련 성이 없다면 문제 없 지만**

**→ 관련성이 있다면 처리를 순서대로 실행할 때나 데이터 교환이 필요한 경우에는
어느 한쪽만 처리를 계속 진행시킬 수 없다**

**그렇기에 다른 태스크의 처리를 기다리거나 혹은 기다리게 하는 구조이다.**

## **쓰레드 간 동기화 필요한 이유는?**

```cpp
int x;
DWORD WINAPI ThreadFun1(LPVOID param)
{
HDC hdc = GetDC((HWND)param);
for(int i=0; i<100; i++)
{
x = 100;
Sleep(1);

 89
TextOut(hdc, x, 100, TEXT("강아지"), 3);
}
ReleaseDC((HWND)param, hdc);
return 0;
}
DWORD WINAPI ThreadFun2(LPVOID param)
{
HDC hdc = GetDC((HWND)param);
for(int i=0; i<100; i++)
{
x = 200;
Sleep(1);
TextOut(hdc, x, 200, TEXT("고양이"), 3);
}
ReleaseDC((HWND)param, hdc);
return 0;
}
LRESULT CALLBACK WndProc( HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
switch( msg )
{
case WM_LBUTTONDOWN:
{
DWORD ThreadID;
HANDLE hThread = CreateThread(NULL, 0, ThreadFun1, hwnd, 0, &ThreadID);
CloseHandle(hThread);
hThread = CreateThread(NULL, 0, ThreadFun2, hwnd, 0, &ThreadID);
CloseHandle(hThread);
}
return 0;

 
```

**다음의 코드는 좌클릭 시 쓰레드를  2개 생성하여 100 , 100 좌표에 강아지를 100번 출력하고  200 , 200 좌표에 고양이를 출력한다.**

**그런데 여기서 보면 FUN1 과 FUN2  2개의 쓰레드가 스위칭 이 되면서 100, 200  이나 200,100 등 값이 꼬이게 되는 것을  볼 수 있다.** 

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2019.png)

**그렇다면 이렇게 중간 중간 스위칭이 되면서 값이 변하는 것을 어떻게 막을까?**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2020.png)

**우리는 언제나 해답을 찾을 것이다.**

**전통적인 방법은 FLAG와 같은 통지형 BOOL 형식으로 활용방식을 구현할 수 있다.**

**하지만 해당처럼 다루는 것은 다른쓰레드가 FLAG 를 통해 WHILE 문 만 돌다가 나가기에 매우 비효율적이다 .**

```cpp
int x;
BOOL g_Wait = FALSE;
DWORD WINAPI ThreadFun1(LPVOID param)
{
HDC hdc = GetDC((HWND)param);
for(int i=0; i<100; i++)
{
while(g_Wait == TRUE);
g_Wait = TRUE;
x = 100;
TextOut(hdc, x, 100, TEXT("강아지"), 3);
g_Wait = FALSE;
Sleep(1);
}
ReleaseDC((HWND)param, hdc);
return 0;
 }
DWORD WINAPI ThreadFun2(LPVOID param)
{
HDC hdc = GetDC((HWND)param);
for(int i=0; i<100; i++)
{
while(g_Wait == TRUE);
g_Wait = TRUE;
x = 200;
TextOut(hdc, x, 200, TEXT("고양이"), 3);
g_Wait = FALSE;
Sleep(1);
}
ReleaseDC((HWND)param, hdc);
return 0;
```

**해결은 되었지만 해당 코드에서는 스케쥴링에서의 관점에서 보면 스위칭 발생시 cpu에 들어가서 while 문 만 게속 돌다가 나가는것과 같다 .** 

# **크리티컬 섹션**

**: 임계영역 - 특정 자원에 접근하는 쓰레드를 1개로 제한** 

**→ 동일한 프로세스에서만 사용이 가능하다 .**

**즉 크리티컬 섹션은 임계영역이라고도 부르는데 공유자원의 독점을 보장하는  것이다.**

**크리티컬 섹션 초기화**

**void InitializeCriticalSection(LPCRITICAL_SECTION lpCriticalSection);**

**크리티컬 섹션 파괴 
void DeleteCriticalSection(LPCRITICAL_SECTION lpCriticalSection);**

**크리티컬 구성 함수** 

**void EnterCriticalSection(LPCRITICAL_SECTION lpCriticalSection);
void LeaveCriticalSection(LPCRITICAL_SECTION lpCriticalSection);**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2021.png)

**→ 함수 구성을 보면 크리티컬 섹션은 포인터를 인수로 취한다 .  두  함수 사이에 코드가 크리티컬 섹션이 된다.**

**크리티컬 섹션 ENTER 이 된 시점부터 다른 쓰레드는 Leave 가 될때까지 들어올 수가 없다.**

```cpp
#include <windows.h>
#include <iostream>
using namespace std;
void WorkFunc() { for( int i=0; i <10000000; i++); }// 시간 지연 함수
// 공유 자원
int g_x = 0;
int g_x1 = 0;
//------------------------------------------------
CRITICAL_SECTION g_cs; // 전역
//------------------------------------------------
DWORD WINAPI Func( PVOID p){
EnterCriticalSection(&g_cs);
for( int i=0; i< 20; i++)
{
g_x = 200;
WorkFunc();
g_x++;
cout << " Func : " << g_x << endl;
}
LeaveCriticalSection(&g_cs);
return 0;
}
void main(){
InitializeCriticalSection(&g_cs); // 임계영역 변수 초기화.
DWORD tid;
HANDLE hThread = CreateThread(NULL, 0, Func, 0, NORMAL_PRIORITY_CLASS, &tid);
// Sleep(1);
EnterCriticalSection(&g_cs);
for( int i=0; i< 20; i++) {
g_x = 200;
WorkFunc();
g_x--;
cout << " Main : " << g_x << endl;
}
LeaveCriticalSection(&g_cs);
WaitForSingleObject(hThread, INFINITE);
CloseHandle(hThread);
DeleteCriticalSection(&g_cs); // 파괴
}
```

**해당 코드를 실행해보면 199 199 201 201 나오는 것을 확인할 수 있다 .** 

**만약 for문안으로 Enter 와 Leave 를 넣으면 어떤일이 벌어질까** 

**→ 199 200 199 201 등 값이 꼬여버리게 된다.**

# **동기화 객체 : 뮤텍스 (소유권)**

**→ 씨스타 소유 아닙니다.👨👨**

**동기화 객체란 말 그대로 동기화에 사용되는 객체이다.**

**프로세스와 쓰레드와 같이 커널 객체를 소유하며 프로세스 한정적인 핸들을 가진다.**

**뮤텍스 아무도 소유안하면 → 시그널**

**뮤텍스를 소유한다면 → 논 식스널**

**티켓 들고 도망간 새기  → 포기된 뮤텍스** 

- **신호상태(Signal) : 스레드의 실행을 허가하는 상태이다. 신호상태의 동기화 객체를 가진
스레드는 계속 실행할 수 있다.**
- **비신호상태(Nonsignal) : 스레드의 실행을 허가하지 않는 상태이며 신호상태가 될 때
까지 스레드는 블록된다.**
- **해당 시그널을 사용하기 위해서는**
    
    **DWORD WatiForSingleObject(HANDLE hHandle, DWORD dwMilliseconds);을**
    
    **사용하는데 해당 함수의 반환 값을 통해 상태를 알 수 있다.**
    
    ![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2022.png)
    

**뮤텍스 생성을 통해 쓰레드생성에 뮤텍스 소유를 false 로 해도 이전에 생성된 소유권을 가진 스레드가 있다면 해당 쓰레드에서 소유권을 해제하면 그 소유권을 이전받아 실행한다 !**

- **뮤텍스를 사용하려면 생성이 필연적이다. 아래의 함수는뮤텍스를 생성하고 그 핸들을 리턴해준다.**

**HANDLE CreateMutex(LPSECURITY_ATTRIBUTE lpMutex, BOOL bInitialOwner,
LPCTSTR lPName)**

**만약 뮤텍스의 이름이 있는 경우** 

**HANDLE OpenMutex(DWORD dwDesiredAccess, BOOL bInheritHandle,
LPCTSTR lpName); 로 구동된다.**

**실행동안 시그널 상태가 되지만 BOOL ReleaseMutex(HANDLE hMutex);를 하면 논시그널 상태가 된다.**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2023.png)

**waitforsingleobject 뮤텍스  signal 상태면 통과  내가 소유권을 소유한다면 통과** 

**Nosignal 상태이고, 소유권도 없다면 - 삐빅 ! 정지 X 3** 

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2024.png)

**다음은 뮤텍스를 활용한 예제이다 .** 

```cpp
// 뮤텍스
#include <windows.h>
#include <iostream>
using namespace std;
void main(){
// 뮤텍스 생성
HANDLE hMutex = CreateMutex(NULL, // 보안속성
FALSE, // 생성시 뮤텍스 소유 여부
TEXT("mutex")); // 이름
// 소유가 ture일때 -> Signal 을 nonsignal로 바꾼다.
cout << "뮤택스를 기다리고 있다." << endl;
DWORD d = WaitForSingleObject(hMutex, INFINITE); // 대기
if( d == WAIT_TIMEOUT)
MessageBox(NULL, TEXT("WAIT_TIMEOUT"), TEXT(""), MB_OK);
else if( d ==WAIT_OBJECT_0)
MessageBox(NULL, TEXT("WAIT_OBJECT_0"), TEXT(""), MB_OK);
else if( d == WAIT_ABANDONED_0)
MessageBox(NULL, TEXT("WAIT_ABANDONED_0"), TEXT(""), MB_OK);
cout << "뮤택스 획득" << endl;
MessageBox(NULL, TEXT("뮤택스를 놓는다.") , TEXT(""), MB_OK);
 //ReleaseMutex(hMutex);
}
```

# **세마포어 (뮤텍스와 유사한 동기화 객체)**

**탈 수 있는 카운트 수 → 탈 때마다 카운트가 감소됨  waitforsingleObject를 통과라떄** 

**count 가 0이 되면 non-signal 이 된다.** 

**뮤텍스는 하나의 공유자원을 보호하기 위해 사용하지만 세마포어의 경우 제한된 일정 개수를 가지는 자원을 보호한다.**

**→세마포어는 사용가능한 자원의 개수를 카운트하는 동기화 객체이다. 유효 자원이 0이면
즉 하나도 사용할 수 없으면 세마포어는 비신호상태가 되며 1이상이면, 즉 하나라도 사용
할 수 있으면 신호상태가 된다.**

**→ 대기함수는 세마포어가 0이면 스레드를 블록시켜 사용가능한 자원이 생길 때까지, 즉 다
른 스레드가 자원을 풀 때까지 대기하도록 하며 세마포어가 1이상이면 유효 자원의 개수
를 1 감소시키고 곧바로 리턴한다.**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2025.png)

**뮤텍스와 비슷하게 세마포어 또한 , 생성과 생성된 핸들을 얻는 함수가 있니**

**→ HANDLE CreateSemaphore(LPSECURITY_ATTRIBUTE lpSemaphoreAttributes,
LONG llnitialCount, LONG lMaximumCount, LPCTSTR lpName);**

**→ HANDLE OpenSemaphore(DWORD dwDesiredAccess, BOOL bInheritHandle,
LPCTSTR lpName);**

- **CreateSemaphore 의 첫 번째 인자는 주로 NULL을 사용한다.**
- **두 번째 인자는 초기 카운트 값을 지정한다.**
- **세 번째 인자는 최대 카운트 값을 지정한다.**
- **마지막 인자는 이름이며, 이 인자를 통해 프로세스간 동기화가 가능해 진다.**

**뮤텍스와 같이 착한 어린이라면 Release 도 해주어야 한다.**

- **BOOL ReleaseSemaphore(HANDLE hSemaphore, LONG lReleaseCount,
LPLONG lpPreviousCount);**

**→ 하지만 뮤텍스에서는 소유한 쓰레드나 프로세스에서만 호출 가능하지만 세마포어는 누구나 호출할 수 있기에 lReleaseCount 값을 주의해야 하며 사용해야 한다.**

```cpp
#include <windows.h>
#include <stdio.h>
void main()
{
HANDLE hSemaphore = CreateSemaphore( 0, // 보안
3, // count 초기값
3, // 최대 count

 103
TEXT("s")); // 이름
printf("세마포어를 대기합니다.\n");
WaitForSingleObject( hSemaphore, INFINITE ); //--count
printf("세마 포어를 획득\n");
MessageBox(0, TEXT("Release??"),TEXT(""), MB_OK);
LONG old;
ReleaseSemaphore( hSemaphore, 1, &old ); // count -= 2nd parameter
CloseHandle( hSemaphore );
}
// 메세지 Box 의 OK를 누르지 말고.. 4번 이상 실행해 보세요..
```

**→ `lReleaseCount`를 3으로 설정하면 세마포어 값이 3만큼 증가하고, 대기 중인 스레드 중 3개가 깨어나서 자원을 획득하게 됩니다. → 즉, 해제하면 해제 한 만큼 다시 증가한다.**

# **이벤트**

**이벤트(EVENT)란 어떤 사건이 일어났음을 알려주는 동기화 객체이다.**  

**이전에 사용한 크리티컬 섹션,  뮤텍스  , 세마포어 는 공유 자원을 보호하기 위해 사용되지만 이벤트는 그에 비해 쓰레드간의 작업 순서나 시기를 조정하기 위해 사용된다.**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2026.png)

**→특정한 조건이 만족할 때 까지 대기해야 하는 쓰레드가 있는 경우 이 쓰레드의 실행을 이벤트로 제어할 수 있다.**

**→ 즉 , 이벤트는 윈도우 메시지와 매우 유사한 구조이며 , 만약 정렬이나 다운로드가 끝났을 때 이벤트를 보내주어 관련된 다른 작업을 하도록 지시할 수 있다.**

**이벤트를 기다리는 스레드는 이벤
트가 신호상태가 될 때까지 대기하며 신호상태가 되면 대기를 풀고 작업을 시작한다**

**이벤트의 리셋되는 방식** 

- **자동 리셋 이벤트 :  쓰레드 순서 제어 → 순서를 제어**
- **수동 리셋 이벤트 : 수동 동시에 여러 쓰레드 깨우기**

**이벤트 생성**

**HANDLE CreateEvent(LPSECURITY_ATTRIBUTES lpEventAttributes,BOOL bManualReset, BOOL bInitialState,LPCTSTR lpName);**

**이벤트 오픈  : HANDLE OpenEvent(DWORD dwDesiredAccess, BOOL bInheriHandle,LPCTSTR lpName);  → bManualRese가 TRUE 면 수동 리셋 이벤트** 

### **수동 리셋 이벤트**

- **→ 기다리던 쓰레드들을 깨워주는 역할**

```cpp
#include <iostream>
#include <windows.h>
using namespace std;
void main()
{
HANDLE hEvent = CreateEvent(NULL, // 보안속성
TRUE, // 수동리셋(TRUE), 자동리셋(FALSE)
FALSE, // 초기 상태( NON SIGNAL )
TEXT("e")); // 공유할 이벤트 이름
/*
AUTO RESET : WaitForSingleObjecct를 통과하는 순간
Signal => NonSignal로 변경시켜 줌.
MANUAL RESET : WaitForSingleObject를 통화하더라도
Signal 상태를 계속 유지함..
=> 기다리던 스레드들을 다 깨워주는 역할..
*/
cout << "Event를 기다린다." <<endl;
WaitForSingleObject(hEvent, INFINITE);
cout << "Event 획득 " << endl;
cout << "Event를 기다린다." << endl;
WaitForSingleObject(hEvent, INFINITE);
cout << "Event 획득" << endl;
CloseHandle(hEvent);
}
```

**다음은 수동 리셋을 하는 것을 확인하였다 그럼 수동이벤트 는 WaitForSingleObject 을 통과하면서 기본 설정 signal 값이 FALSE 여도WaitForSingleObject 통과하면서 nosignal에서 signal 형태로 바꾸면서 다른 쓰레드들을 다 깨우는 것이다.**

**→SetEvent 는 이벤트를 모두 기다리는 쓰레드를 깨우는거고 PluseEvent 는 그 중에서 임의의 쓰레드만 깨우는것이다**

**→ PalseEvent  Setevent는깨우는 이벤트 시그널을 보내고 PLase 는 보내고 즉시 노 시그널을 보낸다 .**

```cpp
LRESULT CALLBACK WndProc( HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
switch( msg )
{
case WM_LBUTTONDOWN:
{
HANDLE h = CreateEvent(NULL, FALSE, TRUE, TEXT("e"));
SetEvent(h); // Sinal...
// PulseEvent(h);
// Set + ReSet 동시에 날린 개념..
CloseHandle(h);
}
return 0;
case WM_RBUTTONDOWN:
return 0;
case WM_DESTROY:
PostQuitMessage(0);
return 0;
}
return DefWindowProc(hwnd, msg, wParam, lParam);
}
```

# **자동 리셋 이벤트**

**자동 리셋이라는 말의 의미는 대기 상태를 풀 때 자동으로 리벤트를 비 신호상태로 만든
다는 뜻이다. 하나의 스레드만을 위해 이벤트를 사용할 때는 자동 리셋 이벤트를 사용하
는 것이 훨씬 편리하며 논리적으로도 별 문제가 되지 않는다.**

**자동 리셋 이벤트는 기존의 동기화 객체와는 다른 성질을 가지는 데 바로 유일하게 스레
드들의 흐름을 제어할 수 있다**

```cpp
#include <iostream>
#include <windows.h>
using namespace std;
HANDLE hEvent1, hEvent2;
BOOL bContinue = TRUE;
int g_x, sum;
DWORD WINAPI ServerThread( LPVOID p)
{
while( bContinue) 
{
WaitForSingleObject(hEvent1, INFINITE);
sum = 0;
for( int i=0; i< g_x; i++)
sum += i;
SetEvent(hEvent2);
}
cout << "Server종료" << endl;
return 0;
}
110
void main()
{
hEvent1 = CreateEvent(0, 0, 0, TEXT("e1"));
hEvent2 = CreateEvent(0, 0, 0, TEXT("e2"));
HANDLE hThread = CreateThread(NULL, NULL, ServerThread, 0, 0, 0);
while( 1)
{
cin >> g_x;
if( g_x == -1 ) break;
SetEvent(hEvent1); // Signal 발생...
// ... 다른 일 수행
WaitForSingleObject(hEvent2, INFINITE);
cout << "결과 : " << sum << endl;
}
// 먼저 ServerThread 종료
bContinue = FALSE;
SetEvent(hEvent1);
WaitForSingleObject(hThread, INFINITE);
CloseHandle(hThread);
}
```

1. **연산에 필요한 정보를 입력받는다.**
2. **연산을 수행한다.**

**3) 결과를 출력한다.**

**본 예제는 위의 역할을 2개의 스레드로 분리해서 구현한 예제이다.
먼저 메인 스레드의 역할은 사용자로부터 연산에 필요한 값을 입력받고, 연산된 결과를
출력한다. 2nd 스레드의 경우 메인 스레드에서 생성된 연산에 필요한 값을 가지고 연산
을 수행한다.
상호간 스레드의 호출 흐름이 중요하기 때문에 이를 위해 Event 객체를 사용하였다.**

**→WaitForSingleObject(hEvent1, INFINITE);는 SetEvent(hEvent1)을 받기전까지 대기한다는것이다.**

**→WaitForSingleObject(hEvent2, INFINITE);는 SetEvent(hEvent2)을 받기전까지 대기한다는것이다.**

# **기타 대기 함수**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2027.png)

**첫 번째 인자는 대기 하는 핸들의 개수 이며, 최대 64개의 핸들값을 대기할 수 있다.
두 번째 인자는 핸들을 담고 있는 배열의 주소값이다.
세 번째 인자는 함수의 리턴 방식인데, 만약 TRUE 일 경우는 대기 하는 모든 핸들이 시그
널 되어야만 리턴되며 FALSE 이면 대기 하는 핸들 중 하나라도 시그널되면 리턴된다.
마지막 인자는 대기 시간으로 기존 WaitForSingleObject 함수와 동일하다.**

```cpp
#include <windows.h>
#include <stdio.h>
void Delay() { for (int i = 0; i < 5000000; ++i); } // 시간 지연
BOOL bWait = TRUE;
CRITICAL_SECTION cs; // 임계영역 cs 안에는 1개의 스레드만 들어 올수 있다.
DWORD WINAPI foo(void* p)
{
	char* name = (char*)p;
	static int x = 0;
	for (int i = 0; i < 20; ++i)
	{
		EnterCriticalSection(&cs); // cs에 들어 간다.
		//---------------------------------------------
		x = 100; Delay();
		x = x + 1; Delay();
		printf("%s : %d\n", name, x); Delay();
		//---------------------------------------------
		LeaveCriticalSection(&cs); // cs에서 나온다.
	}
	printf("Finish...%s\n", name);
	return 0;
}
void main()
{
	InitializeCriticalSection(&cs); // main 제일아래에
	HANDLE h1 = CreateThread(0, 0, foo, "A", 0, 0);
	HANDLE h2 = CreateThread(0, 0, foo, "\tB", 0, 0);
	HANDLE h[2] = { h1, h2 };
	// 64 개 까지의 KO 를 대기할수 있다.
	WaitForMultipleObjects(2, h, TRUE, // 2개 모두 signal 될때 까지 대기
		INFINITE);
	CloseHandle(h1);
	CloseHandle(h2);
	DeleteCriticalSection(&cs);
}
```

# **원자 연산 함수**

```cpp
#include <windows.h>
long value = 0;
DWORD WINAPI ThreadFunc( void* p)
{
int i = 0;
for ( i = 0; i < 10000000; ++i )

//InterlockedIncrement(&value); 아래의 ++value 지우고 이거사용하면 해결 !
 
++value;
return 0;
}
void main()
{
int i = 0;
HANDLE hThread[5];
for ( i = 0; i < 5; ++i )
hThread[i] = CreateThread( 0, 0, ThreadFunc, 0, 0,0);
WaitForMultipleObjects( 5, hThread, TRUE, INFINITE );
for ( i = 0; i < 5; ++i )
CloseHandle( hThread[i]);
 
printf("value = %d\n", value);
}
```

**위의 코드에서 값이 꼬이는 이유는 ?**

**바로 경쟁 상대 때문이다.  5개의 쓰레드가 지속적으로 스위칭 하면서 값을 증가하여 값이 꼬여버린다.**

**만약 경쟁 상대에서 리턴이 필요하거나 값을 가져와야 한다면?  멈추는 경우→ 데드락이 발생한다.**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2028.png)

# **생성자 - 소비자 패턴**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2029.png)

**소비자 패턴 → 갓 전입온 이등병 (나다싶을 잘함);**

**→ 하지만 스케쥴링을 통한 시점으로 본다면 소비자에서 이 이등병은 게속 선임에게 지속적인 질문을 하는 행동은 선임의 화를 유발할 수 있다 ⇒ cpu 상에서의 낭비**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2030.png)

**이 패턴의 소비자는 → 상꺽을 단 후임이다. (슬슬 개김);**

**병장은 이자식이 맘에 안듬 하지만그래도 일이 왔다는건 통지를 해야함**

**→ 아까의 생성자 소비자 패턴에서는 큐를 게속해서 검사하기에 불필요한 cpu 타임을 갖게 된다.**

**이를 줄이기 위해 condiotn_variable 을 사용하였지만 WINAPI 에서는 WaitforSignleobject를 통해 이벤트를 주기 전까지 기다리게 하고 생성자에서 메시지가 발생하면 쓰레드를 깨우는 방법이다.**

```cpp
#include <windows.h>
#include <iostream>
#include <queue> // STL의 Q
#include <time.h>
using namespace std;
queue<int> Q; // 2개의 스레드가 동시에 사용하는 공유 자원
HANDLE hMutex; // Q에 접근을 동기화 하기 위해 Mutex사용(CRITICAL_SECTION 이
// 더 좋긴 하지만.mutex예제를 위해)
HANDLE hSemaphore; // Q의 갯수를 Count 하기 위해.
// 생산자
DWORD WINAPI Produce(void*)
{
	static int value = 0;
	while (1)
	{
		// Q에 생산을 한다.
		++value;
		// Q의 접근에 대한 독점권을 얻는다.
		WaitForSingleObject(hMutex, INFINITE);
		//---------------------------------------------
		Q.push(value);
		printf("Produce : %d\n", value);
		LONG old;
		ReleaseSemaphore(hSemaphore, 1, &old); // 세마포어 갯수를 증가한다.
		//------------------------------------------------
		ReleaseMutex(hMutex);

		 
		 Sleep((rand() % 20) * 100); // 0.1s ~ 2s간 대기.
	}
	return 0;
}
// 소비자
DWORD WINAPI Consume(void* p)
{
	while (1)
	{
		WaitForSingleObject(hSemaphore, INFINITE); // Q가 비어 있다면 대기.
		WaitForSingleObject(hMutex, INFINITE);
		//----------------------------------------------
		int n = Q.front(); // Q의 제일 앞요소 얻기(제거하지 않는다.)
		Q.pop(); // 제거.
		printf(" Consume : %d\n", n);
		//----------------------------------------------
		ReleaseMutex(hMutex);
		Sleep((rand() % 20) * 100); // 0.1s ~ 2s간 대기
	}
	return 0;
}
void main()
{
	hMutex = CreateMutex(0, FALSE, TEXT("Q_ACCESS_GUARD"));
	hSemaphore = CreateSemaphore(0, 0, 1000, TEXT("Q_RESOURCE_COUNT")); //최대
	// 1000개의 , 초기 0
	srand(time(0));
	HANDLE h[2];
	h[0] = CreateThread(0, 0, Produce, 0, 0, 0);
 
 h[1] = CreateThread(0, 0, Consume, 0, 0, 0);
	WaitForMultipleObjects(2, h, TRUE, INFINITE);
	CloseHandle(h[0]);
	CloseHandle(h[1]);
	CloseHandle(hMutex);
	CloseHandle(hSemaphore);
}
```

# **Process 간 데이터 공유 및 전달
메모리 맵 파일(Memory Mapped File)**

**파일이지만메모리처럼 사용한다.** 

**프로그램 실행하면 렘에 올라간다 메모리 부족하면 페이징 파일을 사용한다.**

**하드디스크에 페이징 파일을 생성하여 메인 메모리 처럼사용한다.**

**파일과 프로그램이 메모리 처럼연결된다.**

- **파일 매핑을 위한 순서**
1. **CreateFile API 로 파일을 연다. → 바로 페이징 파일을 사용할 수 있다**
2. **CreateFileMapping API 로 그파일에 대한 매핑 객체를 생성한다.**
3. **MapViewOffice API 로 파일 매핑 객체의 뷰를 생성한다.**

**매핑을 실시한 시점에서 데이터가 모두 물리 메모리에 로드 되는 것은 아니다.**

**WIndows 에서는 프로그램이 논리 페이지에 실제로 접근한 시점에서 처음으로 파일 데이터를 로드하도록 되어 있다. 따라서 접근하지 않는 부분의  데이터는파일에서 읽을 필요가 없다.**

# **공유 섹션을 이용한 메모리 공유**

```jsx
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <windows.h>
void main(){
// 1. 화일 생성
HANDLE hFile = CreateFile( TEXT("a.txt"), GENERIC_READ | GENERIC_WRITE,
FILE_SHARE_READ | FILE_SHARE_WRITE,0,
CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);
// 2. 화일 매핑 KO 생성
HANDLE hMap = CreateFileMapping( hFile, 0, // 매핑할 화일, KO 보안
PAGE_READWRITE, // 접근 권한
 0, 100, // 매핑 객체 크기
 TEXT("map")); // 매핑 객체 이름
 
// 3. 매핑 객체를 사용해서 가상 주소와 파일 연결
char* p = (char*)MapViewOfFileEx( hMap, FILE_MAP_WRITE,
0, 0, // file offset
0, // 크기.(0 매핑 객체 크기)
(void*)0x10000000); // 원하는 주소.
if ( p == 0 )
printf("error");
else{
printf("매핑된 주소 : %p\n", p);
strcpy( p, "hello");
p[50] = 'a';
p[100] = 'b';
}
UnmapViewOfFile( p );
CloseHandle( hMap );
CloseHandle( hFile );
}
```

# **파일 매핑을 이용한 공유 메모리**

**사실 이름
붙은 파일 매핑 객체를 사용하면 간단하게 공유 메모리를 만들 수 있다**

**파일 매핑 객체의 핸들을 얻어 올 때는 이름을 지정해서 OpenFileMapping API를 호출하
면 된다.**

 **생성하는 프로세스가 정해지지 않은 경우에는 우선 CreateFileMaping API를 호
출해 보는 방법도 있다. CreateFileMapping 은 지정한 이름의 객체가 이미 존재할 경우에
는 그 핸들을 돌려 주기 때문이다.**

```jsx
typedef struct _LINE{
POINTS ptFrom;
POINTS ptTo;
} LINE;
LRESULT CALLBACK WndProc( HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam){
static HANDLE hEvent, hMap;
static LINE* pData;
static POINTS ptFrom;
switch( msg )
{
case WM_LBUTTONDOWN: ptFrom = MAKEPOINTS(lParam);return 0;
case WM_MOUSEMOVE:
if ( wParam & MK_LBUTTON )
{
POINTS pt = MAKEPOINTS(lParam);
HDC hdc = GetDC( hwnd );
48
MoveToEx( hdc, ptFrom.x, ptFrom.y, 0);
LineTo ( hdc, pt.x, pt.y );
ReleaseDC( hwnd, hdc );
// MMF 에 넣는다.
pData->ptFrom = ptFrom;
pData->ptTo = pt;
SetEvent( hEvent );
ptFrom = pt;
}
return 0;
case WM_CREATE:
hEvent = CreateEvent( 0, 0, 0, TEXT("DRAW_SIGNAL"));
hMap = CreateFileMapping( (HANDLE)-1, // Paging 화일을 사용해서 매핑
0, PAGE_READWRITE, 0, sizeof(LINE), TEXT("map"));
pData = (LINE*)MapViewOfFile( hMap, FILE_MAP_WRITE, 0, 0,0);
if ( pData == 0 )
MessageBox( 0, TEXT("Fail"), TEXT(""), MB_OK);
return 0;
case WM_DESTROY:
UnmapViewOfFile( pData );
CloseHandle( hMap );
CloseHandle( hEvent );
PostQuitMessage(0);
return 0;
}
return DefWindowProc( hwnd, msg, wParam, lParam);
```

```jsx
typedef struct _LINE
{

 49
POINTS ptFrom;
POINTS ptTo;
} LINE;
DWORD WINAPI foo( void* p ){
HWND hwnd = (HWND)p;
HANDLE hEvent = OpenEvent( EVENT_ALL_ACCESS, FALSE, TEXT("DRAW_SIGNAL"));
HANDLE hMap = OpenFileMapping( FILE_MAP_ALL_ACCESS, FALSE, TEXT("map"));
if ( hMap == 0 )
{
MessageBox( 0, TEXT("1번 프로그램을 먼저 실행하세요"), TEXT(""), MB_OK);
return 0;
}
LINE* pData = (LINE*)MapViewOfFile( hMap, FILE_MAP_READ, 0, 0, 0);
while ( 1 )
{
// Event를 대기한다.
WaitForSingleObject( hEvent, INFINITE );
// 이제 Line의 정보가 pData에 있다.
HDC hdc = GetDC( hwnd );
MoveToEx( hdc, pData->ptFrom.x, pData->ptFrom.y, 0);
LineTo ( hdc, pData->ptTo.x, pData->ptTo.y);
ReleaseDC( hwnd, hdc );
}
UnmapViewOfFile( pData );
CloseHandle( hMap );
CloseHandle( hEvent );
return 0;
}
```

# **공유 섹션을 이용한 메모리 공유**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2031.png)

# ****

**PE 포맷(Windows실행 파일의 구조) 은 구조적으로 섹션이라는 영역이 존재한다. 보통 실
행코드는 .text 섹션에, 데이터(초기화된 변수들)는 .data 섹션으로 지정되며 그 외 필요에
따라서 다른 섹션들도 존재할 수 있다.
만약, 자신이 작성한 실행 코드 혹은 데이터들을 별도의 섹션으로 만들어서 분리를 해야
할 경우가 생긴다면 어떻게 해야 할까? 결론은 섹션을 생성할 수 있는 기능을 지원해준다**

```jsx
#include <stdio.h>
#include <windows.h>
#include <conio.h>

 51
// 초기화된 전역 변수 .data section 에 놓인다 - 기본적으로 COPY ON WRITE
int x = 0;
#pragma data_seg("AAA") // exe(PE) 에 새로운 data section AAA를 만든다
int y = 0;
#pragma data_seg()
// AAA 섹션의 속성을 지정한다. (Read, Write, Share)
#pragma comment(linker, "/section:AAA,RWS")
void main()
{
++x; ++y;
printf("x = %d\n", x);
printf("y = %d\n", y);
getch();
}
```

**위 프로그램을 죽이지 말고 2번 실행하면 .AAA라는 섹션영역에 저장된 변수가 공유되는
결과를 확인할 수 있다.
주석을 읽어 보면 어떠한 프로그램인지 이해할 수 있을 것이다. 아래 그림은 PEView 유틸
을 통해 생성된 .AAA 섹션이 생성된 결과이다. 우측 창에 보면 .AAA 섹션이 IMAGE_SCN_
MEM_SHARED 플래그 값으로 설정된 것을 볼 수 있다.**

# **WM_COPYDATA - IPC 통신**

**Windows 메시지로 다른 프로세스에 포인터를 넘기는 것은 의미가 없다. 그러나 예외가
있다. 예를 들어, WM_GETTEXT 메시지는 WPARAM 타입, LPARAM 타입의 각 전달인자에
버퍼 크기와 주소를 넘겨서 SendMessage API로 송신하면 윈도우의 제목표시줄 문자열
이 버퍼에 복사되어 돌아온다. 이 메시지는 송신지 윈도우가 다른 프로세스라도 작동한다**

**WM_COPYDATA 메시지를 이용하려면 COPYDATASTRUCT 타입 구조체를 준비해서 송신
하려는 데이터를 저장한 버퍼의 크기와 포인터를 cbData, lpData 맴버에 설정한다.
그리고 나서 wParam에 송신측 윈도우 핸들, lParam에 이 구조체의 포인터를 담아서 Sen
dMessage API로 송신하면 Windows에서 자동으로 프로세스 간에 버퍼의 내용을 복사해
준다.**

**수신측 윈도우의 프로시저가 이 메시지를 처리할 경우에는 Windows에서 할당하여 데이
터를 복사한 버퍼의 포인터가 lpData에 설정되어 있다. dwData에 데이터 구조를 식별하
기 위한 ID를 넣어두면 다양한 데이터 형식에서 데이터를 주고 받을 수도 있다.**

**첫 번째는 송신하려는 데이터 안에 포인터를 포함해서는 안 된다. Windows는 버퍼의 데
이터 구조를 인식하지 못하기 때문에 필요에 따라서 주소를 변환하거나 그 포인터가 가
리키는 메모리의 내용까지 복사해 주지는 않기 때문이다.
두 번째로 주의할 점은 버퍼의 프로세스 간 복사는 송신측에서 수신측에게로만 이루어진
다. 수신측에서 버퍼의 내용을 변경해도 그 내용은 메시지의 송신측에는 복사되지 않는다
. 반대 방향으로 데이터를 넘기고 싶다면 수신측에서 별도로 WM_COPYDATA 메시지를
보내야 한다.
세 번째로 복사된 버퍼는 수신측 윈도우 프로시저의 호출에서 복귀하는 시점에 자동으로
해제된다. 따라서 lpData에 설정된 포인터값을 보존해 두어도 의미가 없다. 다음에 버퍼
의 내용이 필요하다면 데이터 그 자체를 다른 장소에 보존해 두지 않으면 안 된다.
이러한 제한을 이해했다면 프로세스 간 동기화에 신경쓸 필요가 없기 때문에 프로그램에
서 간단히 사용할 수 있다.**

```jsx
#include <windows.h>
#include <stdio.h>
54
void main()
{
char buf[256] = {0};
HWND hwnd = FindWindow( 0, TEXT("B"));
if ( hwnd == 0 )
{
printf("B 윈도우를 먼저 실행해 주세요\n"); return;
}
while ( 1 )
{
printf("B에게 전달한 메세지를 입력하세요 >> ");
gets( buf); // 1줄입력 ?
// 원격지로 Pointer를 전달하기 위한 메세지 - WM_COPYDATA
COPYDATASTRUCT cds;
cds.cbData = strlen(buf)+1; // 전달한 data 크기
cds.dwData = 1; // 식별자
cds.lpData = buf; // 전달할 Pointer
SendMessage( hwnd, WM_COPYDATA, 0, (LPARAM)&cds);
}
}
```

```jsx
#include <windows.h>
LRESULT CALLBACK WndProc( HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam){
static HWND hEdit;
switch( msg )
{
case WM_CREATE:
hEdit = CreateWindow( TEXT("edit"), TEXT(""),
 WS_CHILD | WS_VISIBLE | WS_BORDER | ES_MULTILINE,
10,10,400,400, hwnd, (HMENU)1, 0, 0);

 55
return 0;
case WM_COPYDATA:
{
COPYDATASTRUCT* p = (COPYDATASTRUCT*)lParam;
if ( p->dwData == 1 ) // 식별자 조사.
{
// Edit Box에 추가 한다.
SendMessage( hEdit, EM_REPLACESEL, 0, (LPARAM)(p->lpData));
 SendMessage( hEdit, EM_REPLACESEL, 0, (LPARAM)"\r\n");
}
}
return 0;
case WM_DESTROY:
PostQuitMessage(0);
return 0;
}
return DefWindowProc( hwnd, msg, wParam, lParam);
}
```

```jsx
typedef struct tagDATA
{
TCHAR str1[20];
TCHAR str2[20];
int num;
}DATA;
BOOL CALLBACK DlgProc( HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
56
switch (msg) {
case WM_COMMAND:
switch( LOWORD( wParam ) )
{
case IDOK:
{
HWND hwnd = FindWindow( 0, TEXT("B"));
DATA data;
GetDlgItemText(hDlg, IDC_EDIT1, data.str1, sizeof(data.str1));
GetDlgItemText(hDlg, IDC_EDIT2, data.str2, sizeof(data.str2));
data.num = GetDlgItemInt(hDlg, IDC_EDIT3, FALSE, FALSE);
// 원격지로 Pointer를 전달하기 위한 메세지 - WM_COPYDATA
COPYDATASTRUCT cds;
cds.cbData = sizeof(DATA); // 전달한 data 크기
cds.dwData = 1;
// 식별자
cds.lpData = &data; // 전달할 Pointer
SendMessage( hwnd, WM_COPYDATA, 0, (LPARAM)&cds);
}
return 0;
case IDCANCEL: EndDialog(hDlg, IDCANCEL); return 0;
}
return TRUE;
case WM_DESTROY:
return 0;
}
return FALSE;
```

```jsx
typedef struct tagDATA
{
TCHAR str1[20];
TCHAR str2[20];
int num;
}DATA;
BOOL CALLBACK DlgProc( HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
switch (msg) {
case WM_COPYDATA:
{
COPYDATASTRUCT *ps = (COPYDATASTRUCT*)lParam;
DATA *pData = (DATA*)ps->lpData;
SetDlgItemText(hDlg, IDC_EDIT1, pData->str1);
SetDlgItemText(hDlg, IDC_EDIT2, pData->str2);
SetDlgItemInt(hDlg, IDC_EDIT3, pData->num, FALSE);
}
return 0;
case WM_COMMAND:
switch( LOWORD( wParam ) )
{
case IDCANCEL: EndDialog(hDlg, IDCANCEL); return 0;
}
return TRUE;
case WM_DESTROY:
return 0;
}
return FALSE;
}
```

**두 번째 인자는 초기 카운트 값을 지정한다.
세 번째 인자는 최대 카운트 값을 지정한다.
마지막 인자는 이름이며, 이 인자를 통해 프로세스간 동기화가 가능해 진다.**

# **동적 라이브러리 - DLL**

**다른 프로그램에서 이용하는 함수들을 모아둔 것이다.** 

**→ 그러나 표준 C 라이브러리 같은 일반 라이브러리의 파일(.LIB)과는구조나 사용법이 다소 다르다**

- **일반 라이브러리는 소스 코드를 컴파일한 결과로 생성되는 객체 파일(.OBJ)을 그대로 모아둔 것이다.**
- **링커는 이 중에서 필요한 함수가 포함된 객체 파일을 꺼내서 실행 파일에 결합하는 것이다.**
- **이를 '정적 링크'라고 한다.**

**→ 즉 , 정적 라이브러리는 코드의 첫상단부분에 모든 함수들을 포함시키는 것이고 , 동적 라이브러리는 필요할때 마다 함수를 호출하는 것이다**

**이에 반해 DLL은 '동적 링크(dynamic link)' 에 이용한다. 이는 링크 시에 실행 파일에 결
합되는 것이 아니라 프로그램 실행 시에 DLL도 함께 프로그램의 메모리 공간으로 읽어와
호출될 주소 등을 적절하게 바꾸는 것을 말한다.**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2032.png)

- **일단 DLL 코드는 여러 프로그램에서 공유할 수 있다**

- **정적 라이브러리는 라이브러리 코드가 각각의 실행 파일에 포함되어 있다. 즉, 같은 라
이브러리를 사용하는 실행 파일이 두 개 있다면, 각 파일에는 똑같은 코드가 중복된 상태
이다.
이에 반해 DLL 코드는 실행 파일에 포함되지 않는다. 그 때마다 독립된 DLL 파일을 로
드하는 방법으로 공용하면 된다.**

**DLL 은 단독으로 실행되는 것이 아니기에 메인함수나 WIN메인함수를사용하지 않는다**

**→ DLLMAIN이라는  함수를 사용한다.**

**[ DLL 이 호출되는 경우 ]**

- **프로세스가 DLL을 로드할 때**
- **프로세스가 DLL을 언로드할 떄**
- **스레드를 생성할 때(로드 중에만)**
- **스레드를 종료할 때(로드 중에만)**

**다. DLL 내부의 함수에서 참조할 것이 있다면 이 예제처럼 정적 변수로 선언하면 된다**

- **첫번째 인자에는 로드된 DLL 모듈(인스턴스) 핸들이 저장되었다
→ 즉 , DLL을 포함한 리소스를 로드하고 싶을 때 사용할 수 있다**
- **그렇다면 아래의 전역변수의 DLL 내부의 함수에서 참조할 것이 있다면 이 예제처럼
정적 변수로 선언하면 된다.**

**(즉 , 전역 변수를 함수에서 사용 하겠다는것이다. )**

**(DLL 사용 명시적 )**

```jsx
static int internalCounter = 0;

// DLL 내부의 함수
int MyInternalFunction() {
    // 정적 변수인 internalCounter를 참조하고 수정
    internalCounter++;
    return internalCounter;
}
```

```cpp
static char* buf = 0;
// DLL 핸들(Load 된 주소)
// DllMain이 호출된 이유(4가지 중 1개)
// DLL을 Load한 방법(명시적또는 암시적)
BOOL APIENTRY DllMain(HMODULE hModule , DWORD ul_reason_for_call, LPVOID lpReserved )
{
 switch (ul_reason_for_call)
 {
 case DLL_PROCESS_ATTACH:
printf("DLL이 프로세스에 Load됩니다. 주소는 : %p\n", hModule);
printf("Load방법은 %s\n", lpReserved == 0 ? "명시적":"암시적");
buf = (char*)malloc( 1000 );
 case DLL_THREAD_ATTACH: free(buf); printf("DLL이 프로세스에서 해지 됩니다.\n");break;
 case DLL_THREAD_DETACH:printf("새로운 스레드가 생성됩니다.\n"); break;
 case DLL_PROCESS_DETACH:printf("스레드가 소멸됩니다.\n");break;
 break;
 }
 return TRUE;// FALSE를 리턴하면 DLL이 Load되지 않는다.
}
[
```

**DLL 사용( 암식적 )**

```cpp
#include <stdio.h>
#include <windows.h>
#include <conio.h>
DWORD WINAPI foo( void* p)
{
printf("foo\n");Sleep(5000);
122
return 0; // DllMain() 호출-
>스레드 종료
}
void main()
{
getch();
TCHAR temp[] = TEXT("예제 7-1 DllMain.dll");
HMODULE hDll = LoadLibrary(temp); // DllMain()호출.
if ( hDll == 0)
{
printf("DLL을 복사해 오세요\n");
return;
}
getch();
HANDLE h = CreateThread( 0, 0, foo, 0, 0,0); // DllMain()호출
WaitForSingleObject( h, INFINITE);
getch();
FreeLibrary( hDll ); // DllMain()호출 -> 프로세스에서 해제.
}
```

**→ 다른 메인 함수와 달리 DLL 메인 함수는 여러번 호출될 수 있으니 유의하어야 한다.**

**BOOL APIENTRY DllMain(HMODULE hModule , DWORD ul_reason_for_call, LPVOID lpReserved )에서 DLL메인함수를 호출한 이유를 알고싶으면 2번째 인자로 확인할 수 있다.**

**→ 세번째 인자인 lpReserved 는 암시적인지 명시적인지 반환해준다.**

**구체적으로는 외부 프로그램에서 호출할 함수명이나 메모리에 로드되었을 때의 주소정보 등을 DLL 파일 안에 넣어준다.**

**Windows 에서는 이 정보를 기본으로 외부 프로그램에서 오는 호출을 DLL 내부 코드와 연결한다.**

# **함수를 내보내다 → 함수 익스포트 (수출)**

**DLL 내의 함수를 외부에서 사용하려면 익소프트를 해야한다.**

**여기서 함수를 익스포트 한다는 것은 곧 함수를 외부에 공개한다는 뜻이다.**

**→ 구체적으로는 외부 프로그램에서 사용할 함수명이나 메모리에 로드 되었을 때 주소 정보 등을 DLL 파일 안에 넣어준다. [→ 외부 프로그램에서 함수가 호출 시 주소 정보 등을 DLL 에 넣어준다.]**

**함수를 외부에 공개 하는 방법 (익스포트 )**

- **모듈 정의 파일을 준비해 EXPORTS 문을 기술한다.**

```cpp
EXPORTS
 _GetPersonalInfo
```

- **소스코드 내의 함수 정의 _declspec 키워드를 추가한다.**

```cpp
extern "C"
_declspec(dllexport)
BOOL GetPersonalInfo( HWND hwndParent, PERSONALINFO * pInfo);
```

- **__declspec 키워드를 사용하는 방법에서는 소스 코드 안에서 익스포트하려는 함수 정의
에 _declspec(dllexport) 를 추가하면 된다.**
- **컴파일러가 이 키워드를 찾아내면 그 함수를
익스포트하도록 링커에 지시한다.**
- **→ 그렇다면 왜 수출과 호출을 둘다 사용할까?**

 **→DLL 구조 분리 `dllexport`를 사용하여 어떤 함수나 데이터가 공개되었는지를 DLL에서 명시하고, `dllimport`를 사용하여 다른 모듈에서 해당 함수나 데이터를 가져옵니다.**

**→코드 유지 및 변경 용이성 . DLL의 내부 구현을 변경하더라도 공개된 인터페이스 (exported 함수 및 데이터)를 변경하지 않는 한, 다른 모듈에서 사용하는 코드를 변경하지 않고도 호환성을 유지할 수 있습니다.**

- **_declspec 문에 사용할 수 있는 인수는 다음 네 가지 이다.**

![Untitled](NET%20SYSTE%20b8ab5976aa2b42bc96a42c16cd6b8633/Untitled%2033.png)

**extern "C”** 

**C++ 언어는 작성된 모든 함수에 대한 정보를 밖으로 공개하여 별도의 선언없이 외부에
서 함수를 사용할 수 있도록 해준다**

# **임포트 라이브러리 (.lib)**

**클라이언트에서의 임포트 선언은 함수의 원형이 어떠하다는 것을 밝힐 뿐** 

**그 함수가 어느 DLL에 있는지를 알려주지는 않는다**

 **→ 과연 시스템에서 모든 DLL을 열어 함수를 찾아볼까 ?** 

**→ 그렇다면 클라이언트는 자신이 임포트해야 할 함수가 어느 DLL에 있는지 어떻게 알 수 있을까**

 **—→ 그래서 클라이언트 프로그램은 링크시에 임포트 라이브러리를 링크해야 하며 LIB 파일이 지정하는 DLL 파일을 실행시에 읽어와야 한다.**

**임포트 라이브러리는 DLL과 같은 이름을사용하며 확장자 LIB를 가진다. DLL을 만들 때 DLL 과 함께 만들어 주므로 우리는 이 라
 ⇒ 즉 IMPORT 는 .lib 이고 export 는 .dll 이다.**

**프로그램에서 dlld르 찾는 순서** 

1. **클라이언트 프로그램이 포함된 디렉토리**
2. **프로그램의 현재 디렉토리**
3. **윈도우즈의 시스템 디렉토리**
4. **윈도우즈 디렉토리**
5. **PATH 환경 변수가 지정하는 모든 디렉토리**

**→ 이 순서대로 DLL 을 찾아 보고 없으면 에러 메시지를 출력한다 .**

# **DLL 만들기**

**만약 DLL_SOURCE 내부에 정의 되어있다면 dllexport를 통해 확장하고 외부로 공개되고 외부에서 사용할 수 있도록 하며 ,**

**내가 호출한 함수가 해당 DLL_SOURCE에 정의되어있지 않다면 _declspec(dllimport)를 통해 외부에서 함수를 가져오는 것이다.**

**exe 시키면 메모리 올라가다가 같이 필요한 dll 파일도 메모리로 올라간다.**

```cpp
// 헤더.h 
#pragma once
#ifdef DLL_SOURCE
#define DLLFUNC __declspec(dllexport) 
#else
#define DLLFUNC __declspec(dllimport)
#endif
126
#include <windows.h>
EXTERN_C DLLFUNC void Add( int a, int b);
EXTERN_C DLLFUNC void Sub( int a, int b);
EXTERN_C DLLFUNC void GetResult( int *a);
void foo( int flag);
```

**만약 아래의 함수가 EXTERN_C DLLFUNC 이면 dllexport 로 확장해준다.**  

```cpp
//dll.cpp
#include "pch.h"
//#include "pch.h"는 코드에서 미리 컴파일된 헤더 파일
//(pch는 "precompiled header"의 약어입니다)
#include " 헤더.h "
static int g_result;
void Add( int a, int b)
{
g_result = a + b;
foo(1);
}
void Sub( int a, int b)
{
g_result = a - b;
foo(2);
}
void GetResult( int *a)
{
*a = g_result;
}
void foo( int flag)
{
}
```

# **DLL 의 사용 - 묵시적 연결**

 **000000묵시적 연결(Implicit) 이란 함수가 어느 DLL에 있는지 밝히지 않고 그냥 사용한다.**

**프로젝트에 임포트 라이브러리를 포함해 주어야 하며 윈도우즈는 임포트 라이브러리
의 정보를 참조하여 알아서 DLL을 로드하고 함수를 찾아준다**

**클라이언트 프로그램이
로드될 때 DLL이 같이 로드되거나 이미 DLL이 로드되어 있으면 사용 카운트를 1 증
가시킨다.**

**DLL LOAD =  → + DLL COUNT  + 1**

**묵시적 연결을 위한 조건**

1. **함수의 선언부를 알기 위한 .h 파일**
2. **DLL 정보를 위해 필요한 .lib 파일**
3. **함수의 정의부를 가지고 있는 .dll 파일**

```cpp
#include <windows.h> // user32.dll의 모든 함수 선언.
#include <stdio.h>
// DLL 사용하기
#include "예제 7-3 MyDll.h"
// 관련 헤더 include
#pragma comment(lib, "예제 7-3 MyDll.lib") // 라이브러리 추가
void main()
{
Add(10,20); // DLL 함수 사용
int value;
GetResult(&value);
printf("%d\n", value);
}
```

# **DLL 의 사용 - 명시적 연결**

**명시적 사용에는 다음의 3가지 함수가 사용된다.**  

**HINSTANCE LoadLibrary(LPCTSTR lpLibFileName)**

**→ 사용하고자 하는 DLL을 메모리로 읽어와 현재 프로세스의 메모리 영역에 DLL 매핑시켜** 

**사용 할 수 있도록 해주며 DLL 이 이미 올라와 있다면 사용 카운터 만 증가시킨다.** 

**—> 인수로 읽고자 하는 파일이름을 명시하는 널 종료 문자열을 주어 경로를 생략한다.**

**FARPROC GetProcAddress(HMODULE hModule, LPCSTR lpProcName)**

**→DLL 에서 엑스포트한 함수의 번지를 찾아 그 함수의 함수 포인터를 리턴해 준다.
첫 번째 인수 hModule 은 함수가 포함된 DLL의 모듈 핸들이며 LoadLibrary 함수가 리턴
해 준 값이다**

**→두 번째 인수 lpProcName은 찾고자 하는 함수 이름을 지정하는 널 종료문자열이거나 함
수의 서수값이다. 널 종료 문자열로 함수의 이름을 명시할 경우는 특히 대소문자 구분에
유의하여야 한다. C언어는 대소 문자를 철저하게 구분한다는 점을 명심하자.**

**BOOL FreeLibrary(HMODULE hLibModule)**

**→DLL 의 사용 카운트를 1 감소시키며 사용 카운트가 0이 되었을 경우 메모리에서 DLL을
삭제한다. hLibModule 인수는 LoadLibrary 가 리턴한 DLL의 모듈 핸들이다.**

```cpp
#include <windows.h>
#include <conio.h>
#include <stdio.h>
// DLL의 명시적 연결은 헤더와 라이브러리 파일이 필요 없다.
// DLL 자체만 있으면 사용 가능 하다. - 단 함수의 signature를 미리 알고 있어야 한다.
typedef void(*FUNC)(int, int);
typedef void(*FUNC1)(int*);
void main()
{
getch();
HMODULE hDll = LoadLibrary(TEXT("예제 7-3 MyDll.dll"));
if ( hDll == 0 )
{
printf("dll 을 찾을수가 없습니다.\n");
return;
}
printf("DLL이 Load된 주소 : %p\n", hDll );
//----------------------------------------------
FUNC Add = (FUNC)GetProcAddress( hDll, "Add");
FUNC Sub = (FUNC)GetProcAddress(hDll, "Sub");
FUNC1 GetResult = (FUNC1)GetProcAddress(hDll, "GetResult");
if ( Add == 0 || Sub == 0 || GetResult == 0)
 
 131
printf("DLL 안에서 해당 함수를 찾을수가 없습니다.\n");
else
{
int value;
Add(10, 20);
GetResult(&value);
printf("%d\n", value);
}
FreeLibrary( hDll ); // DLL 해지
}
```

**클래스 엑스 포트** 

```cpp
#ifndef DLLEXPORT
#define CINTDLL __declspec(dllexport)
#else
#define CINTDLL __declspec(dllimport)
#endif
class CINTDLL CInt
{
private:
int i;
public:
CInt(int _i) : i(_i) {}
void Inc();
void Dec();
int GetValue() const;
void SetValue(int _i)
```

```cpp
// CPP
#define DLLEXPORT
#include "예제 7-6 CIntDll.h"
void CInt::Inc()
{
i++;
}
void CInt::Dec()
{
i--;
}
 
 133
int CInt::GetValue() const
{
return i;
}
void CInt::SetValue(int _i)
{
i = _i;
}
```

```cpp
#include <stdio.h>
#include "예제 7-6 CIntDll.h"
#pragma comment(lib, "예제 7-6 CIntDll.lib")
void main()
{
CInt *p = new CInt(10);
p->Inc();
printf("%d", p->GetValue());
p->SetValue(100);
p->Dec();
printf("%d", p->GetValue());
}
```

### **DWORD GetLastError(void)**

**→ 스레드는 최후 에러 코드를 기억하고 있으며 API 함수들은 에러 발생시 어떤 종류의 에러
가 발생했다는 것을 최후 에러 코드에 설정해 놓는다.  → 이를 통해 에러코드 조사 가능** 

### **DWORD FormatMessage(DWORD dwFlags, LPCVOID lpSource,
DWORD dwMessageId, DWORD dwLanguageId, LPTSTR lpBuffer,
DWORD nSize, va_list *Arguments)**

**→ 파일이 읽기전용이라기록이 불가능하면 읽기 전용 속성을 해제하고 할 수 있다 . 다음의 함수이다.**