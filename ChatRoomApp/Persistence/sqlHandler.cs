using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationLayer;
using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
    
    public class sqlHandler
    {
        private readonly String url = "ise172.ise.bgu.ac.il,1433\\DB_LAB";
        private readonly String dbName = "MS3";
        private readonly String username = "publicUser";
        private readonly String password = "isANerd";
        private readonly String msgTblName = "Messages";
        private readonly String usrTblName = "Users";
        private DateTime lastUpdate;

        public List<IMessage> retriveAllMessages(String gid, String nickname)
        {
            List<IMessage> output = new List<IMessage>();
            String sql_query = $"select top 200 {usrTblName}.Group_Id, {usrTblName}.Nickname, {msgTblName}.SendTime, {msgTblName}.Body, {msgTblName}.Guid from {msgTblName} left join {usrTblName} on {msgTblName}.User_Id={usrTblName}.Id";
            if (gid == "") sql_query += ";";
            else if (nickname == "") sql_query += $" where {usrTblName}.Group_Id = {gid};";
            else sql_query += $" where {usrTblName}.Group_Id = {gid} AND {usrTblName}.Nickname = {nickname};";

            SqlConnection connection;
            SqlCommand command;

            String connetion_string = $"Data Source={url};Initial Catalog={dbName };User ID={username};Password={password}";
            connection = new SqlConnection(connetion_string);
            SqlDataReader data_reader;

            try
            {
                connection.Open();
                command = new SqlCommand(sql_query, connection);
                data_reader = command.ExecuteReader();
                while (data_reader.Read())
                {
                    DateTime dateFacturation = new DateTime();
                    if (!data_reader.IsDBNull(2))
                        dateFacturation = data_reader.GetDateTime(2);
                    output.Add(new DAMessage(Guid.Parse(data_reader.GetValue(0).ToString()), data_reader.GetValue(0).ToString(), data_reader.GetValue(1).ToString(), data_reader.GetValue(3).ToString(), dateFacturation));

                }
                data_reader.Close();
                command.Dispose();
                connection.Close();

                lastUpdate = DateTime.Now.ToUniversalTime();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());
            }

            return output;
        }

        public List<IMessage> retriveNewMessages(String gid, String nickname)
        {

            List<IMessage> output = new List<IMessage>();
            String sql_query = $"select top 200 {usrTblName}.Group_Id, {usrTblName}.Nickname, {msgTblName}.SendTime, {msgTblName}.Body, {msgTblName}.Guid from {msgTblName} left join {usrTblName} on {msgTblName}.User_Id={usrTblName}.Id Where {msgTblName}.SendTime >= {UpdateSql()}";
            if (gid == "") sql_query += ";";
            else if (nickname == "") sql_query += $" AND {usrTblName}.Group_Id = {gid};";
            else sql_query += $" AND {usrTblName}.Group_Id = {gid} AND {usrTblName}.Nickname = {nickname};";

            SqlConnection connection;
            SqlCommand command;

            String connetion_string = $"Data Source={url};Initial Catalog={dbName };User ID={username};Password={password}";
            connection = new SqlConnection(connetion_string);
            SqlDataReader data_reader;

            try
            {
                connection.Open();
                command = new SqlCommand(sql_query, connection);
                data_reader = command.ExecuteReader();
                while (data_reader.Read())
                {
                    DateTime dateFacturation = new DateTime();
                    if (!data_reader.IsDBNull(2))
                        dateFacturation = data_reader.GetDateTime(2);
                    output.Add(new DAMessage(Guid.Parse(data_reader.GetValue(0).ToString()), data_reader.GetValue(0).ToString(), data_reader.GetValue(1).ToString(), data_reader.GetValue(3).ToString(), dateFacturation));

                }
                data_reader.Close();
                command.Dispose();
                connection.Close();

                lastUpdate = DateTime.Now.ToUniversalTime();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());
            }

            return output;
        }

        private String UpdateSql()
        {
            return $"'{lastUpdate.Year}-{lastUpdate.Month}-{lastUpdate.Day} {lastUpdate.Hour}:{lastUpdate.Minute}:{lastUpdate.Second}.000'";
        }

        /**
        public void sendMessage(IMessage msg)
        {
            SqlConnection connection;
            SqlCommand command;

            String connetion_string = $"Data Source={url};Initial Catalog={dbName };User ID={username};Password={password}";
            connection = new SqlConnection(connetion_string);

            try
            {
                connection.Open();
                command = new SqlCommand(null, connection);

                // Create and prepare an SQL statement.
                // Use should never use something like: query = "insert into table values(" + value + ");" 
                // Especially when selecting. More about it on the lab about security.
                command.CommandText =
                    $"INSERT INTO {msgTblName} ([Guid],[User_Id],[SendTime],[Body]" +
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
        **/

        private sealed class DAMessage : IMessage
        {
            public Guid Id { get; }
            public string UserName { get; }
            public DateTime Date { get; }
            public string MessageContent { get; }
            public string GroupID { get; }
            public DAMessage(Guid id = new Guid(), string groupId = "", string userName = "", string messageContent = "", DateTime utcTime=new DateTime())
            {
                this.Id = id;
                this.UserName = userName;
                this.Date = utcTime;
                this.MessageContent = messageContent;
                this.GroupID = groupId;
            }

        }
    }
}
