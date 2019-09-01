using Servant.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Servant
{
    public static class BlurbModel
    {
        public static List<string[]> GetBlurbList()
        {
            string query = "SELECT * FROM BLURB";
            DataTable result = DBConnection.SELECT(query);

            return DataTableToList(result);
        }

        public static bool SaveBlurb(string id, string pattern, string text)
        {
            string query =
                (id == "") ? "INSERT INTO BLURB (PATTERN, TEXT) VALUES ('" + pattern + "', '" + text + "')" :
                "UPDATE BLURB SET PATTERN = '" + pattern + "', " + "TEXT = '" + text + "' WHERE ID = " + id;

            return DBConnection.INSERT(query);
        }

        public static bool DeleteBlurb(string id)
        {
            string query = "DELETE FROM BLURB WHERE ID = " + id;

            return DBConnection.DELETE(query);
        }

        private static List<string[]> DataTableToList(DataTable datatable)
        {
            List<string[]> blurbList = (from rw in datatable.AsEnumerable()
                                        select new string[]
                                        {
                                            Convert.ToString(rw["ID"]),
                                            Convert.ToString(rw["PATTERN"]),
                                            Convert.ToString(rw["TEXT"])
                                        }).ToList();

            return blurbList;
        }
    }
}
