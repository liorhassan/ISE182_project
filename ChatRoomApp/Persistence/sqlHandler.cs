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

        public void test()
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
                    $"INSERT INTO {usrTblName} ([Group_Id],[Nickname],[Password])" +
                    "VALUES (@groupid,@nick,@pass)";
                SqlParameter groupid = new SqlParameter(@"groupid", SqlDbType.Int, 20);
                SqlParameter nick = new SqlParameter(@"nick", SqlDbType.Text, 20);
                SqlParameter pass = new SqlParameter(@"pass", SqlDbType.Text, 20);

                groupid.Value = 1;
                nick.Value = "asd";
                pass.Value = "asdf";
                command.Parameters.Add(groupid);
                command.Parameters.Add(nick);
                command.Parameters.Add(pass);

                // Call Prepare after setting the Commandtext and Parameters.
                command.Prepare();
                command.ExecuteNonQuery();   
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());

            }
        }

        public Boolean userExists(String nickname, String gid)
        {
            Boolean output = false;
            String sql_query = $"select * from {usrTblName} where Group_Id = {gid} AND Nickname = '{nickname}';";

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
                if (data_reader.HasRows) output = true;
                data_reader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());
            }
            return output;
        }

        public void editMessage(Guid mid, String cont)
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
                    $"UPDATE {msgTblName} SET Body = @cont where Guid = &mid;";
                SqlParameter body = new SqlParameter(@"cont", SqlDbType.Int, 20);
                SqlParameter guid = new SqlParameter(@"mid", SqlDbType.Text, 20);

                body.Value = cont;
                guid.Value = mid;
                command.Parameters.Add(body);
                command.Parameters.Add(guid);

                // Call Prepare after setting the Commandtext and Parameters.
                command.Prepare();
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());

            }
        }

        public Boolean isOwner(Guid mid,String uid)
        {
            Boolean output = false;
            String sql_query = $"select * from {msgTblName} where Guid = {mid} AND User_Id = {uid};";

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
                if (data_reader.HasRows) output = true;
                data_reader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());
            }
            return output;
        }

        public int loginUser(String nickname, String gid,String pass)
        {
            int output=-1;
            String sql_query = $"select Id from {usrTblName} where Group_Id = {gid} AND Nickname = '{nickname}' AND Password = '{pass}';";

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
                if (data_reader.Read()) output = data_reader.GetInt32(0);
                data_reader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());
            }
            return output;
        }


        public void registerUser(String userNick, String userGid, String userPass)
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
                    $"INSERT INTO {usrTblName} ([Group_Id],[Nickname],[Password])" +
                    "VALUES (@groupid,@nick,@pass)";
                SqlParameter groupid = new SqlParameter(@"groupid", SqlDbType.Int, 20);
                SqlParameter nick = new SqlParameter(@"nick", SqlDbType.Text, 20);
                SqlParameter pass = new SqlParameter(@"pass", SqlDbType.Text, 100);

                groupid.Value = Int32.Parse(userGid);
                nick.Value = userNick;
                pass.Value = userPass;
                command.Parameters.Add(groupid);
                command.Parameters.Add(nick);
                command.Parameters.Add(pass);
                

                // Call Prepare after setting the Commandtext and Parameters.
                command.Prepare();
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());

            }
        }


        public List<IMessage> retriveAllMessages(String gid, String nickname)
        {
            List<IMessage> output = new List<IMessage>();
            String sql_query = $"select top 200 {usrTblName}.Group_Id, {usrTblName}.Nickname, {msgTblName}.SendTime, {msgTblName}.Body, {msgTblName}.Guid from {msgTblName} left join {usrTblName} on {msgTblName}.User_Id={usrTblName}.Id";
            if (gid.Equals("")) sql_query += ";";
            else if (nickname.Equals("")) sql_query += $" where {usrTblName}.Group_Id = {gid};";
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
                    output.Add(new DAMessage(Guid.Parse(data_reader.GetValue(4).ToString()), data_reader.GetValue(0).ToString(), data_reader.GetValue(1).ToString(), data_reader.GetValue(3).ToString(), dateFacturation));

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
                    output.Add(new DAMessage(Guid.Parse(data_reader.GetValue(4).ToString()), data_reader.GetValue(0).ToString(), data_reader.GetValue(1).ToString(), data_reader.GetValue(3).ToString(), dateFacturation));

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
        
        public void sendMessage(String uid,String content)
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
                    $"INSERT INTO {msgTblName} ([User_Id],[SendTime],[Body]" +
                    "VALUES (@usrid,@time,@cont)";
                SqlParameter usrid = new SqlParameter(@"usrid", SqlDbType.Int, 20);
                SqlParameter time = new SqlParameter(@"time", SqlDbType.Date, 20);
                SqlParameter cont = new SqlParameter(@"cont", SqlDbType.Text, 120);

                usrid.Value = uid;
                time.Value = DateTime.Now.ToUniversalTime();
                cont.Value = content;
                command.Parameters.Add(usrid);
                command.Parameters.Add(time);
                command.Parameters.Add(cont);

                // Call Prepare after setting the Commandtext and Parameters.
                command.Prepare();
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());

            }
        }

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
                this.UserName = userName.Trim(' ');
                this.Date = utcTime;
                this.MessageContent = messageContent.Trim(' ');
                this.GroupID = groupId.Trim(' ');
            }
            public String ToString()
            {
                return Date.ToString() + " - " + GroupID + " - " + UserName + " - " + MessageContent;
            }
        }
    }
}
