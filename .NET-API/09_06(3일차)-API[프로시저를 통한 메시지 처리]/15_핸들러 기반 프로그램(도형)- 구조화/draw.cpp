//draw.cpp

#include "std.h"

void draw_InfoPrint(HDC hdc, POINTS pt, POINTS cur_pt)
{
	TCHAR buf[50];
	wsprintf(buf, TEXT("ÇöÀç ÁÂÇ¥ -> %d:%d"), pt.x, pt.y);
	TextOut(hdc, 10, 10, buf, (int)_tcslen(buf));

	wsprintf(buf, TEXT("Å¬¸¯ ÁÂÇ¥ -> %d:%d"), cur_pt.x, cur_pt.y);
	TextOut(hdc, 10, 30, buf, (int)_tcslen(buf));
}

void draw_ShapePrint(HDC hdc, const vector<SHAPE>& shapes)
{
	for (int i = 0; i < shapes.size(); i++)
	{
		SHAPE  sh = shapes[i];
		POINTS pt = sh.pt;

		HBRUSH br = CreateSolidBrush(sh.brush_color);
		HBRUSH oldbr = (HBRUSH)SelectObject(hdc, br);

		Rectangle(hdc, pt.x, pt.y, pt.x + 50, pt.y + 50);

		DeleteObject(SelectObject(hdc, oldbr));
	}
}