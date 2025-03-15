using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Example.MainWindow
{
    public class Person
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return Name + "\t" + Phone+ "\t" + Age;
        }

    }
    public class People : List<Person>
    {
        public People()
        {
            Add(new Person() { Name = "홍길동", Phone = "010-1111-1234", Age = 10 });
            Add(new Person() { Name = "일지매", Phone = "010-2222-1234", Age =  30 });
            Add(new Person() { Name = "임꺽정", Phone = "010-3333-1234" ,Age = 50 });
        }
    }
}
