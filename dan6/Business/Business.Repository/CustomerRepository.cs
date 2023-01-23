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

        public async Task<List<Customer>> GetCustomerListAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT Id, FirstName, LastName, DateOfBirth, EmployeeId FROM Customer;", connection);
                await connection.OpenAsync();
                List<Customer> customers = new List<Customer>();
                SqlDataReader readerAsync = await command.ExecuteReaderAsync();

                if (readerAsync.HasRows)
                {
                    while (await readerAsync.ReadAsync())
                    {
                        customers.Add(new Customer(readerAsync.GetGuid(0), readerAsync.GetString(1), readerAsync.GetString(2), readerAsync.GetDateTime(3), readerAsync.GetGuid(4)));
                    }
                    readerAsync.Close();
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
        public async Task<Customer> FindCustomerAsync(Guid id)
        {
            string commandText = "SELECT Id, FirstName, LastName, DateOfBirth, EmployeeId FROM Customer WHERE Id = @Id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Customer localCustomer;
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    localCustomer = new Customer(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetGuid(4));
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
        public async Task AddCustomerAsync(Guid id ,Customer customer)
        {
            string commandText = "INSERT INTO Customer (Id, FirstName, LastName, DateOfBirth, EmployeeId) VALUES (@Id, @FirstName, @LastName, @DateOfBirth, @EmployeeId);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                command.Parameters.AddWithValue("@EmployeeId", customer.EmployeeId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                connection.Close();
            }
        }
        public async Task<bool> UpdateCustomerAsync(Guid id, Customer updatedCustomer)
        {
            string commandText = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth WHERE Id = @Id;";
            string checkText = "SELECT * FROM Customer WHERE Id = @Id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                SqlCommand check = new SqlCommand(checkText, connection);
                command.Parameters.AddWithValue("@FirstName", updatedCustomer.FirstName);
                command.Parameters.AddWithValue("@LastName", updatedCustomer.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", updatedCustomer.DateOfBirth);
                command.Parameters.AddWithValue("@Id", id);
                check.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();
                SqlDataReader reader = await check.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    reader.Close();
                    await command.ExecuteNonQueryAsync();
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
        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            string commandText = "DELETE Customer WHERE Id = @Id;";
            string checkText = "SELECT * FROM Customer WHERE Id = @Id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                SqlCommand check = new SqlCommand(checkText, connection);
                command.Parameters.AddWithValue("@Id", id);
                check.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();
                SqlDataReader reader = await check.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    reader.Close();
                    await command.ExecuteNonQueryAsync();
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
