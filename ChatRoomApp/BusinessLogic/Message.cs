using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationLayer;
using BusinessLogic;

namespace BusinessLogic
{
    // a class to represent a message
    [Serializable]
    public class Message 
    {
        private Guid _id;
        public Guid Id { get => _id; }
        private string _userName;
        public string UserName { get => _userName; }
        private DateTime _date;
        public DateTime Date { get => _date; }
        private string _messageContent;
        public string MessageContent { get => _messageContent; }
        private string _groupID;
        public string GroupID { get => _groupID; }

        // recieves an IMessage and turns it to a message
        public Message(IMessage message)
        {
            _id = message.Id;
            _userName = message.UserName;
            _date = message.Date;
            _messageContent = message.MessageContent;
            _groupID = message.GroupID;
        }

        // string representation of a message
        override
        public String ToString()
        {
            String output = _date.ToString() + " -  " + _userName + " - " + MessageContent;
            return output;
        }
    }
}
