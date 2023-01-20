using Business.Model;
using Business.Service;
using Business.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace Business.WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeeService employeeService = new EmployeeService();
        [HttpGet]
        [Route("api/employee/all")]
        public HttpResponseMessage ListEmployees()
        {
            List<Employee> employees;
            List<EmployeeRest> employeesRest = new List<EmployeeRest>();
            
            employees = employeeService.GetEmployeeList();

            foreach(Employee employee in employees)
            {
                employeesRest.Add(new EmployeeRest(employee));
            }
            if(employees == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no employees!");
            }
            return Request.CreateResponse<List<Employee>>(HttpStatusCode.OK, employees);
        }
        [HttpGet]
        public HttpResponseMessage Find(Guid id)
        {
            Employee localEmployee = new Employee();
            localEmployee = employeeService.FindEmployee(id);
            EmployeeRest employeeRest = new EmployeeRest(localEmployee);

            if(employeeRest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id");

            }
            return Request.CreateResponse(HttpStatusCode.OK, employeeRest);
        }

        [HttpGet]
        [Route("api/employee/customers")]
        public HttpResponseMessage GetAllCustomersFromEmployee(Guid id)
        {
            Employee localEmployee = new Employee();
            localEmployee = employeeService.FindEmployee(id);
            if(localEmployee == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id!");
            }
            localEmployee = employeeService.GetAllCustomersFromEmployee(id);
            EmployeeRest employeeRest = new EmployeeRest(localEmployee);

            if(employeeRest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee has no customers!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, employeeRest);
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody] EmployeeRest employeeRest)
        {
            if (employeeRest.FirstNameOrLastNameNotNull(employeeRest))
            {
                Employee localEmployee = new Employee(employeeRest.Id, employeeRest.FirstName, employeeRest.LastName);
                employeeService.AddEmployee(localEmployee);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Check first and last name");
        }

        [HttpPut]
        public HttpResponseMessage Update(Guid id, [FromBody] EmployeeRest updatedEmployee)
        {
            if (updatedEmployee.FirstNameOrLastNameNotNull(updatedEmployee))
            {
                Employee localEmployee = new Employee(updatedEmployee.Id, updatedEmployee.FirstName, updatedEmployee.LastName);
                if (employeeService.UpdateEmployee(id, localEmployee))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Update successful");
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Check first and last name");
        }

        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            if (employeeService.DeleteEmployee(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Delete successful");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id");
        }
    }
}
