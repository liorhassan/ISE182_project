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
        private Dictionary<Message> recievedMessages;
        private Dictionary<User> registeredUsers;
        private String URL;
        private messagesHandler messHandler;
        private usersHandler usersHandler;
        private xmlHandler xmlHandler;
        private Logger log;

        public Chatroom {
            this.loggedinUser = null;
            this.recievedMessages = new Dictionary<Message>;


    }
    }
}
