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
                    connection.Close();
                    return Request.CreateResponse<List<Customer>>(HttpStatusCode.OK, customers);
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
                Customer localCustomer;
                SqlCommand command = new SqlCommand("SELECT * FROM Employee WHERE Id = '" + id + "';", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    localCustomer = new Customer(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetGuid(3));
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, localCustomer);
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id");
                }
            }
        }
        [HttpPost]
        public HttpResponseMessage Add([FromBody] Customer customer)
        {
            if (customer.FirstName == null || customer.LastName == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Check first and last name");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Customer VALUES (default, '" + customer.FirstName +
                    "', '" + customer.LastName + "', '"+customer.EmployeeId+"');", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public HttpResponseMessage Update(Guid id, [FromBody] Customer updatedCustomer)
        {
            if (updatedCustomer.FirstName == null || updatedCustomer.LastName == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Check first and last name");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE Customer SET FirstName = '" + updatedCustomer.FirstName +
                    "', LastName = '" + updatedCustomer.LastName + "' WHERE Id = '" + id + "';", connection);
                SqlCommand check = new SqlCommand("SELECT * FROM Customer WHERE Id = '" + id + "';", connection);

                connection.Open();
                SqlDataReader reader = check.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    reader = command.ExecuteReader();
                    reader.Read();
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
                SqlCommand command = new SqlCommand("DELETE Customer WHERE Id = '" + id + "' ; ", connection);
                SqlCommand check = new SqlCommand("SELECT * FROM Customer WHERE Id = '" + id + "' ; ", connection);

                connection.Open();
                SqlDataReader reader = check.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    reader = command.ExecuteReader();
                    reader.Read();
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
