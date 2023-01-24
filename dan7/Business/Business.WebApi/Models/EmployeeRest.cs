using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.WebApi.Models
{
    public class EmployeeRest
    {
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private Guid id;
        private List<CustomerRest> customers = new List<CustomerRest>();
        public EmployeeRest() { }
        public EmployeeRest(Guid id, string firstName, string lastName, DateTime dateOfBirth)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
        }
        public EmployeeRest(Employee employee)
        {
            this.id = employee.Id;
            this.firstName = employee.FirstName;
            this.lastName = employee.LastName;
            this.dateOfBirth = employee.DateOfBirth;
            foreach(Customer customer in employee.Customers) 
            {
                customers.Add(new CustomerRest(customer));
            }
        }

        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public DateTime DateOfBirth { get { return dateOfBirth; } set { dateOfBirth = value; } }
        public Guid Id { get { return id; } set { id = value; } }

        public List<CustomerRest> Customers { get { return customers; } }

        public void AddCustomer(CustomerRest customer)
        {
            customers.Add(customer);
        }
        public bool NotAllValuesNull(EmployeeRest employee)
        {
            if (employee.FirstName == null && employee.LastName == null && employee.DateOfBirth == null)
            {
                return false;
            }
            return true;
        }
    }
}
