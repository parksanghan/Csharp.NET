// CircleSigleLinkedList.cpp : 이 파일에는 'main' 함수가 포함됩니다. 거기서 프로그램 실행이 시작되고 종료됩니다.
//
// 원형 구조
#include <iostream>
class Node {
public:

    int data;
    Node* next;
    Node(int val ):data(val), next(nullptr){}
};
class CircleLinkedList {
private:
    Node* tail;
 
public:
    CircleLinkedList() :tail(nullptr){
    }
    ~CircleLinkedList() {

    }
    bool is_empty() const {
        return tail == nullptr;
    }
    void insert_front(int val) {
        Node* newNode = new Node(val);
        if (is_empty()) {
            newNode->next = newNode;
            tail = newNode;
        }
        else {
            newNode->next = tail->next; //tail->next = head임
            tail->next = newNode;  // newNode -> next => head  / tail->next =>newNode
        }
        // tail->next(newNode)=> 초기(head)
    }
    // 10(head)-> 20 ->30(tail)
    void insert_back(int val) { //val - 5
        insert_front(val);
        //10 -> 20 -30->10
        // => val =5 이면  tail->next 
        tail = tail->next; 
        // 이전에 연결 끊어버리고  ->10으로 연결 
        // 최종 10->20->30->newNode(5)
        // 매번 front는 tail뒤에만 추가 
    }
    void print_all()const {
        if (is_empty()) { std::cout << "EMPTY" << std::endl; return; }
        Node* curr = tail->next;//head;
        while (curr != tail) {
            std::cout << curr->data << std::endl;
            curr = curr->next;
        }
        std::cout << curr->data << std::endl;
        
    }
    void clear() {
        if (is_empty())return;
        Node* curr = tail->next;//head
        while (curr!= tail) {
            std::cout << curr->data << std::endl;
            Node* nextNode = curr->next;
            delete curr;
            curr = nextNode;
        }
        delete tail;
        tail = nullptr;
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
