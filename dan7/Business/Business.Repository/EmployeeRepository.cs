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
    public class EmployeeRepository : IEmployeeRepository
    {
        private string connectionString = "Data Source=localhost;Database=Business;trusted_connection=true;";

        public async Task<List<Employee>> GetEmployeeListAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT Employee.Id, Employee.FirstName, Employee.LastName, Employee.DateOfBirth," +
                    " Customer.Id, Customer.FirstName, Customer.LastName, Customer.DateOfBirth, Customer.EmployeeId FROM Employee" +
                    " LEFT JOIN Customer ON Employee.Id = Customer.EmployeeId;", connection);

                await connection.OpenAsync();
                List<Employee> employees = new List<Employee>();
                SqlDataReader readerAsync = await command.ExecuteReaderAsync();

                if (readerAsync.HasRows)
                {
                    while (await readerAsync.ReadAsync())
                    {
                        Employee localEmployee = employees.Find(employee => employee.Id == readerAsync.GetGuid(0));
                        if (localEmployee == null)
                        {
                            employees.Add(new Employee(readerAsync.GetGuid(0), readerAsync.GetString(1), readerAsync.GetString(2), readerAsync.GetDateTime(3)));
                        }
                        if(!readerAsync.IsDBNull(4))
                        {
                            employees.Find(employee => employee.Id == readerAsync.GetGuid(8)).AddCustomer(new Customer(readerAsync.GetGuid(4), readerAsync.GetString(5), readerAsync.GetString(6), readerAsync.GetDateTime(7), readerAsync.GetGuid(8)));
                        }
                    }
                    readerAsync.Close();
                    connection.Close();
                    return employees;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }
        public async Task<Employee> FindEmployeeAsync(Guid id)
        {
            string commandText = "SELECT * FROM Employee WHERE Id = @Id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Employee localEmployee;
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    localEmployee = new Employee(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3));
                    reader.Close();

                    connection.Close();
                    return localEmployee;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }
        public async Task<Employee> GetAllCustomersFromEmployeeAsync(Guid id)
        {
            string commandText = "SELECT Employee.Id, Employee.FirstName, Employee.LastName, Employee.DateOfBirth " +
                "Customer.Id, Customer.FirstName, Customer.LastName, Customer.EmployeeId, Customer.DateOfBirth FROM Employee " +
                "JOIN Customer ON Employee.Id = Customer.EmployeeId WHERE Employee.Id = @Id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                Employee localEmployee = new Employee();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        if (localEmployee.FirstName == null || localEmployee.LastName == null)
                        {
                            localEmployee = new Employee(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3));
                        }
                        localEmployee.AddCustomer(new Customer(reader.GetGuid(4), reader.GetString(5), reader.GetString(6), reader.GetDateTime(8), reader.GetGuid(7)));
                    }
                    reader.Close();
                    connection.Close();
                    return localEmployee;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }
        public async Task AddEmployeeAsync(Guid id, Employee employee)
        {
            string commandText = "INSERT INTO Employee VALUES (@Id, @FirstName, @LastName, @DateOfBirth);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@Id", id);

                command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                connection.Close();
            }
        }
        public async Task<bool> UpdateEmployeeAsync(Guid id, Employee updatedEmployee)
        {
            DateTime defaultDateTime = new DateTime();
            string commandText = BuildUpdateQuery(updatedEmployee);
            string checkText = "SELECT * FROM Employee WHERE Id = @Id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                SqlCommand check = new SqlCommand(checkText, connection);

                if(updatedEmployee.FirstName != null)
                {
                    command.Parameters.AddWithValue("@FirstName", updatedEmployee.FirstName);
                }
                if(updatedEmployee.LastName != null)
                {
                    command.Parameters.AddWithValue("@LastName", updatedEmployee.LastName);
                }
                if(updatedEmployee.DateOfBirth != defaultDateTime)
                {
                    command.Parameters.AddWithValue("@DateOfBirth", updatedEmployee.DateOfBirth);
                }
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
        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            string commandText = "DELETE Employee WHERE Id = @Id;";
            string checkText = "SELECT * FROM Employee WHERE Id = @Id;";
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

        private string BuildUpdateQuery(Employee employee)
        {
            DateTime defaultDateTime = new DateTime();
            StringBuilder commandTextBuilder = new StringBuilder("UPDATE Employee SET", 400);
            if (employee.FirstName != null)
            {
                commandTextBuilder.AppendFormat(" FirstName = @FirstName");
                if (employee.LastName != null || employee.DateOfBirth != null)
                {
                    commandTextBuilder.AppendFormat(",");
                }
            }
            if (employee.LastName != null)
            {
                commandTextBuilder.AppendFormat(" LastName = @LastName");
                if (employee.DateOfBirth != defaultDateTime)
                {
                    commandTextBuilder.AppendFormat(",");
                }
            }
            if (employee.DateOfBirth != defaultDateTime)
            {
                commandTextBuilder.AppendFormat(" DateOfBirth = @DateOfBirth");
            }
            commandTextBuilder.AppendFormat(" WHERE Id = @Id;");
            return commandTextBuilder.ToString();
        }
    }
}
