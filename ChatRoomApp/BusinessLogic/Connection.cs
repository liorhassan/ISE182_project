using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogic
{
    class Connection
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
                sql_query = "select * from [dbo].[order];";
                command = new SqlCommand(sql_query, connection);
                data_reader = command.ExecuteReader();
                while (data_reader.Read())
                {
                    DateTime dateFacturation = new DateTime();
                    if (!data_reader.IsDBNull(1))
                        dateFacturation = data_reader.GetDateTime(1); //2 is the coloumn index of the date. There are such               
                    Console.WriteLine("Order ID: " + data_reader.GetValue(0) + ", Order date: " + dateFacturation.ToString() + ", Order amount: " + data_reader.GetValue(4));
                }
                data_reader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());
            }


            try
            {
                connection.Open();
                command = new SqlCommand(null, connection);

                // Create and prepare an SQL statement.
                // Use should never use something like: query = "insert into table values(" + value + ");" 
                // Especially when selecting. More about it on the lab about security.
                command.CommandText =
                    "INSERT INTO Customer ([FirstName],[LastName],[City],[Country],[Phone]) " +
                    "VALUES (@first_name, @last_name,@city,@country,@phone)";
                SqlParameter first_name_param = new SqlParameter(@"first_name", SqlDbType.Text, 20);
                SqlParameter last_name_param = new SqlParameter(@"last_name", SqlDbType.Text, 20);
                SqlParameter city_param = new SqlParameter(@"city", SqlDbType.Text, 20);
                SqlParameter country_param = new SqlParameter(@"country", SqlDbType.Text, 20);
                SqlParameter phone_param = new SqlParameter(@"phone", SqlDbType.Text, 20);

                first_name_param.Value = "Edsger";
                last_name_param.Value = "Dijkstra";
                city_param.Value = "Amsterdam";
                country_param.Value = "Netherlands";
                phone_param.Value = "054-8965478";
                command.Parameters.Add(first_name_param);
                command.Parameters.Add(last_name_param);
                command.Parameters.Add(city_param);
                command.Parameters.Add(country_param);
                command.Parameters.Add(phone_param);

                // Call Prepare after setting the Commandtext and Parameters.
                command.Prepare();
                int num_rows_changed = command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Console.WriteLine($"ExecuteNonQuery in SqlCommand executed!! {num_rows_changed.ToString()} row was changes\\inserted");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());

            }
            Console.ReadKey();
        }
    }
}