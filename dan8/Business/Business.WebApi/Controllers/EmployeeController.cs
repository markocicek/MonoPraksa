using Business.Common;
using Business.Model;
using Business.Service;
using Business.Service.Common;
using Business.WebApi.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
namespace Business.WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        protected IEmployeeService EmployeeService { get; private set; }
        public EmployeeController(IEmployeeService employeeService)
        {
            this.EmployeeService = employeeService;
        }
        [HttpGet]
        [Route("api/employee/all")]
        public async Task<HttpResponseMessage> ListEmployeesAsync([FromUri]string sort, [FromUri]int rpp, [FromUri]int page, DateTime? dateMin, DateTime? dateMax) //dodati 3 parametra za paging, sorting i filtering
        {
            List<Employee> employees;
            List<EmployeeRest> employeesRest = new List<EmployeeRest>();
            
            employees = await EmployeeService.GetEmployeeListAsync(sort, rpp, page, dateMin, dateMax);
            List<Employee> employeeCheck = await EmployeeService.GetEmployeeListAsync(sort, 4, 1, new DateTime?(), new DateTime?());
            if(employeeCheck == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no employees!");
            }
            if(employees == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nothing on this page!");
            }
            foreach (Employee employee in employees)
            {
                employeesRest.Add(new EmployeeRest(employee));
            }
            return Request.CreateResponse<List<EmployeeRest>>(HttpStatusCode.OK, employeesRest);
        }
        [HttpGet]
        [Route("api/employee/customers/all")]
        public async Task<HttpResponseMessage> ListEmployeesWithCustomersAsync()
        { 
            List<Employee> employees;
            List<EmployeeRest> employeesRest = new List<EmployeeRest>();

            employees = await EmployeeService.GetEmployeeListWithCustomersAsync();

            foreach (Employee employee in employees)
            {
                employeesRest.Add(new EmployeeRest(employee));
            }
            if (employeesRest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no employees!");
            }
            return Request.CreateResponse<List<EmployeeRest>>(HttpStatusCode.OK, employeesRest);
        }
        [HttpGet]
        public async Task<HttpResponseMessage> FindAsync(Guid id)
        {
            Employee localEmployee = new Employee();
            localEmployee = await EmployeeService.FindEmployeeAsync(id);
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
            localEmployee = await EmployeeService.FindEmployeeAsync(id);
            if(localEmployee == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id!");
            }
            localEmployee = await EmployeeService.GetAllCustomersFromEmployeeAsync(id);
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
                await EmployeeService.AddEmployeeAsync(localEmployee);
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
                if (await EmployeeService.UpdateEmployeeAsync(id, localEmployee))
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
            if (await EmployeeService.DeleteEmployeeAsync(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Delete successful");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id");
        }
    }
}
