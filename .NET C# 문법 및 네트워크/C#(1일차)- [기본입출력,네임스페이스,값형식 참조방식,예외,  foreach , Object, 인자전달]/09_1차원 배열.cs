using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
[배열] 참조형 타입!!!
 */ 
namespace _1004
{
    internal class Class8
    {
        //1차원 배열
        static void Exam1()
        {
            int[] arr1;             //주소저장 공간!
            arr1 = new int[5];      //new 힙메모리에 정수 5개 저장할 공간 생성
                                    //생성된 주소를 반환

            int[] arr2 = new int[5];
            int[] arr3 = new int[] { 1, 2, 3, 4, 5 };
            int[] arr4 = new int[5] { 1, 2, 3, 4, 5 };

            //C언어에서 사용한 방법 그대로 
            Console.WriteLine(arr4[0]); //1

            //순회(전통적인 for)
            for(int i=0; i< arr1.Length; i++)
            {
                Console.Write(arr1[i] + "\t");      // 0 0 0 0 0 
            }
            Console.WriteLine();

            //순회(foreach)
            foreach(int value in arr3)
            {
                Console.Write(value + "\t");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Exam1();
        }
    }
}
