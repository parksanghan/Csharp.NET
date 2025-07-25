// Dequeue.cpp : 이 파일에는 'main' 함수가 포함됩니다. 거기서 프로그램 실행이 시작되고 종료됩니다.
//

#include <iostream>
class Node {
public:
    int data;
    Node* prev;
    Node* next;
    Node(int val):data(val),prev(nullptr),next(nullptr){}
};
class Dequeue {
private:
    Node* tail;
public:
    Dequeue():tail(nullptr){}
    ~Dequeue() { clear(); }

    bool is_empty() {
        return tail == nullptr;
    }
    void push_front(int val) {
        Node* newNode = new Node(val);
        if (is_empty) {
            newNode->next = newNode;
            newNode->prev = newNode;
            tail = newNode;
        }
        else {
            Node* head = tail->next;
            newNode->next = head;
            newNode->prev = tail;
            head->prev = newNode;
            tail->next = newNode;
        }
    }
    void push_back(int val) {
        //20 -> 30 -> 10 -> 20  (순환)
        // -> tail->tail->next;  = 30 (tail)
        push_front(val);
        tail = tail->next;
    }
    bool pop_front(int &out) {
        if (is_empty())return false;
        Node* head = tail->next;
        out = head->data;
        if (tail == tail->next) {
            // 1개인 경우 
            delete tail;
            tail = nullptr;
        }
        else {
            Node* newHead = head->next;
            tail->next= newHead;
            newHead->prev = tail;
            delete head;
        }
        return true;

    }

    void clear() {

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
