using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStoneClient.CommunicationLayer;
using BusinessLogic;

namespace BusinessLogic
{
    public class IMessage : MileStoneClient.CommunicationLayer.IMessage
    {
        private Guid _Id;
        public Guid Id { get => _Id; }

        private String _UserName;
        public String UserName { get => _UserName; }

        private User _User;
        public User User { get => _User; }

        private DateTime _Date;
        public DateTime Date { get => _Date; }

        private String _MessageContent;
        public String MessageContent { get => _MessageContent; }

        private String _GroupID;
        public String GroupID { get => _GroupID; }

//        public Message (User user, String MessageContent)
//        {
//            if (!CheckValidity(_MessageContent))
//            {
//                throw new System.ArgumentException("Message is too long");
//            }
//           _User = user;
//            _UserName = user.Nickname;
//            _GroupID = user.getID();
//            _Date = DateTime.Now;
//            _Id = Guid.NewGuid();
//            _MessageContent = MessageContent;
//        }

        public static Boolean CheckValidity(String content)
        {
            if (content.Length > 150)
            {
                return false;
            }
            return true;
        }

        override
        public String ToString()
        {
            return this.MessageContent;
        }
    }
}
