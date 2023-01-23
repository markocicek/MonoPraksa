using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Common
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomerListAsync();
        Task<Customer> FindCustomerAsync(Guid id);
        void AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Guid id, Customer updatedCustomer);
        Task<bool> DeleteCustomerAsync(Guid id);
    }
}
