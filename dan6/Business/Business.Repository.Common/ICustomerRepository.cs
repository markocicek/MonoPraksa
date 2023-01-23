using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.Common
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomerListAsync();
        Task<Customer> FindCustomerAsync(Guid id);
        Task AddCustomerAsync(Guid id, Customer customer);
        Task<bool> UpdateCustomerAsync(Guid id, Customer updatedCustomer);
        Task<bool> DeleteCustomerAsync(Guid id);

    }
}
