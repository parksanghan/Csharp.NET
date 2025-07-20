// SingleLinkedList.cpp : 이 파일에는 'main' 함수가 포함됩니다. 거기서 프로그램 실행이 시작되고 종료됩니다.
//

#include <iostream>

class Node {
public:
    int data;
    Node* next;
    Node(int val) {
        data = val;
        next = nullptr;
    }
    // 같음
    Node(int val) :data(val), next(nullptr){}
};
// head  -> insert   => newNode - > head
// -> head(newNode) -> prevHeadNode
class LinkedList {
private:
    Node* head;
public:
    LinkedList(): head(nullptr){}
    ~LinkedList(){}
    bool is_empty() const {
        return head == nullptr;
    }
    void insert_front(int val) {
        Node* newNode = new Node(val);
        newNode->next = head; // newNode -> head
        head = newNode; // head(newNode) -> prevhead
    }
    bool delete_value(int val) {
        if (is_empty()) return false;
        if (head->data ==val) // 바로 찾은 경우
        {
            Node* temp = head;
            head = head->next;
            delete temp;
            return true;
        }
        // 없다면 게속 진입
        // 가장 왼쪽이 head 
        // head -> next -> next 진입
        Node* prev = head;
        Node* curr = head->next;
        while (curr != nullptr) {
            if (curr->data == val) {
                prev->next = curr->next;
                delete curr;
                 return true;
            }
            prev = curr;
            curr = curr->next;
         
        }
        return false;
    }
    bool search(int val) {
        Node* curr = head;
        while (curr != nullptr) {
            if (curr->data == val)return true;
            curr = curr->next;
        }
        return false;
    }
    int size()const {
        int count = 0;
        Node* curr = head;
        while (curr != nullptr) {
            ++count;
            curr = curr->next;
        }
        return count;
    }
    void clear()  {
        Node* curr = head;
        while (curr != nullptr) {
            Node* temp = curr;
            curr = curr->next;
            delete temp;
        }
        head = nullptr;
    }
    //head -> next1 ->next2 를  null -> next 2 ->next1 -> head   
    void reverse() {
        Node* prev = nullptr;
        Node* next = nullptr;
        Node* curr = head;
        // head = 10 -> 20 -> 30 
        while (curr != nullptr) {
            next = curr->next; // 다음 노드 복사  저장  next = 10
            curr->next = prev; //  curr 의 다음 노드 반대로 설정 curr-> nullptr
            prev = curr; // 이전 노드  현재로 이동 prev = curr  prev =head;
            curr = next;  // 다음 노드 이동 
        }
        // curr(head) 가 이동할때 마다 앞에노드가 없어지는 구조이며 
        // prev가 처음 헤드이며  curr 이 이동할때마다 반전으로 게속 추가됨.
        head = prev;

    }
    void print_all() {
        Node* curr = head;
        while (curr!= nullptr)
        {
            std::cout << curr->data << std::endl;
            curr = curr->next;
        }
        std::cout << "Nullptr" << std::endl;
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
