using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Data
{
    internal class Person
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
           
        public Person(int id, string name , int age)
        {

            this.Id = id;
            this.Name = name;
            this.Age = age;
        }

        public override bool Equals(object? obj)
        {

            return base.Equals(obj);
        }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Age: {Age}";
        }

    }
}
