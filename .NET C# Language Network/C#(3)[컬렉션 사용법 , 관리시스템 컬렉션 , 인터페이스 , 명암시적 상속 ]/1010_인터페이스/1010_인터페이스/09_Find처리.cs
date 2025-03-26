using System;
using System.Collections.Generic;


namespace _1010_인터페이스
{
    class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return Name + "\t" + Price;
        }
    }

    internal class _09_Find
    {
        #region Find
        //람다식(함수의 구조)
        //[  ( 인자  ) => ( 연산 )  ]
        /*
         int Find( )
         {
            foreach(int n in numbers)   //*****
            {
               if(n == 3)   //*****
                  return 3;
            }
         }
         */
        public static void Exam1()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            int foundNumber = numbers.Find(n => n == 3);

            Console.WriteLine(foundNumber); // Output: 3
        }

        static bool IsEven(int n)
        {
            return n % 2 == 0;
        }

        public static void Exam2()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            //Find : numbers를 열거(1...5) -> 열거한 값을 IsEven 전달
            //가장 처음 true발생시키는 값을 반환함
            //방법1)함수 사용
            //int evenNumber = numbers.Find(IsEven);
            //방법2)람다식 사용
            int evenNumber = numbers.Find(n => n % 2 == 0);

            Console.WriteLine(evenNumber); // Output: 2
        }

        //Product 클래스 활용
        static bool IsCheaperThan(Product p, decimal price)
        {
            return p.Price < price;
        }

        public static void Exam3()
        {
            Product p1 = new Product(); //생성자 호출->객체 초기화
            Product p2 = new Product() //디폴트생성자 호출->속성객체 초기화
            {
                Name = "홍길동", Price = 10
            };

            List<Product> products = new List<Product>
            {
                new Product { Name = "apple", Price = 0.50m },
                new Product { Name = "banana", Price = 0.25m },
                new Product { Name = "cherry", Price = 0.75m },
                new Product { Name = "date", Price = 1.00m },
                new Product { Name = "elderberry", Price = 0.90m },
            };
            products.Add(new Product { Name = "test", Price = 0.90m });

            //Find : 6번 순회
            //Product product = products.Find(p => IsCheaperThan(p, 0.40m));
            Product product = products.Find(p => p.Price < 0.40m);

            if (product == null)
            {
                Console.WriteLine("No product found with a price lower than 0.80");
            }
            else
            {
                Console.WriteLine(product.Name); // Output: apple
            }
        }
        #endregion

        #region FindIndex
        public static void Exam4()
        {
            List<Product> products = new List<Product>
            {
                new Product { Name = "apple", Price = 0.50m },
                new Product { Name = "banana", Price = 0.25m },
                new Product { Name = "cherry", Price = 0.75m },
                new Product { Name = "date", Price = 1.00m },
                new Product { Name = "elderberry", Price = 0.90m },
            };

            //int index = products.FindIndex(f => f.Name == "date");

            //시작문자가 c인것을 찾는다.
            int index = products.FindIndex(f => f.Name.StartsWith("e"));

            if (index == -1)
            {
                Console.WriteLine("No fruit starting with 'c' found");
            }
            else
            {
                Console.WriteLine(products[index]); // Output: date
            }
        }
        #endregion

        #region FindAll
        public static void Exam5()
        {
            List<int> numbers = new List<int> { 1, 3, 5, 7, 9 };

            List<int> greaterThan5 = numbers.FindAll(n => n > 5);

            foreach (int number in greaterThan5)
            {
                Console.WriteLine(number); // Output: 7 9
            }
        }
        
        public static void Exam6()
        {
            List<Product> products = new List<Product>
            {
                new Product { Name = "apple", Price = 0.50m },
                new Product { Name = "banana", Price = 0.25m },
                new Product { Name = "cherry", Price = 0.75m },
                new Product { Name = "date", Price = 1.00m },
                new Product { Name = "elderberry", Price = 0.90m },
            };

            List<Product> find_p = products.FindAll(n => n.Price > 0.50m);

            foreach (Product p in find_p)
            {
                Console.WriteLine(p); // Output: 7 9
            }
        }
        
        #endregion 
    }
}
