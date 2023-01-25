﻿using Business.Common;
using Business.Model;
using Business.Repository;
using Business.Repository.Common;
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
        protected IEmployeeRepository EmployeeRepository { get; private set; }
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.EmployeeRepository = employeeRepository;
        }
        public async Task<List<Employee>> GetEmployeeListAsync(string sort, int rpp, int page, DateTime? dateMin, DateTime? dateMax) 
        {
            Sorting sorting = new Sorting(sort);
            Paging paging = new Paging(page, rpp);
            EmployeeFilter employeeFilter = new EmployeeFilter(dateMin, dateMax);
            return await EmployeeRepository.GetEmployeeListAsync(sorting, paging, employeeFilter);
        }
        public async Task<List<Employee>> GetEmployeeListWithCustomersAsync()
        {
            return await EmployeeRepository.GetEmployeeListWithCustomersAsync();
        }
        public async Task<Employee> FindEmployeeAsync(Guid id)
        {
            return await EmployeeRepository.FindEmployeeAsync(id);
        }
        public async Task<Employee> GetAllCustomersFromEmployeeAsync(Guid id)
        {
            return await EmployeeRepository.GetAllCustomersFromEmployeeAsync(id);
        }
        public async Task AddEmployeeAsync(Employee employee)
        {
            Guid id = Guid.NewGuid();
            await EmployeeRepository.AddEmployeeAsync(id, employee);
        }
        public async Task<bool> UpdateEmployeeAsync(Guid id, Employee updatedEmployee)
        {
            return await EmployeeRepository.UpdateEmployeeAsync(id, updatedEmployee);
        }
        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            return await EmployeeRepository.DeleteEmployeeAsync(id);
        }
    }
}
