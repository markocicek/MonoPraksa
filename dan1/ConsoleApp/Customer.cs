using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Customer : IPerson
    {
        private string firstName;
        private string lastName;
        public Customer(string firstName, string lastName) {
            this.firstName = firstName;
            this.lastName = lastName;
        }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
    }
}
