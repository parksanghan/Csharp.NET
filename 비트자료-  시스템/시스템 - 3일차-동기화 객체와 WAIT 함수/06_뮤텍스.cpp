//06_���ؽ�.cpp
/*
���μ����� ���μ��� �� ����ȭ
2�� ����!
ù��° ���� : CreateMutex("mutex")�̸��� ��ü ����
�ι�° ���� : CreateMutex("mutex")�̸��� ��ü �����Ϸ��� �ϴ�
              �̹� "mutex"��� �̸��� ��ü�� �����Ǿ� �ִ�.
			  -> OpenMutex�� �����ϰ� �ȴ�.
              
*/

#include <windows.h> 
#include <iostream> 
using namespace std; 

int main()
{ 
	// ���ؽ� ���� 
	HANDLE hMutex = CreateMutex(NULL, // ���ȼӼ�
								FALSE, // ������ ���ؽ� ���� ���� 
								TEXT("mutex")); // �̸� 			
	
	cout << "���ý��� ��ٸ��� �ִ�." << endl; 
	
	//hMutex : Signal ���¸� ���
	//����, ���� Mutex�� �����ϰ� �ִٸ�(hMutex : Non Signal��Ȳ��)
	//����� �� �ִ�. 
	DWORD d = WaitForSingleObject(hMutex, INFINITE); // ��� 
	if( d == WAIT_TIMEOUT) 
		MessageBox(NULL, TEXT("WAIT_TIMEOUT"), TEXT(""), MB_OK); 
	else if( d ==WAIT_OBJECT_0) 
		MessageBox(NULL, TEXT("WAIT_OBJECT_0"), TEXT(""), MB_OK); 
	else if( d == WAIT_ABANDONED_0) 
		MessageBox(NULL, TEXT("WAIT_ABANDONED_0"), TEXT(""), MB_OK); 
	
	cout << "���ý� ȹ��" << endl; 
	
	MessageBox(NULL, TEXT("���ý��� ���´�.") , TEXT(""), MB_OK); 
	
	//ReleaseMutex(hMutex); 

	return 0;
}