using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigatePage.Data
{
    internal class MyData
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Type? Type { get; set; }

        public MyData(int id, string name , string details , Type type)
        {
            this.Id = id;
            this.Name = name;
            this.Description = details;
            this.Type = type;

        }
        public override string ToString()
        {

            return this.Id.ToString()+this.Name+this.Description+this.Type;
        }
    }
}
