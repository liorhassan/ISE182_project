using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStoneClient.CommunicationLayer;

namespace BusinessLogic
{
    public class User
    {
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

        public Message writeMessage(String message, String url)
        {
            Message msg = Communication.Instance.Send(url, groupID, nickname, message);
            return msg;
        }

        public List<Message> retrive10Messages(String url)
        {
            List<Message> messages = Communication.Instance.GetTenMessages(url);
            return messages;
        }
    }
}
