//02_mycal.h
#pragma once

#ifdef DLL_SOURCE 
	#define DLLFUNC __declspec(dllexport) 
#else 
	#define DLLFUNC __declspec(dllimport) 
#endif

extern "C" DLLFUNC float myadd(int n1, int n2);
extern "C" DLLFUNC float mysub(int n1, int n2);
extern "C" DLLFUNC float mymul(int n1, int n2);
extern "C" DLLFUNC float mydiv(int n1, int n2);
