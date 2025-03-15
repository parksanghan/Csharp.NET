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

        //배열 Sort
        static void Exam2()
        {
            int[] arr = new int[4] { 5, 8, 2, 1 };
            Console.WriteLine("요소의 개수:{0}", arr.Length);
            foreach (int elem in arr)
            {
                Console.WriteLine(elem.ToString());
            }
            Array.Sort(arr);
            Console.WriteLine("정렬 후");
            foreach (int elem in arr)
            {
                Console.WriteLine(elem.ToString());
            }
        }

        //2차원 배열
        static void Exam3()
        {
            int[,] arr1;
            arr1 = new int[2, 3];
            arr1 = new int[,] { { 1, 2, 3 }, { 4, 5, 6, } };

            int[,] arr3 = new int[,] { { 1, 2 }, { 4, 5 }, { 5, 6 } };
            int[,] arr4 = new int[3, 2] { { 1, 2 }, { 4, 5 }, { 5, 6 } };

            Console.WriteLine("arr1 순회");
            Console.WriteLine("차수 : {0} , 요소개수 : {1}", arr1.Rank, arr1.Length);
            for(int i=0; i<2; i++)
            {
                for(int j=0; j<3; j++)
                {
                    Console.Write(arr1[i,j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            foreach(int value in arr1)
            {
                Console.Write(value + "\t");
            }
            Console.WriteLine();

        }

        //가변배열 - foreach 중첩해서 순회!
        static void Exam4()
        {
            //[1][2][3]
            //[4][5]
            int[][] array = new int[][] { 
                new int[] { 1, 2, 3 }, 
                new int[] { 4, 5 } 
            };

            foreach (int[] elemArray in array)
            {
                foreach (int elem in elemArray)
                {
                    Console.Write(elem.ToString());
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Exam4();
        }
    }
}
