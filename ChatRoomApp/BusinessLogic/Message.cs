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

        private User _UserName;
        public User UserName { get => _UserName; }

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
            this.user = user;
            this.GroupID = user.getID();
            this.date = DateTime.Now;
            this.Id = Guid.NewGuid();
            this.MessageContent = MessageContent;
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
