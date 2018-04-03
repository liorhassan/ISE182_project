using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Message : IMessage
    {
        private int guid;
        private User user;
        private DateTime date;
        private String MessageContent;
        private String GroupID;

        public Message (User user, String MessageContent)
        {
            if (!checkValidity(MessageContent))
            {
                throw new System.ArgumentException("Message is too long");
            }
            this.user = user;
            this.GroupID = user.getID();
            this.date = DateTime.Now;
            this.guid = 1;
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
        Guid Id { get; }
        string UserName { get; }
        DateTime Date { get; }
        string MessageContent { get; }
        string GroupID { get; }
        string ToString();
    }
}
