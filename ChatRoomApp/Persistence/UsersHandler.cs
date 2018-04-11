using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    //a class for loading and saving users to the bin file
    //inherits the fileHandler
    public class UsersHandler : fileHandler
    {
        //uses father constructor with the relative Users bin file path
        public UsersHandler() : base("Users.bin") { }
    }
}
