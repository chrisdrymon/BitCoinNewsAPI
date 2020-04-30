using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BitCoinNewsAPI
{
    class DataAccess
    {
        private static string constring = ConfigurationManager.ConnectionStrings["BCArticles"].ConnectionString;
        public static void InsertNews(DateTime datetime, long unixtime, string url, string title, string author, string description)
        {
            using (SqlConnection conn = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("InsertNews", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@datetime", datetime);
                    cmd.Parameters.AddWithValue("@unixtime", unixtime);
                    cmd.Parameters.AddWithValue("@url", url);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@author", author);
                    cmd.Parameters.AddWithValue("@description", description);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
            }
        }
    }
}
