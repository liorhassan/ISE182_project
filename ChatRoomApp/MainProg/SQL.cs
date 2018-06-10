using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogic
{
    class SQL
        /*
         * Main class, connecting to database using C#
         */
    {
        /// <summary>
        /// 
        /// Connect to DB, read records from order table. 
        /// Connect to DB, write records to customer table.
        /// 
        /// </summary>
        /// <param name="args">None</param>
        static void Main(string[] args)
            
        {
            string connetion_string = null;
            string sql_query = null;
            string server_address = "192.168.1.2,1433\\MS3";
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
            System.Threading.Thread.Sleep(10000);
        }
    }
}