using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Data;
using System.ComponentModel.DataAnnotations;


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
                    " LEFT JOIN Customer ON Employee.Id = Customer.EmployeeId;", connection);

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
                        if (!reader.IsDBNull(3))
                        {
                            employees.Find(employee => employee.Id == reader.GetGuid(6)).AddCustomer(new Customer(reader.GetGuid(3), reader.GetString(4), reader.GetString(5), reader.GetGuid(6)));
                        }
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
            string commandText = "SELECT * FROM Employee WHERE Id = @Id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Employee localEmployee;
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@Id", id);

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
            string commandText = "SELECT Employee.Id, Employee.FirstName, Employee.LastName, " +
                "Customer.Id, Customer.FirstName, Customer.LastName, Customer.EmployeeId FROM Employee " +
                "JOIN Customer ON Employee.Id = Customer.EmployeeId WHERE Employee.Id = @Id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@Id", id);

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
            string commandText = "INSERT INTO Employee VALUES (default, @FirstName, @LastName);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                connection.Open();
                command.ExecuteNonQuery();
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
            string commandText = "UPDATE Employee SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id;";
            string checkText = "SELECT * FROM Employee WHERE Id = @Id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                SqlCommand check = new SqlCommand(checkText, connection);

                command.Parameters.AddWithValue("@FirstName", updatedEmployee.FirstName);
                command.Parameters.AddWithValue("@LastName", updatedEmployee.LastName);
                command.Parameters.AddWithValue("@Id", id);

                check.Parameters.AddWithValue("@Id", id);

                connection.Open();
                SqlDataReader reader = check.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    command.ExecuteNonQuery();
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
            string commandText = "DELETE Employee WHERE Id = @Id;";
            string checkText = "SELECT * FROM Employee WHERE Id = @Id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                SqlCommand check = new SqlCommand(checkText, connection);

                command.Parameters.AddWithValue("@Id", id);
                check.Parameters.AddWithValue("@Id", id);

                connection.Open();
                SqlDataReader reader = check.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    command.ExecuteNonQuery();
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
