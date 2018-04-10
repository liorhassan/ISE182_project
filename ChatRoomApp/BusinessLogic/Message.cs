using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStoneClient.CommunicationLayer;
using BusinessLogic;

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


        override
        public String ToString()
        {
            return this.MessageContent;
        }
    }
}
