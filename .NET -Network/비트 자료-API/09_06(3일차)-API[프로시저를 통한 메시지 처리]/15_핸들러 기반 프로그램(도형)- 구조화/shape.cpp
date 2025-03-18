//shape.cpp
#include "std.h"

SHAPE shape_CreateShape(POINTS pt, COLORREF color)
{
	SHAPE sh;

	sh.pt.x = pt.x;
	sh.pt.y = pt.y;
	sh.brush_color = color;

	return sh;
}