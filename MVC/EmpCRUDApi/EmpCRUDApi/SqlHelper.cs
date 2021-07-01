using EmpCRUDApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmpCRUDApi
{
    public class SqlHelper
    {
        private string _connectionString;

        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Employee> GetEmployeeList()
        {
            List<Employee> employeeList = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("usp_GetAllEmployees", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        sqlDataAdapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            employeeList = dt.AsEnumerable()
                                            .Select(row => new Employee()
                                            {
                                                Id = row.Field<int>(0),
                                                FirstName = row.Field<string>(1).ToString(),
                                                LastName = row.Field<string>(2).ToString(),
                                                Salary = row.Field<decimal>(3),
                                                DOB = row.Field<string>(4).ToString(),
                                                HighestQualification = row.Field<string>(5).ToString(),
                                            }).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return employeeList;
        }

        public string SaveEmployee(Employee employee)
        {
            string status = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("usp_SaveEmployee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        command.Parameters.AddWithValue("@LastName", employee.LastName);
                        command.Parameters.AddWithValue("@Salary", employee.Salary);
                        command.Parameters.AddWithValue("@DOB", employee.DOB);
                        command.Parameters.AddWithValue("@HighestQualification", employee.HighestQualification);
                        int rowsAfftected = command.ExecuteNonQuery();
                        if (rowsAfftected > 0)
                            status = "Data Saved Successfully";
                        else
                            status = "Data Not Saved Successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                status = "Error Occured";
                throw ex;
            }

            return status;
        }

        public Employee GetEmployeebyId(int id)
        {
            Employee employee = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("usp_GetEmployeebyId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmpId", id);
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        sqlDataAdapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            employee = new Employee
                            {
                                Id = Convert.ToInt32(dt.Rows[0]["EmpId"]),
                                FirstName = dt.Rows[0]["FirstName"].ToString(),
                                LastName = dt.Rows[0]["LastName"].ToString(),
                                Salary = Convert.ToDecimal(dt.Rows[0]["Salary"]),
                                DOB = dt.Rows[0]["DOB"].ToString(),
                                HighestQualification = dt.Rows[0]["HighestQualification"].ToString(),
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return employee;
        }

        public string UpdateEmployee(Employee employee)
        {
            string status = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("usp_UpdateEmployee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmpId", employee.Id);
                        command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        command.Parameters.AddWithValue("@LastName", employee.LastName);
                        command.Parameters.AddWithValue("@Salary", employee.Salary);
                        command.Parameters.AddWithValue("@DOB", employee.DOB);
                        command.Parameters.AddWithValue("@HighestQualification", employee.HighestQualification);
                        int rowsAfftected = command.ExecuteNonQuery();
                        if (rowsAfftected > 0)
                            status = "Data Updated Successfully";
                        else
                            status = "Data Not Updated Successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                status = "Error Occured";
                throw ex;
            }

            return status;
        }

        public string DeleteEmployeebyId(int id)
        {
            string status = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("usp_DeleteEmployeebyId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmpId", id);
                        int rowsAfftected = command.ExecuteNonQuery();
                        if (rowsAfftected > 0)
                            status = "Record Deleted Successfully";
                        else
                            status = "Record Not Deleted Successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                status = "Error Occured";
                throw ex;
            }

            return status;
        }
    }
}