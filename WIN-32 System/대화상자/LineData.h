//LineData.h
#pragma once

typedef struct tagLineData
{
	int yline;
	int xline;
	int penColor;

}LINEDATA;

//자식이 부모에게 적용 요청하는 사용자 정의 메시지
#define WM_APPLY	WM_USER + 100