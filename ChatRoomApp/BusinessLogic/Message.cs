using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStoneClient.CommunicationLayer;
using BusinessLogic;

namespace BusinessLogic
{
    [Serializable]
    public class Message : IMessage
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


        override
        public String ToString()
        {
            return this.MessageContent;
        }
    }
}
