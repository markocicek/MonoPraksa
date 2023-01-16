using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Secretary : Employee
    {
        private List<Customer> customers;
        public Secretary(string firstName, string lastName, string position) : base(firstName, lastName, position)
        {
            FirstName = firstName;
            LastName = lastName;
            Position = "Secretary";
            customers= new List<Customer>();
        }
        public Secretary(string firstName, string lastName) : base (firstName, lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Position = "Secretary";
            customers = new List<Customer>();
        }
        public override void Work()
        {
            Console.WriteLine("Writes stuff");
        }
        public void AddCustomer(string firstName, string lastName)
        {
            customers.Add(new Customer(firstName, lastName));
        }
        public void RemoveCustomer(string firstName, string lastName)
        {
            var foundCustomer = FindCustomer(firstName, lastName);
            if (foundCustomer != null) {
                customers.RemoveAt(customers.IndexOf(foundCustomer));
                Console.WriteLine("Customer " + firstName + " " + lastName + " has been removed.");
            }
            else
            {
                Console.WriteLine("Customer "+firstName+" "+lastName+" meant for removal has not been found");
            }
        }
        public void ListCustomers()
        {
            Console.WriteLine("Current customers:");
            foreach (Customer customer in customers)
            {
                Console.WriteLine(customer.FirstName+ " " + customer.LastName);
            }
        }

        public void Search(string firstName, string lastName)
        {
            var foundCustomer = FindCustomer(firstName, lastName);
            if(foundCustomer != null)
            {
                Console.WriteLine("Customer "+foundCustomer.FirstName +" "+foundCustomer.LastName+" has been found.");
            }
            else
            {
                Console.WriteLine("Customer "+firstName+" "+lastName+"has not been found.");
            }
        }
        private Customer FindCustomer(string firstName, string lastName)
        {
            var foundCustomer = customers.SingleOrDefault(customer => customer.FirstName == firstName && customer.LastName == lastName);
            return foundCustomer;
        }
    }
}
