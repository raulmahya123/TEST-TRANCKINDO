using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WebApplication3.Model
{
    public class EmployeeDAL
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["EmpConnection"].ConnectionString;

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Employee", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        EmpName = reader["EmpName"].ToString(),
                        Position = reader["Position"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])

                    };
                    employees.Add(employee);
                }   


            }
            return employees;
        }
        public int AddEmp(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Employee (EmpName, Position, Salary) VALUES (@EmpName, @Position, @Salary)", con);
                cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                cmd.Parameters.AddWithValue("@Position", emp.Position);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void UpdateEmp(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Employee SET EmpName=@EmpName, Position=@Position, Salary=@Salary WHERE Id=@Id", con);

                cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                cmd.Parameters.AddWithValue("@Position", emp.Position);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@Id", emp.Id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteEmp(int empId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", empId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        // Get by idS

        public Employee GetEmpById(int empId)

        {
            Employee emp = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * from Employee where Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", empId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    emp = new Employee();
                    emp.Id = Convert.ToInt32(reader["Id"]);
                    emp.EmpName = reader["EmpName"].ToString();
                    emp.Position = reader["Position"].ToString();
                    emp.Salary = Convert.ToDecimal(reader["Salary"]);
                }
            }
            return emp;

            }
    }
}