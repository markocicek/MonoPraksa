using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model.Common
{
    public interface IEmployeeModel
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        Guid Id { get; set; }
    }
}
