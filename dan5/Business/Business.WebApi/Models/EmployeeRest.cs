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
        private Guid id;
        private List<CustomerRest> customers = new List<CustomerRest>();
        public EmployeeRest() { }
        public EmployeeRest(Guid id, string firstName, string lastName)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
        }
        public EmployeeRest(Employee employee)
        {
            this.id = employee.Id;
            this.firstName = employee.FirstName;
            this.lastName = employee.LastName;
            foreach(Customer customer in employee.Customers) 
            {
                customers.Add(new CustomerRest(customer));
            }
        }

        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public Guid Id { get { return id; } set { id = value; } }

        public List<CustomerRest> Customers { get { return customers; } }

        public void AddCustomer(CustomerRest customer)
        {
            customers.Add(customer);
        }
        public bool FirstNameOrLastNameNotNull(EmployeeRest employee)
        {
            if (employee.FirstName == null || employee.LastName == null)
            {
                return false;
            }
            return true;
        }
    }
}
