using Business.Model;
using Business.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class CustomerRepository : ICustomerRepository
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
        }
        public bool UpdateCustomer(Guid id, Customer updatedCustomer)
        {
            string commandText = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id;";
            string checkText = "SELECT * FROM Customer WHERE Id = @Id;";
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
