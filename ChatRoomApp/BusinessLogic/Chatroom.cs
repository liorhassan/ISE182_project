using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Chatroom
    {
        private User _loggedinUser;
        public User loggedinUser { get => _loggedinUser; }
        private Dictionary<Message, Guid> recievedMessages;
        private Dictionary<User, String> registeredUsers;
        private String URL;
        private messagesHandler messHandler;
        private usersHandler usersHandler;
        private xmlHandler xmlHandler;
        private Logger log;
        private ChatroomMenu _ChatroomMenu;
        public ChatroomMenu ChatroomMenu { get => _ChatroomMenu; }

        public Chatroom()
        {
            this.loggedinUser = null;
            this.recievedMessages = new Dictionary<Message, Guid>();
            this.registeredUsers = new Dictionary<User, String>();
            this.URL = "url";
            this.messHandler = new messagesHandler();
            this.usersHandler = new usersHandler();
            this.xmlHandler = new xmlHandler();
            this.log = new Logger();
            this._ChatroomMenu = new ChatroomMenu();
        }

        public Boolean Register(String nickname, int groupID)
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

            int loop = -1;
            while (loop == -1)
            {
                User userTwo = findUser(nickname);
                if (user != null)
                {
                    Console.WriteLine("Nickname already exists, choose another one");
                    nickname = Console.ReadLine();
                }
                else
                {
                    loop = 0;
                }  
            }
            User newUser = new User(nickname, groupID);
            registeredUsers.Add(User);
            return true;
        }

        public Boolean Login(String nickname)
        {
            User user = FindUser(nickname);

            if (user.nickname==nickname)
            {
                this.loggedinUser = user;
                ChatroomMenu.Login = true;
                return true;
            }
            return false;
        }

        public Boolean Logout()
        {
            if (this.loggedinUser != null)
            {
                this.loggedinUser = null;
                ChatroomMenu.Login = false;
                return true;
            }
            return false;
        }

        public int Retrieve10Messages()
        {
            return _loggedinUser.get.retrieve10Messages();
        }

        public String Retrieve20Messages()
        {
            return;
        }

        public String Retrieve20Messages()
        {
            var messages =
                from m in recievedMessages
                where m.Key.user == user
                select m;
        }

        public String RetrieveAllByUser(String nickname)
        {
            User user = FindUser(nickname);
            if (user==null)
            {
                throw new System.ArgumentException("No such user");
            }
            var messages =
                from m in recievedMessages
                where m.Key.user == user
                select m;
            var newMessages = messages.OrderBy(m => m.Date).ToList();
            return newMessages;
        }

        public Boolean WriteMessage(string msg)
        {
            return _loggedinUser.get.WriteMessage(msg);
        }

        public ChatroomMenu GetMenu()
        {
            return ChatroomMenu.get();
        }

        private User FindUser(String nickname)
        {
            var user =
                from u in registeredUsers
                where u.Key.nickname == nickname
                select u;
            return user;
        }
    }
}
