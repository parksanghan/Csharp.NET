//네임스페이스명
/*
C++ namespace std;
cout 사용방식
1) using std::cout;
   cout;

2) std::cout;

C# 
1) using System;
   Console;  //클래스명

2) System.Console;
*/


//내가 만든 클래스도 이름이 있는 공간에 구현하는 것을 권장한다.
/*
[c/c++언어] 파일단위로 코드 관리!
[C#언어]    이름이 있는 공간 단위로 코드 관리!
            다른 소스파일로 구성되어 있어도 namespace명이 동일하면 동일 공간!

            namespace명이 다른 공간에 있는 키워드를 사용하려면?
            using namespace명;
 */
using System;

namespace _1004
{
    /// <summary>
    /// 내가 만든 클래스
    /// </summary>
    internal class Class1
    {
        /// <summary>
        /// 인텔리센스 테스트용 함수
        /// </summary>
        /// <param name="num">테스트용 인자</param>
        static void Fun1(int num)
        {
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            System.Console.WriteLine("Hello, World!");            
        }
    }
}
