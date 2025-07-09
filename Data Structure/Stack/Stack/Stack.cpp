// Stack.cpp : 이 파일에는 'main' 함수가 포함됩니다. 거기서 프로그램 실행이 시작되고 종료됩니다.
//

#include <iostream>
#define MAX 100
class Stack {
private:
    int top;
    int arr[MAX];
public:
    Stack() {
        top = -1;
    }
    int capacity() const {
        return MAX;
    }
    int t_idx() const {
        return top;
    }
    int size() const {
        return top + 1;
    }
    void clear() {
        top = -1;
    }
    bool is_empty()const {
        return top == -1;
    }
    bool is_full() const {
        return top == MAX - 1;
    }
    bool t_idx_val(int& out) const {
        if (is_empty() == true) return  false;
        out = arr[top];
        return true;
    }
    bool push(int val) {
        if ( is_full()== true)return false;
        top++;
        arr[top] = val;
        return true;
    }
    bool pop(int &out) {
        if (is_empty() == true) return false;
        out = arr[top];
        arr[top] = 0;

        top--;
        std::cout << "d" << std::endl;
        return true;
    }
    bool peek(int &out)  {
        if(is_empty())return false;
        out = arr[top];
        return true;
    }
    void print_all() const {
        std::cout << "top = " + top << std::endl;
        for (size_t i = 0; i < top; i++)
        {
            std::cout << arr[i] << std::endl;
            
        }

    }

 
    
};

 


int main()
{
    printf("Dd");
   
}

 