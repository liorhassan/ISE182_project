﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationLayer;
using Persistence;

namespace BusinessLogic
{
    public class Chatroom : ILogger
    {
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

        public Chatroom()
        {
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
            this.mLogger = Logger.Instance;
            this.mFileLogger = new FileLogger
                ("log.txt");
            mFileLogger.Init();
            mLogger.RegisterObserver(this);
            mLogger.RegisterObserver(mFileLogger);
            this._ChatroomMenu = new ChatroomMenu();
        }


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

        public Boolean Login(String nickname)
        {
            

            if (registeredUsers.ContainsKey(nickname))
            {
                User user = registeredUsers[nickname];
                this._loggedinUser = user;
                ChatroomMenu.Login = true;
                mLogger.AddLogMessage("User " + user.Nickname + " logged in successfully");
                return true;
            }
            return false;
        }

        public Boolean Logout()
        {   
            if (this._loggedinUser != null)
            {
                String name = _loggedinUser.Nickname;
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

        public List<Message> Retrieve20Messages()
        {
            var messages =
                (from m in recievedMessages
                orderby m.Value.Date
                select m.Value).Take(20);
            return messages.ToList();  
        }

        public List<Message> RetrieveAllByUser(String nickname, String g_id)
        {
            var messages =
                from m in recievedMessages
                where m.Value.UserName == nickname & m.Value.GroupID == g_id
                orderby m.Value.Date
                select m.Value;
            return messages.ToList();
        }

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

        private Boolean CheckMessageValidity(String content)
        {
            if (content.Length > 150)
            {
                return false;
            }
            return true;
        }

        public void exit()
        {
            mFileLogger.Terminate();
        }
        public void ProcessLogMessage(string message)
        {
            return;
        }


    }
}
