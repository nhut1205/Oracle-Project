using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using Oracle.ManagedDataAccess.Client;
namespace DoAn_Net
{
    class Connection
    {
        private static string stringConnection = @"Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";
        private OracleConnection conn;
        public static OracleConnection GetSqlConnection()
        {
            return new OracleConnection(stringConnection);
        }

        public DataTable getDatatable(string sql)
        {
            conn = new OracleConnection(stringConnection);
            DataTable dt = new DataTable();
            OracleDataAdapter da = new OracleDataAdapter(sql, stringConnection);
            da.Fill(dt);
            return dt;
        }


        public int updateDataBase(DataTable dt)
        {
            OracleDataAdapter ds_Products = new OracleDataAdapter("select * from orders", stringConnection);
            OracleCommandBuilder cb = new OracleCommandBuilder(ds_Products);
            int kq = ds_Products.Update(dt);
            return kq;
        }

        public int updateDataBaseCus(DataTable dt)
        {
            OracleDataAdapter ds_Products = new OracleDataAdapter("select * from customer", stringConnection);
            OracleCommandBuilder cb = new OracleCommandBuilder(ds_Products);
            int kq = ds_Products.Update(dt);
            return kq;
        }

        public int updateDataBaseCT(DataTable dt)
        {
            OracleDataAdapter ds_Products = new OracleDataAdapter("select * from orderDetails", stringConnection);
            OracleCommandBuilder cb = new OracleCommandBuilder(ds_Products);
            int kq = ds_Products.Update(dt);
            return kq;
        }

        public object getScalar(string sql)
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand(sql, conn);
            object kq = cmd.ExecuteScalar();
            return kq;
            conn.Close();
        }

    }
}
