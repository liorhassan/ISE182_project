using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationLayer;
using Persistence;
using System.Windows.Threading;
namespace BusinessLogic
{
    public class Chatroom : ILogger
    {
        private int sortType;
        private DispatcherTimer dispatcherTimer;
        private User _loggedinUser;
        private Dictionary<Guid, Message> recievedMessages;
        private Dictionary<String, User> registeredUsers;
        private readonly String URL = "http://ise172.ise.bgu.ac.il";
        private MessagesHandler messHandler;
        private UsersHandler usersHandler;
        private Logger mLogger;
        private FileLogger mFileLogger;
        private ChatroomMenu _ChatroomMenu;
        public ChatroomMenu ChatroomMenu { get => _ChatroomMenu; }

        // a class for the chatroom
        // constructor assigns handlers, loggers, adds content to dictionaries from handlers
        public Chatroom()
        {
            SortType = 0;
            messHandler = new MessagesHandler();
            usersHandler = new UsersHandler();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            this._loggedinUser = null;
            recievedMessages = (Dictionary<Guid, Message>)messHandler.load();
            if (recievedMessages == null)
            {
                recievedMessages = new Dictionary<Guid, Message>();
                messHandler.save(recievedMessages);
            }
            registeredUsers = (Dictionary<String, User>)usersHandler.load();
            if (registeredUsers == null)
            {
                registeredUsers = new Dictionary<String, User>();
                usersHandler.save(registeredUsers);
            }
            this.mLogger = Logger.Instance;
            this.mFileLogger = new FileLogger("log.txt");
            mFileLogger.Init();
            mLogger.RegisterObserver(this);
            mLogger.RegisterObserver(mFileLogger);
            this._ChatroomMenu = new ChatroomMenu();
        }
        public int SortType
        {
            set
            {
                sortType = value;
            }
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            //Retrieve10Messages();
            return;
        }

        // a function that registers a user
        // doesn't do anything if a user with that nickname exists
        // creates a new user and adds to registered users
        public Boolean Register(String nickname)
        {
            if (registeredUsers.ContainsKey(nickname))
            {
                return false;
            }
            User newUser = new User(nickname);
            registeredUsers.Add(newUser.Nickname, newUser);
            usersHandler.save(registeredUsers);
            mLogger.AddLogMessage("User " + newUser.Nickname + " registered successfully");
            return true;
        }

        // a fuction to login a user
        // finds the user and makes the loggedinUser
        // does nothing if the user doesn't exist
        public Boolean Login(String nickname)
        {
            if (registeredUsers.ContainsKey(nickname))
            {
                User user = registeredUsers[nickname];
                this._loggedinUser = user;
                ChatroomMenu.Login = true;
                mLogger.AddLogMessage("User " + user.Nickname + " logged in successfully");
                dispatcherTimer.Start();
                return true;
            }
            return false;
        }

        // a fuction to logout the loggedinUser
        // checks if the loggedinUser is not null, and then turns it to null
        // else does nothing
        public Boolean Logout()
        {   
            if (this._loggedinUser != null)
            {
                String name = _loggedinUser.Nickname;
                this._loggedinUser = null;
                ChatroomMenu.Login = false;
                mLogger.AddLogMessage("User " + name + " logged out successfully");
                dispatcherTimer.Stop();
                return true;
            }
            return false;
        }

        // a fuction to retrieve 10 messages from the server
        // calls the fuction from the loggedinUser
        // adds the new messages to recievedMessages
        // returns the number of new messages added
        public int Retrieve10Messages()
        {
            int c = 0;
            foreach (IMessage m in _loggedinUser.retrive10Messages(URL))
            {
                if (!recievedMessages.ContainsKey(m.Id))
                {
                    recievedMessages.Add(m.Id, new Message(m));
                    c++;
                }
            }
            messHandler.save(recievedMessages);
            return c;   
        }

        // a fuction to retrieve 20 messages from the dictionary
        public List<Message> Retrieve20Messages()
        {
            var messages =
                (from m in recievedMessages
                orderby m.Value.Date
                select m.Value).Take(20);
            return messages.ToList();  
        }

        // a fuction to retrieve all the messages from a user
        public List<Message> RetrieveAllByUser(String nickname, String g_id)
        {
            var messages =
                from m in recievedMessages
                where m.Value.UserName == nickname & m.Value.GroupID == g_id
                orderby m.Value.Date
                select m.Value;
            return messages.ToList();
        }

        // a fuction to retrieve all the messages by a GID
        public List<Message> RetrieveAllByGroup(String g_id)
        {
            var messages =
                from m in recievedMessages
                where m.Value.GroupID == g_id
                orderby m.Value.Date
                select m.Value;
            return messages.ToList();
        }

        // a fuction to sort the messages by the timestamp
        public List<String> SortByTimestamp(Boolean isAsc)
        {
            List<String> output = new List<string>();
            if (isAsc)
            {
                var messages =
                from m in recievedMessages
                orderby m.Value.Date ascending
                select m.Value;
                foreach (Message m in messages) output.Add(m.ToString());
            }
            else
            {
                var messages =
                from m in recievedMessages
                orderby m.Value.Date descending
                select m.Value;
                foreach (Message m in messages) output.Add(m.ToString());
            }
            return output;
            
        }

        // a fuction to sort the messages by nickname
        public List<Message> SortByNickname(Boolean isAsc)
        {
            if (isAsc)
            {
                var messages =
                from m in recievedMessages
                orderby m.Value.UserName ascending
                select m.Value;
                return messages.ToList();
            }
            else
            {
                var messages =
                from m in recievedMessages
                orderby m.Value.UserName descending
                select m.Value;
                return messages.ToList();
            }
        }

        // a fuction to sort the messages by g_id, nickname, and timestamp
        public List<Message> SortByAll(Boolean isAsc)
        {
            
            if (isAsc)
            {
                var messages =
                from m in recievedMessages
                orderby m.Value.GroupID, m.Value.UserName, m.Value.Date ascending
                select m.Value;
                return messages.ToList();
            }
            else
            {
                var messages =
                from m in recievedMessages
                orderby m.Value.GroupID, m.Value.UserName, m.Value.Date descending
                select m.Value;
                return messages.ToList();
            }

        }

        // a fuction to write a message
        // checks if it's valid
        // creates the message and adds it to the dictionary
        public Boolean WriteMessage(String msg)
        {
            if (!CheckMessageValidity(msg))
            {
                mLogger.AddLogMessage("Invalid message was written");
                return false;
            }
            Message message = new Message(_loggedinUser.writeMessage(msg, URL));
            recievedMessages.Add(message.Id, message);
            messHandler.save(recievedMessages);
            mLogger.AddLogMessage("Message " + message.Id + " was written successfully");
            return true;
        }

        // checks if a message is valid
        private Boolean CheckMessageValidity(String content)
        {
            if (content.Length > 150)
            {
                return false;
            }
            return true;
        }

        // exits chatroom
        public void exit()
        {
            mFileLogger.Terminate();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            int c = Retrieve10Messages();
            if (c != 0)
            {

            }
        }
        // to implement ILogger
        public void ProcessLogMessage(string message)
        {
            return;
        }


    }
}
