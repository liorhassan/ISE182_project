using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        //user for finding if a user allready exists
        public Boolean userExists(String nickname, String gid)
        {
            Boolean output = false;
            

            SqlConnection connection;
            SqlCommand command;

            String connetion_string = $"Data Source={url};Initial Catalog={dbName };User ID={username};Password={password}";
            connection = new SqlConnection(connetion_string);
            SqlDataReader data_reader;

            try
            {
                connection.Open();
                command = new SqlCommand(null, connection);
                command.CommandText = $"select * from {usrTblName} where Group_Id = @gid AND Nickname = @nickname;";
                SqlParameter groupid = new SqlParameter(@"gid", SqlDbType.Int, 100);
                SqlParameter nick = new SqlParameter(@"nickname", SqlDbType.Text, 100);

                nick.Value = nickname;
                groupid.Value = gid;
                command.Parameters.Add(nick);
                command.Parameters.Add(groupid);

                // Call Prepare after

                command.Prepare();
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

        //function for editing a message using its guid
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
                command.CommandText =
                    $"UPDATE {msgTblName} SET Body = @cont , [SendTime] = @time where Guid = @guid;";
                SqlParameter body = new SqlParameter(@"cont", SqlDbType.Text, 100);
                SqlParameter guid = new SqlParameter(@"guid", SqlDbType.UniqueIdentifier, 100);
                SqlParameter time = new SqlParameter(@"time", SqlDbType.DateTime, 100);

                body.Value = cont;
                guid.Value = mid;
                time.Value = DateTime.Now.ToUniversalTime();
                command.Parameters.Add(body);
                command.Parameters.Add(guid);
                command.Parameters.Add(time);

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

        //findes if the user id is the owner of the message id
        public Boolean isOwner(Guid mid,String uid)
        {
            Boolean output = false;
            
            
            SqlConnection connection;
            SqlCommand command;

            String connetion_string = $"Data Source={url};Initial Catalog={dbName };User ID={username};Password={password}";
            connection = new SqlConnection(connetion_string);
            SqlDataReader data_reader;

            try
            {
                connection.Open();
                command = new SqlCommand(null, connection);

                command.CommandText = $"select * from {msgTblName} where Guid = @guid AND User_Id = @uid;";

                SqlParameter guid = new SqlParameter(@"guid", SqlDbType.UniqueIdentifier, 100);
                SqlParameter usid = new SqlParameter(@"uid", SqlDbType.Int, 100);

                guid.Value = mid;
                usid.Value = uid;
                command.Parameters.Add(guid);
                command.Parameters.Add(usid);

                // Call Prepare after setting the Commandtext and Parameters.
                command.Prepare();

                data_reader = command.ExecuteReader();
                if (data_reader.Read()) output = true;
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

        // gets the user detailes and returns its id or -1 if it doesnt exists
        public int loginUser(String nickname, String gid,String pass)
        {
            int output=-1;
            

            SqlConnection connection;
            SqlCommand command;

            String connetion_string = $"Data Source={url};Initial Catalog={dbName };User ID={username};Password={password}";
            connection = new SqlConnection(connetion_string);
            SqlDataReader data_reader;

            try
            {
                connection.Open();
                command = new SqlCommand(null, connection);
                command.CommandText = $"select Id from {usrTblName} where Group_Id = @groupid AND Nickname = @nick AND Password = @passw;";

                SqlParameter groupid = new SqlParameter(@"groupid", SqlDbType.Int, 20);
                SqlParameter nick = new SqlParameter(@"nick", SqlDbType.Text, 100);
                SqlParameter passw = new SqlParameter(@"passw", SqlDbType.Text, 100);

                groupid.Value = gid;
                nick.Value = nickname;
                passw.Value = pass;
                command.Parameters.Add(groupid);
                command.Parameters.Add(nick);
                command.Parameters.Add(passw);

                // Call Prepare after setting the Commandtext and Parameters.
                command.Prepare();
                Console.WriteLine(command.ToString());
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

        //creates a new user with the given parameters
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

        //return the last 200 messages from the server
        public List<IMessage> retriveAllMessages(String gid, String nickname)
        {
            List<IMessage> output = new List<IMessage>();
            
            SqlConnection connection;
            SqlCommand command;

            String connetion_string = $"Data Source={url};Initial Catalog={dbName };User ID={username};Password={password}";
            connection = new SqlConnection(connetion_string);
            SqlDataReader data_reader;

            try
            {
                connection.Open();
                command = new SqlCommand(null, connection);

                command.CommandText = $"select top 200 {usrTblName}.Group_Id, {usrTblName}.Nickname, {msgTblName}.SendTime, {msgTblName}.Body, {msgTblName}.Guid from {msgTblName} left join {usrTblName} on {msgTblName}.User_Id={usrTblName}.Id";
                if (gid.Equals("")) command.CommandText += ";";
                else if (nickname.Equals("")) command.CommandText += $" where {usrTblName}.Group_Id = @groupid;";
                else command.CommandText += $" where {usrTblName}.Group_Id = @groupid AND {usrTblName}.Nickname = @nick;";


                SqlParameter groupid = new SqlParameter(@"groupid", SqlDbType.Int, 20);
                SqlParameter nick = new SqlParameter(@"nick", SqlDbType.Text, 20);

                groupid.Value = Int32.Parse(gid);
                nick.Value = nickname;
                command.Parameters.Add(groupid);
                command.Parameters.Add(nick);

                command.Prepare();
                data_reader = command.ExecuteReader();
                while (data_reader.Read())
                {
                    DateTime dateFacturation = new DateTime();
                    if (!data_reader.IsDBNull(2))
                        dateFacturation = data_reader.GetDateTime(2);
                    Guid gu = new Guid();
                    if (Guid.TryParse(data_reader.GetValue(4).ToString(), out gu))
                        output.Add(new DAMessage(gu, data_reader.GetValue(0).ToString(), data_reader.GetValue(1).ToString(), data_reader.GetValue(3).ToString(), dateFacturation));

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

        //return all the messages that were sent after the last update
        public List<IMessage> retriveNewMessages(String gid, String nickname)
        {

            List<IMessage> output = new List<IMessage>();
            
            SqlConnection connection;
            SqlCommand command;

            String connetion_string = $"Data Source={url};Initial Catalog={dbName };User ID={username};Password={password}";
            connection = new SqlConnection(connetion_string);
            SqlDataReader data_reader;

            try
            {
                connection.Open();
                command = new SqlCommand(null, connection);

                command.CommandText = $"select top 200 {usrTblName}.Group_Id, {usrTblName}.Nickname, {msgTblName}.SendTime, {msgTblName}.Body, {msgTblName}.Guid from {msgTblName} left join {usrTblName} on {msgTblName}.User_Id={usrTblName}.Id Where {msgTblName}.SendTime > '{UpdateSql()}' AND {msgTblName}.SendTime<='{NextSql()}'";
                if (gid.Equals("")) command.CommandText += ";";
                else if (nickname.Equals("")) command.CommandText += $" AND {usrTblName}.Group_Id = @groupid;";
                else command.CommandText += $" AND {usrTblName}.Group_Id = @groupid AND {usrTblName}.Nickname = @nick;";


                SqlParameter groupid = new SqlParameter(@"groupid", SqlDbType.Int, 20);
                SqlParameter nick = new SqlParameter(@"nick", SqlDbType.Text, 20);

                groupid.Value = Int32.Parse(gid);
                nick.Value = nickname;
                command.Parameters.Add(groupid);
                command.Parameters.Add(nick);

                command.Prepare();

                data_reader = command.ExecuteReader();
                while (data_reader.Read())
                {
                    DateTime dateFacturation = new DateTime();
                    if (!data_reader.IsDBNull(2))
                        dateFacturation = data_reader.GetDateTime(2);
                    Guid gu = new Guid();
                    if (Guid.TryParse(data_reader.GetValue(4).ToString(), out gu))
                        output.Add(new DAMessage(gu, data_reader.GetValue(0).ToString(), data_reader.GetValue(1).ToString(), data_reader.GetValue(3).ToString(), dateFacturation));
                    

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

        //private functions for the newmessages functions sql statment
        private String UpdateSql()
        {
            return lastUpdate.ToString("yyyy-MM-dd HH:mm:ss.fff");
           
        }
        private String NextSql()
        {
            return lastUpdate.AddSeconds(2).ToString("yyyy-MM-dd HH:mm:ss.fff");

        }

        //sends a new message to the server with the given params
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
                
                command.CommandText =
                    $"INSERT INTO {msgTblName} ([Guid],[User_Id],[SendTime],[Body])" +
                    "VALUES (@guid,@usrid,@time,@cont)";
                SqlParameter guid = new SqlParameter(@"guid", SqlDbType.UniqueIdentifier, 100);
                SqlParameter usrid = new SqlParameter(@"usrid", SqlDbType.Int, 20);
                SqlParameter time = new SqlParameter(@"time", SqlDbType.DateTime, 20);
                SqlParameter cont = new SqlParameter(@"cont", SqlDbType.Text, 100);

                guid.Value = Guid.NewGuid();
                usrid.Value = uid;
                time.Value = DateTime.Now.ToUniversalTime();
                cont.Value = content;
                command.Parameters.Add(guid);
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

        //private class for the sqlHandler that implements the IMessage interface
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
                return Date.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff") + " - " + GroupID + " - " + UserName + " - " + MessageContent;
            }
        }
    }
}
