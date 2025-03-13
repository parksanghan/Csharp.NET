//shape.cpp

#include "std.h"

SHAPE shape_CreateShape(int type, POINT pt, int size, COLORREF color)
{
	SHAPE sp;

	sp.type = type;
	sp.pt = pt;
	sp.size = size;
	sp.color = color;

	return sp;
}