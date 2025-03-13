//shapeSet.h
#pragma once

void ShapeSet_GetControlHandle(HWND hDlg);
void ShapeSet_Init(SHAPE* psh);
void ShapeSet_Print(HWND hDlg, const SHAPE & psh);

void ShapeSet_UpdateType(SHAPE* psh);
void ShapeSet_UpdateSize(SHAPE* psh);
void ShapeSet_UpdateColor(HWND hDlg, SHAPE* psh);

//내부용 함수
int ss_TypeToComboBoxIdx(int type);
int ss_SizeToComboBoxIdx(int size);
void ss_PictureColorPrint(HWND hDlg, COLORREF color);