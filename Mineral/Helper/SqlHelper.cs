using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Mineral.Helper
{
    class SqlHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        public SqlHelper()
        {
        }
        //参数使用可变参数，params，在需要传递参数的时候传递，不需要的时候可以不写
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    using (OleDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.Parameters.AddRange(parameters);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    using (OleDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.Parameters.AddRange(parameters);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset);
                        return dataset.Tables[0];
                    }
                }
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }
    }
}
