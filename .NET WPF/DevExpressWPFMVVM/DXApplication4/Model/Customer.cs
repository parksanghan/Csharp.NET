
namespace DXApplication4.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Company { get; set; }
        public string? Email { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public Customer() { }
        public Customer(int id)
        {
            Id = id;
            FirstName = $"FirstName{id}";
            LastName = $"LastName{id}";
            Company = $"Company{id}";
            Email = $"{FirstName}.{LastName}@{Company}.com";
        }
    }
}
