using Business.Model;
using Business.Service;
using Business.Service.Common;
using Business.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
namespace Business.WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeeService employeeService = new EmployeeService();
        [HttpGet]
        [Route("api/employee/all")]
        public async Task<HttpResponseMessage> ListEmployeesAsync()
        {
            List<Employee> employees;
            List<EmployeeRest> employeesRest = new List<EmployeeRest>();
            
            employees = await employeeService.GetEmployeeListAsync();

            foreach(Employee employee in employees)
            {
                employeesRest.Add(new EmployeeRest(employee));
            }
            if(employeesRest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no employees!");
            }
            return Request.CreateResponse<List<EmployeeRest>>(HttpStatusCode.OK, employeesRest);
        }
        [HttpGet]
        public async Task<HttpResponseMessage> FindAsync(Guid id)
        {
            Employee localEmployee = new Employee();
            localEmployee = await employeeService.FindEmployeeAsync(id);
            EmployeeRest employeeRest = new EmployeeRest(localEmployee);

            if(employeeRest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id");

            }
            return Request.CreateResponse(HttpStatusCode.OK, employeeRest);
        }

        [HttpGet]
        [Route("api/employee/customers")]
        public async Task<HttpResponseMessage> GetAllCustomersFromEmployeeAsync(Guid id)
        {
            Employee localEmployee = new Employee();
            localEmployee = await employeeService.FindEmployeeAsync(id);
            if(localEmployee == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id!");
            }
            localEmployee = await employeeService.GetAllCustomersFromEmployeeAsync(id);
            EmployeeRest employeeRest = new EmployeeRest(localEmployee);

            if(employeeRest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee has no customers!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, employeeRest);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddAsync([FromBody] EmployeeAddRest employeeAddRest)
        {
            if (ModelState.IsValid)
            {
                Employee localEmployee = new Employee(employeeAddRest.FirstName, employeeAddRest.LastName, employeeAddRest.DateOfBirth);
                await employeeService.AddEmployeeAsync(localEmployee);
                return Request.CreateResponse(HttpStatusCode.OK, "Employee added successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Check first name, last name and date of birth!");
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, [FromBody] EmployeeRest updatedEmployee)
        {
            if (updatedEmployee.NotAllValuesNull(updatedEmployee))
            {
                Employee localEmployee = new Employee(updatedEmployee.Id, updatedEmployee.FirstName, updatedEmployee.LastName, updatedEmployee.DateOfBirth);
                if (await employeeService.UpdateEmployeeAsync(id, localEmployee))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Update successful");
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Change at least one value");
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            if (await employeeService.DeleteEmployeeAsync(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Delete successful");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id");
        }
    }
}
