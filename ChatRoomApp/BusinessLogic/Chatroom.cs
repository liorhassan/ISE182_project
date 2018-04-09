using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStoneClient.CommunicationLayer;
using Persistence;

namespace BusinessLogic
{
    public class Chatroom
    {
        private User _loggedinUser;
        private Dictionary<Guid, IMessage> recievedMessages;
        private Dictionary<String, User> registeredUsers;
        private String URL;
        private MessagesHandler messHandler;
        private UsersHandler usersHandler;
        private Logger mLogger;
        private FileLogger mFileLogger;
        private ChatroomMenu _ChatroomMenu;
        public ChatroomMenu ChatroomMenu { get => _ChatroomMenu; }

        public Chatroom()
        {
            this.messHandler = new MessagesHandler();
            this.usersHandler = new UsersHandler();
            this._loggedinUser = null;
            messHandler = new MessagesHandler("dgfgdf");
            this.recievedMessages = (Dictionary < Guid, IMessage >) messHandler.load();
            this.registeredUsers = (Dictionary<String, User>)usersHandler.load();
            this.URL = "C:\\Users\\Ohad\\SE-Intro_server-master\\server.py";
            this.mLogger = Logger.Instance;
            this.mFileLogger = new FileLogger
                (@"C:\Users\Ohad\Documents\GitHub\ChatRoom24\ChatRoomApp\log.txt");
            this._ChatroomMenu = new ChatroomMenu();
        }

        public Boolean Register(String nickname)
        {
            User userOne = FindUser(nickname);
            if (userOne != null)
            {
                return false;
            }
            User newUser = new User(nickname);
            registeredUsers.Add(newUser.Nickname, newUser);
            usersHandler.save(registeredUsers);
            mLogger.AddLogMessage("User " + newUser.Nickname + " registered successfully");
            return true;
        }

        public Boolean Login(String nickname)
        {
            User user = FindUser(nickname);

            if (user.Nickname==nickname)
            {
                this._loggedinUser = user;
                ChatroomMenu.Login = true;
                mLogger.AddLogMessage("User " + user.Nickname + " logged in successfully");
                return true;
            }
            return false;
        }

        public Boolean Logout()
        {
            String name = _loggedinUser.Nickname;
            if (this._loggedinUser != null)
            {
                this._loggedinUser = null;
                ChatroomMenu.Login = false;
                mLogger.AddLogMessage("User " + name + " logged out successfully");
                return true;
            }
            return false;
        }

        public int Retrieve10Messages()
        {
            int c = 0;
            List<MileStoneClient.CommunicationLayer.IMessage> retrieved = 
                _loggedinUser.retrive10Messages(URL);
            foreach (IMessage m in retrieved)
            {
                if (!recievedMessages.ContainsKey(m.Id))
                {
                    recievedMessages.Add(m.Id, m);
                    c++;
                }
            }
            messHandler.save(recievedMessages);
            return c;   
        }

        public List<String> Retrieve20Messages()
        {
            List<String> msg;
            var messages =
                from m in recievedMessages
                orderby m.Value.Date
                select m.Value.MessageContent;
            if (messages.Count() > 20)
            {
                msg = (List<String>)messages.Take(20);
                return msg;
            }
            else
            {
                msg = (List<String>)messages;
                return msg;
            }
            
        }

        public List<String> RetrieveAllByUser(String nickname)
        {
            User user = FindUser(nickname);
            if (user==null)
            {
                throw new System.ArgumentException("No such user");
            }
            List<String> msg;
            var messages =
                from m in recievedMessages
                where m.Value.User == user
                orderby m.Value.Date
                select m.Value.MessageContent;
            msg = (List<String>)messages;
            return msg;
        }

        public Boolean WriteMessage(String msg, String url)
        {
            IMessage message = _loggedinUser.writeMessage(msg, this.URL);
            if (!message.CheckValidity(message.MessageContent))
            {
                mLogger.AddLogMessage("Invalid message was written");
                return false;
            }
            recievedMessages.Add(message.Id, message);
            messHandler.save(recievedMessages);
            mLogger.AddLogMessage("Message " + message.Id + " was written successfully");
            return true;
        }

        public ChatroomMenu GetMenu()
        {
            return ChatroomMenu.get();
        }

        private User FindUser(String nickname)
        {
            if (registeredUsers.ContainsKey(nickname))
            {
                var user =
                from u in registeredUsers
                where u.Value.Nickname == nickname
                select u.Value;
                return (User)user;
            }
            else
            {
                return null;
            }
        }
    }
}
