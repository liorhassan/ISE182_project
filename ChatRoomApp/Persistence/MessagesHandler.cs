using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class MessagesHandler : fileHandler
    {
        public MessagesHandler(string v) : base(@"C: \Users\Ohad\SE-Intro_server-master\mess.txt") { }
    }
}
