using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SaleClassLibrary
{
    public class DataAccessLayer
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public DataAccessLayer()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CustomerConnection"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
        }

        public int SaveData(Customer customer)
        {
            int rowsAffected = 0;
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Insert into Customer(CustomerName, Address, EmailId) values('" + customer.CustomerName + "', '" + customer.Address + "','" + customer.EmailId + "')", sqlConnection);
                rowsAffected = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }

            return rowsAffected;
        }

        public List<Customer> GetAllCustomer()
        {
            List<Customer> customerList = new List<Customer>();
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select CustomerID, CustomerName, Address, EmailId from Customer", sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    customerList = dt.AsEnumerable()
                       .Select(row => new Customer
                       {
                           // assuming column 0's type is Nullable<long>
                           CustomerID = row.Field<int>(0),
                           CustomerName = row.Field<string>(1),
                           Address = row.Field<string>(2),
                           EmailId = row.Field<string>(3)
                       }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }

            return customerList;
        }

        public int DeleteCustomer(int customerId)
        {
            int rowsAffected = 0;
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Delete from Customer where CustomerID=" + customerId + "", sqlConnection);
                rowsAffected = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }

            return rowsAffected;
        }
    }
}
