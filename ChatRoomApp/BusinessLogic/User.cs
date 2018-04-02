using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    class User
    {
        private readonly int groupID = 24;
        private String nickname;

        public String Nickname
        {
            get { return nickname; }
        }
        
        public User(String nickname)
        {
            this.nickname = nickname;
        }
        
        public Message (String message, String url)
        {

        }
    }
}
