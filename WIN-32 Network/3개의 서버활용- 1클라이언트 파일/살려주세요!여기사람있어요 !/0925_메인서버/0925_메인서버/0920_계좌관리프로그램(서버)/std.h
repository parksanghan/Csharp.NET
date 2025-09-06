//std.h
#pragma once

#include <stdio.h>

//net...
#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()...
#include <process.h>        //_beginthread()....
#include <stdlib.h>
#include <tchar.h>
//...

#include <vector>
using namespace std;

#include "packet.h"
#include "control.h"
#include "net.h"
#include "wbfile.h"