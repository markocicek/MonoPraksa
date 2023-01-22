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
        List<Customer> GetCustomerList();
        Customer FindCustomer(Guid id);
        void AddCustomer(Customer customer);
        bool UpdateCustomer(Guid id, Customer updatedCustomer);
        bool DeleteCustomer(Guid id);
    }
}
