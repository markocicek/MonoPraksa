using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.WebApi
{
    public class Customer
    {
        private string fname;
        private string lname;
        private int id;

        public string Fname { get { return fname; } set { fname = value; } }
        public string Lname { get { return lname; } set { lname = value; } }
        public int Id { get { return id; }set { id = value; } }

        public Customer(string fname, string lname, int id)
        {
            this.fname = fname;
            this.lname = lname;
            this.id = id;
        }
    }
}