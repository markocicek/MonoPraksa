using Business.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class CustomerRepository
    {
        private string connectionString = "Data Source=localhost;Database=Business;trusted_connection=true;";

        public List<Customer> GetCustomerList()
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
                    return customers;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }
        public Customer FindCustomer(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Customer localCustomer;
                SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE Id = '" + id + "';", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    localCustomer = new Customer(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetGuid(3));
                    reader.Close();
                    connection.Close();
                    return localCustomer;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }
        public void AddCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Customer VALUES (default, '" + customer.FirstName +
                    "', '" + customer.LastName + "', '" + customer.EmployeeId + "');", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                reader.Close();
                connection.Close();
            }
        }
        public bool UpdateCustomer(Guid id, Customer updatedCustomer)
        {
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
                    reader.Close();
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
        }
        public bool DeleteCustomer(Guid id)
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
                    reader.Close();
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
        }
    }
}
