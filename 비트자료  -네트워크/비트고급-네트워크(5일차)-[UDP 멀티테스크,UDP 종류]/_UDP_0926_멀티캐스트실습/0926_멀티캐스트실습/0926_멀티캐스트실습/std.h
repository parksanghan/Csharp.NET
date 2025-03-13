//std.h
#pragma once
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"


#define WIN32_LEAN_AND_MEAN // windows.h ���� ���� ������� ���� ���� �����Ͽ���
							// ���� �Ѵ�. winsock2.h ���� �浹�� ���� �ش�.

#include <Windows.h>
#include <tchar.h>
#include "resource.h"
#include <vector>
using namespace std;
#include <CommCtrl.h>		//������Ʈ��
#include <commdlg.h>

//--net
#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()...
#include <process.h>
//--

#include "Control.h"
#include "handler.h"
#include "control.h"

#include "net_send.h"
#include "net_recv.h"

#include "ui_group.h"
#include "ui_print.h"
#include "ui_msg.h"