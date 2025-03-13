//shapeCur.cpp

#include "std.h"

SHAPE g_curShape;

HWND hshapeCur_Combo_Type;
HWND hshapeCur_Combo_Size;
HWND hshapeCur_Picture_ColorPrint;

//�ڵ� ȹ�� , �޺��ڽ� �ʱ�ȭ
void ShapeCur_GetControlHandle(HWND hDlg)
{
	hshapeCur_Combo_Type = GetDlgItem(hDlg, IDC_SET_COMBO_TYPE);
	hshapeCur_Combo_Size = GetDlgItem(hDlg, IDC_SET_COMBO_SIZE);
	hshapeCur_Picture_ColorPrint = GetDlgItem(hDlg, IDC_SET_STATIC_COLOR);

	SendMessage(hshapeCur_Combo_Type, CB_ADDSTRING, 0, (LPARAM)TEXT("�簢��"));
	SendMessage(hshapeCur_Combo_Type, CB_ADDSTRING, 0, (LPARAM)TEXT("Ÿ��"));
	SendMessage(hshapeCur_Combo_Type, CB_ADDSTRING, 0, (LPARAM)TEXT("�ﰢ��"));

	SendMessage(hshapeCur_Combo_Size, CB_ADDSTRING, 0, (LPARAM)TEXT("25"));
	SendMessage(hshapeCur_Combo_Size, CB_ADDSTRING, 0, (LPARAM)TEXT("50"));
	SendMessage(hshapeCur_Combo_Size, CB_ADDSTRING, 0, (LPARAM)TEXT("75"));
}

//������ �ʱ�ȭ
void ShapeCur_Init()
{
	g_curShape.type = -1;
	g_curShape.pt.x = 0;
	g_curShape.pt.y = 0;
	g_curShape.size = -1;
	g_curShape.color = RGB(0, 0, 0);
}

//�����͸� ȭ�鿡 ���
void ShapeCur_Print(HWND hDlg)
{
	SendMessage(hshapeCur_Combo_Type, CB_SETCURSEL, ss_TypeToComboBoxIdx(g_curShape.type), 0);
	
	SetDlgItemInt(hDlg, IDC_CUR_EDIT_X, g_curShape.pt.x, 0);
	SetDlgItemInt(hDlg, IDC_CUR_EDIT_Y, g_curShape.pt.y, 0);
	
	SendMessage(hshapeCur_Combo_Size, CB_SETCURSEL, ss_SizeToComboBoxIdx(g_curShape.size), 0);

	//�ڵ���� ����Ʈ��Ʈ�� �ٷ��
	SetDlgItemInt(hDlg, IDC_CUR_EDIT_R, GetRValue(g_curShape.color), 0);
	SetDlgItemInt(hDlg, IDC_CUR_EDIT_G, GetGValue(g_curShape.color), 0);
	SetDlgItemInt(hDlg, IDC_CUR_EDIT_B, GetBValue(g_curShape.color), 0);
}

int sc_TypeToComboBoxIdx(int type)
{
	return type - 1;
}

int sc_SizeToComboBoxIdx(int size)
{
	if (size == 25)		 return 0;
	else if (size == 50) return 1;
	else if (size == 75) return 2;
	else                 return -1;
}

void ShapeCur_UpdateType(SHAPE* psh)
{
	//1.������ �ε��� ȹ��
	int idx = (int)SendMessage(hshapeCur_Combo_Type, CB_GETCURSEL, 0, 0);

	//2. �ε����� �̿��Ͽ� ������ ����
	psh->type = idx + 1;
}

void ShapeCur_UpdateSize(SHAPE* psh)
{
	//1.������ �ε��� ȹ��
	int idx = (int)SendMessage(hshapeCur_Combo_Size, CB_GETCURSEL, 0, 0);

	//2. �ε����� �̿��Ͽ� ������ ����
	if (idx == 0)		psh->size = 25;
	else if (idx == 1)	psh->size = 50;
	else if (idx == 2)	psh->size = 75;
}