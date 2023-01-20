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

        public List<Employee> GetEmployeeList() 
        {
            return employeeRepository.GetEmployeeList();
        }
        public Employee FindEmployee(Guid id)
        {
            return employeeRepository.FindEmployee(id);
        }
        public Employee GetAllCustomersFromEmployee(Guid id)
        {
            return employeeRepository.GetAllCustomersFromEmployee(id);
        }
        public void AddEmployee(Employee employee)
        {
            employeeRepository.AddEmployee(employee);
        }
        public bool UpdateEmployee(Guid id, Employee updatedEmployee)
        {
            return employeeRepository.UpdateEmployee(id, updatedEmployee);
        }
        public bool DeleteEmployee(Guid id)
        {
            return employeeRepository.DeleteEmployee(id);
        }
    }
}
