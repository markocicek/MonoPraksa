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
    public class EmployeeRepository
    {
        private string connectionString = "Data Source=localhost;Database=Business;trusted_connection=true;";

        public List<Employee> GetEmployeeList()
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
                        if(!reader.IsDBNull(3))
                        {
                            employees.Find(employee => employee.Id == reader.GetGuid(6)).AddCustomer(new Customer(reader.GetGuid(3), reader.GetString(4), reader.GetString(5), reader.GetGuid(6)));
                        }
                    }
                    reader.Close();
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
        public Employee FindEmployee(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Employee localEmployee;
                SqlCommand command = new SqlCommand("SELECT * FROM Employee WHERE Id = '" + id + "';", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    localEmployee = new Employee(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
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
        public Employee GetAllCustomersFromEmployee(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT Employee.Id, Employee.FirstName, Employee.LastName," +
                    " Customer.Id, Customer.FirstName, Customer.LastName, Customer.EmployeeId FROM Employee" +
                    " JOIN Customer ON Employee.Id = Customer.EmployeeId WHERE Employee.Id = '" + id + "';", connection);

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
                    return localEmployee;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
        }
        public void AddEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Employee VALUES (default, '" + employee.FirstName + "', '" + employee.LastName + "');", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                reader.Close();
                connection.Close();
            }
        }
        public bool UpdateEmployee(Guid id, Employee updatedEmployee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE Employee SET FirstName = '" + updatedEmployee.FirstName +
                    "', LastName = '" + updatedEmployee.LastName + "' WHERE Id = '" + id + "';", connection);
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
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
        }
        public bool DeleteEmployee(Guid id)
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
