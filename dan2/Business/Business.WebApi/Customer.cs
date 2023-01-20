using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.WebApi
{
    public class Customer
    {
        private string firstName;
        private string lastName;
        private int id;

        public string Fname { get { return firstName; } set { firstName = value; } }
        public string Lname { get { return lastName; } set { lastName = value; } }
        public int Id { get { return id; }set { id = value; } }

        public Customer(string fname, string lname, int id)
        {
            this.firstName = fname;
            this.lastName = lname;
            this.id = id;
        }
    }
}