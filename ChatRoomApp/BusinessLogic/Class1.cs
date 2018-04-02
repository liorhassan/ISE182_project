﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Chatroom
    {
        private User loggedinUser;
        private Dictionary<Message> recievedMessages;
        private Dictionary<User> registeredUsers;
        private String URL;
        private messagesHandler messHandler;
        private usersHandler usersHandler;
        private xmlHandler xmlHandler;
        private Logger log;

        public Chatroom()
        {
            this.loggedinUser = null;
            this.recievedMessages = new Dictionary<Message>();
            this.registeredUsers = new Dictionary<User>();
            this.URL = "url";
            this.messHandler = new messagesHandler();
            this.usersHandler = new usersHandler();
            this.xmlHandler = new xmlHandler();
            this.log = new Logger();
        }

        public boolean register(String nickname, int groupID)
        {
            var userOne =
                from u in registeredUsers
                where u.nickname == nickname && u.groupID == groupID
                select u;
            if (userOne != null)
            {
                Console.WriteLine("User already exists");
                return false;
            }
            var userTwo =
               from u in registeredUsers
               where u.nickname == nickname
               select u;
            if (user != null)
            {
                Console.WriteLine("Nickname already exists, choose another one");
                String nickname = Console.ReadLine();
            }
            int loop = -1;
            while (loop == -1)
            {
                
            }
            return true;
        }


    
    }
}
