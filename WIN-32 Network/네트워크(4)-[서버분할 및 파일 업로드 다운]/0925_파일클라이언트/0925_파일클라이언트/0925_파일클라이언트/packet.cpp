//packet.cpp
#include "std.h"

pack_LONGMESSAGE pack_LongMessage(TCHAR* msg)
{
	pack_LONGMESSAGE pack;

	pack.flag  = PACK_LONGMESSAGE;
	_tcscpy_s(pack.msg, _countof(pack.msg), msg);

	return pack;
}

pack_FILELIST pack_DownLoad_FileList()
{
	pack_FILELIST pack;

	pack.flag = PACK_FILELIST;

	return pack;
}