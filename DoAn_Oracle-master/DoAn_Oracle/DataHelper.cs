using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DoAn_Net
{
    public class DataHelper
    {
        private string connectionString = @"Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";
        public Account GetUserInfo(string userName)
        {
            Account user = null;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Username, Displayname, Email FROM Account WHERE Username = :UserName";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("UserName", userName));
                    //command.Parameters.AddWithValue("@UserName", userName);
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string displayName = reader["Displayname"].ToString();
                        string email = reader["Email"].ToString();

                        user = new Account(userName, displayName, email);
                    }
                }

                connection.Close();
            }

            return user;
        }
    }

}
