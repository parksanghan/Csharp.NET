//std.h
#pragma once
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"


#define WIN32_LEAN_AND_MEAN // windows.h 에서 자주 사용하지 않은 것은 컴파일에서
							// 제외 한다. winsock2.h 와의 충돌을 막아 준다.

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