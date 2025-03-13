//shape.h
#pragma once

typedef struct  tagSHAPE
{
	int type;			//타입(사각형(1), 타원(2), 삼각형(3))
	POINT pt;			//위치(출력 중앙)
	int size;			//크기(25, 50, 75)
	COLORREF color;		//색상(빨, 녹, 파, 선택…)
}SHAPE;

SHAPE shape_CreateShape(int type, POINT pt, int size, COLORREF color);