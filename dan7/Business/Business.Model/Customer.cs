using Business.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model
{
    public class Customer : ICustomerModel
    {
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private Guid id;
        private Guid employeeId;

        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public DateTime DateOfBirth { get { return dateOfBirth; } set { dateOfBirth = value; } }
        public Guid Id { get { return id; }set { id = value; } }
        public Guid EmployeeId { get { return employeeId; }set { employeeId = value; } }

        public Customer(Guid id, string firstName, string lastName, DateTime dateOfBirth, Guid employeeId)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.id = id;
            this.dateOfBirth = dateOfBirth;
            this.employeeId = employeeId;
        }
        public Customer() { }
    }
}