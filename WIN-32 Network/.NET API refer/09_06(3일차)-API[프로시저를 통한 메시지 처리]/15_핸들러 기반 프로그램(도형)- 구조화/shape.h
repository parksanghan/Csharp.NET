//shape.h
#pragma once

typedef struct tagSHAPE
{
	POINTS	 pt;
	COLORREF brush_color;
}SHAPE;

SHAPE shape_CreateShape(POINTS pt, COLORREF color);