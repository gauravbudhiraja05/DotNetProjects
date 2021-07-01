using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUsingMVVM_Framework.Helpers
{
    public class SqlHelper
    {
        internal static DataTable ExecuteDataTable(string connectionstring, CommandType commandType, string commandName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand(commandName,con))
                    {
                        cmd.CommandType = commandType;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

            return dt;
        }

        internal static void ExecuteNonQuery(string v1, CommandType storedProcedure, string v2, SqlParameter[] myParams)
        {
            throw new NotImplementedException();
        }
    }
}
