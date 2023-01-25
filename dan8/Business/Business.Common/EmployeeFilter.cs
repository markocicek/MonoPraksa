using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Common
{
    public class EmployeeFilter
    {
        private DateTime? dateMin;
        private DateTime? dateMax;
        public DateTime? DateMin { get { return dateMin; } set { dateMin = value; } }
        public DateTime? DateMax { get { return dateMax; } set { dateMax = value; } }

        public EmployeeFilter(DateTime? dateMin, DateTime? dateMax)
        {
            this.DateMin = dateMin;
            this.DateMax = dateMax;
        }
    }
}
