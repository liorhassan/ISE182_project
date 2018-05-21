using System;
using System.Collections.Generic;
using System.Linq;
using CommunicationLayer;
using Persistence;
using System.IO;

namespace BusinessLogic
{
    public class TestChatroom
    {
        private static String projectpath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        private int sortType;//0-timeStamp(default), 1-nickname, 2-everything
        private Boolean isAsc;//determain sort asc/desc
        private int filterType;// 0-no filter(default), 1- filter by groupID, 2 - filter by user
        private string userFilter; //user nickname to filter by
        private string groupFilter; //groupID nickname to filter by
        private User _loggedinUser;
        private Dictionary<Guid, Message> recievedMessages;
        private Dictionary<String, User> registeredUsers;
        private readonly String URL = "http://ise172.ise.bgu.ac.il";
        private MessagesHandler messHandler;
        private UsersHandler usersHandler;

        // a class for the chatroom
        // constructor assigns handlers, loggers, adds content to dictionaries from handlers
        public TestChatroom()
        {
            sortType = 0;
            filterType = 0;
            userFilter = "";
            groupFilter = "";
            isAsc = true;
            messHandler = new MessagesHandler();
            usersHandler = new UsersHandler();
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
        }
        // a function that registers a user
        // doesn't do anything if a user with that nickname exists
        // creates a new user and adds to registered users
        public Boolean Register(String nickname, String group)
        {
            String key = nickname + "@" + group;
            if (registeredUsers.ContainsKey(key))
            {
                return false;
            }
            User newUser = new User(nickname, group);
            registeredUsers.Add(key, newUser);
            usersHandler.save(registeredUsers);
            return true;
        }

        // a fuction to login a user
        // finds the user and makes the loggedinUser
        // does nothing if the user doesn't exist
        public Boolean Login(String nickname, String group)
        {
            String key = nickname + "@" + group;
            if (registeredUsers.ContainsKey(key))
            {
                User user = registeredUsers[key];
                this._loggedinUser = user;
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
                //ChatroomMenu.Login = false;
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
        public List<String> GetAllMessages()
        {
            List<Message> FilteredMessages;
            List<String> output;
            if (filterType == 0)
            {
                FilteredMessages = GetMessagesByAll();
            }
            else if (filterType == 1)
            {
                FilteredMessages = GetAllByGroup();
            }
            else
            {
                FilteredMessages = GetAllByUser();
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

        // a fuction to retrieve 20 messages from the dictionary
        public List<Message> GetMessagesByAll()
        {
            var messages =
                (from m in recievedMessages
                 orderby m.Value.Date
                 select m.Value);
            return messages.ToList();
        }


        // a fuction to retrieve all the messages from a user
        public List<Message> GetAllByUser()
        {
            var messages =
                from m in recievedMessages
                where m.Value.UserName == userFilter & m.Value.GroupID == groupFilter
                orderby m.Value.Date
                select m.Value;
            return messages.ToList();
        }

        // a fuction to retrieve all the messages by a GID
        public List<Message> GetAllByGroup()
        {
            var messages =
                from m in recievedMessages
                where m.Value.GroupID == groupFilter
                orderby m.Value.Date
                select m.Value;
            return messages.ToList();
        }

        // a fuction to sort the messages by the timestamp
        public List<String> SortByTimestamp(List<Message> filteredMessages)
        {
            if (isAsc)
            {
                var messages =
                from m in filteredMessages
                orderby m.Date ascending
                select m.ToString();
                return messages.ToList();
            }
            else
            {
                var messages =
                from m in filteredMessages
                orderby m.Date descending
                select m.ToString();
                return messages.ToList();
            }

        }

        // a fuction to sort the messages by nickname
        public List<String> SortByNickname(List<Message> filteredMessages)
        {
            if (isAsc)
            {
                var messages =
                from m in filteredMessages
                orderby m.UserName ascending
                select m.ToString();
                return messages.ToList();
            }
            else
            {
                var messages =
                from m in filteredMessages
                orderby m.UserName descending
                select m.ToString();
                return messages.ToList();
            }
        }

        // a fuction to sort the messages by g_id, nickname, and timestamp
        public List<String> SortByAll(List<Message> filteredMessages)
        {

            if (isAsc)
            {
                var messages =
                from m in filteredMessages
                orderby Convert.ToInt32(m.GroupID) ascending, m.UserName ascending, m.Date ascending
                select m.ToString();
                return messages.ToList();

            }
            else
            {
                var messages =
                from m in filteredMessages
                orderby Convert.ToInt32(m.GroupID) descending, m.UserName descending, m.Date descending
                select m.ToString();
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
                return -1;
            }
            Message message = new Message(_loggedinUser.writeMessage(msg, URL));
            recievedMessages.Add(message.Id, message);
            messHandler.save(recievedMessages);
            return 1;
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

        public void RestartChatroom()
        {
            Logout();
            recievedMessages.Clear();
            registeredUsers.Clear();
        }
    }
}
