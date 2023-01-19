using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Business.WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        string connectionString = "Data Source=localhost;Database=Business;trusted_connection=true;";
        [HttpGet]
        [Route("api/employee/all")]
        public HttpResponseMessage ListEmployees()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT Employee.Id, Employee.FirstName, Employee.LastName," +
                    " Customer.Id, Customer.FirstName, Customer.LastName, Customer.EmployeeId FROM Employee" +
                    " JOIN Customer ON Employee.Id = Customer.EmployeeId;", connection);
               
                connection.Open();
                List<Employee> employees = new List<Employee>();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee localEmployee = employees.Find(employee => employee.Id == reader.GetGuid(0));
                        if (localEmployee == null)
                        {
                            employees.Add(new Employee(reader.GetGuid(0), reader.GetString(1), reader.GetString(2)));
                        }
                        employees.Find(employee => employee.Id == reader.GetGuid(6)).AddCustomer(new Customer(reader.GetGuid(3), reader.GetString(4), reader.GetString(5), reader.GetGuid(6)));
                    }
                    reader.Close();
                    connection.Close();
                    return Request.CreateResponse<List<Employee>>(HttpStatusCode.OK, employees);
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "List is empty!");
                }
            }
        }
        [HttpGet]
        public HttpResponseMessage Find(Guid id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Employee localEmployee;
                SqlCommand command = new SqlCommand("SELECT * FROM Employee WHERE Id = '"+id+"';", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    localEmployee = new Employee(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                    reader.Close();

                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, localEmployee);
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id");
                }
            }
        }
        [HttpGet]
        [Route("api/employee/customers")]
        public HttpResponseMessage GetAllCustomersFromEmployee(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT Employee.Id, Employee.FirstName, Employee.LastName," +
                    " Customer.Id, Customer.FirstName, Customer.LastName, Customer.EmployeeId FROM Employee" +
                    " JOIN Customer ON Employee.Id = Customer.EmployeeId WHERE Employee.Id = '"+id+"';", connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Employee localEmployee = new Employee();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (localEmployee.FirstName == null || localEmployee.LastName == null)
                        {
                            localEmployee = new Employee(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                        }
                        localEmployee.AddCustomer(new Customer(reader.GetGuid(3), reader.GetString(4), reader.GetString(5), reader.GetGuid(6)));
                    }
                    reader.Close();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, localEmployee);
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "List is empty!");
                }
            }
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody] Employee employee)
        {
            if (employee.FirstName == null || employee.LastName == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Check first and last name");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Employee VALUES (default, '"+employee.FirstName+"', '"+employee.LastName+"');", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                reader.Close();
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public HttpResponseMessage Update(Guid id, [FromBody] Employee updatedEmployee)
        {
            if (updatedEmployee.FirstName == null || updatedEmployee.LastName == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Check first and last name");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE Employee SET FirstName = '"+updatedEmployee.FirstName+
                    "', LastName = '"+updatedEmployee.LastName+"' WHERE Id = '"+id+"';", connection);
                SqlCommand check = new SqlCommand("SELECT * FROM Employee WHERE Id = '" + id + "';", connection);

                connection.Open();
                SqlDataReader reader = check.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    reader = command.ExecuteReader();
                    reader.Read();
                    reader.Close();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Update successful");
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id");
                }
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DELETE Employee WHERE Id = '" + id + "' ; ", connection);
                SqlCommand check = new SqlCommand("SELECT * FROM Employee WHERE Id = '" + id + "' ; ", connection);

                connection.Open();
                SqlDataReader reader = check.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    reader = command.ExecuteReader();
                    reader.Read();
                    reader.Close();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Delete successful");
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id");
                }
            }
        }


    }
}
