﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationLayer;

namespace BusinessLogic
{
    [Serializable]
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

        public IMessage writeMessage(String message, String url)
        {
            IMessage msg = Communication.Instance.Send(url, groupID, nickname, message);
            return msg;
        }

        public List<IMessage> retrive10Messages(String url)
        {
            List<IMessage> messages = Communication.Instance.GetTenMessages(url);
            return messages;
        }
    }
}
