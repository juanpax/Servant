using System;
using System.Data;
using System.Data.SQLite;
using System.Configuration;

namespace Servant.Models
{
    public static class DBConnection
    {
        private static SQLiteConnection SQLiteConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);

        public static DataTable SELECT(string query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                SQLiteConnection.Open();
                SQLiteDataAdapter sqliteAdapter = new SQLiteDataAdapter(query, SQLiteConnection);
                DataSet dataset = new DataSet();
                sqliteAdapter.Fill(dataset);
                dataTable = dataset.Tables[0];
                SQLiteConnection.Close();
            }
            catch (Exception ex)
            {
                // Do something to manage the exceptions
            }

            return dataTable;
        }

        public static bool INSERT(string query)
        {
            return ExecuteQuery(query);
        }

        public static bool DELETE(string query)
        {
            return ExecuteQuery(query);
        }

        private static bool ExecuteQuery(string query)
        {
            try
            {
                SQLiteConnection.Open();
                SQLiteCommand sql_cmd = SQLiteConnection.CreateCommand();
                sql_cmd.CommandText = query;
                sql_cmd.ExecuteNonQuery();
                SQLiteConnection.Close();

                return true;
            }
            catch (Exception ex)
            {
                // Do something to manage the exceptions
            }

            return false;
        }
    }
}
