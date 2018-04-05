using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;

namespace BusinessLogic
{
    public class Chatroom
    {
        private User _loggedinUser;
        private Dictionary<Message, Guid> recievedMessages;
        private Dictionary<User, String> registeredUsers;
        private String URL;
        private MessagesHandler messHandler;
        private UsersHandler usersHandler;
       // private Logger logger;
        private ChatroomMenu _ChatroomMenu;
        public ChatroomMenu ChatroomMenu { get => _ChatroomMenu; }

        public Chatroom()
        {
            this._loggedinUser = null;
            this.recievedMessages = new Dictionary<Message, Guid>();
            this.registeredUsers = new Dictionary<User, String>();
            this.URL = "url";
            this.messHandler = new MessagesHandler();
            this.usersHandler = new UsersHandler();
          //  this.log = new Logger();
            this._ChatroomMenu = new ChatroomMenu();
        }

        public Boolean Register(String nickname)
        {
            int loop = -1;
            while (loop == -1)
            {
                User userOne = FindUser(nickname);
                if (userOne != null)
                {
                    Console.WriteLine("Nickname already exists, choose another one");
                    nickname = Console.ReadLine();
                }
                else
                {
                    loop = 0;
                }  
            }
            User newUser = new User(nickname);
            registeredUsers.Add(newUser, newUser.Nickname);
            return true;
        }

        public Boolean Login(String nickname)
        {
            User user = FindUser(nickname);

            if (user.Nickname==nickname)
            {
                this._loggedinUser = user;
                ChatroomMenu.Login = true;
                return true;
            }
            return false;
        }

        public Boolean Logout()
        {
            if (this._loggedinUser != null)
            {
                this._loggedinUser = null;
                ChatroomMenu.Login = false;
                return true;
            }
            return false;
        }

        public int Retrieve10Messages()
        {
            return 10;   
            // return _loggedinUser.retrive10Messages(this.URL);
        }

        public List<String> Retrieve20Messages()
        {
            List<String> msg;
            var messages =
                from m in recievedMessages
                orderby m.Key.Date
                select m.Key.MessageContent;
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
                where m.Key.User == user
                orderby m.Key.Date
                select m.Key.MessageContent;
            msg = (List<String>)messages;
            return msg;
        }

        public Boolean WriteMessage(String msg, String url)
        {
            return true;
            //return _loggedinUser.writeMessage(msg, this.URL);
        }

        public ChatroomMenu GetMenu()
        {
            return ChatroomMenu.get();
        }

        private User FindUser(String nickname)
        {
            var user =
                from u in registeredUsers
                where u.Key.Nickname == nickname
                select u.Key;
            return (User)user;
        }
    }
}
