//shapeSet.cpp

#include "std.h"

HWND hshapeSet_Combo_Type;
HWND hshapeSet_Edit_PointX;
HWND hshapeSet_Edit_PointY;
HWND hshapeSet_Combo_Size;
HWND hshapeSet_Picture_ColorPrint;

//�ڵ� ȹ�� , �޺��ڽ� �ʱ�ȭ
void ShapeSet_GetControlHandle(HWND hDlg)
{
	hshapeSet_Combo_Type			= GetDlgItem(hDlg, IDC_SET_COMBO_TYPE);
	hshapeSet_Edit_PointX			= GetDlgItem(hDlg, IDC_SET_EDIT_X);
	hshapeSet_Edit_PointY			= GetDlgItem(hDlg, IDC_SET_EDIT_Y);
	hshapeSet_Combo_Size			= GetDlgItem(hDlg, IDC_SET_COMBO_SIZE);
	hshapeSet_Picture_ColorPrint	= GetDlgItem(hDlg, IDC_SET_STATIC_COLOR);

	SendMessage(hshapeSet_Combo_Type, CB_ADDSTRING, 0, (LPARAM)TEXT("�簢��"));
	SendMessage(hshapeSet_Combo_Type, CB_ADDSTRING, 0, (LPARAM)TEXT("Ÿ��"));
	SendMessage(hshapeSet_Combo_Type, CB_ADDSTRING, 0, (LPARAM)TEXT("�ﰢ��"));

	SendMessage(hshapeSet_Combo_Size, CB_ADDSTRING, 0, (LPARAM)TEXT("25"));
	SendMessage(hshapeSet_Combo_Size, CB_ADDSTRING, 0, (LPARAM)TEXT("50"));
	SendMessage(hshapeSet_Combo_Size, CB_ADDSTRING, 0, (LPARAM)TEXT("75"));
}

//������ �ʱ�ȭ
void ShapeSet_Init(SHAPE* psh)
{
	psh->type = 1;  
	psh->pt.x = 100;
	psh->pt.y = 200;
	psh->size = 50;
	psh->color = RGB(255, 0, 0);
}

//�����͸� ȭ�鿡 ���
void ShapeSet_Print(HWND hDlg, const SHAPE& psh)
{
	//SendMessage(hshapeSet_Combo_Type, CB_SETCURSEL, psh.type, 0);
	SendMessage(hshapeSet_Combo_Type, CB_SETCURSEL, ss_TypeToComboBoxIdx(psh.type), 0);

	TCHAR buf[50];
	wsprintf(buf, TEXT("%d"), psh.pt.x);
	SetWindowText(hshapeSet_Edit_PointX, buf);
	wsprintf(buf, TEXT("%d"), psh.pt.y);
	SetWindowText(hshapeSet_Edit_PointY, buf);

	//SendMessage(hshapeSet_Combo_Size, CB_SETCURSEL, psh.size, 0);
	SendMessage(hshapeSet_Combo_Size, CB_SETCURSEL, ss_SizeToComboBoxIdx(psh.size), 0);

	//�ڵ���� ����Ʈ��Ʈ�� �ٷ��
	SetDlgItemInt(hDlg, IDC_SET_EDIT_R, GetRValue(psh.color), 0);
	SetDlgItemInt(hDlg, IDC_SET_EDIT_G, GetGValue(psh.color), 0);
	SetDlgItemInt(hDlg, IDC_SET_EDIT_B, GetBValue(psh.color), 0);
}

int ss_TypeToComboBoxIdx(int type)
{
	return type - 1;
}

int ss_SizeToComboBoxIdx(int size)
{
	if (size == 25)		 return 0;
	else if (size == 50) return 1;
	else if (size == 75) return 2;
	else                 return -1;
}

void ShapeSet_UpdateType(SHAPE* psh)
{
	//1.������ �ε��� ȹ��
	int idx = (int)SendMessage(hshapeSet_Combo_Type, CB_GETCURSEL, 0, 0);

	//2. �ε����� �̿��Ͽ� ������ ����
	psh->type = idx + 1; 
}

void ShapeSet_UpdateSize(SHAPE* psh)
{
	//1.������ �ε��� ȹ��
	int idx = (int)SendMessage(hshapeSet_Combo_Size, CB_GETCURSEL, 0, 0);

	//2. �ε����� �̿��Ͽ� ������ ����
	if (idx == 0)		psh->size = 25;
	else if (idx == 1)	psh->size = 50;
	else if(idx == 2)	psh->size = 75;
}

void ShapeSet_UpdateColor(HWND hDlg, SHAPE* psh)
{
	int r = GetDlgItemInt(hDlg, IDC_SET_EDIT_R,0,0);
	int g = GetDlgItemInt(hDlg, IDC_SET_EDIT_G,0,0);
	int b = GetDlgItemInt(hDlg, IDC_SET_EDIT_B,0,0);
	psh->color = RGB(r, g, b);

	ss_PictureColorPrint(hDlg, psh->color);
}

void ss_PictureColorPrint(HWND hDlg, COLORREF color)
{
	HDC hdc = GetDC(hshapeSet_Picture_ColorPrint);

	RECT rc;
	GetClientRect(hshapeSet_Picture_ColorPrint, &rc);

	HBRUSH br = CreateSolidBrush(color);
	HBRUSH old = (HBRUSH)SelectObject(hdc, br);
	FillRect(hdc, &rc, br);
	DeleteObject(SelectObject(hdc, old));

	ReleaseDC(hshapeSet_Picture_ColorPrint, hdc);
}