# .NET NETWORK

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled.png)

**같은 네트워크 망에서는 통신 가능 → 같은 네트워크 주소를 사용하기에 가능하다**

**NETWORK ADD : 192.2.2.0    / HOST ADD : 192.2.2.1 , 192.2.2.2**

**사용 예 ) 근거리 센서에서 받은 데이터 값은 TCP / IP 통신과 같은 네트워크 전달을 통해 가능.**

**→ 네트워크 계층 : 물리주소를 사용하여 통신 (MAC 주소)**

**→ 인터넷 계층  :  IP (가상 주소 ) 인터페이스를 수행하며 데이터를종단 시스템까지 전달하는 역할을 수행한다.→ 프로그램에서 사용되는 주소** 

**→ 전송 계층 : 어떤 방식으로 보낼지 EX) TCP/IP  |  UDP(차이 : 신뢰성 )  - 연결형 / 비연결형** 

**(파이프 라인의 끝이 서로 가지고 있음 )   [데이터 경계 유/무]**

**→ 애플리케이션 계층  : HTTP 프로토콜과 FTP 프로토콜이 이에 해당** 

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%201.png)

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%202.png)

**애플리케이션 계층: 애플리케이션에서 받은 유저 데이터를 버퍼에 담고** 

**전송계층 :  받은 유저데이터에 TCP 헤더를 붙임 [TCP 헤더에서는 순서관리 기능 동작해줌]**

**→ 예를 들어 세그먼트 분할 ,  순서 관리 [기억안남 ;]**

**인터넷 계층 : IP 헤더를 붙임**

**네트워크 엑세스 계층 : 이더넷 헤더와 이더넷 트레일러를 붙임** 

**⇒ 반대로 역캡슐화에서는 헤더를 해제하는 과정을 통해 유저 데이터를 확인한다.**

# **소켓 : WIN32 API [라이브러리 OR 인터페이스 객체]**

**→네트워크 통신을 할 때 사용되는 프로그래밍 인터페이스**

**소켓이 필요한이유 : IP 주소를 통해 해당 PC 까진 갈수 있지만 어느 소켓에 패킷을 전송해야하는지** 

**몰루? →소켓 포트번호를 통해 어느 소켓에 패킷을 전달하는지 알 수 있다 .**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%203.png)

# **윈도우 네트워크 프로그램**

**주소 소켓 구조체** 

**inet_addr()과 inet_ntoa()**

# **IP 주소 변환**

**intet_addr() → 문자열의 ip 주소를 정수형으로 변환**

**inet_ntoa()—> 정수형의 ip 주소를 문자열로 변환** 

 ****

**union 공용체 (유니온 공용체 )  → * 메이플 유니온 아닙니다.**

**하나만 만들어지고 메모리를 같이 사용 ?→  덮어씌워짐**

 ****

```cpp
aa.ch = ‘1’

aa.n  =  10;

cout << [aa.ch](http://aa.ch) —> 10 이나옴
// 명시성이 매우 아작남 => 즉 , 유지보수 하기 어렵게 코딩하는 법 제 1開門(개문)
```

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%204.png)

**inet_addr() 함수는 문자열 형태의 IP 주소를 바이트 정렬된 4바이트 정수로 반환한다.
inet_ntoa() 함수는 4바이트 정수를 문자열 형태의 IP 주소로 반환한다.
IN_ADDR 구조체는 다음과 같이 정의되어 IP 주소를 1바이트 형태나 2바이트 형태, 4바이트
형태의 정수로 사용하기 쉽도록 정의된 구조체다.**

**→  즉 , IP 주소를 1바이트 씩 가져오기에 문자열이나 정수형으로 변환하는데 도움이 된다.**

**—> 명시성이 좆될거같은데 .** 

```cpp
#include <winsock2.h>
#include <stdio.h>
int main(int argc, char* argv[])
{
WSADATA wsa;
 if(WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
 {
 printf("윈도우 소켓 초기화 실패!\n");
return -1;
 }
// 문자열 주소 출력
char *ipaddr = "230.200.12.5";
printf("IP 문자열 주소 : %s\n", ipaddr);
// 문자열 주소를 4byte 정수로 변환
printf("IP 문자열 주소 = > 정수 = 0x%08X\n", inet_addr(ipaddr));
// 4byte 정수를 문자열 주소로 변환
IN_ADDR in_addr;ㅁ
in_addr.s_addr = inet_addr(ipaddr);
printf("IP 정수 => 문자열 주소 = %s\n", inet_ntoa(in_addr));
WSACleanup();
return 0;
}
```

**WSAStartup() 함수의 인수와 반환값은 다음과 같다.**

**int WSAStartup ( WORD wVersionRequested, LPWSADATA lpWSAData) ;**

**◦ wVersionRequested
◦ 프로그램이 요구하는 최상위 윈속 버전. 하위 8비트에 주(major) 버전을, 상위
8비트에 부(minor) 버전을 넣어서 전달함
◦ lpWSAData
◦ WSADATA 타입 변수의 주소. 시스템에서 제공하는 윈속 구현에 대한 세부 사항을 얻을
수 있음 → 성공은 0, 실패는 오류 코드를 반환한다.**

**int WSACleanup (void) ; 성공은 0, 실패는 SOCKET_ERROR 를 반환한다**

# **바이트 정렬**

**저장하는 표준 방식은 크게 두 가지다 → 작은 주소 시작인가 , 큰 주소 시작인가.**

**첫 번째 리틀 엔디안 : 끝점**

**첫 번째 빅  엔디안 : 시작점 [큰 메모리 to → 작은 메모리 ]**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%205.png)

**→ 왜필요한가 ?**

**네트워크 호환성 문제 : ?** 

**2) 바이트 정렬**

**- big endian**

**- little endian**

- **모든 시스템은 위 2가지 방식 중 하나를 택1 구현 : 모든 시스템은 바이트 정렬 방식이 다를 수 있다.**

**à HostByteOrder**

- **네트워크 API 함수는 빅엔디안 방식을 사용한다.**

**à NetworkByteOrder**

**함수 중 ( 코드에서 –> 소켓 API함수에 주소를 전달!)**

**HostByteOrder à NetworkByteOrder**

**htons (  short byte) ,  htonl (long byte)**

**함수 중 (소켓 API에서 주소를 획득 à 코드에서 출력)**

**NetwordkByteOrder à HostByteOrder**

**ntohs,             ntohl**

**패킷을 최대한 작은 크기로 보냄** 

**윈도우 소켓 →  주소를 다룸** 

**윈도우 소켓 API  - > 윈소 API** 

**윈도우 소켓 구성 → DLL** 

# **소켓 구조체**

```cpp
typedef struct sockaddr {
u_short sa_family; // Address family. // <- 물리적 접근성 ex ) ipv4 or ipv6
 CHAR sa_data[14]; // Up to 14 bytes of direct address.
} SOCKADDR, *PSOCKADDR, FAR *LPSOCKADDR;
 

typedef struct sockaddr_in {
short sin_family;
 USHORT sin_port;
 IN_ADDR sin_addr;
 CHAR sin_zero[8];
} SOCKADDR_IN, *PSOCKADDR_IN;
```

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%206.png)

# **TCP 소켓 서버/클라이언트 프로그램 흐름과 구조**

**TCP 서버는 조금 복잡하며 크게 두 소켓의 흐름으로 구성된다. 하나는 대기 소켓의 흐름이며
하나는 통신 소켓의 흐름이다.**

**대기 소켓 : 클라이언트의 접속을 대기하고 클라이언트 요청을 수락하며 통신 소켓을 생성하는 역할을 담당한다**

**통신 소켓:  대기 소켓에 의해 자동으로 생성되며 클라이언트 소켓과 통신을 담당한다.**

**예) 회원관리 프로그램**

- **서버(Server) → 백엔드**

**서비스를 제공하는 부분**

**데이터 관리!!!**

**(클라이언트로 받은 데이터를 분석, 저장 보관, 클라이언트가 요청한 정보를 보내기)**

- **클라이언트(Client) → 프론트 엔드**

**제공되는 서비스를 사용하는 부분**

**사용자와 연결부분**

**(회원가입, 회원정보 확인하기 위해 사용자는 정보를 입력한다. à 해당 정보를 서버로 전송!)**

**→ 풀스택 → 모든 부분 다 가능** 

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%207.png)

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%208.png)

**해당 서버 , 클라이언트 시스템 구조도는 이전에 사용하였던 구조와 비슷하다 .**

**→ 수신대기에서 실제 연결이  ⓢ  인 이유는 Recv = accept( ) 와 같이 실제 연결은 Recv 에서 진행되기 때문이다.**

 ****

### **socket() 함수?  -  대기 (소켓 생성)**

**→ 소켓을 생성하는 함수** 

**—>소켓은 두 종단 시스템이 통신하기 위한 연결점이다.**

**소켓 함수 SOCKET(int af , int type , int protocal )**

**→ af 주소 체계로 프로토콜 마다 주소를 체계를 지정하는 방법이 달라지면 TCP , UDP 프로토콜의 경우 AF_INET을 지정**

**→ type은 데이터 통신을 위한 프로토콜 유형으로 SOCKET_STREAM, SOCK_DGRAM, SOCK_RAW 저장**

- **Straem : 연결형**
- **Dgram : 비연결형**

**→ protocal 은 프로토콜을 결정한다. tcp 는 IPPROTO_TCP   UDP 는 IPPROTO_UDP**

**⇒[ SOCKET 함수의 반환은 소켓의 핸들이며 실패 시 INVALID_SOCKET으로 반환한다. ]**

### **bind() 함수 - 대기 (포트 번호, 로컬 IP 할당)**

**→bind() 함수는 대기 소켓의 로컬 IP 주소와 로컬 포트 번호를 결정한다**

**int bind( SOCKET s, const struct sockaddr* name, int namelen);의**

**→s 는 socket()함수로 생성한 소켓의 핸들이며 이 소켓에다가 로컬 IP와 포트를 넣는다는 의미** 

**→ name  소켓 구조체를 사용하여 로컬 IP 와 포트를 할당한다.**

**→ namelen 소켓 구조체의 크기** 

**⇒ [bind() 함수는 생성된 소켓에 로컬 IP 와 포트번호를 넣어준다는 의미]**

**— 성공시 0 을 반환하며 실패 시 SOCKET_ERROR 을 반환**

### **listen() 함수 - 대기**

**listen() 함수가 호출되면 대기 소켓은 클라언트의 접속을 받을 수 있는 상태가 되며
클라이언트의 정보를 저장하는 접속 대기 큐(connection queue)가 만들어 진다.**

**→ scanf 와 같이 버퍼에 없다면 기다림** 

**int listen(SOCKET s, int backlog);**

**→ s 는 소켓핸들**

**→ backlog 는 동시에 접속 가능한 클라이언트 접속 개수[접속 대기 큐의 크기만큼 클라이언트 접속가능]**

**⇒ 반환 값은 성공시 0 실패시 SOCKET_ERROR** 

### **accept() 함수  - 통신**

**→ 접속대기 큐 확인하여 클라이언트가 접속하면 클라이언트와 통신할 수 있는
새로운 통신 소켓을 반환한다 → 접속 대기 큐에서 통신가능한 바로 뒤 통신 소켓을 반환**

**SOCKET accept( SOCKET s, struct sockaddr* addr, int* addrlen);**

**→ s : 대기 소켓의 핸들** 

**→ addr :  통신이 가능한 클라이언트 IP주소와 포트 번호를 받아오는 sockaddr 구조체 out parameter 이다. (소켓의 핸들로 언제든지 클라이언트 정보를 확인할 수 있으므로 바로 클라이언트 정보를
반환받고 싶지 않다면 NULL 로 지정한다.)**

**→ addrlen : addrlen 은 addr 의 크기를 초기화하여 인수로 하면 실제 초기화된 구조체의 크기를 반환하는 in,
out parameter 다.**

**⇒반환값은 새로운 통신 소켓의 핸들이며 실패 시 INVALID_SOCKET 을 반환한다**

### **send () 함수→ 버퍼의 데이터를복사해 연결된 소켓에 전송**

**end() 함수는 애플리케이션 버퍼의 데이터를 TCP 프로토콜의 송신 버퍼(이하 송신 버퍼)로
복사한다→데이터를 송신 버퍼로 모두 복사한 후 실제 복사한 크기를 반환한다.**

**[블로킹 소켓일 때 send() 함수는 애플리케이션 버퍼의 데이터를 모두 TCP 프로토콜의 송신
버퍼에 복사할 때까지 블록 상태가 되며 모두 복사가 완료한 후에 반환한다. 넌블로킹 소켓일 때
send 함수는 애플리케이션 버퍼의 데이터가 송신 버퍼의 크기보다 커서 모두 복사할 수 없다면
송신 버퍼의 남은 크기만큼만 복사하고 바로 반환하며 실제 복사한 크기값을 반환한다.]**

**int send(SOCKET s, const char* buf, int len, int flags);**

**→s 는 통신 소켓의 핸들이다.
→buf 는 애플리케이션 버퍼의 주소다.
→len 은 송신 버퍼에 쓸 크기다.
→flags 는 보낼 데이터의 소켓 옵션을 지정할 수 있으며 대부분 0이다. MSG_DONTROUTE 나
MSG_OOB 등을 지정할 수 있으며 거의 사용되지 않는다.**

**⇒반환값은 실제 보낸 크기(바이트)이며 실패 시 SOCKET_ERROR 를 반환한다**

### **recv() 함수→ 수신 버퍼의 데이터를 복사**

**recv() 함수는 TCP 프로토콜 수신 버퍼(이하 수신 버퍼)의 데이터를 애플리케이션 버퍼로
복사한다. → scanf 와 같이 버퍼에 데이터가 없다면 대기** 

**int recv(SOCKET s, char *buf, int len, int flags);**

- **`s`: 데이터를 수신할 소켓의 핸들입니다.**
- **`buf`: 수신한 데이터를 저장할 버퍼의 포인터입니다. 수신한 데이터는 이 버퍼에 저장됩니다.**
- **`len`: 버퍼의 크기를 나타내는 정수입니다. `buf` 버퍼에 저장될 수신 데이터의 최대 길이를 지정합니다.**
- **`flags`: 수신에 대한 특별한 옵션을 설정하는 데 사용됩니다. 보통 0으로 설정됩니다.**
    
    **`recv` 함수는 다음과 같은 반환 값을 가집니다:**
    
    - **성공적으로 데이터를 수신했을 경우: 수신한 바이트 수를 반환합니다.**
    - **연결이 종료되었을 경우: 0을 반환합니다.**
    - **오류가 발생한 경우: SOCKET_ERROR를 반환하며, 오류 코드는 `WSAGetLastError()` 함수를 통해 확인할 수 있습니다.**

 ****

### **connect () 함수 → 연결 요청**

**connect() 함수는 클라이언트가 서버에 접속을 요청하며 TCP 프로토콜(운영체제) 수준의 접속을
위한 절차에 들어간다.→이때 서버는 listen()함수가 호출되어 있어야 접속이 성공한다.**

**⇒접속이 성공하면 connect()함수 호출 이후의 소켓은 통신이 가능한 통신 소켓이 된다.**

**int connect(SOCKET s, const struct sockaddr *name, int namelen);**

- **`s`: 연결할 소켓의 핸들입니다.**
- **`name`: 연결하려는 서버 소켓의 주소 정보를 담고 있는 `struct sockaddr` 구조체의 포인터입니다. 이 구조체에는 서버의 IP 주소와 포트 번호가 포함되어야 합니다.**
- **`namelen`: `name` 구조체의 크기를 나타냅니다.**
- **`connect` 함수는 다음과 같은 반환 값을 가집니다:**
- **연결이 성공했을 경우: 0을 반환합니다.**
- **연결에 실패했을 경우: SOCKET_ERROR를 반환하며, 오류 코드는 `WSAGetLastError()` 함수를 통해 확인할 수 있습니다.**

**nt connect(SOCKET s, const struct sockaddr* name, int namelen);의
s 는 서버와 통신할 통신 소켓의 핸들이다.name 은 서버의 원격 IP 와 원격 포트 번호를 설**

 **const struct sockaddr* name → 주소** 

**htons  htonl : 호스트 > 네트워크 (바이트정렬)
ntohs  ntohl : 네트워크 > 호스트  (바이트정렬)
숏타입 롱타입**

**inet_pton 문자열 > 정수(IPv4 주소)
inet_ntop 정수 > 문자열  (IPv4 주소)**

**서버.cpp**

```cpp
//02_TCP 서버
#include <stdio.h>
//#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <WinSock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>   //inet_pton();
//10.101.94.46
SOCKET listenSock;
int net_init();
void net_run();
void net_exit();

int main()
{
    if (net_init() == -1)
    {
        printf("net_init 실행 실패\n");
        return 0;
    }
    net_run();

    return 0;
}

int net_init()
{
    //1 초기화
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)   //버전을 전달해서 통신을 준비함
    {
        printf("윈도우 소켓 초기화 실패!\n");
        return -1;
    }
    printf("윈도우 소켓 초기화 성공!\n");

    //2. 소켓 생성 -> Bind() 주소할당 -> listen() 망연결

    listenSock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);   //대기 소켓 생성
    if (listenSock == INVALID_SOCKET) { return -1; }   //실패시 종료

    SOCKADDR_IN serveraddr;   //서버주소 생성

    //소켓 주소 구조체의 모든 메모리를 0으로 초기화!
    //ZeroMemory(&serveraddr, sizeof(serveraddr));   // API 아래와 같은 코드
    memset(&serveraddr, 0, sizeof(serveraddr));      // C

    serveraddr.sin_family = AF_INET;            //주소체계
    serveraddr.sin_port = htons(9000);         //지역포트번호
    serveraddr.sin_addr.s_addr = htonl(INADDR_ANY);   //지역IP 주소 <INADDR_ANY -> 알아서 할당해줌>

    //서버 주소 구조체를 완성했으니 이를 토대로 소켓을 bind()
    int retval = bind(listenSock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
	//주소의 정보를 담기위해서 형식은 구조체 포인터에서 레퍼런스를 추가
    if (retval == SOCKET_ERROR) { return -1; }   //실패시 종료

    //bind()했으니 바로 listen() 연결
    retval = listen(listenSock, SOMAXCONN);   //SOMAXCONN << 버퍼 크기. 존나 큼
    if (retval == SOCKET_ERROR) { return -1; }   //실패시 종료

    return 0;
}

void net_run()
{
    SOCKET clientSock;      //통신 소켓
    char buf[512];

    while (1)
    {
        //대기큐에서 연결 요청을 수락해야 함!
        SOCKADDR_IN clientaddr;
        int addrlen = sizeof(clientaddr);   //자기 자신의 주소 구조체 크기

        // 접속 대기
        clientSock = accept(listenSock, (SOCKADDR*)&clientaddr, &addrlen); //&addrlen < 받아오는거 아님...
        if (clientSock == INVALID_SOCKET) { break; }   //실패 시 다시 시도

        //아래의 clientaddr.sin_addr에는 숫자 저장. 무슨 숫자? ip숫자(네트워크 바이트 정렬)
        //그러니까 '정수(네트워크 정렬) > 정수(호스트 정렬) > 문자열의 절차'를 거쳐야함...
        //전에도 한번 했었는데 그때는 정말 긴 코드를 써야했지만, inet_ntoa()이 함수를 통해 편의성이 많이 좋아짐!
        //그나저나 얘네 함수이름 진짜 지지리도 못 짓는다

        //printf("\n>> 클라이언트 접속 - (%s:%d)\n",
           //inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));
           //^ 위처럼 네트워크 바이트를 항상 잘 인식하자

        char recvip[50];
        inet_ntop(AF_INET, &clientaddr.sin_addr, recvip, sizeof(recvip));
        int recvport = ntohs(clientaddr.sin_port);
        printf("\n>> 클라이언트 접속 - (%s:%d)\n", recvip, recvport);

        //수신처리
        while (1)
        {
            int ret = recv(clientSock, buf, sizeof(buf), 0);
            if (ret > 0)
            {
                 
                buf[ret] = '\0';   //??
                printf("\t %s\n", ret);

            }
            else if (ret == 0)
            {
                printf("상대방이 소캣을 종료\n");
                break;
            }
            else   //-1 이하일 때, 에러
            {
                printf("상대방이 소캣을 종료하기 전에 프로그램을 종료함\n");
                break;
            }
        }
    }
}

void net_exit()
{
    closesocket(listenSock);
    WSACleanup();
}
```

**클라이언트.cpp**

```cpp
//03_TCP 클라이언트
#include <stdio.h>
//#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <WinSock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>   //inet_pton();
#define SERVER_IP "220.90.179.75"   //"127.0.0.1" cmd >> ipconfig
#define SERVER_PORT 9000

SOCKET sock;

int net_init();
void net_run();
void net_exit();

int main()
{
    if (net_init() == -1)
    {
        printf("net_init 실행 실패\n");
        return 0;
    }
    printf("연결 성공\n");

    net_run();
    net_exit();
    return 0;
}

int net_init()
{
    int retval;
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
    {
        printf("윈도우 소켓 초기화 실패!\n");
        return -1;
    }

    // 클라이언트 소켓 생성
    sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (sock == INVALID_SOCKET) { return -1; }

    SOCKADDR_IN serveraddr;
    ZeroMemory(&serveraddr, sizeof(serveraddr));

    //서버주소의 정수화
    int n_ip;
    inet_pton(AF_INET, SERVER_IP, &n_ip);

    //서버 주소를 입력
    serveraddr.sin_family = AF_INET;
    serveraddr.sin_port = htons(SERVER_PORT);
    serveraddr.sin_addr.s_addr = n_ip;

    //서버에 접속 요청
    retval = connect(sock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
    if (retval == SOCKET_ERROR) { return -1; }

    return 0;
}

void net_run()
{
    char buf[512];
    while (1)
    {
        printf(">>");
        gets_s(buf, sizeof(buf));
        if (strlen(buf) == 0) { break; }   //엔터만 누르면 반복문 종료

        int r = send(sock, buf, strlen(buf), 0);
        printf("\t 보낸 바이트 크기 : %dbyte \n", r);
    }
}

void net_exit()
{
    closesocket(sock);
    WSACleanup();
}
```

  ****

# **쓰레드를 이용한 다중 에코 서버**

**→ 1. 쓰레드 : 접속을 대기하고 소켓을 생성하는 대기 소켓 쓰레드** 

**→ 2. 쓰레드 : 클라이언트 통신을 담당하는 쓰레드** 

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%209.png)

**→ 연결 된 소켓에서 send 를 하면 recv를 하고 자신의 소켓에서 send를 하면 연결된 소켓에서  recv를 하여 다중 에코 서버 기능을 하는 것..**  

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2010.png)

**→ 해당 구성도에서 accept 호출 시 마다 쓰레드를 생상하여 통신하여 다중 통신 기능 구성**

**쓰레드의 구성 :**

 **전역변수 : 대기소켓, 통신소켓들
SOCKET listenSock;
vector<SOCKET> sockets;**

- **여러 클라이언트의 접속을 대기하고 통신 소켓을 생성하는 대기 소켓 쓰레드**

**→accept 수락 처리 후 → 소켓을 벡터에 저장  → 통신 쓰레드 호출** 

**(해당 코드에서는 여러 클라이언트의 접속을 대기하고 통신 소켓을 생성하는 대기 소켓 쓰레드을 사용하지 않는 구조 )**

```cpp
char recvip[50];
        inet_ntop(AF_INET, &clientaddr.sin_addr, recvip, sizeof(recvip));
        int recvport = ntohs(clientaddr.sin_port);
        printf("\n>> 클라이언트 접속 - (%s:%d)\n", recvip, recvport);

        //3. 통신 소켓 저장
        sockets.push_back(clientSock); // 통신 소켓을 저장
```

```cpp
uintptr_t h1 = _beginthreadex(0, 0, WorkThread, (void*)clientSock, 0, 0);
        CloseHandle((HANDLE)h1);

```

- **클라이언트 통신을 담당하는 통신 소켓 쓰레드다.**

```cpp
unsigned int threadID;
 WaitForSingleObject((HANDLE)_beginthreadex(0,0, ListenThread, 0, 0,
&threadID), INFINITE);
```

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2011.png)

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2012.png)

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2013.png)

# **다중 접속 + 1대 다 통신**

**→ 연결 요청 시 쓰레드를 생성  ⇒ 클라이언트가 접속 요청할 떄마다 쓰레드를 생성 후 할당** 

**모든 통신 쓰레드는 소켓을 한개를 가짐** 

**다른 클라이언트에게 send를 하고자 한다면 해당 client 와 연결된 소켓이 필요하다.**

**→ 즉 클라이언트 1, 2 모두 필요함** 

**send와 recv를 순차적으로 처리하게 되어 음 → 서버가 send 해도 recv 순서가 안되어서 꼬임.**

**—> 간단하게 primary 쓰레드에서는 send만 반복 | 2nd 쓰레드는 recv 만 반복** 

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2014.png)

**서버 →  클라이언트 요청을 수락(accept)한후 출력한뒤에 쓰레드를 생성하여 (무한루프)를 통해  특정 클라이언트와 게속 통신하는 전용 쓰레드를 생성한다.** 

 ****

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2015.png)

 **쓰레드 활용 클라이언트** 

**주 쓰레드 —> connect 이후 쓰레드 생성하여 send를 반복처리**

**서브 쓰레드 —> recv 만 반복처리** 

**다른 구현 —> 연결된 소켓은 accept 를통해 얻는다** 

**다중 에코 서버는 두 가지 쓰레드가 필요 하다.  → 메인함수에서 대기 소켓의 초기화 끝나면 ListenThread호출되고 끝나면서 comThread 호출**  

**gerpeername →소켓의 핸들로 클라이언트의 정보(IP, Port)를 반환하는 함수로 clientaddr 변수가 out
parameter 로 사용되엇다.**

```cpp
int addrlen = sizeof(clientaddr);
 int retval = getpeername(clientSock,
 (SOCKADDR *)&clientaddr, &addrlen);
 if(retval == SOCKET_ERROR)
 {
 DisplayMessage();
 continue;
 }
```

- **대기 소켓을 생성하고 클라이언트의 연결을 대기하는 쓰레드입니다.**

```cpp
unsigned int WINAPI ListenThread(void* pParam)
{
 while(1)
 {
 SOCKET clientSock;
SOCKADDR_IN clientaddr;
int addrlen;
addrlen = sizeof(clientaddr);
// 접속 대기
clientSock = accept(listenSock,(SOCKADDR *)&clientaddr,&addrlen);
if(clientSock == INVALID_SOCKET)
{
 DisplayMessage();
 continue;
 }
printf("\n[TCP 서버] 클라이언트 접속: IP 주소=%s, 포트 번호=%d\n",
 inet_ntoa(clientaddr.sin_addr),
 ntohs(clientaddr.sin_port));
// 클라이언트와 독립적인 통신을 위한 Thread 생성
 unsigned int threadID;
 CloseHandle((HANDLE)_beginthreadex(0,0,ComThread, (void*) clientSock, 0,
&threadID));
}
// 대기 소켓 닫기
closesocket(listenSock);
}
```

- **클라이언트와 실제 통신을 담당하는 쓰레드입니다**

```cpp
unsigned int WINAPI ComThread(void* pParam)
{
 SOCKET clientSock = (SOCKET) pParam;
 int recvByte;
 char buf[BUFFERSIZE];
 SOCKADDR_IN clientaddr;
 while(1)
 {
 // 데이터 받기
 recvByte = recv(clientSock, buf, BUFFERSIZE,0);
 if(recvByte == SOCKET_ERROR)
 { //소켓 비정상 종료
 DisplayMessage();
 break;
 }
 else if(recvByte == 0)
 { //소켓 정상 종료
 DisplayMessage();
 break;
 }
 else
 {
 int addrlen = sizeof(clientaddr);
 int retval = getpeername(clientSock,
 (SOCKADDR *)&clientaddr, &addrlen);
 if(retval == SOCKET_ERROR)
 {
 DisplayMessage();
 continue;
 }
 // 받은 데이터 출력
 buf[recvByte] = '\0';
 printf("[TCP 서버] IP=%s, Port=%d의 메시지:%s\n",
 inet_ntoa(clientaddr.sin_addr),
 ntohs(clientaddr.sin_port),
 buf);
 // 클라이언트로 데이터 에코하기
 retval = send(clientSock, buf, recvByte, 0);
 if(retval == SOCKET_ERROR)
 {
 DisplayMessage();
 break;
 }
 }
 }
 // 통신 소켓 닫기
 closesocket(clientSock);
```

- **좋은코드**

```cpp
//05_다중접속가능한서버.cpp
//04_echo클라이언트로 연결테스트.
/*
------ Primary Thread에서 처리-----
1. accept
2. 연결정보 출력
3. 통신스레드 생성
------------------------------------

* 연결되면 생성되는 전용 스레드
--------통신 스레드 ----------------
1. recv
2. send
* 종료될 때 소캣 close()
-------------------------------------
*/
#include <stdio.h>

#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()...
#include <process.h>        //_beginthread()....

//cmd >> ipconfig :  220.90.179.75
SOCKET listenSock;

int net_init();
void net_run();
void net_exit();

int main()
{
    if (net_init() == -1)
    {
        printf("초기화 오류\n");
        return 0;
    }

    printf("서버 구동...........\n");
    net_run();

    net_exit();

    return 0;
}

int net_init()
{
    //1. 초기화
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
        return -1;

    //2. socket생성-> bind()주소할당 -> listen()망 연결    
    listenSock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (listenSock == INVALID_SOCKET)
        return -1;

    SOCKADDR_IN serveraddr;
    //ZeroMemory(&serveraddr, sizeof(serveraddr));  //API
    memset(&serveraddr, 0, sizeof(serveraddr));     //C

    serveraddr.sin_family = AF_INET;           //주소체계
    serveraddr.sin_port = htons(9000);       //지역포트번호
    serveraddr.sin_addr.s_addr = htonl(INADDR_ANY); //지역IP 주소
    int retval = bind(listenSock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
    if (retval == SOCKET_ERROR)
        return -1;

    retval = listen(listenSock, SOMAXCONN);
    if (retval == SOCKET_ERROR)
        return -1;

    return 0;
}

unsigned int  __stdcall WorkThread(void* p)
{
    SOCKET clientSock = (SOCKET)p;

    char buf[512];
    while (1)
    {
        //수신 처리
        int ret = recv(clientSock, buf, sizeof(buf), 0);
        if (ret > 0)
        {
            buf[ret] = '\0'; //??
            printf("\t %s\n", buf);
        }
        else if (ret == 0)
        {
            printf("상대방이 소캣을 종료\n");
            break;
        }
        else  // -1이하 에러
        {
            printf("상대방이 소켓을 종료하지 않고 프로그램을 종료함\n");
            break;
        }

        //송신 처리
        ret = send(clientSock, buf, (int)strlen(buf), 0);
        printf("\t echo전송 : [%dbyte] %s\n", ret, buf);
    }

    closesocket(clientSock);
    return 0;
}

void net_run()
{
    while (1)
    {
        //1. 대기큐에서 연결 요청 수락!
        SOCKADDR_IN clientaddr;
        int addrlen = sizeof(clientaddr);

        SOCKET clientSock = accept(listenSock, (SOCKADDR*)&clientaddr, &addrlen);
        if (clientSock == INVALID_SOCKET)
            continue; // break; // 

        //2. 정보 출력
        //clinetaddr.sin_addr : ip숫자(네트워크 바이트 정렬)
        //inet_ntoa() : 호스트 바이트 정렬로 변환 ->변환된 숫자를 문자열 형태 IP
        char recvip[50];
        inet_ntop(AF_INET, &clientaddr.sin_addr, recvip, sizeof(recvip));
        int recvport = ntohs(clientaddr.sin_port);
        printf("\n>> 클라이언트 접속 - (%s:%d)\n", recvip, recvport);

        //3. 통신 스레드 호출
        //스레드가 생성될 때 통신할 소켓을 전달!
        uintptr_t h1 = _beginthreadex(0, 0, WorkThread, (void*)clientSock, 0, 0);
        CloseHandle((HANDLE)h1);       
    }
}

void net_exit()
{
    closesocket(listenSock);
    WSACleanup();
}
```

 ****

# **다중 접속 + 1대다 통신 (브로드 캐스트)**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2016.png)

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2017.png)

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2018.png)

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2019.png)

**즉 , 연결이 끊기면 벡터에 있는 소켓에서 삭제 —> while 문 밖에 있음**  

**해당 알고리즘은 주 쓰레드 에서는 send를 하는 역할을 게속 수행하고** 

**서브 쓰레드에서는 recv에 대한 역할을 수행한다.**

# **다 중 에코 클라이언트**

**TCP 프로토콜 기반의 서버와 클라이언트의 구현 시 주의할 점은 TCP 는 ‘데이터의 경계가없다’는 것** 

**네트워크가 혼잡하여 TCP 버퍼의 데이터가 보내지지 않고 데이터를 반복하여 보내면 한번에 보내거나 늦게 가져오는 경우가 생긴다 . 송수신 데이터고려하여 설계**

**→ 데이터 분할 및 재조립** 

**→ 버퍼링  / 데이터 크기와 경계** 

**TCP 데이터를 설계하는 방법은 크게 세 가지가 있다.**

**→경계 구분을 위한 특수한 표식**

**→고정 길이 데이터를 송, 수신한다.**

**→가변 길이 데이터를 송, 수신한다. 서버와 클라이언트는 헤더**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2020.png)

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2021.png)

**TCP 데이터를 설계하는 방법은 크게 세 가지가 있다.**

1. **경계 구분을 위한 특수한 표식(EOR: End Of Record)을 사용한다. 단순한 문자 전송
시스템이나 소형 기기 등의 통신에 사용한다.**
2. **고정 길이 데이터를 송, 수신한다. 서버와 클라이언트는 항상 같은 길이의 데이터를
송수신한다. 사용하기 가장 간단하고 작은 데이터의 송, 수신 시스템에 사용된다.**
    
    **→ 비효율적인방법**
    
3. **가변 길이 데이터를 송, 수신한다. 서버와 클라이언트는 헤더(실제 길이를 포함한
데이터 정보)를 고정길이로 송, 수신한 후 헤더에 포함된 실제 데이터의 길이를
확인하여 데이터의 송, 수신을 완료한다. 송, 수신 데이터가 크거나 가변적일 때
사용된다.→ 가장 우아한 방법** 

**버퍼의 시작주소** 

**→ 필요한 것 : 범위 지정 알고리즘**

```cpp
retval = recvn(sock, buf, 5, 0);
if(retval == SOCKET_ERROR || retval == 0){
    	break;
      }
```

```cpp
int recvn(SOCKET s, char *buf, int len, int flags)
{
	int received;
	char *ptr = buf; // 버퍼의 주소 
	int left = len;  //남은 바이트 크기
 
	while(left > 0){
		received = recv(s, ptr, left, flags);
		if(received == SOCKET_ERROR) 
			return SOCKET_ERROR;
		else if(received == 0) 
			break;
		left -= received;  //남은크기 -= 받은크기
		ptr += received;   //저장 위치 이동
	}
 
	return (len - left);
}
```

# **파일 전송 서버 & 클라이언트**

**서버 → 에코 기능** 

**TCP / IP 기반으로** 

**RECVN 과 같은 기능을 통해 가변길이를 주어 READFILE 이나 SENDFILE 을 통해 파일을 전송함**

**RECVN : 한 바이트 씩 메모리를 읽어가면서 전송** 

 ****

# **SELECT 입출력  모델**

**→ select 모델은 하나의 쓰레드로 다중 클라이언트 에코 서버를 생성할 수 있으며, select()함수를 사용하면 블로킹 , 넌 블로킹 소켓에서 모두 사용할 수 있다. → 즉 , 내가 원할 때마다 블로킹, 논 블로킹 골라서 가능**

**소켓 모드는 두 가지 블로킹 소켓과 넌 블로킹 소켓 모드가 있으며 블로킹 소켓은 소켓 함수를
호출하면 소켓 함수의 목적을 완료할 때까지 반환하지 않고 대기(블로킹)하는 소켓으로 이때
해당(소켓 함수를 호출할 쓰레드) 쓰레드는 블로킹 상태(Blocked State)가 된다.**

** accept() : 클라리언트가 접속할 때까지 블로킹되며 접속하면 반환한다.
 send(), sendto() : 송신 버퍼에 데이터를 모두 복사할 때까지 블로킹되며 모두 복사하면
반환한다.
 recv(), recvfrom() : 수신 버퍼에 데이터가 도착할 때까지 블로킹되며 데이터가 도착하면
데이터를 애플리케이션 버퍼로 복사하고 반환한다.**

# ****

# **블로킹 소켓 VS 논 블로킹 소켓**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2022.png)

**함수는 리턴햇지만 함수는 동작중….??  리턴한다면 함목적을 달성하는지 알 수 가 없다 ?**

**—> 기껏 함수를 실행했더니 ?! 그 함수는 종잇조각이 되어버린다 !?~**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2023.png)

![download.png](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/download.png)

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2024.png)

**—> 즉 , 넌 블로킹 소켓은 소켓 함수를 호출하면  소켓 함수의 목적이 완료되지 않아도 함수가 반환되며 해당 쓰레드는 블로킹이 되지 않고 게속 작업을 수행한다. 그렇기에 , 해당 목적을 완료하였는지에 대한 구분이 필요하다.** 

**⇒  (WSAGetLastError() 함수로 확인하며 소켓 함수가 자신의 목적을 완료하지 않고 반환하면 WSAGetLastError() 함수는 WSAEWOULDBLOCK 을 반환을 통해 오류를 확인한다.)**

- **논 블로킹 소켓 전개 방법 :  ioctlsocket() 호출을 통해 소켓 옵션을 변경한다.**

# **WSAAsyncSelect 입출력 모델**

**WSAAsyncSelect 입출력 모델은 소켓 프로그램이 윈도우 기반의 애플리케이션과 동작하도록 설계된 입출력 모델이다.** 

**→ 해당 모델을 사용하기 위해서는 하나 이상의 윈도우 객체가 필요하며 네트워크에서 발생하는 소켓 이벤트를 윈도우 메시지로 다룰 수 있기에 윈도우 프로시저에서 일반 윈도우 메시지와 함께 네트워크 이벤트 처리가 가능하다 (범용타입)**

**WSAAsyncSelect 입출력 모델의 사용법** 

- **1  소켓을 윈도우 메시지와 연결한다.**
- **2  소켓에 네트워크 이벤트가 발생하면 연결된 윈도우 메시지 윈도우 프로시저에 전달된다.**
- **3  윈도우 프로시저에서 네트워크 이벤트를 확인하고 이벤트에 맞는 적절한 소켓 함수를 호출한다.**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2025.png)

**—> 윈도우 메시지 연결 함수 WSAAsyncSelect( SOCKET s, HWND, hwnd, unsigned int wMsg, long lEvent);**

 ****

**WSAEventSelect 입출력 모델은 소켓의 네트워크 이벤트가 발생하면 커널 오브젝트 이벤트 객체가 신호 상태가 되어 이벤트에 맞는 적절한 소켓 함수를 호출하므로서 작업을 처리한다.**

**데이터에 대해 완료를 bool 형으로 받아 받은 데이터가 다 받았는지 검사한다.**

**또한 , WSAEventSelect 입출력 모델은 WSAEventSelect() 함수는 네트워크 이벤트가 발생하면 소켓의 이벤트를 신호 상태로 만들수 있는 소켓과 커널 이벤트를 연결하는데 애플리케이션에서는 커널 이벤트가 신호 상태가 되는지 대기하고 신호 상태가 되면 어떤 소켓과 연결된 이벤트가 신호상태인지 확인하여 네트워크 이벤트에 맞는 작업을 수행한다.**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2026.png)

- **WSAEventSelect() 모델의 절차는 다음과 같다.**
1. **커널 이벤트를 생성한다. (WSACreateEvent()사용)**
2. **커널 이벤트를 소켓과 연결하여 네트워크 이벤트 발생 시 커널 이벤트가 신호 상태가
되도록한다. (WSAEventSelect() 사용)**
3. **커널 이벤트가 신호 상태가 되는지 대기한다. (WSAWaitForMultipleEvents() 사용)**
4. **커널 이벤트와 연결된 어떤 소켓의 어떤 네트워크 이벤트가 발생되었는지 확인한다.
(WSAEnumNetworkEvents() 사용)**
5. **5. 네트워크 이벤트에 맞는 적절한 작업을 수행한다. (accept(), recv(), send() 등 사용)**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2027.png)

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2028.png)

**—>이벤트가 대기하다가 신호 상태 후에는 int reusltidx =  WSAWaitForMultipleEvents() 에서 이때 성공 시 반환 값에 WSA_WAIT_EVENT_0 을 빼면 신호가 발생한 커널 이벤트 테이블의 인덱스 번호가 된다. !**

 ****

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2029.png)

# **OVERLAPPED 모델**

**overlapped 모델은 기존의 모델과는 달리 비동기 입출력 함수를 사용하여 선 동작 [소켓 함수 먼저 호출] , 후 완료 처리 순으로 작업한다. 그렇기에 , 완료 처리가 중점이며 이 처리 방법에는 2가지가 있다.**

- **커널 이벤트를 활용한 overlapped 모델**
- **루틴이라고 부르는 콜백함수를 사용하는 기법**

**기존의 동기 함수를 통한 제어는 하나하나 완료를 기다려야 했다.**

![**우리가 이전에 사용하던 동기적으로  함수를 사용하여 그 함수가 입출력을 기다림**](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2030.png)

**우리가 이전에 사용하던 동기적으로  함수를 사용하여 그 함수가 입출력을 기다림**

**하지만 비동기 함수를 사용한다면 비동기 입출력은 단순히 운영체제가 각 입출력을 독립된 쓰레드로 수행한다고 생각하면 쉽다. → 내부도 사실 그렇게 작동함**

![**비동기적으로 함수를 호출하여 입출력을 기다리지 않고 게속 실행해 나아감** ](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2031.png)

**비동기적으로 함수를 호출하여 입출력을 기다리지 않고 게속 실행해 나아감** 

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2032.png)

**1번 소켓에서는 , WSARecv 를 게속 하고  2번 소켓에서는WSASend를 게속하는데 비동기 입출력을 통해 신호로 보고한다.**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2033.png)

**통신 소켓이 없을 때 대기를 위한 이벤트가 0으로 반환되고 , 두 소켓이 비동기 입출력이 시작한다는 것은 두 소켓이 비동기 입출력을 한다는것은 함수의 목적을 완료해서 이벤트를 처리하는 것이다.**

**→ 즉 , Overlapped 루틴 모델은 비동기 입출력 완료 보고를 확인하기 위해 애플리케이션이 직접 입출력 완료를 위한 이벤트를 관리하는 것이다.**

**⇒ 주의할 것은 애플리케이션도 작업을 수행중일 것 이기에 애플리케이션이 완료 작업을 처리할 수 있는 준비가 되었을 때 콜백 함수를 호출해야 한다.→ 이때 애플리케이션은 완료 작업을 처리할 준비가 되었다는 것을 운영체제에 알리기위한 방법이 필요한데 이때 쓰레드를 알림 가능 상태(alertable state)로 설정함으로서 가능하다.**

![**서버(생성자)          클라이언트(소비자)(깨우진 않고쓰레드 알림기능 검사)    (쓰레드 state 변경)** ](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2034.png)

**서버(생성자)          클라이언트(소비자)(깨우진 않고쓰레드 알림기능 검사)    (쓰레드 state 변경)** 

**—> 그렇다면 이때 패턴은 즉 , 클라이언트에서 완료작업이벤트를 처리할때 서버에서 쓰레드 알림 가능 상태를 통하여 쓰레드를 깨우는 역할을 하는 패턴이다.  즉 , 소비자 생성자에서 보았던 패턴이나 이제 소비자(클라이언트)가 생성자(서버)를 깨우는 패턴이다.** 

**→ 그런데 이제 서버에서 하던 작업이 끝날때까지 쓰레드 알림기능상태는 0이고 작업을 모두 끝내었다면** 

**쓰레드 알림 기능 상태를 킨다.**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2035.png)

1. **비동기 입출력을 시작한다. 이때 WSARecv()와 WSASend() 마지막 인수로 완료 루 (CompletioinRoutine)의 주소를 설정한다.**
2. **비동기 입출력이 완료되면 운영체제가 APC 큐에 완료 정보를 보관한다.**
3. **애플리케이션이 완료 보고를 받을 준비가 되었음을(alertable state 를 true) 알린다.**
4. **자동으로 완료 루틴을 호출한다.**
5. **알림 가능 상태인 쓰레드가 APC 큐에 있는 모든 완료 정보를 처리한다.**
6. **모든 완료 정보가 처리되면 애플리케이션의 쓰레드는 다시 alertable state 를 false 로
변경된다.**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2036.png)

# **IOCP(I/O Completion Port) 입출력 모델**

**IOCP 가 Overlapped 모델과 다른 점은 Overlapped 모델에서 APC 큐에 보관된 완료 정보는 APC 큐를 소유한 스레드만 확인하고 처리할 수 있지만 IOCP 는 어떤 쓰레드든지 IOCP 핸들을 이용하여 완료 처리를 수행할 수 있다.** 

**→ 그렇기에 많은 입출력 완료처리를 여러 개의 쓰레드로 나누어서 처리할 수 있다.** 

**이는 IOCP 입출력 모델은 완료 작업을 처리하기 위해 쓰레드를 알림 가능 상태로 만들지 않는다.**

**→ 단지 IOCP 핸들을 이용하여 IOCP 에 보관된 완료 정보를 GetQueuedCompletionStatus() 함수를
이용하여 읽어가고 처리한다.(즉,  큐에 저장하여 보관하여 읽어가면서 처리한다는 것)**

**입출력에 참여하는 모든 소켓은 IOCP 와 연결되며 입출력이 완료되면 IOCP 에 완료가 정보가 보관되며 애플리케이션에서는 언제든지 이 IOCP 를 확인하고 완료 정보를 얻어 처리할 수 있는 구조를 갖는다.**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2037.png)

1. **IOCP 후 입출력에 참여하는 모든 소켓을 IOCP 와 연결하고 비동기 입출력을 시작한다.**
2. **비동기 입출력이 완료되면 완료 정보가 IOCP 에 보관된다.**
3. **애플리케이션은 적절한 쓰레드를 생성하여 IOCP 에 비동기 입출력이 완료하는지 확인한다.**
4. **비동기 입출력이 완료되는 것을 확인하면 완료 처리 작업을 진행한다.**

**다음은 비동기 입출력 함수의 인수로 사용되는 WSAOVERLAPPED 구조체 주소를 WorkerThread 의
GetQueuedCompletionStatus() 함수의 네 번제 인수로 받아 소켓 정보 구조체로 형식 변환하여
사용하는 그림을 표현한 것이다.**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2038.png)

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2039.png)

**입출력 작업이 완료되면 IOCP 큐에 들어가는데 이때 모든 작업 쓰레드를 깨운다.  이후 , 큐에 있는 작업완료를  완료한 쓰레드는 리턴하지만 리턴하지 못한것은 아직 작업을 완료하지 못한것으로 쓰레드를 다시 대기(블로킹 ) 시킨다.**  

# **UDP 통신**

1. **유니캐스트 :  보낼때 상대방 주소 정해져 있다.**
2. **브로드 캐스트 : 브로드 캐스트 대역폭 주소  197.1.1.255**

**→ 브로드 캐스트를 위한 setsocket 을 설정** 

1. **멀티 캐스트: 수신 전 특정 ip  로 가입 → recvfrom 을 통한 수신 가능**

![Untitled](NET%20NETWORK%202cf39e531cb744c5a4333ad2843014b0/Untitled%2040.png)

**초기화 과정  서버 : 소켓 생성 → 주소 할당 (BIND )**

                **클라이언트 : 소켓 생성** 

**RECVTO(보낸 주소)                                 SENDTO(보낼 주소)**

**멀티캐스트는 가입 IP 등을 통해 통신을 할 수 있다.**