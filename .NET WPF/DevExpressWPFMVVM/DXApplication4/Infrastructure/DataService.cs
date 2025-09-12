using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DXApplication4.Model;

namespace DXApplication4.Infrastructure
{
    public class DataService : IDataService
    {
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            await Task.Delay(300);
            return Enumerable.Range(1, 50).Select(x => new Customer(x)).ToList();
        }
    }
    public interface IDataService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
    }
}
