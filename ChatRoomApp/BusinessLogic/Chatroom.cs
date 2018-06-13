using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using System.Windows.Threading;
using System.IO;

namespace BusinessLogic
{
    public class Chatroom : ILogger
    {
        private static String projectpath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        private int sortType;//0-timeStamp(default), 1-nickname, 2-everything
        private Boolean isAsc;//determain sort asc/desc
        private int filterType;// 0-no filter(default), 1- filter by groupID, 2 - filter by user
        private string userFilter; //user nickname to filter by
        private string groupFilter; //groupID nickname to filter by
        private int _loggedinUser; //the userID for the logged in user
        private Logger mLogger;
        private FileLogger mFileLogger;
        private sqlHandler _sqlHandler; //the sqlHandler used to get queries from the DB
        private List<Guid> MessageGuid; //stores the GUID's of the displayed messages in order
        // a class for the chatroom
        // constructor assigns handlers, loggers, adds content to dictionaries from handlers
        public Chatroom()
        {
            sortType = 0;
            filterType = 0;
            userFilter = "";
            groupFilter = "";
            isAsc = true;
            _sqlHandler = new sqlHandler();
            this._loggedinUser = -1;
            this.mLogger = Logger.Instance;
            String currpath = Directory.GetCurrentDirectory();
            this.mFileLogger = new FileLogger(projectpath + "\\Data\\log.txt");
            mFileLogger.Init();
            mLogger.RegisterObserver(this);
            mLogger.RegisterObserver(mFileLogger);
        }
        public int SortType
        {
            set
            {
                sortType = value;
            }
        }

        // a function that registers a user
        // doesn't do anything if a user with that nickname exists
        // creates a new user and adds to registered users
        public Boolean Register(String nickname, String group, string pass)
        {
            if (_sqlHandler.userExists(nickname, group))
            {
                return false;
            }
            pass = hashing.passwordToHash(pass);
            _sqlHandler.registerUser(nickname, group, pass);
            mLogger.AddLogMessage("User " + nickname + " in group " + group + " registered successfully");
            return true;
        }

        // a fuction to login a user
        // finds the user and makes the loggedinUser
        // does nothing if the user doesn't exist
        public Boolean Login(String nickname, String group, string pass)
        {
            pass = hashing.passwordToHash(pass);
            int userID = _sqlHandler.loginUser(nickname, group, pass);
            if (userID!=-1)
            {
                this._loggedinUser = userID;
                mLogger.AddLogMessage("User " + nickname + " logged in successfully");
                return true;
            }
            return false;
        }

        // a fuction to logout the loggedinUser
        // checks if the loggedinUser is not null, and then turns it to null
        // else does nothing
        public Boolean Logout()
        {
            if (this._loggedinUser != -1)
            {
                this._loggedinUser = -1;
                mLogger.AddLogMessage("The User has logged out successfully");
                return true;
            }
            return false;
        }


        //set filter and sort arguments givven by the presentation
        public void SetFilterAndSort(int sortType, int filterType, Boolean isAsc, string groupFilter, string userFilter)
        {
            this.sortType = sortType;
            this.filterType = filterType;
            this.isAsc = isAsc;
            this.groupFilter = groupFilter;
            this.userFilter = userFilter;
        }

        //return all the messages to be shown on the message panel, filtered and sorted as needed
        public List<String> GetAllMessages(bool all)
        {
            List<IMessage> FilteredMessages;
            List<String> output;
            if(all)
            {
                MessageGuid = new List<Guid>();
                if (filterType == 0)
                {
                    FilteredMessages = _sqlHandler.retriveAllMessages("", "");
                }
                else if (filterType == 1)
                {
                    FilteredMessages = _sqlHandler.retriveAllMessages(groupFilter, "");
                }
                else
                {
                    FilteredMessages = _sqlHandler.retriveAllMessages(groupFilter, userFilter);
                }
            }
            else
            {
                if (filterType == 0)
                {
                    FilteredMessages = _sqlHandler.retriveNewMessages("", "");
                }
                else if (filterType == 1)
                {
                    FilteredMessages = _sqlHandler.retriveNewMessages(groupFilter, "");
                }
                else
                {
                    FilteredMessages = _sqlHandler.retriveNewMessages(groupFilter, userFilter);
                }
                
                
            }
            if (sortType == 0)
            {
                output = SortByTimestamp(FilteredMessages);
            }
            else if (sortType == 1)
            {
                output = SortByNickname(FilteredMessages);
            }
            else
            {
                output = SortByAll(FilteredMessages);
            }
            return output;
        }

        // a fuction to sort the messages by the timestamp
        public List<String> SortByTimestamp(List<IMessage> filteredMessages)
        {
            if (isAsc)
            {
                var messages =
                from m in filteredMessages
                orderby m.Date ascending
                select m.ToString();

                var guid =
                from m in filteredMessages
                orderby m.Date ascending
                select m.Id;
                UpdateGUIDTable(guid.ToList());
                return messages.ToList();
            }
            else
            {
                var messages =
                from m in filteredMessages
                orderby m.Date descending
                select m.ToString();

                var guid =
                from m in filteredMessages
                orderby m.Date descending
                select m.Id;
                UpdateGUIDTable(guid.ToList());
                return messages.ToList();
            }

        }

        // a fuction to sort the messages by nickname
        public List<String> SortByNickname(List<IMessage> filteredMessages)
        {
            if (isAsc)
            {
                var messages =
                from m in filteredMessages
                orderby m.UserName ascending
                select m.ToString();

                var guid =
                from m in filteredMessages
                orderby m.UserName ascending
                select m.Id;
                UpdateGUIDTable(guid.ToList());

                return messages.ToList();
            }
            else
            {
                var messages =
                from m in filteredMessages
                orderby m.UserName descending
                select m.ToString();

                var guid =
                from m in filteredMessages
                orderby m.UserName descending
                select m.Id;
                UpdateGUIDTable(guid.ToList());

                return messages.ToList();
            }
        }

        // a fuction to sort the messages by g_id, nickname, and timestamp
        public List<String> SortByAll(List<IMessage> filteredMessages)
        {

            if (isAsc)
            {
                var messages =
                from m in filteredMessages
                orderby Convert.ToInt32(m.GroupID) ascending, m.UserName ascending, m.Date ascending
                select m.ToString();

                var guid =
                from m in filteredMessages
                orderby Convert.ToInt32(m.GroupID) ascending, m.UserName ascending, m.Date ascending
                select m.Id;
                UpdateGUIDTable(guid.ToList());

                return messages.ToList();

            }
            else
            {
                var messages =
                from m in filteredMessages
                orderby Convert.ToInt32(m.GroupID) descending, m.UserName descending, m.Date descending
                select m.ToString();

                var guid =
                from m in filteredMessages
                orderby Convert.ToInt32(m.GroupID) descending, m.UserName descending, m.Date descending
                select m.Id;
                UpdateGUIDTable(guid.ToList());

                return messages.ToList();
            }

        }

        // a fuction to write a message
        // checks if it's valid
        // creates the message and adds it to the dictionary
        public int WriteMessage(String msg)
        {
            if (!CheckMessageValidity(msg))
            {
                mLogger.AddLogMessage("Invalid message was written");
                return -1;
            }
          _sqlHandler.sendMessage(_loggedinUser.ToString(), msg);
            mLogger.AddLogMessage("New Message was written successfully");
            return 1;
        }
        // check message owner
        public Boolean isOwner(int index)
        {
            return _sqlHandler.isOwner(MessageGuid.ElementAt(index), _loggedinUser.ToString());
        }
        private void UpdateGUIDTable(List<Guid> list)
        {
            foreach (Guid g in list)
            {
                if (MessageGuid.Count == 200) MessageGuid.RemoveAt(0);
                MessageGuid.Add(g);
            }
        }

        //function for editing a message
        public void EditMesage(int index, String newMessage)
        {
            _sqlHandler.editMessage(MessageGuid.ElementAt(index), newMessage);
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


        // to implement ILogger
        public void ProcessLogMessage(string message)
        {
            return;
        }
        
        //chack validity of password
        public Boolean isPassValid(string pass)
        {
            if (pass.Length < 4)
                return false;
            bool validA = pass.All(c => Char.IsLetterOrDigit(c));
            return validA;

        }


    }
}
