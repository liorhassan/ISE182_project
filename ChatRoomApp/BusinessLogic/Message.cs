using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Message : IMessage
    {
        private Guid Id { get; }
        private User UserName { get; }
        private DateTime Date { get; }
        private String MessageContent { get; }
        private String GroupID { get; }

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
