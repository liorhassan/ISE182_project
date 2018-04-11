using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    //a class for loading and saving messagges to the bin file
    //inherits the fileHandler
    public class MessagesHandler : fileHandler
    {
        //uses father constructor with the relative Messages bin file path
        public MessagesHandler() : base("Messages.bin") { }
    }
}
