using Business.Service.Common;
using Business.Repository;
using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public class CustomerService : ICustomerService
    {
        CustomerRepository customerRepository = new CustomerRepository();
        public async Task<List<Customer>> GetCustomerListAsync()
        {
            return await customerRepository.GetCustomerListAsync();
        }
        public async Task<Customer> FindCustomerAsync(Guid id)
        {
            return await customerRepository.FindCustomerAsync(id);
        }
        public async void AddCustomerAsync(Customer customer)
        {
            Guid id = Guid.NewGuid();
            customerRepository.AddCustomerAsync(id, customer);
        }
        public async Task<bool> UpdateCustomerAsync(Guid id, Customer updatedCustomer)
        {
            return await customerRepository.UpdateCustomerAsync(id, updatedCustomer);
        }
        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            return await customerRepository.DeleteCustomerAsync(id);
        }
    }
}
