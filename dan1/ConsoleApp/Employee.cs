using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    abstract class Employee : IPerson
    {
        private string firstName;
        private string lastName;
        private string position;
        public Employee(string firstName, string lastName, string position)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.position = position;
        }
        public Employee(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.position = Position;
        }
        public string FirstName { set { this.firstName = value; } get { return firstName; } }
        public string LastName { set { this.lastName = value; } get { return lastName; } }

        public string Position { set { this.position = value;} get { return position; } }
        public abstract void Work();

    }
}
