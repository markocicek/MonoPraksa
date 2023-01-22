using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Business.WebApi
{
    public class Employee
    {
        [Required(ErrorMessage = "First name is required!")]
        private string firstName;

        [Required(ErrorMessage = "Last name is required!")]
        private string lastName;

        private Guid id;
        private List<Customer> customers = new List<Customer>();
        public Employee() { }
        public Employee(Guid id, string firstName, string lastName)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
        }
        public string FirstName { get { return firstName; } set { firstName = value;} }
        public string LastName { get { return lastName; } set { lastName = value;} }
        public Guid Id { get { return id; } set { id = value; } }

        public List<Customer> Customers { get { return customers; } }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }
    }

}