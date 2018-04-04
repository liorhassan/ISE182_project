using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Chatroom
    {
        private User loggedinUser;
        private Dictionary<Message, Guid> recievedMessages;
        private Dictionary<User, String> registeredUsers;
        private String URL;
        private messagesHandler messHandler;
        private usersHandler usersHandler;
        private xmlHandler xmlHandler;
        private Logger log;

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
        }

        public Boolean register(String nickname, int groupID)
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
            User user = findUser(nickname);

            if (user.nickname==nickname)
            {
                this.loggedinUser = user;
                return true;
            }
            return false;
        }

        public Boolean Logout()
        {
            if (this.loggedinUser != null)
            {
                this.loggedinUser = null;
                return true;
            }
            return false;
        }

        public int retrieve10Messages()
        {
            return;
        }

        public String retrieve20Messages()
        {
            return;
        }

        public String retrieve20Messages()
        {
            return;
        }

        public String retrieveAllByUser(String nickname)
        {
            User user = findUser(nickname);
            if (user==null)
            {
                throw new System.ArgumentException("No such user");
            }
            var messages =
                from m in recievedMessages
                where m.user == user
                select m;
            return messages;
        }

        private User findUser(String nickname)
        {
            var user =
                from u in registeredUsers
                where u.nickname == nickname
                select u;
            return user;
        }
    }
}
