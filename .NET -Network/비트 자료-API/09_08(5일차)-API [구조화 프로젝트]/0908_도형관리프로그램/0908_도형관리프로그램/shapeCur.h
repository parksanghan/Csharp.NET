//shapeCur.h
#pragma once

void ShapeCur_GetControlHandle(HWND hDlg);
void ShapeCur_Init();
void ShapeCur_Print(HWND hDlg);

void ShapeCur_UpdateType();
void ShapeCur_UpdateSize();

//내부용 함수
int sc_TypeToComboBoxIdx(int type);
int sc_SizeToComboBoxIdx(int size);