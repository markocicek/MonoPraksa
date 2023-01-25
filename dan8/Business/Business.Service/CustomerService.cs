using Business.Service.Common;
using Business.Repository;
using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repository.Common;

namespace Business.Service
{
    public class CustomerService : ICustomerService
    {
        protected ICustomerRepository CustomerRepository { get; private set; }
        public CustomerService(ICustomerRepository customerRepository)
        {
            this.CustomerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetCustomerListAsync()
        {
            return await CustomerRepository.GetCustomerListAsync();
        }
        public async Task<Customer> FindCustomerAsync(Guid id)
        {
            return await CustomerRepository.FindCustomerAsync(id);
        }
        public async Task AddCustomerAsync(Customer customer)
        {
            Guid id = Guid.NewGuid();
            await CustomerRepository.AddCustomerAsync(id, customer);
        }
        public async Task<bool> UpdateCustomerAsync(Guid id, Customer updatedCustomer)
        {
            return await CustomerRepository.UpdateCustomerAsync(id, updatedCustomer);
        }
        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            return await CustomerRepository.DeleteCustomerAsync(id);
        }
    }
}
