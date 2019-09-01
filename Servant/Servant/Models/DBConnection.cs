using System;
using System.Data;
using System.Data.SQLite;
using System.Configuration;

namespace Servant.Models
{
    public static class DBConnection
    {
        // SQL connection coming from the App.config file
        private static SQLiteConnection SQLiteConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);

        /// <summary>
        /// Method to execute a select request
        /// </summary>
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

        /// <summary>
        /// Method to execute a insert request
        /// </summary>
        public static bool INSERT(string query)
        {
            return ExecuteQuery(query);
        }

        /// <summary>
        /// Method to execute a delete request
        /// </summary>
        public static bool DELETE(string query)
        {
            return ExecuteQuery(query);
        }

        /// <summary>
        /// Method to execute a query
        /// </summary>
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
