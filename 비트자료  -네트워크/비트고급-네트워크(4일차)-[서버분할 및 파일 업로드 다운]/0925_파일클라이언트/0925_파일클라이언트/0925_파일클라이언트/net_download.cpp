//net_download.cpp
#include "std.h"

#define DOWN_SERVER_IP			"127.0.0.1"
#define DOWN_SERVER_PORT	8000

//�������� -> send("���ϸ�")->recv("���ϼ���"->����)
void net_download_Start(HWND hDlg, const TCHAR* filename, int size)
{
	//1. ��������
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
    {
        MessageBox(hDlg, TEXT("���̺귯���ʱ�ȭ ����"), TEXT("�ٿ�ε�"), MB_OK);
        return;
    }

    SOCKET sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (sock == INVALID_SOCKET)
    {
        MessageBox(hDlg, TEXT("���� ���� ����"), TEXT("�ٿ�ε�"), MB_OK);
        return;
    }

    int n_ip;
    inet_pton(AF_INET, DOWN_SERVER_IP, &n_ip);  //<-------------------

    SOCKADDR_IN serveraddr;
    ZeroMemory(&serveraddr, sizeof(serveraddr));

    serveraddr.sin_family = AF_INET;
    serveraddr.sin_port = htons(DOWN_SERVER_PORT);
    serveraddr.sin_addr.s_addr = n_ip;
    int retval = connect(sock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
    if (retval == SOCKET_ERROR)
    {
        MessageBox(hDlg, TEXT("���� ���� ����"), TEXT("�ٿ�ε�"), MB_OK);
        return;
    }       

    //2. ���� ��û!
    send(sock, (char*)filename, size*2, 0);

    //3. ���� ����!
    FILE_INFO info;
    //------------------------------------------	
    //3.1) ���� ������ �����Ѵ�.
    recv(sock, (char*)&info, sizeof(info), 0);
    
    //3.2) ���� ����(���� DIR�� ����)
    HANDLE hFile = CreateFile(info.FileName, GENERIC_WRITE, FILE_SHARE_READ,
        0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);

    //3.3) ���� ������ ����
    int total = info.size;
    int current = 0;
    char buf[4096];
    while (total > current)
    {
        int nRecv = recv(sock, buf, 4096, 0);
        if (nRecv <= 0) break;

        DWORD len;
        WriteFile(hFile, buf, nRecv, &len, 0);

        current += nRecv;       
    }
    if (current != total) MessageBox(0, TEXT("Error"), TEXT(""), MB_OK);
    else                    MessageBox(0, TEXT("Success"), TEXT(""), MB_OK);
   
    
    //4.���� ó��
    closesocket(sock);
    CloseHandle(hFile);
    WSACleanup();
}