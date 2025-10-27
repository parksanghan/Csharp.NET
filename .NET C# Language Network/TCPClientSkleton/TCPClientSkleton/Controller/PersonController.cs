using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Data; 
namespace ConsoleApp3.Controller
{
    internal class PersonController:IDisposable
    {
        private readonly List<Person> persons;
        public void Dispose()
        {
            persons.Clear();
            
        }
        public PersonController()
        {
            persons = new List<Person>();
        }   
        public void AddPerson(Person person)
        {
            persons.Add(person);
        }
        public void RemovePerson(Person person)
        {
            persons.Remove(person);
        }
        public void UpdatePerson(int id, Person updatedPerson)
        {
            var person = persons.FirstOrDefault(p => p.Id == id);
            if (person != null)
            {
                person.Name = updatedPerson.Name;
                person.Age = updatedPerson.Age;
            }
        }
        public void ClearPersons()
        {
            persons.Clear();
        }
        public List<Person> GetAllPersons()
        {
            return persons;
        }
        public Person? GetPersonById(int id)
        {
            return persons.FirstOrDefault(p => p.Id == id);
        }
        public List<Person> GetPersonByName(string name)
        {
            return persons.Where(p=>p.Name.Equals(name)).ToList();      
        }
        public List<Person> GetPersonByAge(int age)
        {
            return persons.Where(p => p.Age.Equals(age)).ToList();
        }
    }
}
