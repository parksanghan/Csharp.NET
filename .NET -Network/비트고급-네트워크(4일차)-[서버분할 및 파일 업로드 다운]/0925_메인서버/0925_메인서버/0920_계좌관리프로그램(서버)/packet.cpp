//packet.cpp
#include "std.h"

pack_LONGMESSAGE pack_LongMessage(TCHAR* msg)
{
	pack_LONGMESSAGE pack;
	
	pack.flag = ACK_LONGMESSAGE;
	_tcscpy_s(pack.msg, _countof(pack.msg), msg);

	return pack;
}

pack_FILELIST_ACK pack_FileList(TCHAR(*files)[40], int size)
{
	pack_FILELIST_ACK pack;

	pack.flag = ACK_FILELIST;
	pack.size = size;

	for (int i = 0; i < size; i++)
	{
		_tcscpy_s(pack.filelist[i], _countof(pack.filelist[i]), files[i]);
	}
	return pack;
}