using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Common
{
    public class Sorting
    {
        private string order;
        public string Order { get { return order; } set { order = value; } }

        public Sorting()
        {
            Order = "LastName ASC";
        }
        public Sorting(string sort)
        {
            if(sort == "LastName")
            {
                Order = "LastName ASC";
            }else if(sort == "FirstName")
            {
                Order = "FirstName ASC";
            }else if(sort == "DateOfBirth")
            {
                Order = "DateOfBirth ASC";
            }
            else
            {
                Order = "LastName ASC";
            }
        }
    }
}
