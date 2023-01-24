using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Common
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployeeListAsync();
        Task<Employee> FindEmployeeAsync(Guid id);
        Task<Employee> GetAllCustomersFromEmployeeAsync(Guid id);
        Task AddEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Guid id, Employee updatedEmployee);
        Task<bool> DeleteEmployeeAsync(Guid id);
    }
}
