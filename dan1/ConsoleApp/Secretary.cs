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
        public void AddCustomer(Customer customer)
        {
            customers.Add(new Customer(customer.FirstName, customer.LastName));
        }
        public void RemoveCustomer(Customer customer)
        {
            var foundCustomer = FindCustomer(customer);
            if (foundCustomer != null) {
                customers.RemoveAt(customers.IndexOf(foundCustomer));
                Console.WriteLine("Customer " + customer.FirstName + " " + customer.LastName + " has been removed.");
            }
            else
            {
                Console.WriteLine("Customer "+customer.FirstName+" "+customer.LastName+" meant for removal has not been found");
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

        public void Search(Customer customer)
        {
            var foundCustomer = FindCustomer(customer);
            if(foundCustomer != null)
            {
                Console.WriteLine("Customer "+foundCustomer.FirstName +" "+foundCustomer.LastName+" has been found.");
            }
            else
            {
                Console.WriteLine("Customer "+customer.FirstName+" "+customer.LastName+" has not been found.");
            }
        }
        private Customer FindCustomer(Customer customer)
        {
            StringComparer ordICCmp = StringComparer.OrdinalIgnoreCase;
            var foundCustomer = customers.Find(customerFind => customerFind.FirstName.ToLower() == customer.FirstName.ToLower() && customerFind.FirstName.ToLower() == customer.FirstName.ToLower());
            return foundCustomer;
        }
    }
}
