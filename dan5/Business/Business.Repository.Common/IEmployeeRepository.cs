using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.Common
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployeeList();
        Employee FindEmployee(Guid id);
        Employee GetAllCustomersFromEmployee(Guid id);
        void AddEmployee(Employee employee);
        bool UpdateEmployee(Guid id, Employee updatedEmployee);
        bool DeleteEmployee(Guid id);
    }
}
