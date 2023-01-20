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
        public List<Customer> GetCustomerList()
        {
            return customerRepository.GetCustomerList();
        }
        public Customer FindCustomer(Guid id)
        {
            return customerRepository.FindCustomer(id);
        }
        public void AddCustomer(Customer customer)
        {
            customerRepository.AddCustomer(customer);
        }
        public bool UpdateCustomer(Guid id, Customer updatedCustomer)
        {
            return customerRepository.UpdateCustomer(id, updatedCustomer);
        }
        public bool DeleteCustomer(Guid id)
        {
            return customerRepository.DeleteCustomer(id);
        }
    }
}
