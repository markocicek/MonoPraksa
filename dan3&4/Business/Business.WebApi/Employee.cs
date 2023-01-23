using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Business.WebApi
{
    public class Employee
    {
        private string firstName;


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
        public Employee(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
        [Required(ErrorMessage = "First name is required!")]

        public string FirstName { get { return firstName; } set { firstName = value;} }
        [Required(ErrorMessage = "Last name is required!")]
        public string LastName { get { return lastName; } set { lastName = value;} }
        public Guid Id { get { return id; } set { id = value; } }

        public List<Customer> Customers { get { return customers; } }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }
    }

}