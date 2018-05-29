using System;
using System.Data;
using System.Data.SqlClient;

namespace New
{
    class SQL
    {
        public static void Main(string[] args)        
        {
            string connetion_string = null;
            string sql_query = null;
            string server_address = "192.168.1.2,1010\\MS3";
            string database_name = "MS3";
            string user_name = "publicUser";
            string password = "isANerd";

            SqlConnection connection;
            SqlCommand command;

            connetion_string = $"Data Source={server_address};Initial Catalog={database_name };User ID={user_name};Password={password}";
            connection = new SqlConnection(connetion_string);
            SqlDataReader data_reader;

            try
            {
                connection.Open();
                Console.WriteLine("connected to: " + server_address);
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}