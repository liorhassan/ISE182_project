﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class UsersHandler : fileHandler
    {

        public UsersHandler() : base(@"C:\Temp\Chatroom24\Users.bin") { }
        
    }
}
