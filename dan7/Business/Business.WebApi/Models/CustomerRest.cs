using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Model;
namespace Business.WebApi.Models
{
    public class CustomerRest
    {
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private Guid id;
        private Guid employeeId;

        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public DateTime DateOfBirth { get { return dateOfBirth; } set { dateOfBirth = value; } }
        public Guid Id { get { return id; } set { id = value; } }
        public Guid EmployeeId { get { return employeeId; } set { employeeId = value; } }

        public CustomerRest(Customer customer)
        {
            this.firstName = customer.FirstName;
            this.lastName = customer.LastName;
            this.id = customer.Id;
            this.dateOfBirth = customer.DateOfBirth;
            this.employeeId = customer.EmployeeId;
        }
        public CustomerRest() { }

        public bool FirstNameOrLastNameNotNull(CustomerRest customer)
        {
            if (customer.FirstName == null || customer.LastName == null)
            {
                return false;
            }
            return true;
        }
    }
}