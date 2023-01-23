using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Business.WebApi
{
    public class Customer
    {
        private string firstName;
        private string lastName;
        private Guid id;
        private Guid employeeId;

        [Required(ErrorMessage = "First name is required!")]
        public string FirstName { get { return firstName; } set { firstName = value; } }
        [Required(ErrorMessage = "Last name is required!")]

        public string LastName { get { return lastName; } set { lastName = value; } }
        public Guid Id { get { return id; }set { id = value; } }
        public Guid EmployeeId { get { return employeeId; }set { employeeId = value; } }

        public Customer(Guid id, string firstName, string lastName, Guid employeeId)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.id = id;
            this.employeeId = employeeId;
        }
    }
}