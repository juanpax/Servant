using Servant.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Servant
{
    public static class BlurbModel
    {
        /// <summary>
        /// Method to get all the list of blurb in the database
        /// </summary>
        public static List<string[]> GetBlurbList()
        {
            string query = "SELECT * FROM BLURB";
            DataTable result = DBConnection.SELECT(query);

            return DataTableToList(result);
        }

        /// <summary>
        /// Method to save or update a blurb
        /// </summary>
        public static bool SaveBlurb(string id, string pattern, string format, string text)
        {
            text = text.Replace("'", "''");
            string query =
                (id == "") ? string.Format(@"INSERT INTO BLURB (PATTERN, FORMAT, TEXT) VALUES ('{0}', '{1}', '{2}')", pattern, format, text) :
                string.Format("UPDATE BLURB SET PATTERN = '{0}', FORMAT = '{1}', TEXT = '{2}' WHERE ID = {3}", pattern, format, text, id);

            return DBConnection.INSERT(query);
        }

        /// <summary>
        /// Method to delete an existing blurb based on its Id
        /// </summary>
        public static bool DeleteBlurb(string id)
        {
            string query = "DELETE FROM BLURB WHERE ID = " + id;

            return DBConnection.DELETE(query);
        }

        /// <summary>
        /// Method to parse the datatable result from the query to string[]
        /// </summary>
        private static List<string[]> DataTableToList(DataTable datatable)
        {
            int i = 0;
            List<string[]> blurbList = (from rw in datatable.AsEnumerable()
                                        select new string[]
                                        {
                                            Convert.ToString(++i),
                                            Convert.ToString(rw["PATTERN"]),
                                            Convert.ToString(rw["FORMAT"]),
                                            Convert.ToString(rw["TEXT"]),
                                            Convert.ToString(rw["ID"])
                                        }).ToList();
            return blurbList;
        }
    }
}
