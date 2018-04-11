using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationLayer;

namespace BusinessLogic
{
    //a serializable class represanting a signed-up user in the system
    [Serializable]
    public class User
    {
        //fields represanting the user's credentials and group id(fixed read-only)
        private readonly string groupID = "24";
        private String nickname;

        public String Nickname
        {
            get { return nickname; }
        }

        public User(String nickname)
        {
            this.nickname = nickname;
        }

        //askes the communication layer to sends a new message to the server 
        //using the users credentials and the parameters given by the chatroom
        //returns the IMessage recieved by the server
        public IMessage writeMessage(String message, String url)
        {
            try
            {
                IMessage msg = Communication.Instance.Send(url, groupID, nickname, message);
                return msg;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        //askes the communicatoin layer for the last 10 messages in the server and returns them
        public List<IMessage> retrive10Messages(String url)
        {
            List<IMessage> messages = Communication.Instance.GetTenMessages(url);
            return messages;
        }
    }
}
