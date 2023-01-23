using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace Business.WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        string connectionString = "Data Source=localhost;Database=Business;trusted_connection=true;";

        [HttpGet]
        [Route("api/customer/all")]
        public HttpResponseMessage ListCustomers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Customer;", connection);
                connection.Open();
                List<Customer> customers = new List<Customer>();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetGuid(3)));
                    }
                    reader.Close();
                    connection.Close();
                    return Request.CreateResponse<List<Customer>>(HttpStatusCode.OK, customers);
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no customers!");
                }
            }
        }
        [HttpGet]
        public HttpResponseMessage Find(Guid id)
        {
            string commandText = "SELECT * FROM Customer WHERE Id = @Id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Customer localCustomer;
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    localCustomer = new Customer(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetGuid(3));
                    reader.Close();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, localCustomer);
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no customer with that id");
                }
            }
        }
        [HttpPost]
        public HttpResponseMessage Add([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                string commandText = "INSERT INTO Customer VALUES (default, @FirstName, @LastName, @EmployeeId);";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@EmployeeId", customer.EmployeeId);
                    connection.Open();

                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "First name, last name and/or employeeid missing");
        }

        [HttpPut]
        public HttpResponseMessage Update(Guid id, [FromBody] Customer updatedCustomer)
        {
            string commandText = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id;";
            string checkText = "SELECT * FROM Customer WHERE Id = @Id;";
            if (updatedCustomer.FirstName == null || updatedCustomer.LastName == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Check first and last name");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                SqlCommand check = new SqlCommand(checkText, connection);
                command.Parameters.AddWithValue("@FirstName", updatedCustomer.FirstName);
                command.Parameters.AddWithValue("@LastName", updatedCustomer.LastName);
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
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no customer with that id");
                }
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            string commandText = "DELETE Customer WHERE Id = @Id;";
            string checkText = "SELECT * FROM Customer WHERE Id = @Id;";
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
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no customer with that id");
                }
            }
        }

    }
}
