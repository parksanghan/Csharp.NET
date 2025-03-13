//12_���ؽ�
#include <windows.h>
#include <iostream>
using namespace std;

int main() 
{
	// ���ؽ� ����
	HANDLE	hMutex = CreateMutex(NULL,				// ���ȼӼ�
								FALSE,				// �ƹ��� ���ؽ� �������� ��������->SIGNAL
								TEXT("mutex"));		// �̸�(KEY)

	// ������ ture�϶� -> Signal �� nonsignal�� �ٲ۴�.
	cout << "���ý��� ��ٸ��� �ִ�." << endl;
	//ù��° ������ ���μ����� ���
	//why? ���� ���°� SIGNAL�����̱� ������
	//WaitForSingleObject�� ����ϴ� ����
	//1) hMutex�� �������� ����� ������(���μ���)�� ���� �ȴ�.
	//2) hMutex�� NONSIGNAL ���·� ����.
	DWORD d = WaitForSingleObject(hMutex, INFINITE);		// ���
	if (d == WAIT_TIMEOUT)
		MessageBox(NULL, TEXT("WAIT_TIMEOUT"), TEXT(""), MB_OK);
	else if (d == WAIT_OBJECT_0)
		MessageBox(NULL, TEXT("WAIT_OBJECT_0"), TEXT(""), MB_OK);
	else if (d == WAIT_ABANDONED_0)
		MessageBox(NULL, TEXT("WAIT_ABANDONED_0"), TEXT(""), MB_OK);
	cout << "���ý� ȹ��" << endl;


	MessageBox(NULL, TEXT("���ý��� ���´�."), TEXT(""), MB_OK);
	//���ؽ� �������� �ݳ�
	//hMenux�� SIGNAL
	//ReleaseMutex(hMutex);	
	
	return 0;
}
