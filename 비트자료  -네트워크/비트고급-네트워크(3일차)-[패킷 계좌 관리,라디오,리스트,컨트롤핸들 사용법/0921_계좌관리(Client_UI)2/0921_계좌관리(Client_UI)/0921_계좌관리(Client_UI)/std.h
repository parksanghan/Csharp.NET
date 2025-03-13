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

//--net
#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()...
#include <process.h>
//--

#include "Control.h"
#include "handler.h"
#include "packet.h"
#include "account.h"
#include "control.h"
#include "net.h"
#include "ui_header.h"
#include "ui_insert.h"
#include "ui_select.h"
#include "ui_update.h"
#include "ui_delete.h"
#include "ui_print.h"