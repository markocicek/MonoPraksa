using Business.Model;
using Business.Repository;
using Business.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public class EmployeeService : IEmployeeService
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();

        public async Task<List<Employee>> GetEmployeeListAsync() 
        {
            return await employeeRepository.GetEmployeeListAsync();
        }
        public async Task<Employee> FindEmployeeAsync(Guid id)
        {
            return await employeeRepository.FindEmployeeAsync(id);
        }
        public async Task<Employee> GetAllCustomersFromEmployeeAsync(Guid id)
        {
            return await employeeRepository.GetAllCustomersFromEmployeeAsync(id);
        }
        public async Task AddEmployeeAsync(Employee employee)
        {
            Guid id = Guid.NewGuid();
            await employeeRepository.AddEmployeeAsync(id, employee);
        }
        public async Task<bool> UpdateEmployeeAsync(Guid id, Employee updatedEmployee)
        {
            return await employeeRepository.UpdateEmployeeAsync(id, updatedEmployee);
        }
        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            return await employeeRepository.DeleteEmployeeAsync(id);
        }
    }
}
