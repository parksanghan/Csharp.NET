//std.h
#pragma once

#include <stdio.h>

//net...
#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <ws2tcpip.h>       //inet_pton()...
#include <process.h>        //_beginthread()....
//...

#include <vector>
using namespace std;


#include "account.h"
#include "packet.h"
#include "control.h"
#include "net.h"
