//net_upload.cpp
#include "std.h"

#define UP_SERVER_IP "127.0.0.1"
#define UP_SERVER_PORT 7000

void net_upload_start(HWND hDlg, TCHAR*fileDirectory)
{


	//1.서버 연결
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
	{
		MessageBox(hDlg, TEXT("라이브러리 초기화 오류"), TEXT("다운로드"), MB_OK);
		return;
	}

	SOCKET sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (sock == INVALID_SOCKET)
	{
		MessageBox(hDlg, TEXT("소켓 생성 오류"), TEXT("다운로드"), MB_OK);
		return;
	}
	int n_ip;
	inet_pton(AF_INET, UP_SERVER_IP, &n_ip);  //<-------------------

	SOCKADDR_IN serveraddr;
	ZeroMemory(&serveraddr, sizeof(serveraddr));

	serveraddr.sin_family = AF_INET;
	serveraddr.sin_port = htons(UP_SERVER_PORT);
	serveraddr.sin_addr.s_addr = n_ip;

	int retval = connect(sock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
	if (retval == SOCKET_ERROR) {
		MessageBox(hDlg, TEXT("서버 연결 실패"), TEXT("다운로드"), MB_OK);
		return;
	}


	TCHAR fileDirect[200];// = TEXT("C:\\wb38\\KakaoTalk.png");
	//wsprintf(fileDir, TEXT("c:\\wb38\\%s"), fileName);
	wsprintf(fileDirect, fileDirectory);
	
	//2. 전송할 파일Open
	HANDLE hFile = CreateFile(fileDirect, GENERIC_READ, FILE_SHARE_READ,
		0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0);







	if (hFile == INVALID_HANDLE_VALUE)
	{
		MessageBox(hDlg, TEXT("파일 열기 싫패"), TEXT("업로드"), MB_OK);
		closesocket(sock);
		return;
	}

	//1. 파일 이름, 파일 크기를 전송한다.
	DWORD size1;
	DWORD size2 = GetFileSize(hFile, &size1);

	TCHAR fileDrive[20], fileDir[100], fileName[50], fileExt[20];
	_tsplitpath_s(fileDirect, fileDrive, fileDir, fileName, fileExt);

	TCHAR bufName[100];
	wsprintf(bufName, TEXT("%s%s"), fileName, fileExt);

	//1. 파일 정보 송신
	FILE_INFO info;
	_tcscpy_s(info.FileName, _countof(info.FileName), bufName);
	info.size = size2;

	TCHAR buff[100];
	wsprintf(buff, TEXT("전송할 자료 %s - %dbyte"), info.FileName, info.size);
	MessageBox(hDlg, buff, TEXT("업로드"), 0);
	Sleep(3000);
	send(sock, (char*)&info, sizeof(info), 0);

	//-------------------------------------------------
	// 3. 화일 데이터 전송(한번 읽고 보내는 크기 : 4096byte)
	//    4k 이상 되는 파일일 경우 반복해서 보내게 됨!
	//    - 파일크기 : size2
	int total = size2; // 전송할 전체 크기
	int current = 0;   // 전송한 크기
	int nRead = 0;
	TCHAR buf[4096];    // 4k 버퍼.

	while (total > current)
	{
		DWORD len;
		nRead = ReadFile(hFile, buf, 4096, &len, 0);
		if (len <= 0) break;

		int nSend = send(sock, (char*)buf, len, 0);
		if (nSend <= 0) break;

		current += nSend;

		TCHAR buf[100];
		wsprintf(buf, TEXT("전송 중... (%d / %d)"), current, total);
		MessageBox(hDlg, buf, TEXT("업로드"), 0);
		Sleep(3000);
	}
	if (total != current)	printf("전송에러\n");
	else					printf("전송완료 \n");


	if (current != total) MessageBox(0, TEXT("Error"), TEXT(""), MB_OK);
	else                    MessageBox(0, TEXT("Success"), TEXT(""), MB_OK);
	closesocket(sock);
	CloseHandle(hFile);
	WSACleanup();
}