using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
namespace DoAn_Net
{
    internal class Modify
    {
        OracleCommand sqlCommand;//dung de truy van cau lenh VD : insert , update , delete
        OracleDataReader dataReader;//doc du lieu trong bang
        public List<Account> Users(string query) 
        { 
        List<Account> users = new List<Account>();
            using (OracleConnection oraConnection = Connection.GetSqlConnection())
            {
                oraConnection.Open();


                //string str = "Data Source=localhost:1521/orcl;User ID=s2001215968;Password=p123;";
                
                
              //  sqlCommand = new OracleCommand(query, oraConnection);
                sqlCommand = new OracleCommand(query, oraConnection);
           
                dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    //Console.WriteLine(String.Format("{0}", dataReader.GetString(0), dataReader.GetString(1)));
                    users.Add(new Account(dataReader.GetString(0), dataReader.GetString(1)));
                }
                oraConnection.Close();
            }
            return users;
        }

        public void Command(string query)
        {
            using (OracleConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new OracleCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();//thực thi câu truy vấn
                sqlConnection.Close();
            }
        }

    }
}
