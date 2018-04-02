using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Chatroom
    {
        User loggedinUser;
        Dictionary<Message> recievedMessages;
        Dictionary<User> registeredUsers;
        String URL;
        messagesHandler messHandler;
        usersHandler usersHandler;
        xmlHandler xmlHandler;
        Logger log;

        public Chatroom {
                
        }
    }
}
