using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Message : IMessage
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

        public Message (User user, String MessageContent)
        {
            if (!checkValidity(MessageContent))
            {
                throw new System.ArgumentException("Message is too long");
            }
            _User = user;
            _UserName = user.getNane();
            _GroupID = user.getID();
            _Date = DateTime.Now;
            _Id = Guid.NewGuid();
            _MessageContent = MessageContent;
        }

        private Boolean checkValidity(String content)
        {
            if (content.Length > 150)
            {
                return false;
            }
            return true;
        }

        public String ToString()
        {
            return this.MessageContent;
        }
    }
}
