// DoubleLinkedList.cpp : 이 파일에는 'main' 함수가 포함됩니다. 거기서 프로그램 실행이 시작되고 종료됩니다.
//

#include <iostream>
class Node {
public:
    int data;
    Node* next;
    Node* prev;
    Node(int val) : data(val), prev(nullptr), next(nullptr){}
};
class DoubleLinkedList {
private:
    Node* head;
    Node* tail;
public:
    DoubleLinkedList():head(nullptr), tail(nullptr){}
    ~DoubleLinkedList() {
    }
    bool is_empty()const {
        return head == nullptr;
    }

    void insert_front(int val) {
        //head → [10] ↔ [20] ↔ [30] → tail
        // 앞에 추가시  newNode-> next 는 head가 되고
        // 이전 head->prev는   
        Node* newNode = new Node(val);
        if (is_empty()) head = tail = newNode;
        else {
            newNode->next = head; //  새노드 -> 기존 헤드 노드 
            head->prev = newNode; // 이중 연결이니  -> 에서 < ->
            head = newNode; // 헤드 위치 변경 
        }
    }
    void insert_back(int  val) {
        Node* newNode = new Node(val);
        if (is_empty())
        {
            head= tail =newNode;
        }
        else {
            tail->next = newNode;
            newNode->prev = tail;
            tail = newNode;
        }
    }
    bool delete_front() {
        if (is_empty())return false;
        Node* temp = head;
        if (head == tail)head = tail = nullptr;// 1개만있는 경우
        else {
            head = head->next;
            head->prev = nullptr;
        }
        delete temp;
        return true;
    }
    bool delete_back() {
        if (is_empty())return false;
        Node* temp = tail;
        if (head == tail)head = tail = nullptr;
        else {
            tail = tail->prev;
            tail->next = nullptr;
        }
        delete temp;
        return true;
    }
    void printAll_forward()const {
        Node* curr = head;
        std::cout << "front" << std::endl;
        while (curr != nullptr) {
            std::cout << curr->data << std::endl;
            curr = curr->next;   
        }
    }
    void printAll_backward()const {
        Node* curr = tail;
        std::cout << "backward" << std::endl;
        while (curr != nullptr) {
            std::cout << curr->data << std::endl;
            curr = curr->prev;
        }
    }

};
int main()
{
    std::cout << "Hello World!\n";
}

// 프로그램 실행: <Ctrl+F5> 또는 [디버그] > [디버깅하지 않고 시작] 메뉴
// 프로그램 디버그: <F5> 키 또는 [디버그] > [디버깅 시작] 메뉴

// 시작을 위한 팁: 
//   1. [솔루션 탐색기] 창을 사용하여 파일을 추가/관리합니다.
//   2. [팀 탐색기] 창을 사용하여 소스 제어에 연결합니다.
//   3. [출력] 창을 사용하여 빌드 출력 및 기타 메시지를 확인합니다.
//   4. [오류 목록] 창을 사용하여 오류를 봅니다.
//   5. [프로젝트] > [새 항목 추가]로 이동하여 새 코드 파일을 만들거나, [프로젝트] > [기존 항목 추가]로 이동하여 기존 코드 파일을 프로젝트에 추가합니다.
//   6. 나중에 이 프로젝트를 다시 열려면 [파일] > [열기] > [프로젝트]로 이동하고 .sln 파일을 선택합니다.
