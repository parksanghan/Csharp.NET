//shape.h
#pragma once

typedef struct  tagSHAPE
{
	int type;			//Ÿ��(�簢��(1), Ÿ��(2), �ﰢ��(3))
	POINT pt;			//��ġ(��� �߾�)
	int size;			//ũ��(25, 50, 75)
	COLORREF color;		//����(��, ��, ��, ���á�)
}SHAPE;

SHAPE shape_CreateShape(int type, POINT pt, int size, COLORREF color);